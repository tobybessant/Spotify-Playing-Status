using Alexa.NET.Request;
using SpotifyAPI.Web;
using SpotifyPlayingStatus.ApiClients.Spotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyPlayingStatus.Core.Extensions
{
    public static class SkillRequestExtensions
    {
        private const string containerKey = "CONTAINER";

        public static void RegisterServices(this SkillRequest request)
        {

            if (request.Session.Attributes.ContainsKey(containerKey))
            {
                return;
            }

            var container = new Dictionary<Type, Func<object>>();

            container.Add(
                typeof(SpotifyApiService), () => new SpotifyApiService(new SpotifyClient(request.Context.System.User.AccessToken)));

            request.Session.Attributes.Add(containerKey, container);
        }

        public static T GetInstance<T>(this SkillRequest request)
        {
            if(!request.Session.Attributes.TryGetValue(containerKey, out var value))
            {
                throw new Exception("No session DI container found.");
            }

            if (value is Dictionary<Type, Func<object>> container)
            {
                container.TryGetValue(typeof(T), out var instanceBuilder);

                return (T)instanceBuilder();
            }

            throw new Exception("No session DI container found.");
        }

        public static bool LinkedSpotify(this SkillRequest request)
        {
            var accessToken = request.Context.System.User.AccessToken;

            return accessToken != null && accessToken != string.Empty;
        }
    }
}
