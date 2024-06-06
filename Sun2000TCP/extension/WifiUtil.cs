using SimpleWifi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sun2000TCP.extension
{
    public static class WifiUtil
    {
        public static bool Connect(this AccessPoint ap,  string? password = null, string? username = null)
        {
            AuthRequest authRequest = new AuthRequest(ap);
            authRequest.Username = username;
            authRequest.Password = password;
            return ap.Connect(authRequest);
        }
    }
}
