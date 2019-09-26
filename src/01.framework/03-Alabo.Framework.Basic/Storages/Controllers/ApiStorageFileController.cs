using Alabo.App.Core.Common.Domain.Dtos;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Extensions;
using Alabo.Framework.Basic.Relations.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.Common.Controllers
{

    [ApiExceptionFilter]
    [Route("Api/StorageFile/[action]")]
    public class ApiStorageFileController : ApiBaseController<StorageFile, ObjectId> {

        public ApiStorageFileController() : base() {
            BaseService = Resolve<IStorageFileService>();
        }

        /// <summary>
        ///     文件上传
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpPost]
        [Display(Description = "获取上传状态")]
        public ApiResult<StorageFile> Upload(UploadApiInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure<StorageFile>(this.FormInvalidReason(),
                    MessageCodes.ParameterValidationFailure);
            }

            if (parameter.SavePath.IsNullOrEmpty()) {
                parameter.SavePath = "/uploads/api/";
            }

            try {
                var formFile = Request.Form.Files;

                foreach (var item in formFile) {
                    if (!parameter.FileType.Split(',').ToList().Contains(System.IO.Path.GetExtension(item.FileName))) {
                        return ApiResult.Failure<StorageFile>("后缀不支持!");
                    }
                }

                var info = Resolve<IStorageFileService>().Upload(formFile, parameter.SavePath); //获取上传状态
                return ApiResult.Success(info);
            } catch (Exception e) {
                return ApiResult.Failure<StorageFile>(e.Message);
            }
        }
    }
}