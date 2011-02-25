using System;
using System.Timers;

namespace RinsedPlaylistBlocker
{
	internal class Service
	{
		private readonly Timer _timer = new Timer
		{
			AutoReset = false,
			Interval = TimeSpan.FromSeconds(5).TotalMilliseconds
		};

		private readonly Blocker _blocker = new Blocker();

		public void Start()
		{
			_timer.Start();
			_timer.Elapsed += (s, e) =>
			{
				try
				{
					_blocker.Check();
				}
				catch (Exception) { }

				_timer.Start();
			};
		}

		public void Stop()
		{
			_timer.Stop();
		}
	}
}