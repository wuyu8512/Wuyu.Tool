using System.Text.RegularExpressions;
using Tool.Expansion;

namespace Tool.Web.IpHelper
{
	public static class IpHelp
	{
		public static bool IsValidProxy(string ip)
		{
			if (Regex.IsMatch(ip, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\:[0-9]{1,5}"))
			{
				return true;
			}
			return false;
		}

		public static bool IsValidIP(string ip)
		{
			return Regex.IsMatch(ip, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}");
		}

		public static bool MatchInetAddress(string s)
		{
			MatchInetAddress(s, out bool isMatch);
			return isMatch;
		}

		public static Match MatchInetAddress(string s, out bool isMatch)
		{
			Match match;
			if (s.Contains(":"))
			{
				match = Regex.Match(s, "^([\\da-fA-F]{0,4}:){1,7}[\\da-fA-F]{1,4}$");
				isMatch = match.Success;
			}
			else
			{
				match = Regex.Match(s, "^(\\d+)\\.(\\d+)\\.(\\d+)\\.(\\d+)$");
				isMatch = match.Success;
				foreach (Group group in match.Groups)
				{
					if (group.Value.ToInt32() < 0 || group.Value.ToInt32() > 255)
					{
						isMatch = false;
						break;
					}
				}
			}
			if (!isMatch)
			{
				return null;
			}
			return match;
		}
	}
}
