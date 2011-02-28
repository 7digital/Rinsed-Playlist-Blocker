using System;
using log4net;

namespace RinsedPlaylistBlocker
{
	public class Volume
	{
		private readonly ILog _log = LogManager.GetLogger(typeof(Volume).Name);

		private ushort _leftVol;
		private ushort _rightVol;

		public bool Muted { get; private set; }

		public void Mute()
		{
			_leftVol = 0;
			_rightVol = 0;

			// First store the current volume settings
			var returnValue = GetVolume(ref _leftVol, ref _rightVol);

			if (returnValue != NativeMethods.MMSYSERR_NOERROR)
				throw new Exception(string.Format("Failed to retrieve current volume (Return value: {0}).", returnValue));

			SetVolume(0, 0);
			Muted = true;
		}

		public void Unmute()
		{
			if (_leftVol > 0 && _rightVol > 0)
				SetVolume(_leftVol, _rightVol);

			Muted = false;
		}

		private int GetVolume(ref ushort volLeft, ref ushort volRight)
		{
			uint vol;

			// depending on the sound device type call one of the PInvoke functions
			var hwo = IntPtr.Zero;
			var result = NativeMethods.waveOutGetVolume(hwo, out vol);

			if (result != NativeMethods.MMSYSERR_NOERROR)
				return result;

			// extract the two volume settings from the single vol value
			volLeft = (ushort)(vol & 0x0000ffff);
			volRight = (ushort)(vol >> 16);

			_log.InfoFormat("Current volume: L - {0}, R - {1}.", volLeft, volRight);

			return NativeMethods.MMSYSERR_NOERROR;
		}

		private void SetVolume(ushort volLeft, ushort volRight)
		{
			_log.InfoFormat("Setting volume: L - {0}, R - {1}.", volLeft, volRight);

			var vol = ((uint)volLeft & 0x0000ffff) | ((uint)volRight << 16);
			NativeMethods.waveOutSetVolume(IntPtr.Zero, vol);
		}
	}
}