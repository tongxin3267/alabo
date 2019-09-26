using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.RestfulApi;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Core.WebApis.Controller;
using Alabo.Regexs;
using ZKCloud.Open.ApiBase.Models;

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