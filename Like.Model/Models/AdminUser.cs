using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Model.Models
{
    public class AdminUser
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//数据库是自增才配自增 
        public int Id { get; set; }
        public string UserName { get; set; }

        public string PersonID { get; set; }

        public string Passwordmd5 { get; set; }

        public int? Role { get; set; }
        public int isdelete { get; set; }

        public DateTime? CreateTime { get; set; }

        public string CreateUser { get; set; }
    }
}
