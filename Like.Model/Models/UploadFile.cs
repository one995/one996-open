using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Like.Model.Models
{
    public class UploadFile
    {

        public string Id { get; set; }
        public string FileName { get; set; }
        /// <summary>
        /// 文件类型  1 图片 2  rar文件  3 
        /// </summary>
        public string ContentType { get; set; }

        public string Base64Str { get; set; }
    }
}
