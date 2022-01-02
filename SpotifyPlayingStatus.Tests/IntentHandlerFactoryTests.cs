using Alexa.NET.Request;
using SpotifyPlayingStatus.Core;
using SpotifyPlayingStatus.Core.IntentHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SpotifyPlayingStatus.Tests
{
    public class IntentHandlerFactoryTests
    {
        [Theory]
        [InlineData("PlayingStatus", typeof(SpotifyPlayingStatusHandler))]
        [InlineData("AMAZON.HelpIntent", typeof(HelpIntentHandler))]
        [InlineData("AMAZON.StopIntent", typeof(CancelIntentHandler))]
        [InlineData("AMAZON.CancelIntent", typeof(CancelIntentHandler))]
        [InlineData("AMAZON.FallbackIntent", typeof(FallbackIntentHandler))]
        [InlineData("unknown!!", typeof(UnknownIntentHandler))]
        public void When_I_call_GetHandlerForIntent_I_expect_the_correct_handler_to_be_returned(string intent, Type expectedHandler)
        {
            var alexaIntent = new Alexa.NET.Request.Intent { Name = intent };

            var handler = new IntentHandlerFactory().GetHandlerForIntentRequest(alexaIntent);

            Assert.Equal(handler.GetType(), expectedHandler);
        }
    }
}
