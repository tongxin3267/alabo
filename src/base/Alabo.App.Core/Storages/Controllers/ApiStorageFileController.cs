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
        ///     �ļ��ϴ�
        /// </summary>
        /// <param name="parameter">����</param>
        [HttpPost]
        [Display(Description = "��ȡ�ϴ�״̬")]
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
                        return ApiResult.Failure<StorageFile>("��׺��֧��!");
                    }
                }

                var info = Resolve<IStorageFileService>().Upload(formFile, parameter.SavePath); //��ȡ�ϴ�״̬
                return ApiResult.Success(info);
            } catch (Exception e) {
                return ApiResult.Failure<StorageFile>(e.Message);
            }
        }
    }
}