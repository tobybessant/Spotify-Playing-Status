using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using SpotifyPlayingStatus.ApiClients.Spotify;
using System;
using System.Threading.Tasks;

namespace SpotifyPlayingStatus.Core.IntentHandlers
{
    public class SpotifyPlayingStatusHandler : IIntentHandler
    {
        public async Task<SkillResponse> Handle(SkillRequest request)
        {
            var spotify = new SpotifyApiClient(request.Context.System.User.AccessToken);

            try
            {
                var playerContext = await spotify.GetSpotifyPlayer();

                if (!playerContext.IsPlaying)
                {
                    return ResponseBuilder.Tell("Spotify is not in use at the moment.");
                }

                return ResponseBuilder.Tell($"Spotify is currently playing {playerContext.TrackName} by {playerContext.PrimaryArtist} on {playerContext.DeviceName}");
            }
            catch
            {
                return ResponseBuilder.Tell("Error fetching data from Spotify.");
            }
        }
    }
}
