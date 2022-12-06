using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Model.Models
{
    public class ArtcleLike
    {
        public long id { get; set; }
        public long userid { get; set; }
        public long artcleid { get; set; }
        public int likeconut { get; set; }
        public DateTime createtime { get; set; }
    }
}
