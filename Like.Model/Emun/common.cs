using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Model.Emun
{
    public class common
    {
    }
   public enum RoleUser
    {
        user=1,
        vip = 2,
        admin = 3,
        superadmin = 4
    }
    public enum SEXEducation
    {

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "男")]
        Nan=0,

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "女")]
        Nv=1,

        /// <summary>
        ///
        /// </summary>
        [Display(Name = "保密")]
        Middel=2
    }
   
    public enum LogType
    {
        Error,
        Info,
        Debug,
        Warning,
        UserDao,

    }
    public class DateTimeFromt
    {
        public static string yyyyMMddHHmmss { get { return "yyyy-MM-dd HH:mm:ss"; } }
    }
}
