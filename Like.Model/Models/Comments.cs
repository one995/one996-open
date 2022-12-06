using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Model.Models
{
    [SugarTable("comments")]//当和数据库名称不一样可以设置表别名 指定表明
    public class Comments
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//数据库是自增才配自增 
        public int Id { get; set; }
        public string? Commentsmsg { get; set; }
        public DateTime? Createtime { get; set; }
        public string? Userid { get; set; }
        public string? Artcleid { get; set; }
        public int? Isdelete { get; set; }

        public string? Localtion { get; set; }
    }
}
