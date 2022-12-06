using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.TCPServer.TCP
{
    public class TCPApi
    {
        public bool SignCheck(Model.Models.TCPPullData data,string key= "123456789")
        {
            string signdata = data.ActionName+data.ClinetId+data.Data+key;
           string sigin=  Common.Helper.MD5Helper.MD5Encrypt32(signdata);
            return data.sign.Equals(sigin);
        }


    }
}
