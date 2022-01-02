using Moq;
using SpotifyAPI.Web;
using SpotifyPlayingStatus.ApiClients.Spotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SpotifyPlayingStatus.Tests
{
    public class SpotifyApiServiceTests
    {
        private readonly SpotifyApiService spotifyApiService;

        private readonly Mock<ISpotifyClient> spotifyClientMock; 

        public SpotifyApiServiceTests()
        {
            spotifyClientMock = new Mock<ISpotifyClient>();

            spotifyApiService = new SpotifyApiService(spotifyClientMock.Object);
        }

        [Fact]
        public async void When_I_call_GetSpotifyPlayer_the_currently_playing_details_should_be_fetched_from_the_spotify_client()
        {
            var currentlyPlaying = new CurrentlyPlayingContext
            {
                Device = new Device { Name = "Toby iPhone" },
                IsPlaying = true,
                Item = new FullTrack
                {
                    Name = "Dreams - 2004 Remaster",
                    Artists = new List<SimpleArtist> { new SimpleArtist { Name = "Fleetwood Mac" } },
                }
            };

            spotifyClientMock.Setup(s => s.Player.GetCurrentPlayback()).ReturnsAsync(currentlyPlaying);

            await spotifyApiService.GetSpotifyPlayer();

            spotifyClientMock.Verify(s => s.Player, Times.Once);
        }

        [Fact]
        public async void When_I_call_GetSpotifyPlayer_the_currently_playing_details_should_be_returned()
        {
            var deviceName = "Toby iPhone";
            var isPlaying = true;
            var trackName = "Dreams - 2004 Remaster";
            var artistName = "Fleetwood Mac";

            var currentlyPlaying = new CurrentlyPlayingContext
            {
                Device = new Device { Name = deviceName },
                IsPlaying = isPlaying,
                Item = new FullTrack
                {
                    Name = trackName,
                    Artists = new List<SimpleArtist> { new SimpleArtist { Name = artistName } },
                }
            };

            spotifyClientMock.Setup(s => s.Player.GetCurrentPlayback()).ReturnsAsync(currentlyPlaying);

            var playing = await spotifyApiService.GetSpotifyPlayer();

            Assert.Equal(playing, new PlayerContext
            {
                DeviceName = deviceName,
                IsPlaying = isPlaying,
                TrackName = trackName,
                PrimaryArtist = artistName
            });
        }

        [Fact]
        public async void When_I_call_GetSpotifyPlayer_only_the_first_artist_should_be_returned()
        {
            var deviceName = "Toby iPhone";
            var isPlaying = true;
            var trackName = "Life Of The Party (with Andre 3000)";

            var artistName = "Kanye West";
            var artistName2 = "Andre 3000";

            var currentlyPlaying = new CurrentlyPlayingContext
            {
                Device = new Device { Name = deviceName },
                IsPlaying = isPlaying,
                Item = new FullTrack
                {
                    Name = trackName,
                    Artists = new List<SimpleArtist>
                    {
                        new SimpleArtist { Name = artistName },
                        new SimpleArtist { Name = artistName2 }
                    },
                }
            };

            spotifyClientMock.Setup(s => s.Player.GetCurrentPlayback()).ReturnsAsync(currentlyPlaying);

            var playing = await spotifyApiService.GetSpotifyPlayer();

            Assert.Equal(playing, new PlayerContext
            {
                DeviceName = deviceName,
                IsPlaying = isPlaying,
                TrackName = trackName,
                PrimaryArtist = artistName
            });
        }

        [Fact]
        public async void When_I_call_GetSpotifyPlayer_it_should_return_not_busy_if_Spotify_returns_null()
        {
            spotifyClientMock.Setup(s => s.Player.GetCurrentPlayback()).Returns(Task.FromResult<CurrentlyPlayingContext>(null));

            var playing = await spotifyApiService.GetSpotifyPlayer();

            Assert.Equal(playing, new PlayerContext
            {
                IsPlaying = false
            });
        }
    }
}
