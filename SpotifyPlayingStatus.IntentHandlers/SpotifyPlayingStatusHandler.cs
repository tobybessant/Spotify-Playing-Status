using System;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using SpotifyPlayingStatus.ApiClients.Spotify;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Alexa.NET;

namespace SpotifyPlayingStatus.IntentHandlers
{
    public class SpotifyPlayingStatusHandler : IIntentHandler
    {
        public async Task<SkillResponse> Handle(SkillRequest request)
        {
            var spotify = new SpotifyApiClient(request.Session.User.UserId);

            var playing = await spotify.GetSpotifyPlayingStatus();

            if (!playing.IsPlaying)
            {
                return ResponseBuilder.Tell("Spotify is not in use at the moment.");
            }

            return ResponseBuilder.Tell($"Spotify is currently playing {playing.TrackName} by {playing.PrimaryArtist} on {playing.DeviceName}");
        }
    }
}
