using Like.Common.SerilogDao;
using Like.Model.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Like.LikeYou.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : BaseController
    {
        public ArticleController(ISqlSugarClient db) : base(db)
        {
        }

        [Route("Add")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddArticle([FromBody] Model.Models.Article Inarticle)
        {
            try
            {
                if (Inarticle is null)
                {
                    return Ok(UnifyResult.GetUnifyResult("请求数据不能为空！", code: ResultCode.ValidError));
                }
                if (string.IsNullOrEmpty(Inarticle.tag))
                {
                    return Ok(UnifyResult.GetUnifyResult("文章标签不能为空！", code: ResultCode.ValidError));
                }
                Inarticle.Isdelete = 0;
                Inarticle.Msgcount = 0;
                Inarticle.Createtime = DateTime.Now;
                Inarticle.IsCheck = 0;
                Inarticle.AID = Common.Helper.CommonHelper.GetUUID();
                await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.Add(Inarticle);
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                return Ok(UnifyResult.GetUnifyResult("发生未知异常", code: ResultCode.Error));
            }
            return Ok(UnifyResult.GetUnifyResult());
        }

        [Route("Del")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DelArticle(int AID)
        {
            if (AID <= 0)
            {
                return BadRequest(UnifyResult.GetUnifyResult("请求参数不能为空！", ResultCode.ValidError));
            }
            return Ok(UnifyResult.GetUnifyResult(await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.Delete(new Model.Models.Article() { Id=AID }) ? "删除成功" : "删除失败"));
        }
        [Route("UDel")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UDelArticle(string AID)
        {
            bool res = await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.Update(new Model.Models.Article() { Isdelete = 1 },s=>s.AID==AID);
            return Ok(UnifyResult.GetUnifyResult(res ? "删除成功" : "删除失败", res?ResultCode.Success:ResultCode.ValidError));
        }

        [Route("Update")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UpdateArticle([FromBody] Model.Models.Article Inarticle)
        {
            //Inarticle.AID = Common.Helper.CommonHelper.GetUUID();
            //article.Id = Inarticle.Id;
            try
            {
                Inarticle.Isdelete = 0;
                Inarticle.UpdateTime = DateTime.Now;

                await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.Update(Inarticle);
            }
            catch(Exception ex)
            {
                return Ok(UnifyResult.GetUnifyResult("操作失败",ResultCode.Error));
            }
           
            return Ok(UnifyResult.GetUnifyResult());
        }


        [Route("Get")]
        [HttpGet]
        public async Task<UnifyResult<Model.Models.Article>> GetArticle(string AID) {
            if(AID is null)
            {
                return  new UnifyResult<Model.Models.Article>();
            }
            int? count = ++BLL.SqlSugarBLL<Model.Models.Article>.baseDao.GetModel(s => s.AID==AID).Result.SeeCount;
            if (count is null)
            {
                count = 0;
                count++;
            }
            await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.Update(new Model.Models.Article() { SeeCount=count }, s => s.AID==AID);
            return  new UnifyResult<Model.Models.Article>(await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.GetModel((S)=>S.AID==AID&&S.Isdelete==0));
        } 
        

        [Route("GetUserArticle")]
        [HttpGet]
        public async Task<UnifyResult<Model.Models.Article>> GetListByUserArticle(string Userid)=> new UnifyResult<Model.Models.Article>(await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.GetModel((S)=>S.Userid== Userid && S.Isdelete == 0));
        

        [Route("GetArticleList")]
        [HttpGet]
        public async Task<UnifyResult<Model.Models.Article>> GetListArticle(int  count)=> new UnifyResult<Model.Models.Article>(await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.GetModels((s)=>s.Isdelete==0 && s.IsCheck == 1, count>0?count:200));

        [Route("GetArticlePageList")]
        [HttpGet]
        public async Task<UnifyResult<Model.Result.PageMsg>> GetArticlePageList(int pageIndex, int pageSize,int ?IsCheck = null, int? checkResult=null, string CheckUser="",string tag="") {
            Model.Result.PageMsg page = new Model.Result.PageMsg();
            try
            {
                RefAsync<int> totalCount = 0;
                List<Model.Models.Article> articles = new List<Model.Models.Article>();

                articles = await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.GetPageListWhere((s) => s.Isdelete == 0  ,pageIndex > 0 ? pageIndex : 1, pageSize > 0 ? pageSize : 20,
                    totalCount,
                   wherecolumn1: IsCheck==null?null:((s)=>s.IsCheck==IsCheck),
                   wherecolumn2: checkResult==null ? null : ((s) => s.CheckResult==checkResult),
                   wherecolumn3: string.IsNullOrEmpty(CheckUser) ? null : ((s) => s.CheckUser==CheckUser),
                     wherecolumn4: string.IsNullOrEmpty(tag) ? null : ((s) => s.tag==tag)
               );

                page.data = articles;
                page.totalsize = totalCount;
                page.index=pageIndex;
                page.pagesize = pageSize;
                Serilog.Log.Information(LogEnmeType.Article.ToString(), "请求文章数据成功");
            }
            catch (Exception ex) {
                Serilog.Log.Error(LogEnmeType.Article.ToString(), ex);
                return new UnifyResult<Model.Result.PageMsg>(page,ResultCode.Error);
               
            }
            return new UnifyResult<Model.Result.PageMsg>(page,ResultCode.Success); 
        }

        [Route("GetArticlePageListIndex")]
        [HttpGet]
        public async Task<UnifyResult<Model.Result.PageMsg>> GetArticlePageListIndex(int pageIndex=1, int pageSize=5)
        {
            Model.Result.PageMsg page = new Model.Result.PageMsg();
            try
            {
                RefAsync<int> totalCount = 0;
                List<Model.Models.Article> articles = await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.GetPageListIndex((s) =>  s.Isdelete == 0 && s.IsCheck == 1, OrderByType.Desc,s=>s.Id
                ,pageIndex > 0 ? pageIndex : 1, pageSize>0 ? pageSize : 20, totalCount);
                page.data = articles;
                page.totalsize = totalCount;
                page.index=pageIndex;
                page.pagesize = pageSize;
                Serilog.Log.Information(LogEnmeType.Article.ToString(), "请求文章数据成功");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(LogEnmeType.Article.ToString(), ex);
                return new UnifyResult<Model.Result.PageMsg>(page, ResultCode.Error);

            }
            return new UnifyResult<Model.Result.PageMsg>(page, ResultCode.Success);
        }

        [Route("GetArticlePageListIndexHot")]
        [HttpGet]
        public async Task<UnifyResult<Model.Result.PageMsg>> GetArticlePageListIndexHot(int pageIndex = 1, int pageSize = 5)
        {
            Model.Result.PageMsg page = new Model.Result.PageMsg();
            try
            {
                RefAsync<int> totalCount = 0;

                List<Model.Models.Article> articles = await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.GetPageListIndex((s) => s.Isdelete == 0 &&s.IsCheck==1, OrderByType.Desc, s => s.Msgcount
                , pageIndex > 0 ? pageIndex : 1, pageSize>0 ? pageSize : 20, totalCount);
                page.data = articles;
                page.totalsize = totalCount;
                page.index=pageIndex;
                page.pagesize = pageSize;
                Serilog.Log.Information(LogEnmeType.Article.ToString(), "请求文章数据成功");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(LogEnmeType.Article.ToString(), ex);
                return new UnifyResult<Model.Result.PageMsg>(page, ResultCode.Error);

            }
            return new UnifyResult<Model.Result.PageMsg>(page, ResultCode.Success);
        }

        [Route("ArticleSeach")]
        [HttpGet]
        public async Task<UnifyResult<Model.Result.PageMsg>> GetArticlePageListIndexSeach(int pageIndex = 1,
        int pageSize = 10,string searchText="")
        {      
            Model.Result.PageMsg page = new Model.Result.PageMsg();
            try
            {
                RefAsync<int> totalCount = 0;
                if (string.IsNullOrEmpty(searchText))
                {
                    return new UnifyResult<Model.Result.PageMsg>(page, ResultCode.ValidError);
                }
                else
                {
                    List<Model.Models.Article> articles = await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.GetPageListIndex((s) => s.IsCheck==1 &&s.CheckResult==1 &&s.Isdelete == 0&&(s.tag.Contains(searchText)||s.SubTitle.Contains(searchText)||s.Title.Contains(searchText)||s.AritcleTxt.Contains(searchText)||s.tag.Contains(searchText)), OrderByType.Desc, s => s.Msgcount
             , pageIndex > 0 ? pageIndex : 1, pageSize>0 ? pageSize : 20, totalCount);
                    page.data = articles;
                    page.totalsize = totalCount;
                    page.index=pageIndex;
                    page.pagesize = pageSize;
                }
             
                Serilog.Log.Information(LogEnmeType.Article.ToString(), "请求文章数据成功");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(LogEnmeType.Article.ToString(), ex);
                return new UnifyResult<Model.Result.PageMsg>(page, ResultCode.Error);

            }
            return new UnifyResult<Model.Result.PageMsg>(page, ResultCode.Success);
        }

        [Route("UserArticle")]
        [HttpGet]
        public async Task<UnifyResult<Model.Result.PageMsg>> GetArticlePageListUserArticle(int pageIndex = 1,
        int pageSize = 10, string Userid = "")
        {
            Model.Result.PageMsg page = new Model.Result.PageMsg();
            try
            {
                RefAsync<int> totalCount = 0;
                if (string.IsNullOrEmpty(Userid))
                {
                    return new UnifyResult<Model.Result.PageMsg>(page, ResultCode.ValidError);
                }
                else
                {
                    List<Model.Models.Article> articles = await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.GetPageListIndex((s) => s.Isdelete == 0&&s.Userid==Userid, OrderByType.Desc, s => s.Msgcount
             , pageIndex > 0 ? pageIndex : 1, pageSize>0 ? pageSize : 20, totalCount);
                    page.data = articles;
                    page.totalsize = totalCount;
                    page.index=pageIndex;
                    page.pagesize = pageSize;
                }

                Serilog.Log.Information(LogEnmeType.Article.ToString(), "请求文章数据成功");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(LogEnmeType.Article.ToString(), ex);
                return new UnifyResult<Model.Result.PageMsg>(page, ResultCode.Error);

            }
            return new UnifyResult<Model.Result.PageMsg>(page, ResultCode.Success);
        }

        [Route("GetComments")]
        [HttpGet]
        public async Task<UnifyResult<Model.Result.PageMsg>> GetComments(string AID, int pageIndex = 1,
        int pageSize = 10 )
        {
            Model.Result.PageMsg page = new Model.Result.PageMsg();
            try
            {
                RefAsync<int> totalCount = 0;
                if (string.IsNullOrEmpty(AID))
                {
                    return new UnifyResult<Model.Result.PageMsg>(page, ResultCode.ValidError);
                }
                else
                {
                    List<Model.Models.Comments> articles = await BLL.SqlSugarBLL<Model.Models.Comments>.baseDao.GetPageListIndex((s) => s.Isdelete == 0&&s.Artcleid==AID, OrderByType.Desc, s => s.Createtime
             , pageIndex > 0 ? pageIndex : 1, pageSize>0 ? pageSize : 20, totalCount);
                    page.data = articles;
                    page.totalsize = totalCount;
                    page.index=pageIndex;
                    page.pagesize = pageSize;
                }

                Serilog.Log.Information(LogEnmeType.Article.ToString(), "请求文章数据成功");
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(LogEnmeType.Article.ToString(), ex);
                return new UnifyResult<Model.Result.PageMsg>(page, ResultCode.Error);

            }
            return new UnifyResult<Model.Result.PageMsg>(page, ResultCode.Success);
        }

        [Route("AddComments")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComments([FromBody] Model.Models.Comments Inarticle)
        {
            try
            {
                if (Inarticle is null)
                {
                    return Ok(UnifyResult.GetUnifyResult("请求数据不能为空！", code: ResultCode.ValidError));
                }
                if (string.IsNullOrEmpty(Inarticle.Artcleid))
                {
                    return Ok(UnifyResult.GetUnifyResult("文章id不能为空！", code: ResultCode.ValidError));
                }
                //if (string.IsNullOrEmpty(Inarticle.Userid))
                //{
                //    return Ok(UnifyResult.GetUnifyResult("文章id不能为空！", code: ResultCode.ValidError));
                //}
                if (string.IsNullOrEmpty(Inarticle.Commentsmsg))
                {
                    return Ok(UnifyResult.GetUnifyResult("评论不能为空！", code: ResultCode.ValidError));
                }
                if (string.IsNullOrEmpty(Inarticle.Localtion))
                {
                    Inarticle.Localtion="科技信徒";
                }
                Inarticle.Isdelete = 0;
                Inarticle.Createtime = DateTime.Now;

                await BLL.SqlSugarBLL<Model.Models.Comments>.baseDao.Add(Inarticle);
                int ?count = ++BLL.SqlSugarBLL<Model.Models.Article>.baseDao.GetModel(s => s.AID==Inarticle.Artcleid).Result.Msgcount;
                if(count is null)
                {
                    count = 0;
                    count++;
                }
                await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.Update(new Model.Models.Article() { Msgcount=count },s=>s.AID==Inarticle.Artcleid);
            }
            catch (Exception ex)
            {
                //Logger.Error(ex);
                Serilog.Log.Error($"【{Inarticle.Userid}】评论失败:"+ex.ToString());
                return Ok(UnifyResult.GetUnifyResult("发生未知异常", code: ResultCode.Error));
            }
            return Ok(UnifyResult.GetUnifyResult());
        }
    }
}
