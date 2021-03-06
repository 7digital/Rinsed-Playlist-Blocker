﻿using System;
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
		private bool _stopping;

		public void Start()
		{
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

			if (!_stopping)
				_timer.Start();
		}

		private static void ConfigureLogging()
		{
			XmlConfigurator.ConfigureAndWatch(new FileInfo("log4net.config"));
		}

		public void Stop()
		{
			_stopping = true;
			_timer.Stop();
		}
	}
}