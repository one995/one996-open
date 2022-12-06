using Like.Model.Emun;
using System;
using System.ComponentModel.DataAnnotations;
using SqlSugar;
namespace Like.Model
{
    public class User
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]//数据库是自增才配自增 
        public int id { get; set; }
        /// <summary>
        ///
        /// </summary>
        [SugarColumn(ColumnDataType = "Nvarchar(255)")]//自定格式的情况 length不要设置

        public String? UserName { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(255)")]//自定格式的情况 length不要设置

        public String? PassWord { get; set; }

        public DateTime? CreateTime { get; set; }
        [SugarColumn(IsNullable = true)]//自定格式的情况 length不要设置
        public DateTime? UpdateTime { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(255)", IsNullable = true)]//自定格式的情况 length不要设置
     
        public String ?CreateUser { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(255)", IsNullable = true)]//自定格式的情况 length不要设置

        public String PhoneNumber { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(255)", IsNullable = true)]//自定格式的情况 length不要设置

        public String PersonID { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(255)", IsNullable = true)]//自定格式的情况 length不要设置

        public String NikoID { get; set; }
        [SugarColumn(ColumnDataType = "varchar(max)", IsNullable = true)]//自定格式的情况 length不要设置

        public String PicPath { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(255)", IsNullable = true)]//自定格式的情况 length不要设置
  
        public String IDCard { get; set; }

  
        public int Role { get; set; }

        public int SEX { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(50)", IsNullable = true)]//自定格式的情况 length不要设置
      
        public String Eamil { get; set; }

        public int VIP { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(255)", IsNullable = true)]//自定格式的情况 length不要设置

        public String remake { get; set; }

        public int isdelete { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(500)", IsNullable = true)]//自定格式的情况 length不要设置
      
        public String usertile { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(500)", IsNullable = true)]//自定格式的情况 length不要设置
    
        public String address { get; set; }


        public int userlevel { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(100)", IsNullable = true)]//自定格式的情况 length不要设置
       
        public String nationplace { get; set; }


    
        public int focus { get; set; }

        public int fans { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(255)", IsNullable = true)]//自定格式的情况 length不要设置
  
        public String commpany { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(255)", IsNullable = true)]//自定格式的情况 length不要设置

        public String depatment { get; set; }
        [SugarColumn(ColumnDataType = "Nvarchar(255)", IsNullable = true)]//自定格式的情况 length不要设置
     
        public String technology { get; set; }
  
        public int integralid { get; set; }

        [SugarColumn(ColumnDataType = "Nvarchar(50)", IsNullable = true)]//自定格式的情况 length不要设置

        public string QQNumber { get; set; }

        //public int? InYear { get; set; }

    }
}
