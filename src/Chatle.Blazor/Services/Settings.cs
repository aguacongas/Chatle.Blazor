using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chatle.Blazor.Services
{
    public class Settings
    {
        public string ApiBaseUrl { get; set; } = "http://localhost:5000";

        public string UserApi { get; set; } = "/api/users";

        public string ConvApi { get; set; } = "/api/chat/conv";

        public string ChatApi { get; set; } = "/api/chat";

        public string ChatHub { get; set; } = "/chat";

        public string AccountApi { get; set; } = "/account";

    }
}
