using Like.Common.Helper;
using Like.Model.Emun;
using Like.Model.Result;
using Like.Model.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Like.AuthorizedServiceCenter.Controllers
{
    [AllowAnonymous]
    [Route("accountjwt/{action}")]
    public class AccountJWTController : BaseController
    {
        public AccountJWTController(ISqlSugarClient db) : base(db)
        {
        }

        //private Like.BLL.Acconut.AccountBLL _accountbll 

        [HttpGet]
        public IActionResult  Login(string returnUrl)
        {
           //获取请求过来的URL，方便跳转回去
           LoginViewModel login = new LoginViewModel();
            login.ReturnUrl = "/home/index";
            return View(login);
        }
        /// <summary>
        /// 用户名登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Name")]
        public async Task<IActionResult> LoginName([FromBody]LoginViewModel login)
        {
            if(login is null)
            {
                return Error("登录失败！",ResultCode.ValidError);
            }
            if(string.IsNullOrWhiteSpace(login?.Username) || string.IsNullOrWhiteSpace(login?.Password))
            {
                return Error("账号或者密码不能为空！", ResultCode.ValidError);
            }
          
            Model.User user = await db.Queryable<Model.User>().Where(x => x.UserName == login.Username&&x.isdelete==0).FirstAsync();
            //await Like.BLL.SqlSugarBLL<Model.User>.baseDao.GetModel((s)=>s.UserName==login.Username);
            if (user is null)
            {
                return Error("账号未注册！",ResultCode.ValidError);
            }
            if (user?.PassWord !=  Common.Helper.MD5Helper.MD5Encrypt32(login?.Password))
            {
                return Error("密码错误！", ResultCode.ValidError);
            }
           

            Serilog.Log.Information("用户登录成功：" + user.UserName + "-" + user.Eamil);
            //生成token
            //查询人员权限
            TokenModelJwt modelJwt = new TokenModelJwt();
            modelJwt.Uid = user.id;
            modelJwt.Work=user.UserName.ToString();
            modelJwt.Role=user.Role.ToString();
            //返回touken，同时跳转回原来的地方
            Models.LoginDto loginDto = new Models.LoginDto();
            loginDto.Message = "登录成功";
            loginDto.Token= Common.Helper.JwtHelper.IssueJwt(modelJwt,user);
            loginDto.Expired = "7200";
            loginDto.IsSucceed = true;
            loginDto.UserName = user.UserName;
            loginDto.Role = user.Role.ToString();
            
           
            return Success(loginDto,"登录成功");
            //return View();
        }
        /// <summary>
        /// 邮箱登录
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("email")]
        public async Task< IActionResult> Loginemail([FromBody] LoginViewModel login)
        {
            if (login is null)
            {
                return Error(ResultCode.ValidError);
            }
            if (string.IsNullOrWhiteSpace(login?.Email) || string.IsNullOrWhiteSpace(login?.Password))
            {
                return Error(ResultCode.ValidError);
            }
            Model.User user = await Like.BLL.SqlSugarBLL<Model.User>.baseDao.GetModel((s)=>s.Eamil==login.Email);
            if (user is null)
            {
                return Error("邮箱未注册！", ResultCode.ValidError);
            }
            if (user.PassWord != Common.Helper.MD5Helper.MD5Encrypt32(login?.Password))
            {
                return Error("密码错误！", ResultCode.ValidError);
            }
            Serilog.Log.Information("用户登录成功：" + user.UserName + "-" + user.Eamil);
            //生成token
            //查询人员权限
            TokenModelJwt modelJwt = new TokenModelJwt();
            modelJwt.Uid = user.id;
            modelJwt.Work = user.UserName.ToString();
            modelJwt.Role = user.Role.ToString();
            //返回touken，同时跳转回原来的地方
            Models.LoginDto loginDto = new Models.LoginDto();
            loginDto.Message = "登录成功";
            loginDto.Token = Common.Helper.JwtHelper.IssueJwt(modelJwt,user);
            loginDto.Expired = "7200";
            loginDto.IsSucceed = true;
            return Success(loginDto, "登录成功");
        }

        [HttpGet]
        public IActionResult Reregister()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Reregister([FromBody] LoginViewModel login)
        {
            try
            {
                if (string.IsNullOrEmpty(login?.Username) || string.IsNullOrEmpty(login?.Password) || string.IsNullOrEmpty(login?.Email))
                {
                    return Error("注册失败，请填写完整测注册信息");
                }
                if (Common.Helper.CommonHelper.IsIllegalChar(login.Username))
                {
                    return Error("注册失败，名字含有非法字符");
                }
                //Model.Models.User user = BLL.Acconut.AccountBLL.GetUserByName(login?.Username);
                Model.User user = await Like.BLL.SqlSugarBLL<Model.User>.baseDao.GetModel((s)=>s.UserName==login.Username);
                if (user != null)
                {
                    return Error("注册失败，用户名已存在！");
                }
                user = await Like.BLL.SqlSugarBLL<Model.User>.baseDao.GetModel((s) => s.Eamil == login.Email );
                if (user != null)
                {
                    return Error("注册失败，邮箱已存在！");
                }
                Model.User adduser = new Model.User();
                adduser.Role = Convert.ToInt16(Model.Emun.RoleUser.user);
                adduser.UserName = login.Username;
                adduser.PassWord = Common.Helper.MD5Helper.MD5Encrypt32(login.Password);
                adduser.Eamil = login.Email;
                adduser.CreateTime =Convert.ToDateTime(DateTime.Now.ToString(DateTimeFromt.yyyyMMddHHmmss));
                //adduser.SEX = SEXEducation.Nan;
                adduser.PersonID = Common.Helper.CommonHelper.GuidTo16String();
                if (BLL.Acconut.AccountBLL.Add(adduser) > 0)
                {
                    Serilog.Log.Information("新增用户成功："+adduser.UserName + "-"+adduser.Eamil);
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
            catch(Exception ex)
            {
                Serilog.Log.Error(ex.ToString());
                return Error("注册失败，发生未知异常！");
            }    
        }

        [HttpPost]
        [Route("GetAPIToken")]
        public IActionResult GetAPIToken(string appid,string key)
        {
            //验证api权限 第三方授权
            if (string.IsNullOrEmpty(appid))
            {
                return Error("appid错误",ResultCode.ValidError);
            }
            if (string.IsNullOrEmpty(key))
            {
                return Error("key错误", ResultCode.ValidError);
            }



            return Ok(new { token= Common.Helper.JwtHelper.GetToken(new Model.User(),key) });
        }

        [HttpGet]
        public IActionResult GetAPIReToken(string token)
        {
            //验证token

            return Ok(new { token = Common.Helper.JwtHelper.GetToken(new Model.User()) });
        }

        [HttpPost]
        [ActionName("Logout")]
        public async Task<IActionResult> Logout([FromBody] Models.LogoutRequest logout)
        {
            TokenModelJwt tokenModel= Common.Helper.JwtHelper.SerializeJwt(logout?.Token);
            //if(tokenModel.) 移除redis 的缓存，移除
            //- 90 3尺 Common.DBHelper.RedisHelper.
           await Task.Delay(500);
            Models.LogoutResponse  logout1 = new Models.LogoutResponse();
            logout1.IsSuccess=true;
            return Success(logout1, "注销成功");
        }



    }
}
