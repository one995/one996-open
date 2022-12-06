using IdentityServer4.Extensions;
using Like.Common.TCP;
using Like.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using SqlSugar;
using System;
using System.Threading.Tasks;

namespace Like.LikeYou.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemInfoController : BaseController
    {

        public SystemInfoController(ISqlSugarClient db) : base(db)
        {
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Model.Models.ComputerUserInfo sysUserInfo)
        {

            try
            {
                if (sysUserInfo is null)
                {
                    return Json(new { msg = "上传数据异常", code = 0 });
                }

                if (string.IsNullOrEmpty(sysUserInfo.UserName))
                {
                    return Json(new { msg = "用户名不能为空", code = 0 });
                }

                var old = db.Queryable<Model.Models.ComputerUserInfo>().Where(s => s.UserName==sysUserInfo.UserName).First();
                if (old is null)
                {
                    return Json(new { msg = "登录失败,用户不存在", code = 0 });
                }
                else
                {
                   if(old.Password ==Common.Helper.MD5Helper.MD5Encrypt32(sysUserInfo.Password).ToUpper())
                    {
                        return Json(new { msg = "登录成功", code = 1 });
                    }
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error("登录用户信息异常:"+ex);
            }

            return Json(new { msg = "登录失败", code = 0 });
        }

        [HttpPost]
        [Route("addCpuUser")]
        public async Task<IActionResult> CpuUser([FromBody] Model.Models.ComputerUserInfo sysUserInfo)
        {

            try
            {
                //if (dy is null)
                //{
                //    return Json(new { msg = "上传数据不能为空1", code = 0 });
                //}
                //var data = dy.ToString();
                //if (string.IsNullOrWhiteSpace(data))
                //{
                //    return Json(new { msg = "上传数据不能为空2", code = 0 });
                //}
                //Serilog.Log.Error("上传电脑信息:"+dy.ToString());
                //Model.Models.ComputerUserInfo sysUserInfo = JsonConvert.DeserializeObject<Model.Models.ComputerUserInfo>(data);
                if (sysUserInfo is null)
                {
                    return Json(new { msg = "注册失败", code = 0 });
                }

                if (string.IsNullOrEmpty(sysUserInfo.UserName))
                {
                    return Json(new { msg = "注册失败,用户名不能为空", code = 0 });
                }
                if (string.IsNullOrEmpty(sysUserInfo.Password))
                {
                    return Json(new { msg = "注册失败,密码不能为空", code = 0 });
                }
                var old = db.Queryable<Model.Models.ComputerUserInfo>().Where(s => s.UserName==sysUserInfo.UserName).First();
                var result = 0;
                if (old!=null)
                {
                    return Json(new { msg = "注册失败,用户已经存在", code = 0 });
                }
                else
                {
                    sysUserInfo.Password=Common.Helper.MD5Helper.MD5Encrypt32(sysUserInfo.Password).ToUpper();
                    result =  await db.Saveable(sysUserInfo).ExecuteCommandAsync();
                }

                if (result>0)
                {
                    //TCP转发给终端
                    return Json(new { msg = "注册成功", code = 1 });
                }

            }
            catch (Exception ex)
            {
                Serilog.Log.Error("注册电脑CPU用户信息异常:"+ex);
            }

            return Json(new { msg = "注册失败", code = 0 });
        }

        [HttpPost]
        [Route("updateCpuUser")]
        public async Task<IActionResult> UpdateCpuUser([FromBody]Model.Models.ComputerUserInfo sysUserInfo)
        {

            try
            {
                if (sysUserInfo is null)
                {
                    return Json(new { msg = "上传数据不能为空1", code = 0 });
                }

                //Serilog.Log.Error("上传电脑信息:"+dy.ToString());
                //Model.Models.ComputerUserInfo sysUserInfo = JsonConvert.DeserializeObject<Model.Models.ComputerUserInfo>(data);
                if (sysUserInfo is null)
                {
                    return Json(new { msg = "上传数据异常", code = 0 });
                }

                if (string.IsNullOrEmpty(sysUserInfo.UserName))
                {
                    return Json(new { msg = "上传数据异常,MAC不能为空", code = 0 });
                }

                var old = db.Queryable<Model.Models.ComputerUserInfo>().Where(s => s.UserName==sysUserInfo.UserName);
                var result = 0;
                if (old is
                     null)
                {
                    return Json(new { msg = "修改失败,用户不存在", code = 0 });
                }
                else
                {
                    result=  await db.Updateable<Model.Models.ComputerUserInfo>(sysUserInfo).IgnoreColumns(true).Where(s=>s.UserName==sysUserInfo.UserName).ExecuteCommandAsync();
                }
                if (result>0)
                {
                    //TCP转发给终端
                    return Json(new { msg = "修改成功", code = 1 });
                }

            }
            catch (Exception ex)
            {
                Serilog.Log.Error("注册电脑CPU用户信息异常:"+ex);
            }

            return Json(new { msg = "注册失败", code = 0 });
        }

        [HttpPost]
        [Route("cpu")]
        public async Task<IActionResult> Cpu([FromBody]Model.Models.ComputerInfo sysUserInfo)
        {

            try
            {
                if(sysUserInfo is null)
                {
                    return Json(new { msg = "上传数据异常", code = 0 });
                }

                if (string.IsNullOrEmpty(sysUserInfo.Mac))
                {
                    return Json(new { msg = "上传数据异常,MAC不能为空", code = 0 });
                }

                var old= db.Queryable<Model.Models.ComputerInfo>().Where(s => s.Mac==sysUserInfo.Mac).First();
                var result = 0;
                if (old!=null)
                {
                    result= await db.Updateable<Model.Models.ComputerInfo>(sysUserInfo).Where(s => s.Mac==sysUserInfo.Mac).ExecuteCommandAsync();
                }
                else
                {
                    result=  await db.Saveable(sysUserInfo).ExecuteCommandAsync();
                }
               
                if (result>0)
                {
                    old = db.Queryable<Model.Models.ComputerInfo>().Where(s => s.Mac==sysUserInfo.Mac).First();
                    //TCP转发给终端
                    var resultmsg=  TCPMessage.SendMsg(old.UserName, JsonConvert.SerializeObject(sysUserInfo), ActionType.SendData.ToString());
                    return Json(new { msg = "上传成功", code = 1,data= resultmsg });
                }
            }
            catch(Exception ex)
            {
                Serilog.Log.Error("上传电脑信息异常:"+ex);
            }

            return Json(new {msg="上传失败",code=0});
        }
    }
}
