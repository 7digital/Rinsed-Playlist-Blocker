using System;

namespace RinsedPlaylistBlocker.LastFm
{
	public class Track : IEquatable<Track>
	{
		public string Artist { get; set; }
		public string Name { get; set; }
		public int PlayCount { get; set; }

		public bool Equals(Track other)
		{
			if(ReferenceEquals(null, other))
			{
				return false;
			}
			if(ReferenceEquals(this, other))
			{
				return true;
			}
			return Equals(other.Artist, Artist) && Equals(other.Name, Name);
		}

		public override bool Equals(object obj)
		{
			if(ReferenceEquals(null, obj))
			{
				return false;
			}
			if(ReferenceEquals(this, obj))
			{
				return true;
			}
			if(obj.GetType() != typeof(Track))
			{
				return false;
			}
			return Equals((Track) obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return ((Artist != null ?Artist.GetHashCode() : 0) * 397) ^ (Name != null ?Name.GetHashCode() : 0);
			}
		}

		public static bool operator ==(Track left, Track right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Track left, Track right)
		{
			return !Equals(left, right);
		}
	}
}