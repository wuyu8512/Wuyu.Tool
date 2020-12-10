using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tool.Windows.Win32
{
	public class Proc
	{
		public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

		public delegate void TimerProc(IntPtr hWnd, uint uMsg, UIntPtr nIDEvent, uint dwTime);
	}
}
