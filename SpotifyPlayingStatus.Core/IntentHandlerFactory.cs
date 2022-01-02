using System;
using Alexa.NET.Request;
using SpotifyPlayingStatus.Core.IntentHandlers;

namespace SpotifyPlayingStatus.Core
{
    public class IntentHandlerFactory
    {
        public IIntentHandler GetHandlerForIntentRequest(Alexa.NET.Request.Intent intent)
        {
            return intent.Name switch
            {
                Intent.PlayingStatus => new SpotifyPlayingStatusHandler(),

                Intent.Help => new HelpIntentHandler(),

                Intent.Cancel => new CancelIntentHandler(),

                Intent.Stop => new CancelIntentHandler(),

                Intent.Fallback => new FallbackIntentHandler(),

                _ => new UnknownIntentHandler()
            };
        }
    }
}
