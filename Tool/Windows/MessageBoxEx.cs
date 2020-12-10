#if NET48
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Tool.Windows.Win32;
using static Tool.Windows.Win32.Proc;
using static Tool.Windows.Win32.User32;

namespace Tool.Windows
{
	public static class MessageBoxEx
	{
		public enum CbtHookAction
		{
			HCBT_MOVESIZE,
			HCBT_MINMAX,
			HCBT_QS,
			HCBT_CREATEWND,
			HCBT_DESTROYWND,
			HCBT_ACTIVATE,
			HCBT_CLICKSKIPPED,
			HCBT_KEYSKIPPED,
			HCBT_SYSCOMMAND,
			HCBT_SETFOCUS
		}

		public struct CWPRETSTRUCT
		{
			public IntPtr lResult;

			public IntPtr lParam;

			public IntPtr wParam;

			public uint message;

			public IntPtr hwnd;
		}

		private static IWin32Window _owner;

		private static HookProc _hookProc = MessageBoxHookProc;

		private static IntPtr _hHook = IntPtr.Zero;

		public const int WH_CALLWNDPROCRET = 12;

		public static DialogResult Show(string text)
		{
			Initialize();
			return MessageBox.Show(text);
		}

		public static DialogResult Show(string text, string caption)
		{
			Initialize();
			return MessageBox.Show(text, caption);
		}

		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
		{
			Initialize();
			return MessageBox.Show(text, caption, buttons);
		}

		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			Initialize();
			return MessageBox.Show(text, caption, buttons, icon);
		}

		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defButton)
		{
			Initialize();
			return MessageBox.Show(text, caption, buttons, icon, defButton);
		}

		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defButton, MessageBoxOptions options)
		{
			Initialize();
			return MessageBox.Show(text, caption, buttons, icon, defButton, options);
		}

		public static DialogResult Show(IWin32Window owner, string text)
		{
			_owner = owner;
			Initialize();
			return MessageBox.Show(owner, text);
		}

		public static DialogResult Show(IWin32Window owner, string text, string caption)
		{
			_owner = owner;
			Initialize();
			return MessageBox.Show(owner, text, caption);
		}

		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
		{
			_owner = owner;
			Initialize();
			return MessageBox.Show(owner, text, caption, buttons);
		}

		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			_owner = owner;
			Initialize();
			return MessageBox.Show(owner, text, caption, buttons, icon);
		}

		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defButton)
		{
			_owner = owner;
			Initialize();
			return MessageBox.Show(owner, text, caption, buttons, icon, defButton);
		}

		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defButton, MessageBoxOptions options)
		{
			_owner = owner;
			Initialize();
			return MessageBox.Show(owner, text, caption, buttons, icon, defButton, options);
		}

		private static void Initialize()
		{
			if (_hHook != IntPtr.Zero)
			{
				throw new NotSupportedException("multiple calls are not supported");
			}
			if (_owner != null)
			{
				_hHook = SetWindowsHookEx(12, _hookProc, IntPtr.Zero, Kernel32.GetCurrentThreadId());
			}
		}

		private static IntPtr MessageBoxHookProc(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode < 0)
			{
				return CallNextHookEx(_hHook, nCode, wParam, lParam);
			}
			CWPRETSTRUCT cWPRETSTRUCT = (CWPRETSTRUCT)Marshal.PtrToStructure(lParam, typeof(CWPRETSTRUCT));
			IntPtr hHook = _hHook;
			if (cWPRETSTRUCT.message == 5)
			{
				try
				{
					CenterWindow(cWPRETSTRUCT.hwnd);
				}
				finally
				{
					UnhookWindowsHookEx(_hHook);
					_hHook = IntPtr.Zero;
				}
			}
			return CallNextHookEx(hHook, nCode, wParam, lParam);
		}

		private static void CenterWindow(IntPtr hChildWnd)
		{
			Rectangle lpRect = new Rectangle(0, 0, 0, 0);
			GetWindowRect(hChildWnd, ref lpRect);
			int num = lpRect.Width - lpRect.X;
			int num2 = lpRect.Height - lpRect.Y;
			Rectangle lpRect2 = new Rectangle(0, 0, 0, 0);
			GetWindowRect(_owner.Handle, ref lpRect2);
			Point point = new Point(0, 0);
			point.X = lpRect2.X + (lpRect2.Width - lpRect2.X) / 2;
			point.Y = lpRect2.Y + (lpRect2.Height - lpRect2.Y) / 2;
			Point point2 = new Point(0, 0);
			point2.X = point.X - num / 2;
			point2.Y = point.Y - num2 / 2;
			point2.X = ((point2.X >= 0) ? point2.X : 0);
			point2.Y = ((point2.Y >= 0) ? point2.Y : 0);
			MoveWindow(hChildWnd, point2.X, point2.Y, num, num2, bRepaint: false);
		}
	}
}

#endif
