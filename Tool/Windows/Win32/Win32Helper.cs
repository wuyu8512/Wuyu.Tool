using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Tool.Windows.Win32
{
	public static class Win32Help
	{
		public static string GetStrA(IntPtr lpString, Encoding encoding)
		{
			int num = Kernel32.LstrlenA(lpString);
			if (num == 0)
			{
				return string.Empty;
			}
			byte[] array = new byte[num];
			Marshal.Copy(lpString, array, 0, num);
			return encoding.GetString(array);
		}

		public static string GetStrW(IntPtr lpString, Encoding encoding)
		{
			int num = Kernel32.LstrlenW(lpString);
			if (num == 0)
			{
				return string.Empty;
			}
			byte[] array = new byte[num];
			Marshal.Copy(lpString, array, 0, num);
			return encoding.GetString(array);
		}

		public static string IniReadValue(string filepath, string Section, string Key, string def = null, int TextLength = 65535)
		{
			StringBuilder stringBuilder = new StringBuilder(TextLength);
			Kernel32.GetPrivateProfileString(Section, Key, def, stringBuilder, TextLength, filepath);
			return stringBuilder.ToString();
		}

		public static int IniWriteValue(string filepath, string Section, string Key, string Value)
		{
			return Kernel32.WritePrivateProfileString(Section, Key, Value, filepath);
		}
	}
}
