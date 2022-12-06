using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Model.Result
{
    public class PageMsg
    {
        public object data { get; set; }

        public int index { get; set; }

        public int pagesize { get; set; }

        public int totalsize { get; set; }
    }
}
