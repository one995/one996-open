using Like.Model.Emun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
namespace Like.Model.Models
{
    public class SLogs
    {
        [SugarColumn(IsIdentity =true,IsPrimaryKey =true)]
        public int id { get; set; }
        [SugarColumn(Length =5000)]
        public string LogsMsg { get; set; }

        public DateTime ?CreateTime { get; set; }

        public string? CreateUser { get; set; }

        public LogType logType { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        [SugarColumn(Length =200)]
        public string FlagMsg { get; set; }
    }
}
