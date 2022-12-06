using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Model
{
    public class TCPSendMsg
    {
        public string ToUser { get; set;  }

        public string FromUser { get; set; }    

        public string data { get; set; }

        public string ActionName { get; set; }
        public string ClinetId { get; set; }
    }
}
