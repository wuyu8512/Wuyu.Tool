using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using static Wuyu.Tool.Windows.Win32.Proc;

namespace Wuyu.Tool.Windows.Win32
{
	public static class User32
	{
		public static int MessageBoxW(int hwnd, string neirong, string title, int nom) => User.MessageBoxW(hwnd, neirong, title, nom);
		public static bool GetWindowRect(IntPtr hWnd, ref Rectangle lpRect) => User.GetWindowRect(hWnd, ref lpRect);
		public static int MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint)
			=> User.MoveWindow(hWnd, X, Y, nWidth, nHeight, bRepaint);
		public static UIntPtr SetTimer(IntPtr hWnd, UIntPtr nIDEvent, uint uElapse, TimerProc lpTimerFunc)
			=> User.SetTimer(hWnd, nIDEvent, uElapse, lpTimerFunc);
		public static IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam) => User.SendMessage(hWnd, Msg, wParam, lParam);
		public static IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, uint threadId)
			=> User.SetWindowsHookEx(idHook, lpfn, hInstance, threadId);
		public static int UnhookWindowsHookEx(IntPtr idHook) => User.UnhookWindowsHookEx(idHook);
		public static IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam) => User.CallNextHookEx(idHook, nCode, wParam, lParam);
		public static int GetWindowTextLength(IntPtr hWnd) => User.GetWindowTextLength(hWnd);
		public static int GetWindowText(IntPtr hWnd, StringBuilder text, int maxLength) => User.GetWindowText(hWnd, text, maxLength);
		public static int EndDialog(IntPtr hDlg, IntPtr nResult) => User.EndDialog(hDlg, nResult);


		private class User
		{
			[DllImport("user32.dll", CharSet = CharSet.Unicode)]
			public static extern int MessageBoxW(int hwnd, string neirong, string title, int nom);

			[DllImport("user32.dll")]
			public static extern bool GetWindowRect(IntPtr hWnd, ref Rectangle lpRect);

			[DllImport("user32.dll")]
			public static extern int MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

			[DllImport("User32.dll")]
			public static extern UIntPtr SetTimer(IntPtr hWnd, UIntPtr nIDEvent, uint uElapse, TimerProc lpTimerFunc);

			[DllImport("User32.dll")]
			public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

			[DllImport("user32.dll")]
			public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, uint threadId);

			[DllImport("user32.dll")]
			public static extern int UnhookWindowsHookEx(IntPtr idHook);

			[DllImport("user32.dll")]
			public static extern IntPtr CallNextHookEx(IntPtr idHook, int nCode, IntPtr wParam, IntPtr lParam);

			[DllImport("user32.dll")]
			public static extern int GetWindowTextLength(IntPtr hWnd);

			[DllImport("user32.dll", CharSet = CharSet.Unicode)]
			public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int maxLength);

			[DllImport("user32.dll")]
			public static extern int EndDialog(IntPtr hDlg, IntPtr nResult);
		}
	}
}
