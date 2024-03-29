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

namespace CameraHumanDetection
{
	public partial class ObjectCutterFromImage : Form
	{
		// thread
		private Thread? trackerThread;
		private Thread detectionThread;
		private Semaphore _semaphore = new Semaphore(2, 2);
		int amountModelPascalSSD = 5; // mặc định là 1 
		Dictionary<SSD, bool> pascalSSDs; // true là đang hoạt động, false là không hoạt động
										  // image file 
		FolderTree _rootInputFolder = new FolderTree();
		FolderTree _rootOutputFolder = new FolderTree();

		// khởi tạo 
		public ObjectCutterFromImage()
		{
			this.pascalSSDs = new Dictionary<SSD, bool>();
			for (int i = 0; i < this.amountModelPascalSSD; i++)
				this.pascalSSDs.Add(new SSD(@"runtimes\SSD\PascalSSD"), false);
			// Form được load sau khi khởi tạo các thuật toán điều khiển, có thay đổi gì thì tính sau 
			this.InitializeComponent();
			this.FormSizeLimited();
			panelMain.BackColor = Color.FromArgb(230, 230, 230);

			var nvidias = SSD.GPU_NVIDIA_Names();
			this.comboBoxDevices.Items.Add("Cpu");
			nvidias.ForEach(nv => this.comboBoxDevices.Items.Add(nv));
			// thay đổi các tham số quan trọng trong gui về giá trị chuẩn hóa với giá trị quan sát
			ChangeNumericUpDownARC();

			// Nên tạo một luồng riêng chuyên sử dụng trong xác định đói tượng để không ảnh hưởng tới GUI 
			//this.detectionThread = new Thread(() =>
			//{
			//	while (true)
			//		if (this._rootInputFolder.GetImageFiles().Count() != 0)
			//		{
			//			_semaphore.WaitOne(); // Đợi cho đến khi semaphore được mở
			//			Emgu.CV.Image<Bgr, byte> emguImage = new Emgu.CV.Image<Bgr, byte>(this._rootInputFolder.GetImageFiles().First());
			//			emguImage.DrawRectsToImage(pascalSSD.Detector(emguImage, double.Parse(labelARCIndex.Text)), thickness: 1);

			//			//if (this.isbuttonSavePress)
			//			//    emguImage.Save(@$"D:\@@@@\CameraCapture\processed{k++}.png");
			//			//this.isbuttonSavePress = false;
			//			pictureBoxOriginImage.Image = emguImage.ToBitmap(); // xuất hình ra pictureBox 
			//			_semaphore.Release(); // Giải phóng semaphore khi kết thúc vòng lặp
			//			if (this.isPause)
			//				this.isDevicePause = true;
			//		}
			//});
			//this.detectionThread.IsBackground = true; // detection nên cho là DaemonThread 
			//this.detectionThread.Start();
		}

		#region Xử lý các nút và nhãn 
		#region ARC accuracy (độ chính xác của thuật toán SSD), chỉ số càng cao thì bộ lọc càng dễ bỏ qua đối tượng
		private void NumericUpDownARC_ValueChanged(object sender, EventArgs e)
		{
			this.ChangeNumericUpDownARC();
		}
		private void ChangeNumericUpDownARC()
		{
			decimal value = this.numericUpDownARC.Value;
			this.labelARCIndex.Text = $"{((double)value).ToString()}";
		}
		#endregion ARC accuracy (độ chính xác của thuật toán SSD), chỉ số càng cao thì bộ lọc càng dễ bỏ qua đối tượng 

		#region Thiết bị 
		private bool isPause = false;
		private bool isDevicePause = false;
		EDeviceUsage connvertDevice = EDeviceUsage.Cpu; // khởi tạo được dùng cpu 
		private async void comboBoxDevices_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.pascalSSDs.Count == 0)
				return;
			string? selected = comboBoxDevices?.SelectedItem?.ToString();
			if (selected == "Cpu" && this.pascalSSDs.ElementAt(0).Key.EUsage != EDeviceUsage.Cpu)
			{
				this.isPause = true;
				this.connvertDevice = EDeviceUsage.Cpu;
			}
			else
			{
				this.isPause = true;
				this.connvertDevice = EDeviceUsage.Cuda;
			}

			await Task.Run(() =>
			{
				while (!this.isDevicePause)
				{
					// Đợi cho tới khi isDevicePause trở thành true
					Thread.Sleep(10); // Đợi trong một khoảng thời gian ngắn để tránh việc sử dụng CPU quá mức
				}

				// phải setup cho tất cả các mô hình đang hoạt động sử dụng chung một thiết bị
				this.pascalSSDs.ToList().ForEach(pascalSSD => pascalSSD.Key.DeviceSetup(this.connvertDevice));
				this.isPause = false;
				this.isDevicePause = false;
			});
		}
		#endregion Thiết bị
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
			await Task.Run(() =>
			{
				this.detectionThread = new Thread(() =>
				{
					// tổng số lượng file ảnh 
					int totalImages = this._rootInputFolder.TotalImageFiles(); 
					// cập nhật trong label 
					this.Invoke((MethodInvoker)delegate { this.labelRemainingImages.Text = totalImages.ToString(); });
					//this._rootInputFolder.GetImageFiles(this.textBoxSaveFolderPath.Text).AsParallel().ForAll(imagePath =>
					this._rootInputFolder.GetImageFiles(this.textBoxSaveFolderPath.Text).AsParallel().ForAll(imagePath =>
						{
							try
							{
								string imageInputPath = Path.Combine(this.textBoxOpenFolderPath.Text, imagePath);
								Emgu.CV.Image<Bgr, byte> emguImage = new Emgu.CV.Image<Bgr, byte>(imageInputPath);
								////var rects = pascalSSD.Detector(emguImage, double.Parse(labelARCIndex.Text));
								////emguImage.DrawRectsToImage(pascalSSD.Detector(emguImage, double.Parse(labelARCIndex.Text)), thickness: 3);

								SSD? pascalSSDNotWorking = null;

								while (pascalSSDNotWorking == null)
								{
									pascalSSDNotWorking = this.pascalSSDs.Where(pascalSSD => !pascalSSD.Value).FirstOrDefault().Key;
									if (pascalSSDNotWorking == null) { }
									//Thread.Sleep(10);// Optional: Ngủ một chút để giảm tải CPU
								}

								lock (pascalSSDNotWorking)
								{
									this.pascalSSDs[pascalSSDNotWorking] = true;
									var rects = pascalSSDNotWorking.Detector(emguImage, double.Parse(labelARCIndex.Text));
									if (rects.Count > 0)
									{
										Rectangle rect = rects[0];
										Emgu.CV.Image<Bgr, byte> emguImageOutput = emguImage.GetSubRect(rect);
										string imageOutputPath = Path.Combine(this.textBoxSaveFolderPath.Text, imagePath);
										emguImageOutput.Save(imageOutputPath);
										emguImageOutput.Dispose();
									}
									// lưu xong hủy luôn emguImage để giải phóng bộ nhớ
									emguImage.Dispose();
									this.pascalSSDs[pascalSSDNotWorking] = false;
									this.Invoke((MethodInvoker)delegate { this.labelRemainingImages.Text = (--totalImages).ToString(); });
								}
								if (this.isPause)
									this.isDevicePause = true;
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