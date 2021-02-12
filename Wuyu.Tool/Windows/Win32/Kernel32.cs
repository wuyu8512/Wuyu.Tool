using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Wuyu.Tool.Windows.Win32
{
	public static class Kernel32
	{
		public static int LstrlenA(IntPtr lpString) => Kernel.LstrlenA(lpString);
		public static int LstrlenW(IntPtr lpString) => Kernel.LstrlenW(lpString);
		public static int GetLastError() => Kernel.GetLastError();
		public static int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath)
			=> Kernel.GetPrivateProfileString(section, key, def, retVal, size, filePath);
		public static int WritePrivateProfileString(string section, string key, string val, string filePath)
			=> Kernel.WritePrivateProfileString(section, key, val, filePath);
		public static IntPtr LoadLibrary(string lpLibFileName) => Kernel.LoadLibrary(lpLibFileName);
		public static int FreeLibrary(IntPtr hLibModule) => Kernel.FreeLibrary(hLibModule);
		public static uint GetCurrentThreadId() => Kernel.GetCurrentThreadId();


		private static class Kernel
		{
			[DllImport("kernel32.dll", CharSet = CharSet.Ansi, EntryPoint = "lstrlenA")]
			public static extern int LstrlenA(IntPtr lpString);

			[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "lstrlenA")]
			public static extern int LstrlenW(IntPtr lpString);

			[DllImport("kernel32.dll")]
			public static extern int GetLastError();

			[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
			public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

			[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
			public static extern int WritePrivateProfileString(string section, string key, string val, string filePath);

			[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
			public static extern IntPtr LoadLibrary(string lpLibFileName);

			[DllImport("kernel32.dll")]
			public static extern int FreeLibrary(IntPtr hLibModule);

			[DllImport("kernel32.dll")]
			public static extern uint GetCurrentThreadId();
		}
	}
}
