using Alexa.NET.Response;
using Alexa.NET.Request;
using Alexa.NET.Request.Type;
using Amazon.Lambda.Core;
using SpotifyPlayingStatus.Core;

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
        public SkillResponse FunctionHandler(SkillRequest request, ILambdaContext _)
        {
            if (request.Request is IntentRequest intentRequest)
            {
                var handler = IntentHandlerFactory.GetHandlerForIntentRequest(intentRequest);

                var body = handler.Handle(intentRequest);

                return Response(request, body);
            }

            return Response(request, new ResponseBody
            {
                OutputSpeech = new PlainTextOutputSpeech("How do you do?")
            });
        }

        private SkillResponse Response(SkillRequest request, ResponseBody body = null)
        {
            return new SkillResponse
            {
                Version = request.Version,
                Response = body
            };
        }
    }
}
