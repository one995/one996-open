using Like.Model.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Like.LikeYou.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : BaseController
    {
        public HomeController(ISqlSugarClient db) : base(db)
        {
        }

        #region 首页- 热门文章
        [HttpPost]
        [Authorize]
        [Route("AddHotArtile")]
        public async Task<UnifyResult> AddHotArtile(Model.Models.ArtcleHot hot)
        {
            if (BLL.SqlSugarBLL<Model.Models.Article>.baseDao.GetModel(os=>os.AID==hot.artcleid.ToString()) is null)
            {
                return new UnifyResult("文章不存在！",ResultCode.Success);
            }
            if(  await BLL.SqlSugarBLL<Model.Models.ArtcleHot>.baseDao.Add(hot) is null)
            {
                return new UnifyResult("热门文章添加失败！", ResultCode.Error);
            }

            return new UnifyResult("文章不存在！", ResultCode.Success);
        }

        [HttpGet]
        [Authorize]
        [Route("HotArtile")]
        public async Task<List<Model.Models.ArtcleHot>> GetHotArtiles(int index=1,int pagesize=10)
        {
            RefAsync<int> totalCount = 0;
            return await BLL.SqlSugarBLL<Model.Models.ArtcleHot>.baseDao.GetPageList(os=>os.ID!=0,index,pagesize, totalCount);
        }
        #endregion

        #region 首页 设置

        [HttpPost]
        [Route("Set")]
        [Authorize]
        public async Task<IActionResult> AddSet([FromBody] Model.Models.HomeSet home)
        {
            
            if(home is null)
            {
                return Error("内容不能为空！", ResultCode.OperaParamNull);
            }
            var cou=await   BLL.SqlSugarBLL<Model.Models.HomeSet>.baseDao.Add(home);
            if (cou.Id>0)
            {
                return Success(new UnifyResult() { StatusCode=ResultCode.Success, Message="设置成功" });
            }
            else
            {
                return Error("设置失败", ResultCode.OperaParamNull);
            }

        }
        [HttpPut]
        [Route("Set")]
        [Authorize]
        public async Task<IActionResult> UpdateSet([FromBody] Model.Models.HomeSet home)
        {

            if (home is null)
            {
                   return Error("内容不能为空", ResultCode.OperaParamNull);
            }
            var cou = await BLL.SqlSugarBLL<Model.Models.HomeSet>.baseDao.Update(home);
            if (cou)
            {
                return Success(new UnifyResult() { StatusCode=ResultCode.Success, Message="设置成功" });
            }
            else
            {
                return Error("设置失败", ResultCode.OperaParamNull);
            }

        }

        [HttpGet]
        [Route("Set")]
        public async Task<IActionResult> Set()
        {
            var cou = await BLL.SqlSugarBLL<Model.Models.HomeSet>.baseDao.GetModel(s=>s.Id!=0);
            if (cou !=null)
            {
                return Success(new UnifyResult<Model.Models.HomeSet>(cou) { StatusCode=ResultCode.Success, Message="获取成功" });
            }
            else
            {
                return Error("获取失败", ResultCode.OperaParamNull);
            }
        }

        #endregion

        #region 文章浏览记录
        [HttpPost]
        [Authorize]
        [Route("Record")]
        public async Task<UnifyResult<Model.PageModel<Model.Models.ArtcleRecord>>> GetRecord(string UserID, int index, int pagesize)
        {
            int total = 0;

            List<Model.Models.ArtcleRecord> list_model = await BLL.SqlSugarBLL<Model.Models.ArtcleRecord>.baseDao.GetPageList(os => os.Userid==UserID, index, pagesize, total);
            Model.PageModel<Model.Models.ArtcleRecord> page = new Model.PageModel<Model.Models.ArtcleRecord>();
            page.data = list_model;
            page.dataCount = total;
            page.PageSize = pagesize;
            page.page = index;

            return new UnifyResult<Model.PageModel<Model.Models.ArtcleRecord>>(page);
        }
        [HttpPost]
        [Authorize]
        [Route("AddRecrd")]
        public async Task<UnifyResult> AddRecrd(Model.Models.ArtcleRecord record)
        {
            if (await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.GetModel(OS => OS.AID==record.ArticleID.ToString()) is null)
            {
                return new UnifyResult("文章不存在！", ResultCode.Error);
            }
            record.CreateTime =System.DateTime.Now;
            if (await BLL.SqlSugarBLL<Model.Models.ArtcleRecord>.baseDao.Add(record) is null)
            {
                return new UnifyResult("新增浏览记录失败！", ResultCode.Error);
            }
            return new UnifyResult("新增浏览记录成功", ResultCode.Success);
        }


        #endregion

        #region 收藏
        [HttpPost]
        [Authorize]
        [Route("addCollection")]
        public async Task<UnifyResult<Model.Models.Article>> AddLike(Model.Models.Collection collection)
        {
            //添加收藏
            await BLL.SqlSugarBLL<Model.Models.Collection>.baseDao.Add(collection);
            return new UnifyResult<Model.Models.Article>("收藏成功");
        }
        [HttpPost]
        [Authorize]
        [Route("DelCollection")]
        public async Task<UnifyResult<Model.Models.Article>> ReLike(long artid)
        {
            //移除收藏
            await BLL.SqlSugarBLL<Model.Models.Collection>.baseDao.Delete(new Model.Models.Collection() { Id = artid }); 
            return new UnifyResult<Model.Models.Article>("移除成功");
        }

        [HttpPost]
        [Authorize]
        [Route("getCollection")]
        public async Task<UnifyResult<Model.Models.Article>> GetLike(long UserID)
        {
            return new UnifyResult<Model.Models.Article>(await BLL.SqlSugarBLL<Model.Models.Collection>.baseDao.GetModel((s)=>s.Userid==UserID));
        }
        #endregion

        #region 评论

        [HttpPost]
        [Authorize]
        [Route("AddComments")]
        public async Task<UnifyResult> AddComments(Model.Models.Comments comments)
        {
            if(await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.GetModel(OS=>OS.AID== comments.Artcleid.ToString()) is null)
            {
                return new UnifyResult("评论文章不存在！", ResultCode.OperaParamNull);
            }

            if(string.IsNullOrEmpty(comments.Commentsmsg)) { return new UnifyResult("评论不能为空！", ResultCode.OperaParamNull); }

            comments.Createtime =System.DateTime.Now;
            if (await BLL.SqlSugarBLL<Model.Models.Comments>.baseDao.Add(comments) is null)
            {
                return new UnifyResult("评论文章失败！", ResultCode.OperaParamNull);
            }
            return  new UnifyResult("评论成功",ResultCode.Success);

        }
        [HttpPost]
        [Authorize]
        [Route("DelComments")]
        public async Task<UnifyResult> DelComments(int id)
        {
            if(await BLL.SqlSugarBLL<Model.Models.Comments>.baseDao.Delete(new Model.Models.Comments() { Id=id }))
            {
                return new UnifyResult("评论删除成功",ResultCode.Success);
            }

            return  new UnifyResult("评论删除失败", ResultCode.Error);
        }
        #endregion

        #region 点赞表

        [HttpPost]
        [Authorize]
        [Route("AddArtLike")]
        public async Task<UnifyResult> AddArtLike(Model.Models.ArtcleLike artcle)
        {
            if(await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.GetModel(OS=>OS.AID==artcle.artcleid.ToString()) is null)
            {
                return new UnifyResult("文章不存在！",ResultCode.Error);
            }
            artcle.createtime =System.DateTime.Now;
            if(await BLL.SqlSugarBLL<Model.Models.ArtcleLike>.baseDao.Add(artcle) is null)
            {
                return new UnifyResult("点赞失败！", ResultCode.Error);
            }
            return new UnifyResult("点赞成功", ResultCode.Success);
        }


        #endregion

        #region  转发表
        [HttpPost]
        [Authorize]
        [Route("AddForwarding")]
        public async Task<UnifyResult> AddForwarding(Model.Models.Forwarding forwarding)
        {
            if (await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.GetModel(OS => OS.AID==forwarding.Artcleid.ToString()) is null)
            {
                return new UnifyResult("文章不存在！", ResultCode.Error);
            }
            forwarding.Createtime =System.DateTime.Now;
            if (await BLL.SqlSugarBLL<Model.Models.Forwarding>.baseDao.Add(forwarding) is null)
            {
                return new UnifyResult("转发失败！", ResultCode.Error);
            }
            return new UnifyResult("转发成功", ResultCode.Success);

        }


        #endregion


        #region 文章数据统计
        [HttpGet]
        [Route("DayData")]
        public async Task<IActionResult> GetDayData()
        {
            int Arttotal=  await  BLL.SqlSugarBLL<Model.Models.Article>.baseDao.Count(s=>s.Isdelete==0);
            int ischeckcount = await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.Count(s => s.Isdelete==0&&s.IsCheck==1);

            int questioncount = await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.Count(s => s.Isdelete==0&&s.AritcleType==1 );
            int Artcount = await BLL.SqlSugarBLL<Model.Models.Article>.baseDao.Count(s => s.Isdelete==0&&s.AritcleType==0);
            //int UserCount =await BLL.SqlSugarBLL<Model.User>.baseDao.Count(s => s.isdelete==0);
            //int DayUserCount = await BLL.SqlSugarBLL<Model.User>.baseDao.Count(s => s.isdelete==0);

            return Json(new UnifyResult<Model.Models.HomeSet>(new { Arttotal = Arttotal, checkcount = ischeckcount, notcheck = (Arttotal-ischeckcount), questioncount= questioncount, Artcount= Artcount }) { StatusCode=ResultCode.Success, Message="获取成功" } );

        }

        [HttpGet]
        [Route("DayUser")]
        public async Task<IActionResult> GetDayUser()
        {

            int UserCount =await BLL.SqlSugarBLL<Model.User>.baseDao.Count(s => s.isdelete==0);
         
            var list = db.Queryable<Model.User>()
            .Select(it => new {
                name = it.PersonID,
                year = it.CreateTime.Value.Year,
                month = it.CreateTime.Value.Month
            })
            .MergeTable()//将查询出来的结果合并成一个新表
            .GroupBy(it => new { it.year, it.month })//对新表进行分组
            .Select(it => new {
                date = SqlFunc.MergeString(it.year.ToString(), "-", it.month.ToString()),
                count = SqlFunc.AggregateCount(it.name)
            }).
            ToList();

            return Json(new UnifyResult<Model.Models.HomeSet>(new { UserTotal = UserCount, DayUserCount = list }) { StatusCode=ResultCode.Success, Message="获取成功" });

        }

        #endregion
    }
}
