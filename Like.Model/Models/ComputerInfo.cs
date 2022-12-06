using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
namespace Like.Model.Models
{
    public class ComputerInfo
    {
        [SugarColumn(IsIdentity =true,IsPrimaryKey =true)]
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Mac { get; set; }
        public string CPUTemp { get; set; }
        public string CPUTotal { get; set; }

        public string CPUFan { get; set; }
        public string MainboardFan { get; set; }
        public string MainboardTemp { get; set; }
        public string HDDTemp { get; set; }
        public string HDDLoad { get; set; }
        public string RAMLoad { get; set; }
        public string RAMData { get; set; }

        public string GPULoad { get; set; }
        public string GPUData { get; set; }

        public string GPUTemp { get; set; }

        public DateTime ?CreaTime { get; set; }=DateTime.Now;
    }
}
