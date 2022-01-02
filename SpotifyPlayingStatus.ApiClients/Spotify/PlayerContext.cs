using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyPlayingStatus.ApiClients.Spotify
{
    public class PlayerContext : IEquatable<PlayerContext>
    {
        public string DeviceName { get; set; }

        public string TrackName { get; set; }

        public bool IsPlaying { get; set; }

        public string PrimaryArtist { get; set; }

        public bool Equals(PlayerContext other)
        {
            return DeviceName == other.DeviceName
                && TrackName == other.TrackName
                && PrimaryArtist == other.PrimaryArtist
                && IsPlaying == other.IsPlaying;
        }
    }
}
