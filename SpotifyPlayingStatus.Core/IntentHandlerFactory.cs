using System;
using Alexa.NET.Request.Type;
using SpotifyPlayingStatus.IntentHandlers;

namespace SpotifyPlayingStatus.Core
{
    public class IntentHandlerFactory
    {
        public static IIntentHandler GetHandlerForIntentRequest(IntentRequest intentRequest)
        {
            return intentRequest.Intent.Name switch
            {
                Intent.PlayingStatus => new SpotifyPlayingStatusHandler(),

                _ => new UnknownIntentHandler()
            };
        }
    }
}
