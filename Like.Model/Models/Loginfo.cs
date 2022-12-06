using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Model.Models
{
    public class Loginfo
    {
        public long Id { get; set; }
        public string Msg { get; set; }
        public int Level { get; set; }
        public DateTime Cratetime { get; set; }
        public string Localtion { get; set; }
        public string Loginuser { get; set; }
    }
}
