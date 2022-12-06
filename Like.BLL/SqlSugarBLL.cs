using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
namespace Like.BLL
{
   public class SqlSugarBLL<T> where T : class, new()
    {
        public static Dao.BaseDao<T> baseDao = new Dao.BaseDao<T>();
  

    }
}
