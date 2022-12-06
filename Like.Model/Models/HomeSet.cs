using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
namespace Like.Model.Models
{
    public class HomeSet
    {
        [SugarColumn(IsIdentity =true,IsPrimaryKey =true)]
        public int Id { get; set; }
        /// <summary>
        /// 推荐标题
        /// </summary>
        [SqlSugar.SugarColumn(ColumnDataType = "Nvarchar(50)", IsNullable = true)]
        public string RecommendedArtTitle { get; set; }

        /// <summary>
        /// 推荐URL
        /// </summary>
        [SqlSugar.SugarColumn(ColumnDataType = "Nvarchar(500)", IsNullable = true)]
        public string RecommendedArtRUL { get; set; }
        /// <summary>
        /// 推荐文章
        /// </summary>
        [SqlSugar.SugarColumn(ColumnDataType = "Nvarchar(500)", IsNullable = true)]
        public string RecommendedArt { get; set; }

        public int ArtId { get; set; }
        /// <summary>
        /// 热门文章
        /// </summary>
        [SqlSugar.SugarColumn(ColumnDataType = "Nvarchar(5000)", IsNullable = true)]
        public string HotArt { get; set; }
        /// <summary>
        /// 每日一句
        /// </summary>
        [SqlSugar.SugarColumn(ColumnDataType = "Nvarchar(200)", IsNullable = true)]
        public string EvedayOne { get; set; }
        /// <summary>
        /// 网站公告
        /// </summary>
        [SqlSugar.SugarColumn(ColumnDataType = "Nvarchar(500)",IsNullable =true)]
        public string SiteShow { get; set; }
        /// <summary>
        /// 主页图片
        /// </summary>
        [SqlSugar.SugarColumn(ColumnDataType = "Nvarchar(1500)",IsNullable =true)]
        public string HomePicShow { get; set; }
        [SqlSugar.SugarColumn( IsNullable = true)]
        public DateTime ?LastUpdated { get; set; }



    }
}
