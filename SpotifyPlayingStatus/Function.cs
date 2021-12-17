using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Alexa.NET.Response;
using Alexa.NET.Request;

using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializerAttribute(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace SpotifyPlayingStatus
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and returns both the upper and lower case version of the string.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public SkillResponse FunctionHandler(SkillRequest input, ILambdaContext context)
        {
            var response = new SkillResponse();

            response.Version = input.Version;

            response.Response = new ResponseBody();
            response.Response.OutputSpeech = new PlainTextOutputSpeech("How do you do!");

            return response;
        }
    }
}
