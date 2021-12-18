using System;
using Alexa.NET.Request;
using SpotifyPlayingStatus.IntentHandlers;

namespace SpotifyPlayingStatus.Core
{
    public class IntentHandlerFactory
    {
        public static IIntentHandler GetHandlerForIntentRequest(Alexa.NET.Request.Intent intent)
        {
            return intent.Name switch
            {
                Intent.PlayingStatus => new SpotifyPlayingStatusHandler(),

                _ => new UnknownIntentHandler()
            };
        }
    }
}
