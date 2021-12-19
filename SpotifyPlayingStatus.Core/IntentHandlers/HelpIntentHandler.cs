using Alexa.NET;
using Alexa.NET.Request;
using Alexa.NET.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyPlayingStatus.Core.IntentHandlers
{
    public class HelpIntentHandler : IIntentHandler
    {
        public Task<SkillResponse> Handle(SkillRequest request)
        {
            var helpResponse = ResponseBuilder.Tell(Phrases.Help);
            helpResponse.Response.ShouldEndSession = false;

            return Task.FromResult(helpResponse);
        }
    }
}
