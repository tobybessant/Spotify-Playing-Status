using Alexa.NET.Response;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Amazon.Lambda.Core;
using SpotifyPlayingStatus.Core;
using System.Threading.Tasks;
using Alexa.NET;
using SpotifyPlayingStatus.Core.Extensions;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SpotifyPlayingStatus
{
    public class Function
    {
        /// <summary>
        /// Alexa Skill entry function.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<SkillResponse> FunctionHandler(SkillRequest request, ILambdaContext _)
        {
            if (!request.LinkedSpotify())
            {
                return ResponseBuilder.TellWithLinkAccountCard(Phrases.PleaseAuthenticate);
            }

            request.RegisterServices(Container.Initialise);

            if (request.Request is IntentRequest intentRequest)
            {
                var factory = request.GetInstance<IIntentHandlerFactory>();
                var handler = factory.GetHandlerForIntentRequest(intentRequest.Intent);

                return await handler.Handle(request);
            }

            return ResponseBuilder.Ask(Phrases.LaunchIntro, new Reprompt(Phrases.LaunchReprompt));
        }
    }
}
