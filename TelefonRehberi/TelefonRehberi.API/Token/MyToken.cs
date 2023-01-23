using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TelefonRehberi.API.token
{
    public class MyToken
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public DateTime Expiration { get; set; }
    }
}
