using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Domain.Service;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common.Domain.Dtos;
using Alabo.App.Core.Common.Domain.Entities;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.Core.Regex;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using Alabo.Reflections;
using Alabo.RestfulApi;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Common.Controllers {

    /// <summary>
    ///     通用Api接口文档
    /// </summary>
    [ApiExceptionFilter]
    //[Produces("application/json")]
    //[Consumes("application/json", "multipart/form-data")] //此处为新增
    [Route("Api/Common/[action]")]
    public class ApiController : ApiBaseController {

        /// <summary>
        ///     Initializes a new instance of the <see cref="ApiAController" /> class.
        /// </summary>
        /// <param name="restClientConfig">The rest client configuration.</param>
        public ApiController(RestClientConfig restClientConfig

        ) : base() {
        }

        /// <summary>
        ///     根据枚举获取KeyValues，可以通过此来构建下拉菜单、picker。枚举值可在数据结构中查找，系统提供超过200以上的枚举：示范值：Status、访问/Admin/Table/List 查看所有枚举
        /// </summary>
        /// <param name="type"></param>
        [HttpGet]
        [Display(Description = "根据枚举获取KeyValues")]
        public ApiResult<IList<KeyValue>> GetKeyValuesByEnum([FromQuery] string type) {
            if (type.IsNullOrEmpty()) {
                ApiResult.Failure("类型不能为空");
            }

            var keyValues = KeyValueExtesions.EnumToKeyValues(type);
            if (keyValues == null) {
                return ApiResult.Failure<IList<KeyValue>>("枚举不存在");
            }

            return ApiResult.Success(keyValues);
        }

        /// <summary>
        ///     发送手机验证码
        /// </summary>
        /// <param name="mobile">手机号码</param>
        [HttpGet]
        [Display(Description = "发送手机验证码")]
        public ApiResult SendMobileVerifiyCode([FromQuery] string mobile) {
            if (!RegexHelper.CheckMobile(mobile)) {
                return ApiResult.Failure("手机号码格式不正确");
            }
            // var code = Ioc.Resolve<IOpenService>().GenerateVerifiyCode(mobile);

            //_messageManager.SendRaw(mobile, $"您的验证码是{code},请在5分钟内按页面提示提交验证码,切勿将验证码泄露于他人!");
            //return ApiResult.Success();

            //   var result = _messageManager.SendMobileVerifiyCode(mobile);

            return ToResult(null);
        }

        /// <summary>
        ///     发送手机验证码
        /// </summary>
        /// <param name="mobile">手机号码</param>
        /// <param name="message">短信内容</param>
        [HttpGet]
        [Display(Description = "发送短信")]
        public ApiResult SendMessage(string mobile, string message) {
            if (!RegexHelper.CheckMobile(mobile)) {
                return ApiResult.Failure("手机号码格式不正确");
            }
            // _messageManager.SendRaw(mobile, message);

            return ApiResult.Success();
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

        /// <summary>
        ///     获取AutoConfig,系统提供超过100以上的AutoConfig，可以在数据结构中查看：示范值：WebSiteConfig、MoneyTypeConfig、UserTypeConfig。访问/Admin/Table/List
        ///     查看所有自动配置
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpGet]
        [Display(Description = "获取AutoConfig")]
        public ApiResult<object> GetAutoConfig([FromQuery] string parameter) {
            if (parameter.IsNullOrEmpty()) {
                ApiResult.Failure("类型不能为空");
            }

            var configType = Resolve<IAutoConfigService>().GetTypeByName(parameter);
            if (configType == null) {
                return ApiResult.Failure<object>("输入参数不合法！");
            }

            var classPropertyAttribute = configType.GetAttribute<ClassPropertyAttribute>();
            if (classPropertyAttribute != null) {
                if (classPropertyAttribute.PageType == Domains.Enums.ViewPageType.Edit) {
                    var configValue = Resolve<IAutoConfigService>().GetValue(configType.FullName);
                    configValue = Resolve<IApiService>().InstanceToApiImageUrl(configValue);
                    return ApiResult.Success(configValue);
                }
            }
            return ApiResult.Failure<object>("输入参数不合法！或使用Api/GetAutoConfigList接口");
        }

        /// <summary>
        ///     获取AutoConfig,系统提供超过100以上的AutoConfig，可以在数据结构中查看：示范值：WebSiteConfig、MoneyTypeConfig、UserTypeConfig。访问/Admin/Table/List
        ///     查看所有自动配置
        /// </summary>
        /// <param name="parameter">参数</param>
        [HttpGet]
        [Display(Description = "获取AutoConfig")]
        public ApiResult<List<JObject>> GetAutoConfigList([FromQuery] string parameter) {
            if (parameter.IsNullOrEmpty()) {
                ApiResult.Failure("类型不能为空");
            }

            var configType = Resolve<IAutoConfigService>().GetTypeByName(parameter);
            if (configType == null) {
                return ApiResult.Failure<List<JObject>>("输入参数不合法！");
            }

            var classPropertyAttribute = configType.GetAttribute<ClassPropertyAttribute>();
            if (classPropertyAttribute != null) {
                if (classPropertyAttribute.PageType == Domains.Enums.ViewPageType.List) {
                    var configValue = Resolve<IAutoConfigService>().GetList(configType.FullName);
                    //var configJson = Resolve<IApiService>().InstanceToApiImageUrl(configValue.ToJson());
                    //configValue = configJson.ToObject<List<JObject>>();
                    return ApiResult.Success(configValue);
                }
            }
            return ApiResult.Failure<List<JObject>>("输入参数不合法！或使用Api/GetAutoConfig接口");
        }

        /// <summary>
        ///     根据AutoConfig获取KeyValues，可以通过此来构建下拉菜单、picker.示范值：MoneyTypeConfig、UserTypeConfig
        /// </summary>
        /// <param name="type"></param>
        [HttpGet]
        [Display(Description = "根据AutoConfig获取KeyValues")]
        public ApiResult<IList<KeyValue>> GetKeyValuesByAutoConfig([FromQuery] string type) {
            if (type.IsNullOrEmpty()) {
                return ApiResult.Failure<IList<KeyValue>>("类型不能为空");
            }

            var configValue = Resolve<ITypeService>().GetAutoConfigDictionary(type);
            IList<KeyValue> keyValues = new List<KeyValue>();
            foreach (var item in configValue) {
                var keyValue = new KeyValue {
                    Name = item.Value.ToStr(),
                    Key = item.Key,
                    Value = item.Value.ToStr()
                };
                keyValues.Add(keyValue);
            }

            return ApiResult.Success(keyValues);
        }

        /// <summary>
        /// 随机生成ObjectId
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult GetObjectId() {
            var id = ObjectId.GenerateNewId();
            return ApiResult.Success(id);
        }
    }
}