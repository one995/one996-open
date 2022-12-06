using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
namespace Like.Model.Models
{
    /// <summary>
    /// 文章表
    /// </summary>
    [SugarTable("article")]//当和数据库名称不一样可以设置表别名 指定表明
    public class Article
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//数据库是自增才配自增 
        public long Id { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(255)")]//自定格式的情况 length不要设置
        
        public string Title { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(255)")]//自定格式的情况 length不要设置

        public string SubTitle { get; set; }

     
   
        public string AritcleTxt { get; set; }

      
        public string AritcleTxtShow { get; set; }

        public string Userid { get; set; }
        public DateTime? Createtime { get; set; }
        public int ?Msgcount { get; set; }
        public int? Isdelete { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(500)")]//自定格式的情况 length不要设置
        public string AID { set; get; }

        /// <summary>
        /// 文章内容
        /// </summary>

        public string? HtmlString { get; set; }
        public DateTime ?UpdateTime { get; set; }

        public string UpdateBy { get; set; }

        /// <summary>
        /// 文章url
        /// </summary>
        public string? AritcleUrl { get; set; }
        /// <summary>
        ///1 问题、0文章
        /// </summary>
        public int ?AritcleType { get; set; }
        /// <summary>
        /// 允许转发
        /// </summary>
        public int? IsForwarding { get; set; }

        /// <summary>
        /// 图片url
        /// </summary>
        public string? PicURL { get; set; }

        /// <summary>
        /// 标签  上位机 、net 、数据库
        /// </summary>
        public string? tag { get; set; }

        /// <summary>
        /// 展示图
        /// </summary>
        public string? PicShow { get; set; }

        public int ?SeeCount { get; set; }
        //public int ETCount { get; set; }

        public int ?IsCheck { get; set; }

        public string CheckUser { get; set; }

        public int? CheckResult { get; set; }
    }
}
