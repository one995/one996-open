using Like.Model;
using Like.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Dao.Idao
{
  public  interface  IAccount
    {
        List<User> Get();

        User GetUserByName(string username);


         User GetUserByEamil(string eamil);

        int Add(User user);
    }
}
