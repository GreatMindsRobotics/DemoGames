using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pong
{
    public static class WebServiceConnection
    {
        public static WebService.GMRPongServiceClient Client = new WebService.GMRPongServiceClient();

        public static string GameName = "";
    }
}
