using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Wuyu.Tool.Web.IpHelper;

namespace Wuyu.Tool.Web.FtpHelper
{
	public class FtpHelp
	{
		private string _BaseUrl = string.Empty;

		public string FtpServer
		{
			get;
			set;
		}

		public string Username
		{
			get;
			set;
		}

		public string Password
		{
			get;
			set;
		}

		public int Port
		{
			get;
			set;
		}

		public FtpHelp(string serverIp, int point = 21)
		{
			if (!IpHelp.MatchInetAddress(serverIp))
			{
				throw new ArgumentException("IP地址格式不正确");
			}
			FtpServer = serverIp;
			_BaseUrl = "ftp://" + FtpServer + ":" + Port.ToString() + "/";
		}

		public FtpHelp(string serverIp, int port, string username, string password)
		{
			if (!IpHelp.MatchInetAddress(serverIp))
			{
				throw new ArgumentException("IP地址格式不正确");
			}
			FtpServer = serverIp;
			Port = port;
			Username = username;
			Password = password;
			_BaseUrl = "ftp://" + FtpServer + ":" + Port.ToString() + "/";
		}

		public void Download(string remoteFileName, string localFileName, bool ifCredential = false, Action<int, int> updateProgress = null)
		{
			using (FileStream fileStream = new FileStream(localFileName, FileMode.Create))
			{
				if (FtpServer == null || FtpServer.Trim().Length == 0)
				{
					throw new Exception("ftp下载目标服务器地址未设置！");
				}
				Uri requestUri = new Uri(_BaseUrl + remoteFileName);
				FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(requestUri);
				ftpWebRequest.UseBinary = true;
				FtpWebRequest ftpWebRequest2 = (FtpWebRequest)WebRequest.Create(requestUri);
				ftpWebRequest2.UseBinary = true;
				ftpWebRequest2.KeepAlive = false;
				if (ifCredential)
				{
					ftpWebRequest.Credentials = new NetworkCredential(Username, Password);
					ftpWebRequest2.Credentials = new NetworkCredential(Username, Password);
				}
				ftpWebRequest.Method = "SIZE";
				using (FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse())
				{
					long contentLength = ftpWebResponse.ContentLength;
					ftpWebRequest2.Method = "RETR";
					FtpWebResponse ftpWebResponse2 = (FtpWebResponse)ftpWebRequest2.GetResponse();
					using (ftpWebResponse2)
					{
						Stream responseStream = ftpWebResponse2.GetResponseStream();
						using (responseStream)
						{
							updateProgress?.Invoke((int)contentLength, 0);
							long num = 0L;
							int num2 = 1048576;
							byte[] buffer = new byte[num2];
							if (responseStream != null)
							{
								for (int num3 = responseStream.Read(buffer, 0, num2); num3 > 0; num3 = responseStream.Read(buffer, 0, num2))
								{
									num = num3 + num;
									fileStream.Write(buffer, 0, num3);
									updateProgress?.Invoke((int)contentLength, (int)num);
								}
							}
						}
					}
				}
			}
		}

		public void BrokenDownload(string remoteFileName, string localFileName, bool ifCredential, long size, Action<int, int> updateProgress = null)
		{
			using (FileStream fileStream = new FileStream(localFileName, FileMode.Append))
			{
				if (FtpServer == null || FtpServer.Trim().Length == 0)
				{
					throw new Exception("ftp下载目标服务器地址未设置！");
				}
				Uri requestUri = new Uri(_BaseUrl + remoteFileName);
				FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(requestUri);
				ftpWebRequest.UseBinary = true;
				ftpWebRequest.ContentOffset = size;
				FtpWebRequest ftpWebRequest2 = (FtpWebRequest)WebRequest.Create(requestUri);
				ftpWebRequest2.UseBinary = true;
				ftpWebRequest2.KeepAlive = false;
				ftpWebRequest2.ContentOffset = size;
				if (ifCredential)
				{
					ftpWebRequest.Credentials = new NetworkCredential(Username, Password);
					ftpWebRequest2.Credentials = new NetworkCredential(Username, Password);
				}
				ftpWebRequest.Method = "SIZE";
				using (FtpWebResponse ftpWebResponse = (FtpWebResponse)ftpWebRequest.GetResponse())
				{
					long contentLength = ftpWebResponse.ContentLength;
					ftpWebRequest2.Method = "RETR";
					using (FtpWebResponse ftpWebResponse2 = (FtpWebResponse)ftpWebRequest2.GetResponse())
					{
						using (Stream stream = ftpWebResponse2.GetResponseStream())
						{
							updateProgress?.Invoke((int)contentLength, 0);
							long num = 0L;
							int num2 = 1048576;
							byte[] buffer = new byte[num2];
							if (stream != null)
							{
								for (int num3 = stream.Read(buffer, 0, num2); num3 > 0; num3 = stream.Read(buffer, 0, num2))
								{
									num = num3 + num;
									fileStream.Write(buffer, 0, num3);
									updateProgress?.Invoke((int)contentLength, (int)num);
								}
							}
						}
					}
				}
			}
		}

