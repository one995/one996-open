using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Model.Models
{
    public class ArtcleHot
    {
        public int ID { get; set; }

        public int userid { get; set; }
        public int artcleid { get; set; }
        public int commentsconut { get; set; }
        public int likeconut { get; set; }
        public int forwarding { get; set; }
        public int collection { get; set; }

    }
}
