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

        public static void RegisterServices(this SkillRequest request, Func<SkillRequest, Dictionary<Type, object>> createContainer)
        {
            if (request.Session.Attributes.ContainsKey(containerKey))
            {
                return;
            }

            var container = createContainer(request);

            request.Session.Attributes.Add(containerKey, container);
        }

        public static T GetInstance<T>(this SkillRequest request)
        {
            if(!request.Session.Attributes.TryGetValue(containerKey, out var value))
            {
                throw new Exception("No session DI container found.");
            }

            if (value is Dictionary<Type, object> container)
            {
                if (!container.TryGetValue(typeof(T), out var instance) || instance == null)
                {
                    throw new Exception($"No requested instance for '{typeof(T)}' found.");
                }

                return (T)instance;
            }

            throw new Exception("No session DI container found.");
        }

        public static bool LinkedSpotify(this SkillRequest request)
        {
            var accessToken = request.Context?.System?.User?.AccessToken;

            return accessToken != null && accessToken != string.Empty;
        }
    }
}
