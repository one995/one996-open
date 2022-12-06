using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Model.Models
{
    public class Forwarding
    {
        public long Id { get; set; }
        public long Userid { get; set; }
        public long Artcleid { get; set; }
        public long Forwardingconut { get; set; }
        public DateTime Createtime { get; set; }
    }
}
