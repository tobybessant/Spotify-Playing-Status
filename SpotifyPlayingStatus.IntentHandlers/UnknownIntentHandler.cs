using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;
using System.Threading.Tasks;

namespace SpotifyPlayingStatus.IntentHandlers
{
    public class UnknownIntentHandler : IIntentHandler
    {
        public Task<SkillResponse> Handle(SkillRequest request)
        {
            var intentRequest = request.Request as IntentRequest;

            return Task.FromResult(ResponseBuilder.Tell($"Unknown intent {intentRequest?.Intent.Name ?? string.Empty}"));
        }
    }
}
