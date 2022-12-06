using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Like.Dao.Idao;
using Like.Model;
using Like.Model.Models;

namespace Like.Dao.Account
{
    public class Account : DbDao, IAccount
    {
        public  List<User> Get()
        {
            return RWDb.Queryable<User>()?.ToList();
        }
        public Like.Model.User GetUserByName(string username)
        {
            return RWDb.Queryable<User>().Where(s=>s.UserName==username)?.First();
        }

        public  User GetUserByEamil(string eamil)
        {
            return RWDb.Queryable<User>().Where(s => s.Eamil == eamil).First();
        }

        public int Add(Model.User user)
        {
            return RWDb.Insertable<Model.User>(user).ExecuteCommand();
        }
    }
}
