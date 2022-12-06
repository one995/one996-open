using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
namespace Like.Model.Models
{
    public class ComputerUserInfo
    {
        [SugarColumn(IsIdentity =true,IsPrimaryKey =true)]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public DateTime Creatime { get; set; }  


    }
}
