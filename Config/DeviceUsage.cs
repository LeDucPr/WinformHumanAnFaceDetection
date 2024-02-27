using Emgu.CV.Dnn;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinformHumanAnFaceDetection.Config
{
	public enum EDeviceUsage
	{
		Cpu = 0,
		Cuda = 1,
		Vulkan = 2,
	}

	public class DeviceUsage
	{
		EDeviceUsage eUsage;
		[NotNull]
		public Emgu.CV.Dnn.Backend Backend { get; set; }
		[NotNull]
		public Emgu.CV.Dnn.Target Target { get; set; }
		public static DeviceUsage Setup(EDeviceUsage eUsage)
		{
			return eUsage switch
			{
				EDeviceUsage.Cpu => new DeviceUsage
				{
					Target = Target.Cpu,
					Backend = Backend.Default
				},
				EDeviceUsage.Cuda => new DeviceUsage
				{
					Target = Target.Cuda,
					Backend = Backend.Cuda
				},
				EDeviceUsage.Vulkan => new DeviceUsage
				{
					Target = Target.Vulkan,
					Backend = Backend.VkCom
				},
				_ => throw new ArgumentException("Giá trị không hợp lệ.", nameof(eUsage))
			};
		}
	}

	//public static class DeviceUsageExtension
	//{
	//	public static DeviceUsage Setup(this EDeviceUsage eUsage)
	//	{
	//		return eUsage switch
	//		{
	//			EDeviceUsage.Cpu => new DeviceUsage
	//			{
	//				Target = Target.Cpu,
	//				Backend = Backend.Default
	//			},
	//			EDeviceUsage.Cuda => new DeviceUsage
	//			{
	//				Target = Target.Cuda,
	//				Backend = Backend.Cuda
	//			},
	//			EDeviceUsage.Vulkan => new DeviceUsage
	//			{
	//				Target = Target.Vulkan,
	//				Backend = Backend.VkCom
	//			},
	//			_ => throw new ArgumentException("Giá trị không hợp lệ.", nameof(eUsage))
	//		};
	//	}
	//}
}
