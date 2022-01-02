using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

using SpotifyPlayingStatus;
using Alexa.NET.Request;
using Moq;
using Alexa.NET.Response;
using SpotifyPlayingStatus.Core;
using SpotifyPlayingStatus.Core.Extensions;
using Alexa.NET.Request.Type;
using SpotifyPlayingStatus.Core.IntentHandlers;

namespace SpotifyPlayingStatus.Tests
{
    public class FunctionTest
    {
        private readonly Function function;

        private readonly Mock<ILambdaContext> lambdaContextMock;

        public FunctionTest()
        {
            function = new Function();

            lambdaContextMock = new Mock<ILambdaContext>();
        }

        [Fact]
        public async void When_I_call_the_lambda_with_no_Spotify_credentials_the_user_should_be_prompted_with_a_LinkAccount_card()
        {
            var request = CreateSkillRequest<IntentRequest>(authenticated: false);

            var response = await function.FunctionHandler(request, lambdaContextMock.Object);

            Assert.Equal("LinkAccount", response.Response.Card?.Type);
        }

        [Fact]
        public async void When_I_call_the_lambda_with_no_Spotify_credentials_alexa_should_tell_the_user_they_need_to_authenticate()
        {
            var request = CreateSkillRequest<IntentRequest>(authenticated: false);

            var response = await function.FunctionHandler(request, lambdaContextMock.Object);

            Assert.Equal(Phrases.PleaseAuthenticate, (response.Response.OutputSpeech as PlainTextOutputSpeech).Text);
        }

        [Fact]
        public async void When_I_call_the_lambda_with_no_intent_expect_the_intro_to_play()
        {
            var request = CreateSkillRequest<LaunchRequest>(authenticated: true);

            var response = await function.FunctionHandler(request, lambdaContextMock.Object);

            Assert.Equal(Phrases.LaunchIntro, (response.Response.OutputSpeech as PlainTextOutputSpeech).Text);
        }

        [Fact]
        public async void When_I_call_the_lambda_with_intent_expect_the_handler_factory_to_be_called()
        {
            // Arrange
            var request = CreateSkillRequest<IntentRequest>(authenticated: true);

            var handlerFactoryMock = new Mock<IIntentHandlerFactory>();
            var handlerMock = new Mock<IIntentHandler>();

            handlerFactoryMock
                .Setup(h => h.GetHandlerForIntentRequest(It.IsAny<Alexa.NET.Request.Intent>()))
                .Returns(handlerMock.Object);

            request.Session.Attributes.Add("CONTAINER", new Dictionary<Type, object>
            {
                { typeof(IIntentHandlerFactory), handlerFactoryMock.Object }
            });

            // Act
            var response = await function.FunctionHandler(request, lambdaContextMock.Object);

            // Assert
            handlerFactoryMock.Verify(h => h.GetHandlerForIntentRequest(It.IsAny<Alexa.NET.Request.Intent>()), Times.Once);
        }

        [Fact]
        public async void When_I_call_the_lambda_with_intent_expect_the_apprpriate_handler_to_be_called_with_the_request_object()
        {
            // Arrange
            var request = CreateSkillRequest<IntentRequest>(authenticated: true);

            var handlerFactoryMock = new Mock<IIntentHandlerFactory>();
            var handlerMock = new Mock<IIntentHandler>();

            handlerFactoryMock
                .Setup(h => h.GetHandlerForIntentRequest(It.IsAny<Alexa.NET.Request.Intent>()))
                .Returns(handlerMock.Object);

            request.Session.Attributes.Add("CONTAINER", new Dictionary<Type, object>
            {
                { typeof(IIntentHandlerFactory), handlerFactoryMock.Object }
            });

            // Act
            var response = await function.FunctionHandler(request, lambdaContextMock.Object);

            // Assert
            handlerMock.Verify(h => h.Handle(It.IsAny<SkillRequest>()), Times.Once);
        }

        private SkillRequest CreateSkillRequest<T>(bool authenticated, T requestType = null) where T : Request, new()
        {
            return new SkillRequest
            {
                Context = new Context
                {
                    System = new AlexaSystem
                    {
                        User = new User
                        {
                            AccessToken = authenticated ? "token!" : ""
                        },
                    },
                },
                Request = requestType ?? new T(),
                Session = new Session
                {
                    Attributes = new Dictionary<string, object>()
                }
            };
        }
    }
}
