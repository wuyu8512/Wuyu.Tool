using System.Net;

namespace Wuyu.Tool.Web.HttpHelper
{
	public static class CookieHelp
	{
		public static CookieCollection StrToCookieCollection(string Cookiestr)
		{
			CookieCollection cookieCollection = new CookieCollection();
			string[] array = Cookiestr.Split(';');
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split('=');
				if (array2.Length == 2)
				{
					cookieCollection.Add(new Cookie(array2[0].Trim(), array2[1].Trim()));
				}
			}
			return cookieCollection;
		}

		public static string CookieCollectionToStr(CookieCollection cookies)
		{
			string text = string.Empty;
			foreach (Cookie cooky in cookies)
			{
				text = (!string.IsNullOrEmpty(text)) ? (text + ";" + cooky.Name + "=" + cooky.Value) : (cooky.Name + "=" + cooky.Value);
			}
			return text;
		}
	}
}
