using System;
using System.Threading;
using System.Threading.Tasks;

namespace Wuyu.Tool.Common
{
	public abstract class Monitor
	{
		public delegate void StartEventHandle();

		public delegate void CloseEventHandle();

		private CancellationTokenSource token;

		protected Task task;

		protected object flag;

		public bool IsOpen
		{
			get;
			private set;
		}

		public int TimeInterval
		{
			get;
			set;
		} = 30;

		public event StartEventHandle StartEvent;

		public event CloseEventHandle CloseEvent;

		protected Monitor(object flag)
		{
			this.flag = flag;
			StartEvent += delegate
			{
				LogHelp.WriteLine("Log", "任务已经启动：" + flag?.ToString());
			};
			CloseEvent += delegate
			{
				LogHelp.WriteLine("Log", "任务已经关闭：" + flag?.ToString());
			};
			task = new Task(async delegate
			{
				var i = TimeInterval;
				while (true)
				{
					if (i == TimeInterval)
					{
						try
						{
							await Handle();
						}
						catch (Exception e)
						{
							LogHelp.WriteLine("错误", e.ToString());
						}
						i = 0;
					}
					else
					{
						i++;
						try
						{
							await Task.Delay(1000, token.Token);
						}
						catch
						{
							return;
						}
					}
				}
			});
		}

		public void Start()
		{
			if (IsOpen) return;
			IsOpen = true;
			token = new CancellationTokenSource();
			task.Start();
			StartEvent?.Invoke();
		}

		public void Close()
		{
			IsOpen = false;
			token.Cancel();
			CloseEvent?.Invoke();
		}

		protected virtual async Task Handle()
		{
			LogHelp.WriteLine($"正在对{flag}监控");
		}

		public void ClearAllStartEvent()
		{
			if (StartEvent != null)
			{
				Delegate[] invocationList = this.StartEvent.GetInvocationList();
				foreach (Delegate @delegate in invocationList)
				{
					StartEvent -= @delegate as StartEventHandle;
				}
			}
		}

		public void ClearAllCloseEvent()
		{
			if (CloseEvent != null)
			{
				Delegate[] invocationList = this.CloseEvent.GetInvocationList();
				foreach (Delegate @delegate in invocationList)
				{
					CloseEvent -= @delegate as CloseEventHandle;
				}
			}
		}
	}
}
