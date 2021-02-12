using System.Runtime.InteropServices;

namespace Wuyu.Tool.Windows.Win32
{
	public static class Ole32
	{
		public static int CoInitializeEx(object pvReserved, int dwCoInit) => _Ole32.CoInitializeEx(pvReserved, dwCoInit);
		public static void CoUninitialize() => _Ole32.CoUninitialize();

		static class  _Ole32{

			[DllImport("Ole32.dll", EntryPoint = "CoInitializeEx")]
			public static extern int CoInitializeEx(object pvReserved, int dwCoInit);

			[DllImport("Ole32.dll", EntryPoint = "CoUninitialize")]
			public static extern void CoUninitialize();
		}
	}
}
