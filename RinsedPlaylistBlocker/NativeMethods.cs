using System;
using System.Runtime.InteropServices;

namespace RinsedPlaylistBlocker
{
	public static class NativeMethods
	{
		public const int MMSYSERR_NOERROR = 0;

		[DllImport("winmm.dll")]
		public static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

		[DllImport("winmm.dll")]
		public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);
	}
}