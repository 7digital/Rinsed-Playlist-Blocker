using System;
using NUnit.Framework;
using RinsedPlaylistBlocker.LastFm;

namespace RinsedPlaylistBlocker.Tests.LastFm
{
	public class SixMusicTests
	{
		private SixMusic _sixMusic;

		[SetUp]
		public void SetUp()
		{
			_sixMusic = new SixMusic();
		}

		[Test]
		public void Get_rinsed_tracks()
		{
			var rinsedTracks = _sixMusic.RinsedTracks();

			foreach (var track in rinsedTracks)
			{
				Assert.That(track.PlayCount, Is.GreaterThan(10));

				Console.WriteLine(string.Format("Artist: {0}, Name: {1}, Play count: {2}", 
					track.Artist, track.Name, track.PlayCount));
			}
		}

		[Test]
		public void Rinsed_tracks_are_cached()
		{
			var rinsedTracks = _sixMusic.RinsedTracks();
			var rinsedTracks2 = _sixMusic.RinsedTracks();

			Assert.That(rinsedTracks, Is.SameAs(rinsedTracks2));
		}

		[Test]
		public void Currently_playing()
		{
			var track = _sixMusic.CurrentlyPlaying();

			Assert.That(track, Is.Not.Null);
			Console.WriteLine(string.Format("Artist: {0}, Name: {1}", track.Artist, track.Name));
		}
	}
}