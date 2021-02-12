using System.Linq;
using System.Text;
using System.Web;

#if NET48
using System.Web.Script.Serialization;
#endif

namespace Wuyu.Tool.Web.HttpHelper
{
	public static class Url
	{
#if NET48
		private static JavaScriptSerializer JsonSerializer = new JavaScriptSerializer();

		public static string GetShortUrl(string url)
		{
			string shUrl = Encoding.UTF8.GetString(HttpNet.Get("https://api.uomg.com/api/long2dwz?dwzapi=tcn&url=" + Encode(url, Encoding.UTF8)));

			if (!string.IsNullOrWhiteSpace(shUrl))
			{
				var json = JsonSerializer.Deserialize<dynamic>(shUrl);
				if (json["code"] == 1) return json["ae_url"];
			}
			return shUrl;
		}
#endif


		public static string Encode(string data, Encoding encoding)
		{
			return HttpUtility.UrlEncode(data, encoding);
		}

		public static string Decode(string data, Encoding encoding)
		{
			return HttpUtility.UrlDecode(data, encoding);
		}
	}
}
