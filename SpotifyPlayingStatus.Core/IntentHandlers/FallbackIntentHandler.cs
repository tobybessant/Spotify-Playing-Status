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
    public class FallbackIntentHandler : IIntentHandler
    {
        public Task<SkillResponse> Handle(SkillRequest request)
        {
            return Task.FromResult(ResponseBuilder.Tell("I'm not sure what you mean, sorry."));
        }
    }
}
