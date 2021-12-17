using Alexa.NET.Request.Type;
using Alexa.NET.Response;

namespace SpotifyPlayingStatus.IntentHandlers
{
    public interface IIntentHandler
    {
        public ResponseBody Handle(IntentRequest intentRequest);
    }
}