		public void Download(string remoteFileName, string localFileName, bool ifCredential, bool brokenOpen, Action<int, int> updateProgress = null)
		{
			if (brokenOpen)
			{
				long size = 0L;
				if (File.Exists(localFileName))
				{
					using (FileStream fileStream = new FileStream(localFileName, FileMode.Open))
					{
						size = fileStream.Length;
					}
				}
				BrokenDownload(remoteFileName, localFileName, ifCredential, size, updateProgress);
			}
			Download(remoteFileName, localFileName, ifCredential, updateProgress);
		}

		public void UploadFile(string relativePath, string localFullPathName, Action<int, int> updateProgress = null)
		{
			FileInfo fileInfo = new FileInfo(localFullPathName);
			if (FtpServer == null || FtpServer.Trim().Length == 0)
			{
				throw new Exception("ftp上传目标服务器地址未设置！");
			}
			FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri(_BaseUrl + relativePath + "/" + fileInfo.Name));
			ftpWebRequest.KeepAlive = false;
			ftpWebRequest.UseBinary = true;
			ftpWebRequest.Credentials = new NetworkCredential(Username, Password);
			ftpWebRequest.Method = "STOR";
			ftpWebRequest.ContentLength = fileInfo.Length;
			int num = 1048576;
			byte[] buffer = new byte[num];
			using (FileStream fileStream = fileInfo.OpenRead())
			{
				using (Stream stream = ftpWebRequest.GetRequestStream())
				{
					int num2 = fileStream.Read(buffer, 0, num);
					int arg = (int)fileInfo.Length;
					updateProgress?.Invoke(arg, 0);
					int num3 = 0;
					while (num2 != 0)
					{
						num3 = num2 + num3;
						stream.Write(buffer, 0, num2);
						updateProgress?.Invoke(arg, num3);
						num2 = fileStream.Read(buffer, 0, num);
					}
				}
			}
		}

		public bool UploadBroken(string localFullPath, string remoteFilepath, Action<int, int> updateProgress = null)
		{
			if (remoteFilepath == null)
			{
				remoteFilepath = "";
			}
			FileInfo fileInfo = new FileInfo(localFullPath);
			long length = fileInfo.Length;
			string text;
			if (fileInfo.Name.IndexOf("#", StringComparison.Ordinal) == -1)
			{
				text = RemoveSpaces(fileInfo.Name);
			}
			else
			{
				text = fileInfo.Name.Replace("#", "＃");
				text = RemoveSpaces(text);
			}
			long fileSize = GetFileSize(text, remoteFilepath);
			if (fileSize >= length)
			{
				return false;
			}
			long num = fileSize;
			updateProgress?.Invoke((int)length, (int)fileSize);
			string uriString = (remoteFilepath.Length != 0) ? (_BaseUrl + remoteFilepath + "/" + text) : (_BaseUrl + text);
			FtpWebRequest ftpWebRequest = (FtpWebRequest)WebRequest.Create(new Uri(uriString));
			ftpWebRequest.Credentials = new NetworkCredential(Username, Password);
			ftpWebRequest.KeepAlive = false;
			ftpWebRequest.Method = "APPE";
			ftpWebRequest.UseBinary = true;
			ftpWebRequest.ContentLength = fileInfo.Length;
			int num2 = 1048576;
			byte[] buffer = new byte[num2];
			using (FileStream fileStream = fileInfo.OpenRead())
			{
				using (Stream stream = ftpWebRequest.GetRequestStream())
				{
					fileStream.Seek(fileSize, SeekOrigin.Begin);
					int num3 = fileStream.Read(buffer, 0, num2);
					while (num3 != 0)
					{
						stream.Write(buffer, 0, num3);
						num3 = fileStream.Read(buffer, 0, num2);
						num += num3;
						updateProgress?.Invoke((int)length, (int)num);
					}
				}
			}
			return true;
		}

