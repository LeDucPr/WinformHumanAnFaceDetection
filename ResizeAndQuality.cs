using System.Drawing;
using System.Drawing.Imaging;
using Emgu.CV;
using Emgu.CV.Structure;
using System.IO;
using AForge.Video.DirectShow;
using AForge.Video;
using System.Windows.Forms.VisualStyles;
using AForge.Imaging.ColorReduction;
using Emgu.CV.Shape;
using WinformHumanAnFaceDetection.Config;
using WinformHumanAnFaceDetection.DataConfig;
using Emgu.CV.CvEnum;

namespace CameraHumanDetection
{
	public partial class ResizeAndQuality : Form
	{
		// thread
		private Thread? trackerThread;
		private Thread detectionThread;
		private Semaphore _semaphore = new Semaphore(2, 2);
		// image file 
		FolderTree _rootInputFolder = new FolderTree();
		FolderTree _rootOutputFolder = new FolderTree();

		// khởi tạo 
		public ResizeAndQuality()
		{
			this.InitializeComponent();
			this.FormSizeLimited();
			panelMain.BackColor = Color.FromArgb(230, 230, 230);
		}

		#region Xử lý các nút và nhãn 
		#region CheckBox
		// nếu this.checkBoxRatio được chọn thì chỉ 1 trong 2 checkBoxWidth hoặc checkBoxHeight được chọn
		private void checkBoxRatio_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBoxRatio.Checked)
			{
				if (this.checkBoxWidth.Checked || this.checkBoxHeight.Checked)
				{
					this.checkBoxWidth.Checked = true;
					this.checkBoxHeight.Checked = false;
				}
				else
				{
					this.checkBoxWidth.Checked = true;
					this.checkBoxHeight.Checked = false;
				}
			}
			else
			{
				this.checkBoxWidth.Checked = true;
				this.checkBoxHeight.Checked = true;
			}
		}
		private void checkBoxWidth_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBoxRatio.Checked)
			{
				this.checkBoxHeight.Checked = !this.checkBoxWidth.Checked;
			}
			else
			{
				this.checkBoxWidth.Checked = true;
				this.checkBoxHeight.Checked = true;
			}
		}

		private void checkBoxHeight_CheckedChanged(object sender, EventArgs e)
		{
			if (this.checkBoxRatio.Checked)
			{
				this.checkBoxWidth.Checked = !this.checkBoxHeight.Checked;
			}
			else
			{
				this.checkBoxWidth.Checked = true;
				this.checkBoxHeight.Checked = true;
			}
		}
		#endregion CheckBox
		#region KeyPress
		private void textBoxQuality_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
				e.Handled = true;
			TextBox textBox = (TextBox)sender;
			if (textBox.Text.Length == 0 && e.KeyChar != '1' && char.IsDigit(e.KeyChar))
				if (textBox.Text.Length >= 2)
					e.Handled = true;
				// If the first digit is 1, limit to 3 digits
				else if (textBox.Text.Length > 0 && textBox.Text[0] == '1')
					if (textBox.Text.Length >= 3)
						e.Handled = true;
		}
		private void textBoxWidthHeight_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
				e.Handled = true;
			TextBox textBox = (TextBox)sender;
			// Limit to 5 digits
			if (char.IsDigit(e.KeyChar) && textBox.Text.Length >= 5)
				e.Handled = true;
		}
		#endregion KeyPress
		#endregion Xử lý các nút và nhãn 

		#region Thay đổi kích thước Form và kích thước của PictureBox
		private void FormSizeLimited()
		{
			this.MaximumSize = new Size(this.Width, this.Height);
			this.MinimumSize = new Size(this.Width, this.Height);
		}
		#endregion Thay đổi kích thước Form và kích thước của PictureBox

		#region Load và Refresh Folder
		private async void buttonOpenFolder_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog dialog = new FolderBrowserDialog())
			{
				if (dialog.ShowDialog() == DialogResult.OK)
				{
					string selectedPath = dialog.SelectedPath;
					this.textBoxOpenFolderPath.Text = selectedPath;
					// Chọn đường dẫn đầu ra 
					using (FolderBrowserDialog saveDialog = new FolderBrowserDialog())
					{
						if (saveDialog.ShowDialog() == DialogResult.OK)
						{
							string savePath = saveDialog.SelectedPath;
							this.textBoxSaveFolderPath.Text = savePath;
						}
					}
					// Folder lớn thì chọn các đọc bất đồng bộ trong lúc chọn thư mục đầu ra 
					await Task.Run(() =>
					{
						this.Invoke((MethodInvoker)async delegate
						{
							await this.RefreshTreeView(this.treeViewInputFolder, this._rootInputFolder, this.textBoxOpenFolderPath.Text);
							await this.RefreshTreeView(this.treeViewOutputFolder, this._rootOutputFolder, this.textBoxSaveFolderPath.Text);
						});
					});
				}
			}
		}

		private async Task RefreshTreeView(TreeView treeView, FolderTree root, string? selectedPath = null)
		{
			await Task.Run(() =>
			{
				try { treeView.Nodes.Clear(); } catch { } // sự kiện không có Nodes để xóa (null)
														  ////root.Dispose();
														  //this.Invoke((MethodInvoker)delegate{});
				if (!string.IsNullOrEmpty(selectedPath))
					root.Populate(selectedPath, selectedPath);
				root.PopulateTreeView(treeView, root);
			});
		}

		private async void buttonRefreshOpenFolder_Click(object sender, EventArgs e)
		{
			await this.RefreshTreeView(this.treeViewInputFolder, this._rootInputFolder);
		}
		private async void buttonRefreshSaveFolder_Click(object sender, EventArgs e)
		{
			await this.RefreshTreeView(this.treeViewOutputFolder, this._rootOutputFolder);
		}

		#endregion Load và Refresh Folder

		private async void button1_Click(object sender, EventArgs e)
		{
			int totalImages = this._rootInputFolder.TotalImageFiles(); // tổng số lượng file ảnh 
			this.labelRemainingImages.Text = $"RemainingImages: {totalImages.ToString()}"; // cập nhật trong label 

			List<string> errors = new List<string>(); // lỗi 
			int width = 0, height = 0, quality = 0; // chiều rộng, chiều dài, chất lượng ảnh
			if (checkBoxRatio.Checked)
			{
				if (checkBoxWidth.Checked)
				{
					if (!int.TryParse(textBoxWidth.Text, out width))
						errors.Add("Nhập số vào chiều rộng");
				}
				else if (checkBoxHeight.Checked)
				{
					if (!int.TryParse(textBoxHeight.Text, out height))
						errors.Add("Nhập số vào chiều dài");
				}
			}
			else
			{
				if (!int.TryParse(textBoxWidth.Text, out width))
					errors.Add("Nhập số vào chiều rộng");
				if (!int.TryParse(textBoxHeight.Text, out height))
					errors.Add("Nhập số vào chiều dài");
			}
			if (!int.TryParse(textBoxQuality.Text, out quality))
				errors.Add("Nhập số vào chất lượng ảnh");
			if (errors.Count > 0)
			{
				MessageBox.Show(string.Join(", ", errors));
				return;
			}
			await Task.Run(() =>
			{
				this.detectionThread = new Thread(() =>
				{
					this._rootInputFolder.GetImageFiles(this.textBoxSaveFolderPath.Text).AsParallel().ForAll(imagePath =>
						{
							try
							{
								string imageInputPath = Path.Combine(this.textBoxOpenFolderPath.Text, imagePath);
								Emgu.CV.Image<Bgr, byte> emguImage = new Emgu.CV.Image<Bgr, byte>(imageInputPath);

								int newWidth, newHeight;
								double aspectRatio;
								int qualityValue = quality;
								if (checkBoxRatio.Checked)
								{
									aspectRatio = (double)emguImage.Height / emguImage.Width;
									if (checkBoxWidth.Checked)
									{
										newWidth = width;
										newHeight = (int)(width * aspectRatio);
									}
									else // checkBoxHeight.Checked
									{
										newHeight = height;
										newWidth = (int)(height / aspectRatio);
									}
								}
								else // !checkBoxRatio.Checked = false 
								{
									newWidth = width;
									newHeight = height;
								}
								// Resize image
								emguImage = emguImage.Resize(newWidth, newHeight, Emgu.CV.CvEnum.Inter.Linear);
								string imageOutputPath = Path.Combine(this.textBoxSaveFolderPath.Text, imagePath);
								// Save image with 50% quality
								var parameters = new List<KeyValuePair<Emgu.CV.CvEnum.ImwriteFlags, int>>
									{
										new KeyValuePair<Emgu.CV.CvEnum.ImwriteFlags, int>(Emgu.CV.CvEnum.ImwriteFlags.JpegQuality, qualityValue)
									};
								Emgu.CV.CvInvoke.Imwrite(imageOutputPath, emguImage, parameters.ToArray());
								// lưu xong hủy luôn emguImage để giải phóng bộ nhớ
								emguImage.Dispose();
								this.labelRemainingImages.Text = $"RemainingImages: {(--totalImages).ToString()}";
							}
							catch { }
						});
				});
				this.detectionThread.IsBackground = true; // detection nên cho là DaemonThread 
				this.detectionThread.Start();
			});
		}
	}
}