using Alexa.NET.Request;
using SpotifyPlayingStatus.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SpotifyPlayingStatus.Tests
{
    public class SkillRequestExtensionTests
    {
        private readonly SkillRequest request;

        public SkillRequestExtensionTests()
        {
            request = new SkillRequest();
        }

        #region SpotifyLinked
        [Fact]
        public void When_the_user_context_has_a_valid_ApiAccessToken_SpotifyLinked_should_return_true()
        {
            request.Context = new Context
            {
                System = new AlexaSystem
                {
                    User = new User
                    {
                        AccessToken = "token!!"
                    }
                }
            };

            var spotifyLinked = request.LinkedSpotify();

            Assert.True(spotifyLinked);
        }

        [Fact]
        public void When_the_user_context_has_an_empty_ApiAccessToken_SpotifyLinked_should_return_true()
        {
            request.Context = new Context
            {
                System = new AlexaSystem
                {
                    User = new User
                    {
                        AccessToken = ""
                    }
                }
            };

            var spotifyLinked = request.LinkedSpotify();

            Assert.False(spotifyLinked);
        }

        [Fact]
        public void When_the_user_context_has_a_null_empty_ApiAccessToken_SpotifyLinked_should_return_true()
        {
            var spotifyLinked = request.LinkedSpotify();

            Assert.False(spotifyLinked);
        }
        #endregion

        #region RegisterServices
        [Fact]
        public void When_RegisterServices_is_called_expect_request_container_to_be_created_with_provided_creation_function()
        {
            request.Session = new Session
            {
                Attributes = new Dictionary<string, object>()
            };

            var container = new Dictionary<Type, object>
            {
                { typeof(string), "hey" }
            };

            Func<SkillRequest, Dictionary<Type, object>> createContainer = (_) => container;

            request.RegisterServices(createContainer);

            Assert.Equal(request.Session.Attributes["CONTAINER"], container);
        }

        [Fact]
        public void When_RegisterServices_is_called_expect_request_container_to_be_untouched_if_it_already_exists()
        {
            var firstContainer = new Dictionary<Type, object>
            {
                { typeof(string), "hey" }
            };


            request.Session = new Session
            {
                Attributes = new Dictionary<string, object>
                {
                    { "CONTAINER", firstContainer }
                }
            };

            var newContainer = new Dictionary<Type, object>
            {
                { typeof(string), "register services called again!" }
            };

            Func<SkillRequest, Dictionary<Type, object>> createNewContainer = (_) => newContainer;

            request.RegisterServices(createNewContainer);

            Assert.Equal(request.Session.Attributes["CONTAINER"], firstContainer);
        }
        #endregion

        #region GetInstance
        [Fact]
        public void When_GetInstance_is_called_expect_correct_object_to_be_returned()
        {
            var value = "hey";

            request.Session = new Session
            {
                Attributes = new Dictionary<string, object>
                {
                    { "CONTAINER", new Dictionary<Type, object>
                        {
                            { typeof(string), value}
                        }
                    }
                }
            };

            var instance = request.GetInstance<string>();

            Assert.Equal(instance, value);
        }

        [Fact]
        public void When_GetInstance_is_called_expect_exception_if_no_container_is_present()
        {
            request.Session = new Session
            {
                Attributes = new Dictionary<string, object>()
            };

            Action action = () => request.GetInstance<string>();

            Assert.Throws<Exception>(action);
        }

        [Fact]
        public void When_GetInstance_is_called_expect_message_if_no_container_is_present()
        {
            request.Session = new Session
            {
                Attributes = new Dictionary<string, object>()
            };

            try
            {
                request.GetInstance<string>();
            }
            catch (Exception ex)
            {
                Assert.Equal("No session DI container found.", ex.Message);
            }
        }

        [Fact]
        public void When_GetInstance_is_called_expect_exception_if_container_is_the_wrong_type()
        {
            request.Session = new Session
            {
                Attributes = new Dictionary<string, object>
                {
                    { "CONTAINER", "wrong type!" }
                }
            };

            Action action = () => request.GetInstance<string>();

            Assert.Throws<Exception>(action);
        }

        [Fact]
        public void When_GetInstance_is_called_expect_message_if_container_is_the_wrong_type()
        {
            request.Session = new Session
            {
                Attributes = new Dictionary<string, object>
                {
                    { "CONTAINER", "wrong type!" }
                }
            };

            try
            {
                request.GetInstance<string>();
            }
            catch (Exception ex)
            {
                Assert.Equal("No session DI container found.", ex.Message);
            }
        }

        [Fact]
        public void When_GetInstance_is_called_expect_exception_if_no_requested_instance_is_present()
        {
            request.Session = new Session
            {
                Attributes = new Dictionary<string, object>
                {
                    { "CONTAINER", new Dictionary<Type, object>() }
                }
            };

            Action action = () => request.GetInstance<string>();

            Assert.Throws<Exception>(action);
        }

        [Fact]
        public void When_GetInstance_is_called_expect_message_if_no_requested_instance_is_present()
        {
            request.Session = new Session
            {
                Attributes = new Dictionary<string, object>
                {
                    { "CONTAINER", new Dictionary<Type, object>() }
                }
            };

            try
            {
                request.GetInstance<string>();
            }
            catch (Exception ex)
            {
                Assert.Equal($"No requested instance for '{typeof(string)}' found.", ex.Message);
            }
        }
        #endregion GetInstance
    }
}
