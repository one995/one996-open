using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Model.Models
{
    public class TCPPullData
    {
        public string ClinetId { get; set; }

        public string Data { get; set; }

        public string ActionName { get; set; }

        public string sign { set; get; }

        public string ToUser { get; set; }
        public string FromUser { get; set; }
    }
}
