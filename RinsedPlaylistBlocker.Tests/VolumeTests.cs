using NUnit.Framework;

namespace RinsedPlaylistBlocker.Tests
{
	public class VolumeTests
	{
		[Test]
		public void Mute_and_unmute()
		{
			var volume = new Volume();
			volume.Mute();
			volume.Unmute();
		}
	}
}