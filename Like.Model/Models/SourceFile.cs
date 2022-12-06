using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Model.Models
{
    [SugarTable("sourcefile")]//当和数据库名称不一样可以设置表别名 指定表明
    public class SourceFile
    {

        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int id { get; set; }
        public String name { get; set; }
        public String fifleurl { get; set; }
        public DateTime creatime { get; set; }
        public String md5 { get; set; }
        public int dloadcount { get; set; }

        public String userid { get; set; }
    }
}
