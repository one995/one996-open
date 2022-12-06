using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Model.Models
{
    public class ArtcleRecord
    {
        public int  ID { get; set; }
        public DateTime CreateTime { get; set; }
        public string ArticleID { get; set; }
        public string Userid { get; set; }
    }
}
