using System.Net;

namespace Wuyu.Tool.Web.HttpHelper
{
	public static class HeaderHelp
	{
		public static WebHeaderCollection GetDefaultHeader(string Referer = null, string ContentType = null)
		{
			WebHeaderCollection webHeaderCollection = new WebHeaderCollection
			{
				{
					"Accept",
					"text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8"
				},
				{
					"Accept-Language",
					"zh-cn"
				},
				{
					"User-Agent",
					"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.89 Safari/537.36"
				}
			};
			if (!string.IsNullOrWhiteSpace(Referer))
			{
				webHeaderCollection.Add("Referer", Referer);
			}
			if (!string.IsNullOrWhiteSpace(ContentType))
			{
				webHeaderCollection.Add("Content-Type", "application/x-www-form-urlencoded");
			}
			return webHeaderCollection;
		}
	}
}
