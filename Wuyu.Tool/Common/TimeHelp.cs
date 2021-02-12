using System;
using System.Net;

namespace Wuyu.Tool.Common
{
	public static class TimeHelp
	{
		public static DateTime GetNetDateTime()
		{
			WebRequest webRequest = null;
			WebResponse webResponse = null;
			WebHeaderCollection webHeaderCollection = null;
			string value = string.Empty;
			try
			{
				webRequest = WebRequest.Create("https://www.baidu.com");
				webRequest.Timeout = 3000;
				webRequest.Credentials = CredentialCache.DefaultCredentials;
				webResponse = webRequest.GetResponse();
				webHeaderCollection = webResponse.Headers;
				string[] allKeys = webHeaderCollection.AllKeys;
				foreach (string text in allKeys)
				{
					if (text == "Date")
					{
						value = webHeaderCollection[text];
					}
				}
				return Convert.ToDateTime(value);
			}
			catch (Exception)
			{
				return DateTime.Now;
			}
			finally
			{
				webRequest?.Abort();
				webResponse?.Close();
				webHeaderCollection?.Clear();
			}
		}

		public static long GetCurrentTimeUnix(bool IsMilliseconds = false)
		{
			System.Threading.Thread.Sleep(1);
			TimeSpan timeSpan = DateTime.Now - new DateTime(1970, 1, 1) - TimeZoneInfo.Utc.BaseUtcOffset;
			if (IsMilliseconds)
			{
				return Convert.ToInt64(timeSpan.TotalMilliseconds);
			}
			return Convert.ToInt64(timeSpan.TotalSeconds);
		}


		public static DateTime TimeStampToDate(long timeStamp)
		{
			DateTime dtStart = (new DateTime(1970, 1, 1)).ToLocalTime();
			long lTime = (timeStamp * 10000000);
			TimeSpan toNow = new TimeSpan(lTime);
			DateTime targetDt = dtStart.Add(toNow);
			return targetDt;
		}
	}
}
