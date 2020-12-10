using System;

namespace Tool
{
	public static class LogHelp
	{
		public delegate void BoilerLogHandler(string type, string message);

		public static event BoilerLogHandler BoilerEventLog;

		static LogHelp()
		{
			BoilerEventLog += Log;
		}

		public static void WriteLine(string type, string message)
		{
			BoilerEventLog?.Invoke(type, message);
		}

		public static void WriteLine(string message)
		{
			BoilerEventLog?.Invoke("Log", message);
		}

		private static void Log(string type, string message)
		{
			Console.WriteLine(message);
		}
	}
}
