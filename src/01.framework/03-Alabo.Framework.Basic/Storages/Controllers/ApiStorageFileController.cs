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