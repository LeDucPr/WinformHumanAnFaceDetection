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

namespace CameraHumanDetection
{
	public partial class Camera : Form
	{
		// video capture
		private FilterInfoCollection VideoCaptureDevices;
		private VideoCaptureDevice? FinalVideo;
		// thread
		private Thread? trackerThread;
		private Thread detectionThread;
		private Semaphore _semaphore = new Semaphore(2, 2);
		// image 
		private Queue<Image<Bgr, byte>> detectionImages = new Queue<Image<Bgr, byte>>();
		SSD faceSSD;
		SSD personSSD;
		// khởi tạo 
		public Camera()
		{
			faceSSD = new SSD(@"runtimes\SSD\FaceSSD");
			personSSD = new SSD(@"runtimes\SSD\PascalSSD");
			// Form được load sau khi khởi tạo các thuật toán điều khiển, có thay đổi gì thì tính sau 
			this.InitializeComponent();
			this.Load_CameraDevices();
			this.FormSizeLimited();
			panelMain.BackColor = Color.FromArgb(230, 230, 230);

			var nvidias = SSD.GPU_NVIDIA_Names();
			this.comboBoxDevices.Items.Add("Cpu");
			nvidias.ForEach(nv => this.comboBoxDevices.Items.Add(nv));

			// thay đổi các tham số quan trọng trong gui về giá trị chuẩn hóa với giá trị quan sát
			ChangeNumericUpDownFPS();
			ChangeNumericUpDownARC();

			//int k = 0;
			// Nên tạo một luồng riêng chuyên sử dụng trong xác định đói tượng để không ảnh hưởng tới GUI 
			this.detectionThread = new Thread(() =>
			{
				while (true)
					if (!this.isDevicePause && this.detectionImages.Count > this.delayFrame)
					{
						_semaphore.WaitOne(); // Đợi cho đến khi semaphore được mở
						this.AutoPFS((int)this.numericUpDownFPS.Minimum, (int)this.numericUpDownFPS.Maximum);
						bool isSave = this.isbuttonSavePress;
						Emgu.CV.Image<Bgr, byte> emguImage = detectionImages.Dequeue();
						emguImage.DrawRectsToImage(faceSSD.Detector(emguImage, double.Parse(labelARCIndex.Text)), thickness: 3);
						emguImage.DrawRectsToImage(personSSD.Detector(emguImage, double.Parse(labelARCIndex.Text)), thickness: 1);

						//if (this.isbuttonSavePress)
						//    emguImage.Save(@$"D:\@@@@\CameraCapture\processed{k++}.png");
						//this.isbuttonSavePress = false;
						pictureBoxCameraPreview.Image = emguImage.ToBitmap(); // xuất hình ra pictureBox 
						this.WriteLabelText(this.labelAmountFrame, $"Frames: {amountFrame}");
						this.WriteLabelText(this.labelDelayFrame, $"DelayFrame: {this.detectionImages.Count}");
						_semaphore.Release(); // Giải phóng semaphore khi kết thúc vòng lặp
						if (this.isPause)
							this.isDevicePause = true;
					}
			});
			this.detectionThread.IsBackground = true; // detection nên cho là DaemonThread 
			this.detectionThread.Start();
		}


		// Save Image 
		private bool isbuttonSavePress = false;
		private void buttonSave_Click(object sender, EventArgs e)
		{
			this.isbuttonSavePress = true;
		}

