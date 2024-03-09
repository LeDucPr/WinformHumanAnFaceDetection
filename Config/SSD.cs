using Emgu.CV.Structure;
using Emgu.CV.Cuda;
using Emgu.CV.Dnn;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NvAPIWrapper.Native.Display;
using Newtonsoft.Json;
using System.Security.AccessControl;
using NvAPIWrapper.GPU;
using Emgu.CV.Shape;
#pragma warning disable CS8602 // Dereference of a possibly null reference.

namespace WinformHumanAnFaceDetection.Config
{
	public interface ISSD
	{
		public EDeviceUsage EUsage { get; set; }
		// chỉ hoạt động từ GTX660 trở lên
		public void DeviceSetup(EDeviceUsage eUsage);
		public List<Rectangle> Detector(Image<Bgr, byte> emguImage, double ARCIndex);
	}

	public partial class SSD : ISSD
	{
		private class DataOfSSDModel
		{
			public string? textFileName;
			public string? caffemodelFile;
			public double scaleFactor;
			public Size size;
			public MCvScalar scalar;
			public List<int>? class_id;
		}

		public EDeviceUsage EUsage { get; set; }
		private double scaleFactor;
		private Size size;
		private MCvScalar scalar;
		private List<int>? class_id;
		private Net? net;
		public SSD(string directoryPath)
		{
#pragma warning disable CS8604 // Possible null reference argument.
			// đường dẫn chính 
			string folderPath = AppDomain.CurrentDomain.BaseDirectory;
			DataOfSSDModel? dataOfSSDModel = LoadParamFromDirectory(directoryPath);
			if (dataOfSSDModel != null)
			{
				folderPath = Path.Combine(folderPath, directoryPath);
				string deployPrototxtPath = Path.Combine(folderPath, dataOfSSDModel.textFileName);
				string caffemodelPath = Path.Combine(folderPath, dataOfSSDModel.caffemodelFile);
				scaleFactor = dataOfSSDModel.scaleFactor;
				size = dataOfSSDModel.size;
				scalar = dataOfSSDModel.scalar;
				class_id = dataOfSSDModel.class_id;
				net = DnnInvoke.ReadNetFromCaffe(deployPrototxtPath, caffemodelPath);
			}
#pragma warning restore CS8604 // Possible null reference argument.
			this.DeviceSetup(EDeviceUsage.Cpu);
			//this.DeviceSetup(EDeviceUsage.Cuda);// mặc định thế, còn nếu có cuda thì tự set dòng này
		}

		public void DeviceSetup(EDeviceUsage eUsage)
		{
			this.EUsage = eUsage;
			this.net?.Usage(eUsage);
		}

		/// <summary>
		/// Tất nhiên này đường dẫn này được tính từ file thực hiện tức là file đuôi .exe 
		/// </summary>
		/// <param name="directoryPathPart"></param>
		/// <returns></returns>
		private DataOfSSDModel? LoadParamFromDirectory(string directoryPathPart)
		{
			string directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, directoryPathPart);
			string[] files = Directory.GetFiles(directoryPath);
			foreach (string file in files)
				if (Path.GetExtension(file) == ".json")
				{
					string deserializedString = File.ReadAllText(file);
					DataOfSSDModel? dossdm = JsonConvert.DeserializeObject<DataOfSSDModel>(deserializedString);
					return dossdm;
				}
			return null;
		}

		public List<Rectangle> Detector(Image<Bgr, byte> emguImage, double ARCIndex)
		{
			List<Rectangle> rects = new List<Rectangle>();
			Mat image = emguImage.Mat;
			Mat blob = DnnInvoke.BlobFromImage(image, scaleFactor, size, scalar);
			net.SetInput(blob);
			Mat detections = net.Forward();
			for (int i = 0; i < detections.SizeOfDimension[2]; i++)
			{
				float confidence = GetDataAt(detections, new int[] { 0, 0, i, 2 }, 1)[0];
				if (confidence > ARCIndex)
				{
					int idx = (int)GetDataAt(detections, new int[] { 0, 0, i, 1 }, 1)[0];
					if (class_id.Contains(idx))
					{
						float[] boxData = GetDataAt(detections, new int[] { 0, 0, i, 3 }, 4);
						RectangleF box = new RectangleF(boxData[0] * emguImage.Width, boxData[1] * emguImage.Height,
							(boxData[2] - boxData[0]) * emguImage.Width, (boxData[3] - boxData[1]) * emguImage.Height);
						rects.Add(Rectangle.Round(box));
					}
				}
			}
			return rects;
		}

		private float[] GetDataAt(Mat mat, int[] points, int amount)
		{
			nint ptr = mat.GetDataPointer(points);
			float[] data = new float[amount];
			Marshal.Copy(ptr, data, 0, amount);
			return data;
		}

	}
	public partial class SSD // NVIDIA
	{
		public static List<string> GPU_NVIDIA_Names()
		{
			List<string> deviceNames = new List<string>();
			try // trong trường hợp máy không có card nvidia thì lỗi nvapi.dll
			{
				var physicalGPUs = PhysicalGPU.GetPhysicalGPUs();
				foreach (var gpu in physicalGPUs)
				{
					string gpuName = gpu.FullName;
					deviceNames.Add(gpuName);
				}
			}
			catch {}
			return deviceNames;
		}
	}
}
#pragma warning restore CS8602 // Dereference of a possibly null reference.



/*
 * Các chỉ số trong Mat: 
 * [1]: image_id
 * [0]: class_id
 * [2]: confidence
 * [3]: x_min
 * [4]: y_min
 * [5]: x_max
 * [6]: y_max
*/
//DataOfSSDModel dd = new DataOfSSDModel()
//{
//    textFileName = "weights-prototxt.txt",
//    caffemodelFile = "res_ssd_300Dim.caffeModel",
//    scaleFactor = 1.0,
//    size = size,
//    scalar = scalar,
//    class_id = new List<int> { 1 }
//};
//string serializedString = JsonConvert.SerializeObject(dd);
//string folderPath = AppDomain.CurrentDomain.BaseDirectory;
//string dataFromKnownFacesFile = @"runtimes\SSD\FaceSSD\modelparam.json";
//dataFromKnownFacesFile = Path.Combine(folderPath, dataFromKnownFacesFile);
//if (!File.Exists(dataFromKnownFacesFile))
//    File.WriteAllText(dataFromKnownFacesFile, serializedString);