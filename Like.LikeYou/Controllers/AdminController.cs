using Like.Model.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Like.LikeYou.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class AdminController : BaseController
    {
        public AdminController(ISqlSugarClient db) : base(db)
        {
        }

        [HttpPost]
        [Route("GetUser")]
       
        public async Task<UnifyResult<Model.User>> GetUser(string PersonID)
        {
            return new UnifyResult<Model.User>(await BLL.SqlSugarBLL<Model.User>.baseDao.GetModel((s) => s.PersonID == PersonID && s.isdelete != 1));
        }

        [HttpPost]
        [Route("UpdateUser")]
        public async Task<UnifyResult<string>> UpdateUser([FromBody] Model.User login)
        {
            string user = "修改失败";
            try
            {
                if (login!=null)
                {
                    Model.User user1=  await  BLL.SqlSugarBLL<Model.User>.baseDao.GetModel(s=>s.id==login.id);
                    if(user1 is null)
                    {
                        return new UnifyResult<string>("用户不存在！", ResultCode.ValidError);
                    }
                    if (user1.PassWord!=Common.Helper.MD5Helper.MD5Encrypt32(login.PassWord))
                    {
                        login.PassWord=Common.Helper.MD5Helper.MD5Encrypt32(login.PassWord);
                    }
                    login.UpdateTime = DateTime.Now;
                   
                    bool result = await BLL.SqlSugarBLL<Model.User>.baseDao.Update(login, (s) => s.id == login.id && s.isdelete != 1);
                    if (result)
                    {
                        user = "修改成功";
                        return new UnifyResult<string>(user, ResultCode.Success);
                    }
                }
                
            }
            catch(Exception ex)
            {
                //Serilog.LogException(ex);
                return new UnifyResult<string>(user, ResultCode.Error);
            }
            return new UnifyResult<string>(user,ResultCode.ValidError);
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<UnifyResult<PageMsg>> GetUsers(int Pagesize =0,int index=0)
        {

            PageMsg page = new PageMsg();
            try
            {
                SqlSugar.RefAsync<int> total = 0;
                if (Pagesize!=0&&index!=0)
                {
                    List<Model.User> users = await BLL.SqlSugarBLL<Model.User>.baseDao.GetPageListIndex(s => s.isdelete==0, SqlSugar.OrderByType.Desc, s => s.CreateUser, index, Pagesize, total);
                    page.data=users;
                    page.totalsize=total;
                    page.pagesize=Pagesize;
                    page.index=index;           
                }
            }
            catch (Exception ex)
            {
                //Serilog.LogException(ex);
                return new UnifyResult<PageMsg>(page, ResultCode.Error);
            }
            return new UnifyResult<PageMsg>(page, ResultCode.Success);
        }


        [HttpGet]
        [Route("DelUser")]
        public async Task<UnifyResult<string>> DelUser(string UserId)
        {
            string user = "删除失败";
            try
            {
                if (!string.IsNullOrEmpty(UserId))
                {

                    bool result = await BLL.SqlSugarBLL<Model.User>.baseDao.DeleteWhere(s=>s.PersonID==UserId);
                    if (result)
                    {
                        user = "删除成功";
                        return new UnifyResult<string>(user, ResultCode.Success);
                    }
                }
            }
            catch (Exception ex)
            {
                //Serilog.LogException(ex);
                return new UnifyResult<string>(user, ResultCode.Error);
            }
            return new UnifyResult<string>(user, ResultCode.ValidError);
        }


        #region  admin 管理员界面


        #endregion
    }
}
