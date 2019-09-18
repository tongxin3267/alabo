using System;
using Alabo.App.Core.ApiStore.CallBacks;
using Alabo.App.Core.ApiStore.MiniProgram.Dtos;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Connectors;
using ZKCloud.Open.ApiBase.Formatters;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.ApiStore.MiniProgram.Clients {

    public class MiniProgramClient : ApiStoreClient, IMiniProgramClient {
        private static readonly Func<IConnector> _connectorCreator = () => new HttpClientConnector();

        private static readonly Func<IDataFormatter> _formmaterCreator = () => new JsonFormatter();

        private static readonly Uri _baseUri = new Uri("https://api.weixin.qq.com/");

        /// <summary>
        ///     Initializes a new instance of the <see cref="MiniProgramClient" /> class.
        /// </summary>
        public MiniProgramClient()
            : base(_baseUri, _connectorCreator(), _formmaterCreator()) {
        }

        /// <summary>
        ///     Logins the asynchronous.
        /// </summary>
        /// <param name="miniProgramLoginInput">The mini program login input.</param>
        public ApiResult<SessionOutput> Login(LoginInput miniProgramLoginInput) {
            var miniProgram = Service<IAutoConfigService>().GetValue<MiniProgramConfig>();
            var loginUrl =
                $"/sns/jscode2session?appid={miniProgram.AppID}&secret={miniProgram.AppSecret}&js_code={miniProgramLoginInput.JsCode}&grant_type={miniProgramLoginInput.GrantType}";
            var url = BuildQueryUri(loginUrl);
            var result = Connector.Get(url);

            //如果请求错误，错误数据的格式示例：{"errcode":40029,"errmsg":"invalid code, hints: [ req_id: Hs2Q7a0732th50 ]"}
            if (result.Contains("errmsg") && result.Contains("errcode")) {
                var errorMessage = result.DeserializeJson<MiniErrorMessage>();
                var apiResult = new ApiResult {
                    Status = ResultStatus.Error,
                    Message = errorMessage.Errmsg,
                    MessageCode = errorMessage.Errcode.ConvertToInt()
                };
                return ApiResult.Failure<SessionOutput>(apiResult.ToJson());
            }

            var sessionOutput = result.DeserializeJson<SessionOutput>();
            return ApiResult.Success(sessionOutput);
        }

        public ApiResult<SessionOutput> PubLogin(LoginInput miniProgramLoginInput) {
            var miniProgram = Service<IAutoConfigService>().GetValue<WeChatPaymentConfig>();
            var loginUrl =
                $"/sns/oauth2/access_token?appid={miniProgram.AppId}&secret={miniProgram.AppSecret}&code={miniProgramLoginInput.JsCode}&grant_type=authorization_code";
            //var loginUrl =
            //    $"/sns/oauth2/access_token?appid=wx3845717402bcb006&secret=a977a30163b6c14516236a912842521b&code={miniProgramLoginInput.JsCode}&grant_type=authorization_code";
            var url = BuildQueryUri(loginUrl);
            var result = Connector.Get(url);

            //如果请求错误，错误数据的格式示例：{"errcode":40029,"errmsg":"invalid code, hints: [ req_id: Hs2Q7a0732th50 ]"}
            if (result.Contains("errmsg") && result.Contains("errcode")) {
                var errorMessage = result.DeserializeJson<MiniErrorMessage>();
                var apiResult = new ApiResult {
                    Status = ResultStatus.Error,
                    Message = errorMessage.Errmsg,
                    MessageCode = errorMessage.Errcode.ConvertToInt()
                };
                Service<IUserService>().Log($"公众号登录失败,code:{miniProgramLoginInput.JsCode},原因:{apiResult.ToJson()}");
                return ApiResult.Failure<SessionOutput>(apiResult.ToJson());
            }

            var sessionOutput = result.DeserializeJson<SessionOutput>();
            //Service<IUserService>().Log($"公众号登录成功,code:{miniProgramLoginInput.JsCode},openId:{sessionOutput.openid}");
            return ApiResult.Success(sessionOutput);
        }
    }
}