		#region Bước này xử lý hình ảnh và đưa ảnh được thu vào hàng đợi 
		private int delayFrame = 0;
		private int framePerSecond = 15;
		private int amountFrame = 0;
		private DateTime dtCFrame = DateTime.Now;
		private void FinalVideo_NewFrame(object sender, NewFrameEventArgs eventArgs)
		{
			// Thay đổi kích thước khung máy ảnh để lấp đầy PictureBox
			float screenRatio = (float)(Screen.PrimaryScreen?.Bounds.Width ?? 0) / (float)(Screen.PrimaryScreen?.Bounds.Height ?? 1);
			Size desiredFrameSize = new Size((int)(pictureBoxCameraPreview.Height * screenRatio), pictureBoxCameraPreview.Height);
			if (screenRatio > pictureBoxCameraPreview.Width / pictureBoxCameraPreview.Height) // túc là chiều cao lớn hơn chiều rộng 
				desiredFrameSize = new Size(pictureBoxCameraPreview.Width, (int)(pictureBoxCameraPreview.Width / screenRatio));
			// Thay đổi kích thước PictureBox để phù hợp với khung máy ảnh mới
			pictureBoxCameraPreview.Size = desiredFrameSize;
			// Tạo một Bitmap mới để lưu trữ khung lớn hơn
			Bitmap bitmap = new Bitmap(desiredFrameSize.Width, desiredFrameSize.Height);
			// Sao chép eventArgs.Frame vào Bitmap, chia tỷ lệ khi cần thiết
			Graphics.FromImage(bitmap).DrawImage((Bitmap)eventArgs.Frame, 0, 0, bitmap.Width, bitmap.Height);
			// chỉnh ảnh lật ngang lại 180 độ 
			bitmap.RotateFlip(RotateFlipType.Rotate180FlipY);

			// Đặt hình ảnh của PictureBox thành Bitmap của khung mới, lớn hơn
			// Thực hiện trên luồng khác để tránh ảnh hưởng đến độ trễ trong giao diện người dùng 
			pictureBoxCameraPreview.Invoke(new Action(() =>
			{
				// fps 
				TimeSpan ts = DateTime.Now - this.dtCFrame;
				if (ts.Milliseconds >= 1000 / this.framePerSecond)
				{
					pictureBoxCameraPreview.SizeMode = PictureBoxSizeMode.StretchImage;
					Bitmap cloneImage = bitmap.Clone(new Rectangle(0, 0, pictureBoxCameraPreview.Width, pictureBoxCameraPreview.Height), bitmap.PixelFormat);

					lock (this.detectionImages)
					{
						if (bitmap != null)
						{
							Image<Bgr, byte> emguImage = bitmap.ToImage<Bgr, byte>();
							this.detectionImages.Enqueue(emguImage);
						}
					}
					amountFrame++;
					this.dtCFrame = DateTime.Now;
				}
			}));
		}
		private void AutoPFS(int min, int max, int delayMin = 1, int delayMax = 4)
		{
			if (this.detectionImages.Count >= delayMax && this.framePerSecond > min)
				this.framePerSecond--;
			else if (this.detectionImages.Count <= delayMin && this.framePerSecond < max)
				this.framePerSecond++;
			this.labelFPSIndex.Text = this.framePerSecond.ToString();
		}
		#endregion Bước này xử lý hình ảnh và đưa ảnh được thu vào hàng đợi 

		#region Xử lý các nút và nhãn 
		#region btStart click và bật tắt Camera
		private void buttonStart_Click(object sender, EventArgs e)
		{
			this.OnOffCamera();
		}
		private void OnOffCamera()
		{
			if (this.buttonStart.Text.Equals("Start"))
			{
				if (FinalVideo != null && FinalVideo.IsRunning) // Camera is already running
					return;
				this.buttonStart.Text = "Stop";
				// Create new instance of selected video device
				FinalVideo = new VideoCaptureDevice(VideoCaptureDevices[comboBoxCamera.SelectedIndex].MonikerString);
				// Register to provide new frames using the FinalVideo_NewFrame method
				FinalVideo.NewFrame += new NewFrameEventHandler(FinalVideo_NewFrame);
				FinalVideo.Start();
			}
			else if (this.buttonStart.Text.Equals("Stop"))
			{
				this.buttonStart.Text = "Start";
				if (FinalVideo != null && FinalVideo.IsRunning)
				{
					FinalVideo.SignalToStop();
					FinalVideo = null;
				}
			}
			this.CreateOldCamSize(); // khởi tạo kích thước Camera hiện tại 
		}
		#endregion btStart click và bật tắt Camera

		#region FPS Input
		private void NumericUpDownFPS_ValueChanged(object sender, EventArgs e)
		{
			this.ChangeNumericUpDownFPS();
		}
		private void ChangeNumericUpDownFPS()
		{
			decimal value = this.numericUpDownFPS.Value;
			this.framePerSecond = (int)value;
			this.labelFPSIndex.Text = value.ToString();
		}
		#endregion FPS Input

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

