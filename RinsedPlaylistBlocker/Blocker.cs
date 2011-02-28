using System.Linq;
using log4net;
using RinsedPlaylistBlocker.LastFm;

namespace RinsedPlaylistBlocker
{
	internal class Blocker
	{
		private readonly SixMusic _sixMusic = new SixMusic();
		private readonly Volume _volume = new Volume();

		private readonly ILog _log = LogManager.GetLogger(typeof(Blocker).Name);

		public void Check()
		{
			var currentlyPlaying = _sixMusic.CurrentlyPlaying();
			var rinsedTracks = _sixMusic.RinsedTracks();
			var rinsedTrack = rinsedTracks.Contains(currentlyPlaying);

			if (!_volume.Muted && rinsedTrack)
			{
				Block(currentlyPlaying);
			}
			else if (_volume.Muted && !rinsedTrack)
			{
				Unblock();
			}
		}

		private void Block(Track currentlyPlaying)
		{
			_log.WarnFormat("Blocking: {0}.", currentlyPlaying);
			_volume.Mute();
		}

		private void Unblock()
		{
			_log.WarnFormat("Unblocking.");
			_volume.Unmute();
		}
	}
}