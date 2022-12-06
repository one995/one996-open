using Like.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.BLL.UserAuther
{
  public  class UserAuther
    {
        public static sysUserInfo GetSysUser(string uid)
        {
            return new sysUserInfo();
        }

        public static string GetUserRole(string uLoginName, string uLoginPWD)
        {
            return "2341232353634545";
        }
    }
}
