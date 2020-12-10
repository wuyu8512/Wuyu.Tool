using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tool.ImageHelper;

namespace Test
{
	class Program
	{
		static void Main(string[] args)
		{
			ImageHelp.CompressImage(@"C:\Users\wuyu\Desktop\yande.re 609240 hatsune_miku kieed vocaloid.jpg", @"C:\Users\wuyu\Desktop\yande.re 609240 hatsune_miku kieed vocaloid_1.jpg", size: 200);
		}
	}
}
