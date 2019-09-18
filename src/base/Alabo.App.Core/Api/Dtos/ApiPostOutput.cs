using Alabo.Domains.Entities;
using Alabo.Domains.Query.Dto;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.Api.Dtos {

    /// <summary>
    ///     通用Post数据提交接口
    /// </summary>
    public class ApiPostOutput : EntityDto {

        public ApiPostOutput() {
        }

        /// <summary>
        ///     构建返回值
        /// </summary>
        /// <param name="serviceResult"></param>
        public ApiPostOutput(ServiceResult serviceResult) {
            Succeeded = serviceResult.Succeeded;
            ErrorMessages = serviceResult.ErrorMessages?.ToString();
            ReturnMessage = serviceResult.ReturnMessage;
        }

        public ApiPostOutput(bool resut) {
            Succeeded = resut;
        }

        /// <summary>
        ///     Post数据结构是否成功
        /// </summary>
        public bool Succeeded { get; }

        /// <summary>
        ///     返回信息
        /// </summary>

        public string RetrunMessage { get; set; }

        public string ErrorMessages { get; }

        public string ReturnMessage { get; }

        public static ApiPostOutput Success { get; } = new ApiPostOutput(true);

        public static ApiPostOutput Failed { get; } = new ApiPostOutput(false);

        public ApiResult<ApiPostOutput> PostResult {
            get {
                if (Succeeded) {
                    return ApiResult.Success(this);
                }

                return ApiResult.Failure(this);
            }
        }

        public override string ToString() {
            if (ErrorMessages == null) {
                return string.Empty;
            }

            return string.Join("<br />", ErrorMessages);
        }

        public static ApiPostOutput FailedWithMessage(string message) {
            return FailedWithMessage(message);
        }
    }
}