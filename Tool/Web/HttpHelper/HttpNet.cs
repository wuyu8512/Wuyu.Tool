using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Tool.Web.HttpHelper
{
	public class HttpNet : WebClient
	{
		public string Method
		{
			get;
			set;
		}

		public string UserAgent
		{
			get;
			set;
		}

		public string Referer
		{
			get;
			set;
		}

		public int TimeOut
		{
			get;
			set;
		}

		public string Accept
		{
			get;
			set;
		}

		public CookieCollection CookieCollection
		{
			get;
			set;
		}

		public string ContentType
		{
			get;
			set;
		}

		public bool AllowAutoRedirect
		{
			get;
			set;
		}

		public int MaximumAutomaticRedirections
		{
			get;
			set;
		}

		public bool KeepAlive
		{
			get;
			set;
		} = true;

		public bool AutoCookieMerge
		{
			get;
			set;
		}

		static HttpNet()
		{
			ServicePointManager.ServerCertificateValidationCallback = CheckValidationResult;
			// ServicePointManager.SecurityProtocol = GetSecurityAllValue();
			ServicePointManager.DefaultConnectionLimit = 64;
		}

		public static byte[] Get(string url, ref CookieCollection cookies, ref WebHeaderCollection headers, string referer = null, string userAgent = null, string accept = null, int timeout = 10000, WebProxy proxy = null, Encoding encoding = null, bool allowAutoRedirect = true, bool autoCookieMerge = true)
		{
			HttpNet httpNet = new HttpNet
			{
				CookieCollection = cookies,
				Headers = headers,
				Referer = referer,
				UserAgent = userAgent,
				Accept = accept,
				TimeOut = timeout,
				Encoding = (encoding ?? Encoding.UTF8),
				Proxy = proxy,
				AllowAutoRedirect = allowAutoRedirect,
				AutoCookieMerge = autoCookieMerge
			};
			byte[] result= httpNet.DownloadData(new Uri(url));
			headers = httpNet.ResponseHeaders;
			cookies = httpNet.CookieCollection;
			return result;
		}

		public static byte[] Get(string url, string referer = null, string userAgent = null, string accept = null, int timeout = 10000, WebProxy proxy = null, Encoding encoding = null, bool allowAutoRedirect = true, bool autoCookieMerge = true)
		{
			CookieCollection cookies = new CookieCollection();
			WebHeaderCollection headers = new WebHeaderCollection();
			return Get(url, ref cookies, ref headers, referer, userAgent, accept, timeout, proxy, encoding, allowAutoRedirect, autoCookieMerge);
		}

		public static byte[] Get(string url, ref WebHeaderCollection headers, string referer = null, string userAgent = null, string accept = null, int timeout = 10000, WebProxy proxy = null, Encoding encoding = null, bool allowAutoRedirect = true, bool autoCookieMerge = true)
		{
			CookieCollection cookies = new CookieCollection();
			return Get(url, ref cookies, ref headers, referer, userAgent, accept, timeout, proxy, encoding, allowAutoRedirect, autoCookieMerge);
		}

		public static byte[] Get(string url, ref CookieCollection cookies, string referer = null, string userAgent = null, string accept = null, int timeout = 10000, WebProxy proxy = null, Encoding encoding = null, bool allowAutoRedirect = true, bool autoCookieMerge = true)
		{
			WebHeaderCollection headers = new WebHeaderCollection();
			return Get(url, ref cookies, ref headers, referer, userAgent, accept, timeout, proxy, encoding, allowAutoRedirect, autoCookieMerge);
		}

		public static byte[] Post(string url, byte[] data, string contentType, string referer, string userAgent, string accept, int timeout, ref CookieCollection cookies, ref WebHeaderCollection headers, WebProxy proxy, Encoding encoding, bool allowAutoRedirect = true, bool autoCookieMerge = true)
		{
			HttpNet httpNet = new HttpNet
			{
				ContentType = contentType,
				Referer = referer,
				UserAgent = userAgent,
				Accept = accept,
				TimeOut = timeout,
				CookieCollection = cookies,
				Headers = headers,
				Proxy = proxy,
				AutoCookieMerge = autoCookieMerge,
				AllowAutoRedirect = allowAutoRedirect,
				Encoding = (encoding ?? Encoding.UTF8)
			};
			byte[] result = httpNet.UploadData(new Uri(url), data);
			headers = httpNet.ResponseHeaders;
			cookies = httpNet.CookieCollection;
			return result;
		}

		public static byte[] Post(string url, byte[] data, string contentType, string referer, ref CookieCollection cookies, ref WebHeaderCollection headers, WebProxy proxy, Encoding encoding, bool allowAutoRedirect = true, bool autoCookieMerge = true)
		{
			return Post(url, data, contentType, referer, string.Empty, string.Empty, 0, ref cookies, ref headers, proxy, encoding, allowAutoRedirect, autoCookieMerge);
		}

		public static byte[] Post(string url, byte[] data, string contentType, string referer, ref CookieCollection cookies, ref WebHeaderCollection headers, Encoding encoding, bool allowAutoRedirect = true, bool autoCookieMerge = true)
		{
			return Post(url, data, contentType, referer, ref cookies, ref headers, null, encoding, allowAutoRedirect, autoCookieMerge);
		}

		public static byte[] Post(string url, byte[] data, string contentType, string referer, ref CookieCollection cookies, ref WebHeaderCollection headers, bool allowAutoRedirect = true, bool autoCookieMerge = true)
		{
			return Post(url, data, contentType, referer, ref cookies, ref headers, Encoding.UTF8, allowAutoRedirect, autoCookieMerge);
		}

		public static byte[] Post(string url, byte[] data, string contentType, string referer, ref CookieCollection cookies, bool allowAutoRedirect = true, bool autoCookieMerge = true)
		{
			WebHeaderCollection headers = new WebHeaderCollection();
			return Post(url, data, contentType, referer, ref cookies, ref headers, allowAutoRedirect, autoCookieMerge);
		}

		public static byte[] Post(string url, byte[] data, string contentType, string referer, ref WebHeaderCollection headers, bool allowAutoRedirect = true)
		{
			CookieCollection cookies = new CookieCollection();
			return Post(url, data, contentType, referer, ref cookies, ref headers, allowAutoRedirect, autoCookieMerge: false);
		}

		public static byte[] Post(string url, byte[] data, string contentType, ref CookieCollection cookies, bool allowAutoRedirect = true, bool autoCookieMerge = true)
		{
			return Post(url, data, contentType, string.Empty, ref cookies, allowAutoRedirect, autoCookieMerge);
		}

		public static byte[] Post(string url, byte[] data, string contentType, ref WebHeaderCollection headers, bool allowAutoRedirect = true)
		{
			return Post(url, data, contentType, string.Empty, ref headers, allowAutoRedirect);
		}

		public static byte[] Post(string url, byte[] data, string contentType, string referer, bool allowAutoRedirect = true)
		{
			WebHeaderCollection headers = new WebHeaderCollection();
			return Post(url, data, contentType, referer, ref headers, allowAutoRedirect);
		}

		public static byte[] Post(string url, byte[] data, string contentType, bool allowAutoRedirect = true)
		{
			return Post(url, data, contentType, string.Empty, allowAutoRedirect);
		}

		public static byte[] Post(string url, byte[] data, bool allowAutoRedirect = true)
		{
			return Post(url, data, string.Empty, string.Empty, allowAutoRedirect);
		}

		public static CookieCollection UpdateCookie(CookieCollection oldCookies, CookieCollection newCookies)
		{
			if (oldCookies == null)
			{
				throw new ArgumentNullException("oldCookies");
			}
			if (newCookies == null)
			{
				throw new ArgumentNullException("newCookies");
			}
			for (int i = 0; i < newCookies.Count; i++)
			{
				int num = CheckCookie(oldCookies, newCookies[i].Name);
				if (num >= 0)
				{
					oldCookies[num].Value = newCookies[i].Value;
				}
				else
				{
					oldCookies.Add(newCookies[i]);
				}
			}
			return oldCookies;
		}

		private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}

		private static int CheckCookie(CookieCollection cookie, string name)
		{
			for (int i = 0; i < cookie.Count; i++)
			{
				if (cookie[i].Name == name)
				{
					return i;
				}
			}
			return -1;
		}

		private static SecurityProtocolType GetSecurityAllValue()
		{
			SecurityProtocolType securityProtocolType = SecurityProtocolType.SystemDefault;
			foreach (SecurityProtocolType value in Enum.GetValues(typeof(SecurityProtocolType)))
			{
				securityProtocolType |= value;
			}
			return securityProtocolType;
		}

		protected override WebRequest GetWebRequest(Uri address)
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)base.GetWebRequest(address);
			httpWebRequest.ProtocolVersion = HttpVersion.Version11;
			httpWebRequest.KeepAlive = KeepAlive;
			if (CookieCollection != null)
			{
				httpWebRequest.CookieContainer = new CookieContainer();
				httpWebRequest.CookieContainer.Add(address, CookieCollection);
			}
			//else
			//{
			//	httpWebRequest.CookieContainer = new CookieContainer();
			//}
			if (!string.IsNullOrEmpty(UserAgent))
			{
				httpWebRequest.UserAgent = UserAgent;
			}
			else
			{
				httpWebRequest.UserAgent = "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36";
			}
			if (TimeOut > 0)
			{
				httpWebRequest.Timeout = TimeOut;
			}
			if (!string.IsNullOrEmpty(Accept))
			{
				httpWebRequest.Accept = Accept;
			}
			else
			{
				httpWebRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
			}
			httpWebRequest.AllowAutoRedirect = AllowAutoRedirect;
			if (AllowAutoRedirect)
			{
				if (MaximumAutomaticRedirections <= 0)
				{
					httpWebRequest.MaximumAutomaticRedirections = 5;
				}
				else
				{
					httpWebRequest.MaximumAutomaticRedirections = MaximumAutomaticRedirections;
				}
			}
			if (!string.IsNullOrEmpty(Referer))
			{
				httpWebRequest.Referer = Referer;
			}
			if (httpWebRequest.Method.ToUpper() != "GET")
			{
				if (!string.IsNullOrEmpty(ContentType))
				{
					httpWebRequest.ContentType = ContentType;
				}
				else
				{
					httpWebRequest.ContentType = "application/x-www-form-urlencoded";
				}
			}
			return httpWebRequest;
		}

		protected override WebResponse GetWebResponse(WebRequest request)
		{
			HttpWebResponse httpWebResponse = (HttpWebResponse)base.GetWebResponse(request);
			Method = httpWebResponse.Method;
			ContentType = httpWebResponse.ContentType;
			if (AutoCookieMerge && CookieCollection != null)
			{
				UpdateCookie(CookieCollection, httpWebResponse.Cookies);
			}
			else
			{
				CookieCollection = httpWebResponse.Cookies;
			}
			return httpWebResponse;
		}

		public static string GetRedirectUrl(string url)
		{
			//HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
			//request.AllowAutoRedirect = false;
			//HttpWebResponse response = (HttpWebResponse)request.GetResponse();
			//response.Close();
			//return response.Headers["Location"];

			WebHeaderCollection webHeader = new WebHeaderCollection();
			Get(url, headers: ref webHeader, allowAutoRedirect: false);
			return webHeader["Location"];
		}
	}
}
