using Like.Model.Emun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.BLL
{
    public class SLogs
    {
        private static Dao.SLogsDao sLogs = new Dao.SLogsDao();
        public static int AddSLogs(string msg, string FlagMsg, string user, LogType log = LogType.Info)
        {
          return  sLogs.AddLogs(msg,FlagMsg,user,log);
        }
    }
}