		private string RemoveSpaces(string str)
		{
			string text = "";
			for (int i = 0; i < str.Length; i++)
			{
				char c = str[i];
				if (Encoding.ASCII.GetBytes(c.ToString())[0] != 32)
				{
					text += c.ToString();
				}
			}
			return text.Split('.')[text.Split('.').Length - 2] + "." + text.Split('.')[text.Split('.').Length - 1];
		}

		public long GetFileSize(string filePath, string remoteFilepath)
		{
			try
			{
				FileInfo fileInfo = new FileInfo(filePath);
				string requestUriString = (remoteFilepath.Length != 0) ? (_BaseUrl + remoteFilepath + "/" + fileInfo.Name) : (_BaseUrl + fileInfo.Name);
				FtpWebRequest obj = (FtpWebRequest)WebRequest.Create(requestUriString);
				obj.KeepAlive = false;
				obj.UseBinary = true;
				obj.Credentials = new NetworkCredential(Username, Password);
				obj.Method = "SIZE";
				return ((FtpWebResponse)obj.GetResponse()).ContentLength;
			}
			catch
			{
				return 0L;
			}
		}

		public List<string> GetFilesDetails(string relativePath = "")
		{
			List<string> list = new List<string>();
			FtpWebRequest obj = (FtpWebRequest)WebRequest.Create(new Uri(Path.Combine(_BaseUrl, relativePath).Replace("\\", "/")));
			obj.Credentials = new NetworkCredential(Username, Password);
			obj.Method = "LIST";
			using (WebResponse webResponse = obj.GetResponse())
			{
				using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream() ?? throw new InvalidOperationException(), Encoding.UTF8))
				{
					for (string text = streamReader.ReadLine(); text != null; text = streamReader.ReadLine())
					{
						list.Add(text);
					}
					return list;
				}
			}
		}

		public List<string> GetFiles(string relativePath = "", string mask = "*.*")
		{
			List<string> list = new List<string>();
			FtpWebRequest obj = (FtpWebRequest)WebRequest.Create(new Uri(Path.Combine(_BaseUrl, relativePath).Replace("\\", "/")));
			obj.UseBinary = true;
			obj.Credentials = new NetworkCredential(Username, Password);
			obj.Method = "NLST";
			using (WebResponse webResponse = obj.GetResponse())
			{
				using (StreamReader streamReader = new StreamReader(webResponse.GetResponseStream() ?? throw new InvalidOperationException(), Encoding.UTF8))
				{
					for (string text = streamReader.ReadLine(); text != null; text = streamReader.ReadLine())
					{
						if (mask.Trim() != string.Empty && mask.Trim() != "*.*")
						{
							string text2 = mask.Substring(0, mask.IndexOf("*", StringComparison.Ordinal));
							if (text.Substring(0, text2.Length) == text2)
							{
								list.Add(text);
							}
						}
						else
						{
							list.Add(text);
						}
					}
					return list;
				}
			}
		}

		public string[] GetDirectories(string relativePath)
		{
			List<string> filesDetails = GetFilesDetails(relativePath);
			string text = string.Empty;
			foreach (string item in filesDetails)
			{
				int num = item.IndexOf("<DIR>", StringComparison.Ordinal);
				if (num > 0)
				{
					text = text + item.Substring(num + 5).Trim() + "\n";
				}
				else if (item.Trim().StartsWith("d"))
				{
					string text2 = item.Split(new string[1]
					{
						" "
					}, StringSplitOptions.RemoveEmptyEntries)[8];
					if (text2 != "." && text2 != "..")
					{
						text2 = item.Substring(item.IndexOf(text2, StringComparison.Ordinal));
						text = text + text2 + "\n";
					}
				}
			}
			char[] separator = new char[1]
			{
				'\n'
			};
			return text.Split(separator);
		}

		public void Delete(string filePath)
		{
			FtpWebRequest obj = (FtpWebRequest)WebRequest.Create(new Uri(Path.Combine(_BaseUrl, filePath).Replace("\\", "/")));
			obj.Credentials = new NetworkCredential(Username, Password);
			obj.KeepAlive = false;
			obj.Method = "DELE";
			using (FtpWebResponse ftpWebResponse = (FtpWebResponse)obj.GetResponse())
			{
				using (Stream stream = ftpWebResponse.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(stream ?? throw new InvalidOperationException()))
					{
						streamReader.ReadToEnd();
					}
				}
			}
		}

		public void RemoveDirectory(string dirPath)
		{
			FtpWebRequest obj = (FtpWebRequest)WebRequest.Create(new Uri(Path.Combine(_BaseUrl, dirPath).Replace("\\", "/")));
			obj.Credentials = new NetworkCredential(Username, Password);
			obj.KeepAlive = false;
			obj.Method = "RMD";
			using (FtpWebResponse ftpWebResponse = (FtpWebResponse)obj.GetResponse())
			{
				using (Stream stream = ftpWebResponse.GetResponseStream())
				{
					using (StreamReader streamReader = new StreamReader(stream ?? throw new InvalidOperationException()))
					{
						streamReader.ReadToEnd();
					}
				}
			}
		}

		public long GetFileSize(string filePath)
		{
			FtpWebRequest obj = (FtpWebRequest)WebRequest.Create(new Uri(Path.Combine(_BaseUrl + FtpServer, filePath).Replace("\\", "/")));
			obj.Method = "SIZE";
			obj.UseBinary = true;
			obj.Credentials = new NetworkCredential(Username, Password);
			using (FtpWebResponse ftpWebResponse = (FtpWebResponse)obj.GetResponse())
			{
				return ftpWebResponse.ContentLength;
			}
		}

		public bool DirectoryExist(string remoteDirPath)
		{
			try
			{
				string[] directories = GetDirectories(remoteDirPath);
				for (int i = 0; i < directories.Length; i++)
				{
					if (directories[i].Trim() == remoteDirPath.Trim())
					{
						return true;
					}
				}
				return false;
			}
			catch
			{
				return false;
			}
		}

		public bool FileExist(string remoteFileName)
		{
			foreach (string file in GetFiles("*.*"))
			{
				if (file.Trim() == remoteFileName.Trim())
				{
					return true;
				}
			}
			return false;
		}

		public void MakeDir(string relativePath, string newDir)
		{
			FtpWebRequest obj = (FtpWebRequest)WebRequest.Create(new Uri(Path.Combine(_BaseUrl, relativePath, newDir).Replace("\\", "/")));
			obj.Method = "MKD";
			obj.UseBinary = true;
			obj.Credentials = new NetworkCredential(Username, Password);
			using (FtpWebResponse ftpWebResponse = (FtpWebResponse)obj.GetResponse())
			{
				using (ftpWebResponse.GetResponseStream())
				{
				}
			}
		}

		public void Rename(string relativePath, string currentFilename, string newFilename)
		{
			FtpWebRequest obj = (FtpWebRequest)WebRequest.Create(new Uri(Path.Combine(_BaseUrl, relativePath, currentFilename).Replace("\\", "/")));
			obj.Method = "RENAME";
			obj.RenameTo = newFilename;
			obj.UseBinary = true;
			obj.Credentials = new NetworkCredential(Username, Password);
			using (FtpWebResponse ftpWebResponse = (FtpWebResponse)obj.GetResponse())
			{
				using (ftpWebResponse.GetResponseStream())
				{
				}
			}
		}

		public void MoveFile(string relativePath, string currentFilename, string newDirectory)
		{
			Rename(relativePath, currentFilename, newDirectory);
		}
	}
}
