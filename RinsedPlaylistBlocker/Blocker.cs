using System.Linq;
using RinsedPlaylistBlocker.LastFm;

namespace RinsedPlaylistBlocker
{
	internal class Blocker
	{
		private readonly SixMusic _sixMusic = new SixMusic();
		private bool _blocking;
		private readonly Volume _volume = new Volume();

		public void Check()
		{
			var currentlyPlaying = _sixMusic.CurrentlyPlaying();
			var rinsedTracks = _sixMusic.RinsedTracks();
			var rinsedTrack = rinsedTracks.Contains(currentlyPlaying);

			if (!_blocking && rinsedTrack)
			{
				Block();
			}
			else if (_blocking && !rinsedTrack)
			{
				Unblock();
			}
		}

		private void Block()
		{
			_volume.Mute();
			_blocking = true;
		}

		private void Unblock()
		{
			_volume.Unmute();
			_blocking = false;
		}
	}
}