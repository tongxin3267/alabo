using Alabo.Extensions;
using Alabo.Framework.Basic.Storages.Domain.Entities;
using Alabo.Framework.Basic.Storages.Domain.Services;
using Alabo.Framework.Basic.Storages.Dtos;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Basic.Storages.Controllers {

    [ApiExceptionFilter]
    [Route("Api/StorageFile/[action]")]
    public class ApiStorageFileController : ApiBaseController<StorageFile, ObjectId> {

        public ApiStorageFileController() {
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
                    if (!parameter.FileType.Split(',').ToList().Contains(Path.GetExtension(item.FileName))) {
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