using Like.Model.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Like.LikeYou.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UploadFileController : BaseController
    {
        public UploadFileController(ISqlSugarClient db) : base(db)
        {
        }

        [Route("uploadfilePic")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> uploadfilePic([FromBody] Model.Models.UploadFile Inarticle)
        {
            Model.Models.UploadFile upload = new Model.Models.UploadFile();

            upload.Base64Str = Inarticle.Base64Str;
            upload.FileName = Inarticle.FileName;
            upload.ContentType = Inarticle.ContentType;

            //File


            await BLL.SqlSugarBLL<Model.Models.UploadFile>.baseDao.Add(upload);
           
            return Ok(UnifyResult.GetUnifyResult());
        }

        [Route("uploadfile")]
        [HttpPost]
        [Authorize]
        public async Task<UnifyResult> uploadfile(Model.Models.SourceFile Inarticle)
        {
            try
            {
                if (string.IsNullOrEmpty(Inarticle?.fifleurl))
                {
                    return new UnifyResult() { Message="上传文件失败,文件url不能为空", StatusCode=ResultCode.ValidError };
                }
                if (string.IsNullOrEmpty(Inarticle?.name))
                {
                    return new UnifyResult() { Message="上传文件失败,文件名不能为空", StatusCode=ResultCode.ValidError };
                }
                if (string.IsNullOrEmpty(Inarticle?.userid))
                {
                    return new UnifyResult() { Message="上传文件失败,上传用户不能为空", StatusCode=ResultCode.ValidError };
                }
                if (string.IsNullOrEmpty(Inarticle?.md5))
                {
                    return new UnifyResult() { Message="上传文件失败,md5不能为空", StatusCode=ResultCode.ValidError };
                }
                Model.Models.SourceFile count = await BLL.SqlSugarBLL<Model.Models.SourceFile>.baseDao.Add(Inarticle);
                return new UnifyResult() { Message="上传文件成功，id："+count.id, StatusCode=ResultCode.Success };
            }
            catch (Exception ex)
            {
                Serilog.Log.Error(ex.ToString());
            }
           

            return new UnifyResult() { Message="上传文件失败", StatusCode=ResultCode.ValidError };
        }

        [Route("Getuploadfile")]
        [HttpPost]
        [Authorize]
        public async Task<UnifyResult<Model.Result.PageMsg>> Getuploadfile(int index,int pagesize,string name)
        {
            Model.Result.PageMsg page = new Model.Result.PageMsg();
            try
            {
                RefAsync<int> totalCount = 0;
               List<Model.Models.SourceFile> sourceFiles=  BLL.SqlSugarBLL<Model.Models.SourceFile>.baseDao.GetPageList(s=>s.id!=0&&s.name.Contains(name),index,pagesize, totalCount).Result;
                page.data = sourceFiles;
                page.totalsize = totalCount;
                page.pagesize=pagesize;
                page.index=index;
                return new UnifyResult<Model.Result.PageMsg> { Data=page, Message="请求成功",StatusCode=ResultCode.Success };
            }catch (Exception ex)
            {
                Serilog.Log.Error(ex.ToString());
            }
            return new UnifyResult<Model.Result.PageMsg>(page, ResultCode.ValidError);
        }

        }
}
