using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.TCPServer.TCP
{
    public class TCPResponse
    {
        public int code { set;get; }

        public string message { set;get; }

        public string data { get; set; }
    }


}