		#region Label 
		private void WriteLabelText(Label lb, string text)
		{
			lb.Text = text;
		}
		#endregion Label 
		#region Thiết bị 
		private bool isPause = false;
		private bool isDevicePause = false;
		EDeviceUsage connvertDevice = EDeviceUsage.Cpu; // khởi tạo được dùng cpu 
		private async void comboBoxDevices_SelectedIndexChanged(object sender, EventArgs e)
		{
			string? selected = comboBoxDevices?.SelectedItem?.ToString();
			if (selected == "Cpu" && this.faceSSD.EUsage != EDeviceUsage.Cpu)
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

				this.faceSSD.DeviceSetup(this.connvertDevice);
				this.personSSD.DeviceSetup(this.connvertDevice);
				this.isPause = false;
				this.isDevicePause = false;
			});
		}
		#endregion Thiết bị
		#endregion Xử lý các nút và nhãn 

		#region Thay đổi kích thước Form và kích thước của PictureBox
		private Size oldPanelMainSize;
		private Size expantFormSize = new Size(0, 0);
		private void Form_SizeChanged(object sender, EventArgs e)
		{
			// tất cả thay đổi đều được tính toán theo giá trị của khung hình camera 
			// do đó, các thay đôi đều được thực hiện dựa trên kích thước khung hình camera 
			this.pictureBoxCameraPreview.Size += this.panelMain.Size - this.oldPanelMainSize;
			Size oldpictureBoxCameraPreviewSize = this.pictureBoxCameraPreview.Size;
			if (this.pictureBoxCameraPreview.Width > 800)
			{
				this.pictureBoxCameraPreview.Width = 800;
			}
			// Thay đổi kích thước khung máy ảnh để lấp đầy PictureBox
			float screenRatio = (float)(Screen.PrimaryScreen?.Bounds.Width ?? 0) / (float)(Screen.PrimaryScreen?.Bounds.Height ?? 1);
			Size desiredFrameSize = new Size((int)(pictureBoxCameraPreview.Height * screenRatio), pictureBoxCameraPreview.Height);
			// túc là chiều cao lớn hơn chiều rộng kèm theo điều kiện không xảy ra lỗi khi nhấn Minialize 
			if (pictureBoxCameraPreview.Height > 0 && screenRatio > pictureBoxCameraPreview.Width / pictureBoxCameraPreview.Height)
				desiredFrameSize = new Size(pictureBoxCameraPreview.Width, (int)(pictureBoxCameraPreview.Width / screenRatio));
			// Thay đổi kích thước PictureBox để phù hợp với khung máy ảnh mới
			pictureBoxCameraPreview.Size = desiredFrameSize;
			//this.Size += this.pictureBoxCameraPreview.Size - oldpictureBoxCameraPreviewSize;
			this.oldPanelMainSize = this.panelMain.Size;
		}

		// cái này được gọi khi được khởi động máy ảnh, trong trường hợp này là nhấn buttonStart 
		private void CreateOldCamSize()
		{
			this.oldPanelMainSize = this.panelMain.Size;
		}
		private void FormSizeLimited()
		{
			this.MaximumSize = new Size(this.Width, this.Height);
			this.MinimumSize = new Size(this.Width, this.Height);
		}
		#endregion Thay đổi kích thước Form và kích thước của PictureBox

		#region Open and close
		private void Form_FormClosing(object sender, FormClosingEventArgs e)
		{
			// khóa camera lại không là nó cứ bật mãi vậy à
			if (FinalVideo != null && FinalVideo.IsRunning)
			{
				FinalVideo.SignalToStop();
				FinalVideo = null;
			}
		}

		private void Load_CameraDevices()
		{
			// Tải các thiết bị quay video có sẵn
			VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
			// Thêm từng tên thiết bị vào comboBox để cho phép người dùng lựa chọn
			foreach (FilterInfo VideoCaptureDevice in VideoCaptureDevices)
				comboBoxCamera.Items.Add(VideoCaptureDevice.Name);
			// Chọn thiết bị đầu tiên theo mặc định
			if (comboBoxCamera.Items.Count > 0)
				comboBoxCamera.SelectedIndex = 0;
		}
		#endregion
	}
}