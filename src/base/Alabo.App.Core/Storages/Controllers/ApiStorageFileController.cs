using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common.Domain.Entities;
using Alabo.App.Core.Common.Domain.Services;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Common.Domain.Dtos;
using ZKCloud.Open.ApiBase.Models;
using Alabo.Extensions;
using System;
using System.Linq;

namespace Alabo.App.Core.Common.Controllers {

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