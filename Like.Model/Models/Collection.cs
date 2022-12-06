using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Model.Models
{

    public class Collection
    {
        public long Id { get; set; }
        public long Userid { get; set; }
        public long Artcleid { get; set; }
        public long Collectionconut { get; set; }
        public DateTime Createtime { get; set; }
        public int Touserid { get; set; }
    }
}
