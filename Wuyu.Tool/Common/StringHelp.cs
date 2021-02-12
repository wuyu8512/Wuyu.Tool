using System;
using System.Text;

namespace Wuyu.Tool.Common
{
	public static class StringHelp
	{
		public static string CreateCheckCode(int len = 10)
		{
			char[] array = new char[61]
			{
				'A',
				'B',
				'C',
				'D',
				'E',
				'F',
				'G',
				'H',
				'J',
				'K',
				'L',
				'M',
				'N',
				'O',
				'P',
				'Q',
				'R',
				'S',
				'T',
				'U',
				'V',
				'W',
				'X',
				'Y',
				'Z',
				'a',
				'b',
				'c',
				'd',
				'e',
				'f',
				'g',
				'h',
				'i',
				'j',
				'k',
				'l',
				'm',
				'n',
				'o',
				'p',
				'q',
				'r',
				's',
				't',
				'u',
				'v',
				'w',
				'x',
				'y',
				'z',
				'0',
				'1',
				'2',
				'3',
				'4',
				'5',
				'6',
				'7',
				'8',
				'9'
			};
			string text = string.Empty;
			Random random = new Random();
			for (int i = 0; i < len; i++)
			{
				text += array[random.Next(array.Length)].ToString();
			}
			return text;
		}

		public static string Rid(int length = 12)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < length; i++)
			{
				Random random = new Random(Guid.NewGuid().GetHashCode());
				stringBuilder.Append(random.Next(0, 10));
			}
			return stringBuilder.ToString();
		}
	}
}
