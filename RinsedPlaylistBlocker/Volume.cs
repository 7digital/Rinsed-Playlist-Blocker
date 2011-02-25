using System;

namespace RinsedPlaylistBlocker
{
	public class Volume
	{
		private ushort _leftVol;
		private ushort _rightVol;

		public void Mute()
		{
			_leftVol = 0;
			_rightVol = 0;

			// First store the current volume settings
			var returnValue = GetVolume(ref _leftVol, ref _rightVol);

			if (returnValue == NativeMethods.MMSYSERR_NOERROR)
			{
				SetVolume(0, 0);
			}
		}

		public void Unmute()
		{
			if (_leftVol <= 0 && _rightVol <= 0)
				return;

			SetVolume(_leftVol, _rightVol);
		}

		private int GetVolume(ref ushort volLeft, ref ushort volRight)
		{
			uint vol = 0;
			int result = 9999;

			// depending on the sound device type call one of the PInvoke functions
			var hwo = IntPtr.Zero;
					result = NativeMethods.waveOutGetVolume(hwo, out vol);

			if (result != NativeMethods.MMSYSERR_NOERROR)
				return result;

			// extract the two volume settings from the single vol value
			volLeft = (ushort)(vol & 0x0000ffff);
			volRight = (ushort)(vol >> 16);

			return NativeMethods.MMSYSERR_NOERROR;
		}

		private void SetVolume(ushort volLeft, ushort volRight)
		{
			var vol = ((uint)volLeft & 0x0000ffff) | ((uint)volRight << 16);
			NativeMethods.waveOutSetVolume(IntPtr.Zero, vol);
		}
	}
}