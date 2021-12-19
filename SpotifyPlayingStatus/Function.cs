using Alexa.NET.Response;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Amazon.Lambda.Core;
using SpotifyPlayingStatus.Core;
using System.Threading.Tasks;
using Alexa.NET;

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
            var accessToken = request.Context.System.User.AccessToken;

            if (accessToken == null || accessToken == string.Empty)
            {
                return ResponseBuilder.TellWithLinkAccountCard(Phrases.PleaseAuthenticate);
            }

            if (request.Request is IntentRequest intentRequest)
            {
                var handler = IntentHandlerFactory.GetHandlerForIntentRequest(intentRequest.Intent);

                return await handler.Handle(request);
            }

            var launchResponse = ResponseBuilder.Tell(Phrases.LaunchIntro);
            launchResponse.Response.ShouldEndSession = false;

            return launchResponse;
        }
    }
}
