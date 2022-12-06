using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Common.Common
{
    public class AppsttingAPI
    {

        public static string GetSecurityKey()
        {
            string key = Appsettings.app(new string[] { "SecurityKey" });
            return key;
        }

    }
}
