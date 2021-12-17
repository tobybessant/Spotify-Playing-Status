using System;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;

namespace SpotifyPlayingStatus.IntentHandlers
{
    public class SpotifyPlayingStatusHandler : IIntentHandler
    {
        public ResponseBody Handle(IntentRequest intentRequest)
        {
            return new ResponseBody
            {
                OutputSpeech = new PlainTextOutputSpeech("How do you do!")
            };
        }
    }
}
