using Alexa.NET.Request;
using SpotifyAPI.Web;
using SpotifyPlayingStatus.ApiClients.Spotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyPlayingStatus.Core
{
    public static class Container
    {
        public static Dictionary<Type, object> Initialise(SkillRequest request)
        {
            var container = new Dictionary<Type, object>();

            container.Add(
                typeof(SpotifyApiService),
                new SpotifyApiService(new SpotifyClient(request.Context.System.User.AccessToken)));

            container.Add(typeof(IntentHandlerFactory), new IntentHandlerFactory());

            return container;
        } 
    }
}
