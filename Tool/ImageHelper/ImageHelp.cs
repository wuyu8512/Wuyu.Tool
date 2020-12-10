using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Tool.ImageHelper
{
	public static class ImageHelp
	{
		public static void Cut(string url, int width, int height, string savePath, string fileExt)
		{
			Image.FromFile(url);
			Bitmap bitmap = new Bitmap(url);
			decimal d = Math.Ceiling(bitmap.Height / (decimal)height);
			decimal d2 = Math.Ceiling(bitmap.Width / (decimal)width);
			for (decimal d3 = default; d3 < d; ++d3)
			{
				if (!Directory.Exists(savePath))
				{
					Directory.CreateDirectory(savePath);
				}
				for (decimal d4 = default; d4 < d2; ++d4)
				{
					string str = d3.ToString() + "," + d4.ToString() + "." + fileExt;
					Bitmap bitmap2 = new Bitmap(width, height);
					for (int i = 0; i < width; i++)
					{
						for (int j = 0; j < height; j++)
						{
							if (d4 * width + i < bitmap.Width && d3 * height + j < bitmap.Height)
							{
								bitmap2.SetPixel(i, j, bitmap.GetPixel((int)(d4 * width + i), (int)(d3 * height + j)));
							}
						}
					}
					ImageFormat format = ImageFormat.Png;
					switch (fileExt.ToLower())
					{
						case "png":
							format = ImageFormat.Png;
							break;
						case "bmp":
							format = ImageFormat.Bmp;
							break;
						case "gif":
							format = ImageFormat.Gif;
							break;
						case "jpg":
							format = ImageFormat.Jpeg;
							break;
					}
					bitmap2.Save(savePath + "//" + str, format);
				}
			}
		}

		public static bool CompressImage(string sFile, string dFile, bool force = false, int flag = 90, double scale = 1.0, int size = 2048)
		{
			if (scale <= 0.0 || scale > 1.0)
			{
				throw new Exception("缩放值必须大于0,小于等于1");
			}
			if (!force)
			{
				FileInfo fileInfo = new FileInfo(sFile);
				if (size != 0 && fileInfo.Length < size * 1024)
				{
					fileInfo.CopyTo(dFile);
					return true;
				}
			}

			Image image = Image.FromFile(sFile);
			ImageFormat rawFormat = image.RawFormat;
			int num = (int)(image.Height * scale);
			int num2 = (int)(image.Width * scale);
			Size size2 = new Size(image.Width, image.Height);
			int num4;
			int num3;
			if (size2.Width > num || size2.Width > num2)
			{
				if (size2.Width * num > size2.Width * num2)
				{
					num3 = num2;
					num4 = num2 * size2.Height / size2.Width;
				}
				else
				{
					num4 = num;
					num3 = size2.Width * num / size2.Height;
				}
			}
			else
			{
				num3 = size2.Width;
				num4 = size2.Height;
			}
			Bitmap bitmap = new Bitmap(num2, num);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.Clear(Color.WhiteSmoke);
			graphics.CompositingQuality = CompositingQuality.HighQuality;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
			graphics.DrawImage(image, new Rectangle((int)((num2 - num3) * scale), (int)((num - num4) * scale), num3, num4), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
			graphics.Dispose();
			EncoderParameters encoderParameters = new EncoderParameters();
			EncoderParameter encoderParameter = new EncoderParameter(value: new long[1]
			{
				flag
			}, encoder: Encoder.Quality);
			encoderParameters.Param[0] = encoderParameter;
			try
			{
				ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
				ImageCodecInfo imageCodecInfo = null;
				for (int i = 0; i < imageEncoders.Length; i++)
				{
					if (imageEncoders[i].FormatDescription.Equals("JPEG"))
					{
						imageCodecInfo = imageEncoders[i];
						break;
					}
				}
				if (imageCodecInfo != null)
				{
					bitmap.Save(dFile, imageCodecInfo, encoderParameters);
					FileInfo fileInfo2 = new FileInfo(dFile);
					if (size != 0 && fileInfo2.Length > 1024 * size)
					{
						flag -= 5;
						CompressImage(sFile, dFile, force, flag, 1.0, size);
					}
				}
				else
				{
					bitmap.Save(dFile, rawFormat);
				}
				return true;
			}
			catch (Exception ex)
			{
				LogHelp.WriteLine("Exception", ex.ToString());
				return false;
			}
			finally
			{
				image.Dispose();
				bitmap.Dispose();
			}
		}

		public static void ChangeMd5(string sFile, string dFile)
		{
			Random random = new Random();
			if (sFile != dFile) File.Copy(sFile, dFile, true);
			FileStream file = new FileStream(dFile, FileMode.Append, FileAccess.Write);
			file.WriteByte(BitConverter.GetBytes(random.Next(0, 255))[0]);
			file.Dispose();
		}
	}
}