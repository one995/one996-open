using Like.Model.Emun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Like.Model;
namespace Like.Dao
{
    public class SLogsDao : DbDao
    {

        /// <summary>
        ///
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="FlagMsg"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        public int AddLogs(string msg,string FlagMsg,string user ,LogType log = LogType.Info )
        {
            return RWDb.Insertable<Model.Models.SLogs>(new Model.Models.SLogs() { CreateUser=user, FlagMsg=FlagMsg,LogsMsg=msg,logType=log,CreateTime=DateTime.Parse( DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))}).ExecuteCommand();
        } 
    }
}
