using Alexa.NET.Request.Type;
using Alexa.NET.Response;

namespace SpotifyPlayingStatus.IntentHandlers
{
    public class UnknownIntentHandler : IIntentHandler
    {
        public ResponseBody Handle(IntentRequest intentRequest)
        {
            return new ResponseBody
            {
                OutputSpeech = new PlainTextOutputSpeech($"Unknown intent {intentRequest.Intent.Name}")
            };
        }
    }
}
