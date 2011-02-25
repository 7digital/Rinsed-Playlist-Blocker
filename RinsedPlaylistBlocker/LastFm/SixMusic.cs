using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Caching;
using System.Xml.Linq;

namespace RinsedPlaylistBlocker.LastFm
{
	public class SixMusic
	{
		private readonly ObjectCache _cache = new MemoryCache("SixMusic");

		public IEnumerable<Track> RinsedTracks()
		{
			const string key = "RinsedTracks";

			if (!_cache.Contains(key))
			{
				_cache.Add(new CacheItem(key, WeeklyTrackChart().Where(t => t.PlayCount > 10).ToList()), 
					new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.AddHours(12) });
			}

			return (IEnumerable<Track>) _cache[key];
		}

		private IEnumerable<Track> WeeklyTrackChart()
		{
			var request = WebRequest.Create("http://ws.audioscrobbler.com/2.0/user/bbc6music/weeklytrackchart.xml");
			var response = request.GetResponse();

			using (var reader = new StreamReader(response.GetResponseStream()))
			{
				var xml = reader.ReadToEnd();
				var document = XDocument.Parse(xml);
				var root = document.Element("weeklytrackchart");
				foreach (var trackNode in root.Elements("track"))
				{
					yield return new Track
					{
						Artist = trackNode.Element("artist").Value,
						Name = trackNode.Element("name").Value,
						PlayCount = Convert.ToInt32(trackNode.Element("playcount").Value)
					};
				}
			}
		}

		public Track CurrentlyPlaying()
		{
			var request = WebRequest.Create("http://ws.audioscrobbler.com/2.0/user/bbc6music/recenttracks.rss");
			var response = request.GetResponse();

			using (var reader = new StreamReader(response.GetResponseStream()))
			{
				var xml = reader.ReadToEnd();
				var document = XDocument.Parse(xml);
				var root = document.Element("rss").Element("channel");
				var trackNode = root.Element("item");
				var title = trackNode.Element("title").Value;
				var split = title.Split('–');
				return new Track
				{
					Artist = split[0].Trim(),
					Name = split[1].Trim(),
				};
			}
		}
	}
}