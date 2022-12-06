using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.BLL.Acconut
{
    public class AccountBLL
    {
        private static Like.Dao.Idao.IAccount _account = new Like.Dao.Account.Account();

        public static Model.User GetUserByName(string usernmae)
        {
            return _account.GetUserByName(usernmae);
        }

        public static Model.User GetUserByEamil(string eamil)
        {
            return _account.GetUserByEamil(eamil);
        }

        public static int Add(Model.User user)
        {
            return _account.Add(user);
        }
    }
}
