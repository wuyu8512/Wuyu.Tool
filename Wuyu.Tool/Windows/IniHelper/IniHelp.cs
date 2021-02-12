using System;
using System.IO;
using System.Text;
using Wuyu.Tool.Windows.Win32;

namespace Wuyu.Tool.Windows.IniHelper
{
	public class IniHelp
	{
		private string FilePath;

		public string Path
		{
			get
			{
				return FilePath;
			}
			set
			{
				if (value.Substring(0, 1) == "\\" || value.Substring(0, 1) == "/")
				{
					FilePath = AppDomain.CurrentDomain?.ToString() + value;
				}
				else
				{
					FilePath = value;
				}
			}
		}

		public IniHelp(string _Path)
		{
			Path = _Path;
		}

		public void WriteValue(string Section, string Key, string Value)
		{
			Kernel32.WritePrivateProfileString(Section, Key, Value, FilePath);
		}

		public string ReadValue(string Section, string Key)
		{
			try
			{
				StringBuilder stringBuilder = new StringBuilder(204800);
				Kernel32.GetPrivateProfileString(Section, Key, "", stringBuilder, 204800, FilePath);
				return stringBuilder.ToString();
			}
			catch
			{
				return "";
			}
		}

		public bool RemoveSection(string Section)
		{
			return Kernel32.WritePrivateProfileString(Section, null, null, FilePath) == 0;
		}

		public bool Exists()
		{
			return File.Exists(FilePath);
		}
	}
}
