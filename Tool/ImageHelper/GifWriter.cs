using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Tool.ImageHelper
{
	public class GifWriter : IDisposable
	{
		private const long SourceGlobalColorInfoPosition = 10L;

		private const long SourceImageBlockPosition = 789L;

		private readonly BinaryWriter _writer;

		private bool _firstFrame = true;

		private readonly object _syncLock = new object();

		public int DefaultWidth
		{
			get;
			set;
		}

		public int DefaultHeight
		{
			get;
			set;
		}

		public int DefaultFrameDelay
		{
			get;
			set;
		}

		public int Repeat
		{
			get;
		}

		public GifWriter(Stream OutStream, int DefaultFrameDelay = 500, int Repeat = -1)
		{
			if (OutStream == null)
			{
				throw new ArgumentNullException(nameof(OutStream));
			}
			if (DefaultFrameDelay <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(DefaultFrameDelay));
			}
			if (Repeat < -1)
			{
				throw new ArgumentOutOfRangeException(nameof(Repeat));
			}
			_writer = new BinaryWriter(OutStream);
			this.DefaultFrameDelay = DefaultFrameDelay;
			this.Repeat = Repeat;
		}

		public GifWriter(string FileName, int DefaultFrameDelay = 500, int Repeat = -1)
			: this(new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read), DefaultFrameDelay, Repeat)
		{
		}

		public void WriteFrame(Image Image, int Delay = 0)
		{
			lock (_syncLock)
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					Image.Save(memoryStream, ImageFormat.Gif);
					if (_firstFrame)
					{
						InitHeader(memoryStream, _writer, Image.Width, Image.Height);
					}
					WriteGraphicControlBlock(memoryStream, _writer, (Delay == 0) ? DefaultFrameDelay : Delay);
					WriteImageBlock(memoryStream, _writer, !_firstFrame, 0, 0, Image.Width, Image.Height);
				}
			}
			if (_firstFrame)
			{
				_firstFrame = false;
			}
		}

		private void InitHeader(Stream SourceGif, BinaryWriter Writer, int Width, int Height)
		{
			Writer.Write("GIF".ToCharArray());
			Writer.Write("89a".ToCharArray());
			Writer.Write((short)((DefaultWidth == 0) ? Width : DefaultWidth));
			Writer.Write((short)((DefaultHeight == 0) ? Height : DefaultHeight));
			SourceGif.Position = 10L;
			Writer.Write((byte)SourceGif.ReadByte());
			Writer.Write((byte)0);
			Writer.Write((byte)0);
			WriteColorTable(SourceGif, Writer);
			if (Repeat != -1)
			{
				Writer.Write((short)(-223));
				Writer.Write((byte)11);
				Writer.Write("NETSCAPE2.0".ToCharArray());
				Writer.Write((byte)3);
				Writer.Write((byte)1);
				Writer.Write((short)Repeat);
				Writer.Write((byte)0);
			}
		}

		private static void WriteColorTable(Stream SourceGif, BinaryWriter Writer)
		{
			SourceGif.Position = 13L;
			byte[] array = new byte[768];
			SourceGif.Read(array, 0, array.Length);
			Writer.Write(array, 0, array.Length);
		}

		private static void WriteGraphicControlBlock(Stream SourceGif, BinaryWriter Writer, int FrameDelay)
		{
			SourceGif.Position = 781L;
			byte[] array = new byte[8];
			SourceGif.Read(array, 0, array.Length);
			Writer.Write((short)(-1759));
			Writer.Write((byte)4);
			Writer.Write((byte)((array[3] & 0xF7) | 8));
			Writer.Write((short)(FrameDelay / 10));
			Writer.Write(array[6]);
			Writer.Write((byte)0);
		}

		private static void WriteImageBlock(Stream SourceGif, BinaryWriter Writer, bool IncludeColorTable, int X, int Y, int Width, int Height)
		{
			SourceGif.Position = 789L;
			byte[] array = new byte[11];
			SourceGif.Read(array, 0, array.Length);
			Writer.Write(array[0]);
			Writer.Write((short)X);
			Writer.Write((short)Y);
			Writer.Write((short)Width);
			Writer.Write((short)Height);
			if (IncludeColorTable)
			{
				SourceGif.Position = 10L;
				Writer.Write((byte)((SourceGif.ReadByte() & 0x3F) | 0x80));
				WriteColorTable(SourceGif, Writer);
			}
			else
			{
				Writer.Write((byte)((array[9] & 7) | 7));
			}
			Writer.Write(array[10]);
			SourceGif.Position = 789L + (long)array.Length;
			for (int num = SourceGif.ReadByte(); num > 0; num = SourceGif.ReadByte())
			{
				byte[] buffer = new byte[num];
				SourceGif.Read(buffer, 0, num);
				Writer.Write((byte)num);
				Writer.Write(buffer, 0, num);
			}
			Writer.Write((byte)0);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			_writer.Write((byte)59);
			_writer.BaseStream.Dispose();
			_writer.Dispose();
		}
	}
}
