using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Like.Common.Helper
{
    public class FileHeper
    {
        public string Base64ToImage(string base64Str, string path, string imgName)
        {
            string filename = "";//声明一个string类型的相对路径
            //取图片的后缀格式
            string hz = base64Str.Split(',')[0].Split(';')[0].Split('/')[1];
            string[] str = base64Str.Split(',');  //base64Str为base64完整的字符串，先处理一下得到我们所需要的字符串
            byte[] imageBytes = Convert.FromBase64String(str[1]);
            //读入MemoryStream对象
            MemoryStream memoryStream = new MemoryStream(imageBytes, 0, imageBytes.Length);
            memoryStream.Write(imageBytes, 0, imageBytes.Length);
            filename = path + imgName + "." + hz;//所要保存的相对路径及名字
            //string tmpRootDir = Server.MapPath(path); //获取程序根目录 
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string imagesurl2 = path + imgName + "." + hz; //转换成绝对路径 
            //  转成图片
            Image image = Image.FromStream(memoryStream);
            //   图片名称
            string iname = DateTime.Now.ToString("yyMMddhhmmss");
            image.Save(imagesurl2);  // 将图片存到本地Server.MapPath("pic\\") + iname + "." + hz
            return filename;
        }
    }
}
