using Like.Model.Result;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SqlSugar;

namespace Like.LikeYou.Controllers
{
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
       public ISqlSugarClient db;
        public BaseController(ISqlSugarClient db)
        {
            this.db = db;//通过构造函数拿到注入的db
        }
        /// <summary>
        /// 当前登录用户信息
        /// </summary>
        //public Models.CurrentUser CurrentUser => HttpContext.User.Identity.Name;

        /// <summary>
        /// 创建 JsonResult 对象，该对象使用指定 JSON 请求行为将指定对象序列化为 JavaScript 对象表示法 (JSON) 格式。
        /// </summary>
        /// <param name="data">要序列化的 JavaScript 对象图</param>
        /// <param name="format">时间格式</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Json")]
        public JsonResult Json(object data, string format)
        {
            var setting = new JsonSerializerSettings
            {
                DateFormatString = format
            };
            return base.Json(data, setting);
        }
        [HttpGet]
        [Route("Error1")]
        public ActionResult Error(ResultCode code)
        {
            return Json(new UnifyResult { StatusCode = code, Message = code.ToString() });
        }
        [HttpGet]
        [Route("Error")]
        public ActionResult Error(string message, ResultCode code = ResultCode.ValidError)
        {
            return Json(new UnifyResult { StatusCode = code, Message = message });
        }
        [HttpGet]
        [Route("Success")]
        public ActionResult Success<T>(T data, string message = null)
        {
            return Json(new UnifyResult<T> { Data = data, Message = message ?? "操作成功" });
        }
    }
}
