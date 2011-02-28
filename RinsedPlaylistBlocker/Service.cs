using System;
using System.Diagnostics;
using System.IO;
using System.Timers;
using log4net;
using log4net.Config;

namespace RinsedPlaylistBlocker
{
	internal class Service
	{
		private readonly ILog _log = LogManager.GetLogger(typeof(Service).Name);

		private readonly Timer _timer = new Timer
		{
			AutoReset = false,
			Interval = TimeSpan.FromSeconds(5).TotalMilliseconds
		};

		private readonly Blocker _blocker = new Blocker();

		public void Start()
		{
			Debugger.Break();

			ConfigureLogging();

			_timer.Start();
			_timer.Elapsed += (s, e) => Check();
		}

		private void Check()
		{
			try
			{
				_blocker.Check();
			}
			catch (Exception ex)
			{
				_log.Error("Oops.", ex);
			}

			_timer.Start();
		}

		private static void ConfigureLogging()
		{
			XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net.config"));
		}

		public void Stop()
		{
			_timer.Stop();
		}
	}
}