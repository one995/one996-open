using Like.Common.Helper;
using Like.Model.Emun;
using Like.Model.Result;
using Like.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Threading.Tasks;

namespace Like.AuthorizedServiceCenter.Controllers
{

    [Route("admin/{action}")]
    public class AdminController : BaseController
    {
        public AdminController(ISqlSugarClient db) : base(db)
        {
        }

        /// <summary>
        /// 用户名登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Name")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginName([FromBody] LoginViewModel login)
        {
            //返回touken，同时跳转回原来的地方
            Models.LoginDto loginDto = new Models.LoginDto();
            try { 
            if (login is null)
            {
                return Error("登录失败！", ResultCode.ValidError);
            }
            if (string.IsNullOrWhiteSpace(login?.Username) || string.IsNullOrWhiteSpace(login?.Password))
            {
                return Error("账号或者密码不能为空！", ResultCode.ValidError);
            }

            Model.Models.AdminUser user = await db.Queryable<Model.Models.AdminUser>().Where(x => x.UserName == login.Username&&x.isdelete==0).FirstAsync();
            //await Like.BLL.SqlSugarBLL<Model.User>.baseDao.GetModel((s)=>s.UserName==login.Username);
            if (user is null)
            {
                return Error("账号未注册！", ResultCode.ValidError);
            }
            if (user?.Passwordmd5 !=  Common.Helper.MD5Helper.MD5Encrypt32(login?.Password))
            {
                return Error("密码错误！", ResultCode.ValidError);
            }


            Serilog.Log.Information("用户登录成功：" + user.UserName );
            //生成token
            //查询人员权限
            TokenModelJwt modelJwt = new TokenModelJwt();
            modelJwt.Uid = user.Id;
            modelJwt.Work=user.UserName.ToString();
            modelJwt.Role=user.Role.ToString();
       
            loginDto.Message = "登录成功";
            loginDto.Token= Common.Helper.JwtHelper.IssueJwtAdmin(modelJwt, user);
            loginDto.Expired = "7200";
            loginDto.IsSucceed = true;
            loginDto.UserName = user.UserName;
            loginDto.Role = user.Role.ToString();
        }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.ToString());
                return Error("登录失败，发生未知异常！");
    }
            return Success(loginDto, "登录成功");
        }
    
        [HttpPost]
        [ActionName("admin")]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] Model.Models.AdminUser login)
        {
            try
            {
                if (string.IsNullOrEmpty(login?.UserName) || string.IsNullOrEmpty(login?.Passwordmd5))
                {
                    return Error("注册失败，请填写完整测注册信息");
                }
                if (Common.Helper.CommonHelper.IsIllegalChar(login.UserName))
                {
                    return Error("注册失败，名字含有非法字符");
                }
                //Model.Models.User user = BLL.Acconut.AccountBLL.GetUserByName(login?.Username);
                Model.Models.AdminUser user = await Like.BLL.SqlSugarBLL<Model.Models.AdminUser>.baseDao.GetModel((s) => s.UserName==login.UserName);
                if (user != null)
                {
                    return Error("注册失败，用户名已存在！");
                }
                Model.Models.AdminUser adduser = new Model.Models.AdminUser();
                adduser.Role = login.Role;
                adduser.UserName = login.UserName;
                adduser.Passwordmd5 = Common.Helper.MD5Helper.MD5Encrypt32(login.Passwordmd5);
                adduser.isdelete=0;
                adduser.CreateTime=DateTime.Now;
                adduser.CreateUser="admin";
                //adduser.SEX = SEXEducation.Nan;
                adduser.PersonID = Common.Helper.CommonHelper.GuidTo16String();
                if (db.Insertable<Model.Models.AdminUser>(adduser).ExecuteCommand() > 0)
                {
                    Serilog.Log.Information("新增用户成功："+adduser.UserName );
                    UnifyResult unify = new UnifyResult();
                    unify.StatusCode = ResultCode.Success;
                    unify.Message = "注册成功";
                    return Success<UnifyResult>(unify);
                }
                else
                {
                    return Error("注册失败，数据库操作异常！");
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.ToString());
                return Error("注册失败，发生未知异常！");
            }
        }


        [HttpPut]
        [Authorize]
        [ActionName("admin")]
        public async Task<IActionResult> Put([FromBody] Model.Models.AdminUser login)
        {
            try
            {
                if (string.IsNullOrEmpty(login?.UserName))
                {
                    return Error("更新失败，请填写完整测注册信息");
                }
                if (Common.Helper.CommonHelper.IsIllegalChar(login.UserName))
                {
                    return Error("更新失败，名字含有非法字符");
                }
                //Model.Models.User user = BLL.Acconut.AccountBLL.GetUserByName(login?.Username);
                Model.Models.AdminUser user = await Like.BLL.SqlSugarBLL<Model.Models.AdminUser>.baseDao.GetModel((s) => s.PersonID==login.PersonID);
                if (user == null)
                {
                    return Error("更新失败，用户不存在！");
                }
                //user.UserName = login.UserName;
                if (user.Passwordmd5!=Common.Helper.MD5Helper.MD5Encrypt32(login.Passwordmd5))
                {
                    user.Passwordmd5 = Common.Helper.MD5Helper.MD5Encrypt32(login.Passwordmd5);
                }
                user.Role=login.Role;
                user.isdelete=0;
                user.CreateTime=DateTime.Now;
                //adduser.SEX = SEXEducation.Nan;
                if (db.Updateable<Model.Models.AdminUser>(user).IgnoreColumns(true).ExecuteCommand() > 0)
                {
                    Serilog.Log.Information("更新用户成功："+user.UserName);
                    UnifyResult unify = new UnifyResult();
                    unify.StatusCode = ResultCode.Success;
                    unify.Message = "更新用户成功";
                    return Success<UnifyResult>(unify);
                }
                else
                {
                    return Error("注册失败，数据库操作异常！");
                }
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.ToString());
                return Error("注册失败，发生未知异常！");
            }
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpDelete]
        [ActionName("admin")]
        [Authorize]
        public async Task<IActionResult> Delete(string Username)
        {
            if (string.IsNullOrWhiteSpace(Username) )
            {
                return Error("账号不能为空！", ResultCode.ValidError);
            }
            if (Username=="admin")
            {
                   return Error("管理员用户不能删除！", ResultCode.ValidError);
            }

            Model.Models.AdminUser user = await db.Queryable<Model.Models.AdminUser>().Where(x => x.UserName ==Username&&x.isdelete==0).FirstAsync();
            //await Like.BLL.SqlSugarBLL<Model.User>.baseDao.GetModel((s)=>s.UserName==login.Username);
            if (user is null)
            {
                return Error("账号不存在！", ResultCode.ValidError);
            }

            int count= db.Deleteable<Model.Models.AdminUser>(s => s.PersonID==user.PersonID).ExecuteCommand();
            UnifyResult unify = new UnifyResult();
            if (count > 0)
            {
                Serilog.Log.Information("用户删除成功：" + user.UserName);
                unify.StatusCode = ResultCode.Success;
                unify.Message = "用户删除成功";
            }
            else
            {
                unify.StatusCode = ResultCode.Error;
                unify.Message = "用户删除失败";
            }
            return Success(unify, "删除成功");
        }

        [HttpGet]
        [Authorize]
        [ActionName("admin")]
        public async Task<IActionResult> Get(int index=1,int pagesize=10)
        {
            PageMsg msg = new PageMsg();
            //if (page!=null)
            //{
                RefAsync<int> async = 0;
                msg.data= await db.Queryable<Model.Models.AdminUser>().ToPageListAsync(index,pagesize, async);
                msg.totalsize=async;
            //}
            return Success(msg);
        }

        [HttpPost]
        [ActionName("Logout")]
        public async Task<IActionResult> Logout([FromBody] Models.LogoutRequest logout)
        {
            TokenModelJwt tokenModel = Common.Helper.JwtHelper.SerializeJwt(logout?.Token);
            //if(tokenModel.) 移除redis 的缓存，移除
            //- 90 3尺 Common.DBHelper.RedisHelper.
            await Task.Delay(500);
            Models.LogoutResponse logout1 = new Models.LogoutResponse();
            logout1.IsSuccess=true;
            return Success(logout1, "注销成功");
        }

    }
}
