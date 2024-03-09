using Emgu.CV.Structure;
using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Emgu.CV.Dnn.Backend;
using static Emgu.CV.Dnn.Target;
using Emgu.CV.Cuda;

namespace WinformHumanAnFaceDetection.Config
{
	public static class SSDExtension
	{

		public static void DrawRectsToImage(this Image<Bgr, byte> emguImage, List<Rectangle> rects, int thickness)
		{
			foreach (Rectangle rect in rects)
				emguImage.Draw(rect, new Bgr(168, 160, 116), thickness);
		}
		public static void Usage(this Emgu.CV.Dnn.Net net, EDeviceUsage eUsage)
		{
			if (!CudaInvoke.HasCuda && eUsage == EDeviceUsage.Cuda) return; // không có card mà vãn cố dùng thì thôi 
			DeviceUsage deviceUsage = DeviceUsage.Setup(eUsage);
			net.SetPreferableBackend(deviceUsage.Backend);
			net.SetPreferableTarget(deviceUsage.Target);
		}
	}
}
