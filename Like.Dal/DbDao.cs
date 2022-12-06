using Like.Common.DBHelper;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Dao
{
    public class DbDao
    {
        public static SqlSugarScope RWDb = DBHelperEX._instance.RWscope;
    }
}
