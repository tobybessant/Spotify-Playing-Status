using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.Request.Type;
using Alexa.NET.Response;

namespace SpotifyPlayingStatus.Interfaces
{
    public interface IIntentHandler
    {
        public ResponseBody Handle(IntentRequest intentRequest);
    }
}
