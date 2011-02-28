using System;
using System.Runtime.InteropServices;

namespace RinsedPlaylistBlocker
{
	public static class NativeMethods
	{
		public const int MMSYSERR_NOERROR = 0;
		public const int MMSYSERR_BADDEVICEID = 2;
		public const int MMSYSERR_INVALHANDLE = 5;
		public const int MMSYSERR_NODRIVER = 6;
		public const int MMSYSERR_NOMEM = 7;
		public const int MMSYSERR_NOTSUPPORTED = 8;
		public const int MMSYSERR_INVALPARAM = 11;

		[DllImport("winmm.dll")]
		public static extern int waveOutGetVolume(IntPtr hwo, out uint dwVolume);

		[DllImport("winmm.dll")]
		public static extern int waveOutSetVolume(IntPtr hwo, uint dwVolume);
	}
}