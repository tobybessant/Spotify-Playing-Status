using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyPlayingStatus.ApiClients.Spotify
{
    public class PlayingItem
    {
        public string Name { get; set; }
    }

    public class SpotifyApiClient
    {
        private readonly ISpotifyClient spotifyClient;

        public SpotifyApiClient(string spotifyClientId)
        {
            spotifyClient = new SpotifyClient(spotifyClientId);
        }

        public async Task<PlayerContext> GetSpotifyPlayer()
        {
            var response = await spotifyClient.Player.GetCurrentPlayback();

            if (response == null || !response.IsPlaying)
            {
                return new PlayerContext
                {
                    IsPlaying = false
                };
            }

            var item = response.Item as FullTrack;

            return new PlayerContext
            {
                DeviceName = response.Device.Name,
                TrackName = item.Name,
                IsPlaying = response.IsPlaying,
                PrimaryArtist = item.Artists?.FirstOrDefault()?.Name,
            };
        }
    }
}
