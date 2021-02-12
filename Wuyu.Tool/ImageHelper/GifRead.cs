using System.Drawing;
using System.Drawing.Imaging;

namespace Wuyu.Tool.ImageHelper
{
	public class GifRead
	{
		private Image gif;

		public GifRead(string path)
		{
			gif = Image.FromFile(path);
		}

		public GifRead(Image image)
		{
			gif = image;
		}

		public int GetFrameCount()
		{
			return gif.GetFrameCount(FrameDimension.Time);
		}

		public Image GetFrame(int index)
		{
			gif.SelectActiveFrame(FrameDimension.Time, index);
			return (Image)gif.Clone();
		}
	}
}
