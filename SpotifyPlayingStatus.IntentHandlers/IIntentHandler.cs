using Alexa.NET.Request;
using Alexa.NET.Response;
using System.Threading.Tasks;

namespace SpotifyPlayingStatus.IntentHandlers
{
    public interface IIntentHandler
    {
        public Task<SkillResponse> Handle(SkillRequest intentRequest);
    }
}
