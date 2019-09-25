using Alabo.App.Core.ApiStore.MiniProgram.Clients;
using Alabo.App.Core.ApiStore.MiniProgram.Dtos;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Services;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Core.ApiStore.MiniProgram.Services {

    public class MiniProgramService : ServiceBase, IMiniProgramService {
        private readonly IMiniProgramClient _messageApiClient;

        public MiniProgramService(IUnitOfWork unitOfWork) : base(unitOfWork) {
            _messageApiClient = new MiniProgramClient();
        }

        public ApiResult<LoginOutput> Login(LoginInput loginInput) {
            //var loginOutput = new LoginOutput();
            //var apiResult = _messageApiClient.Login(loginInput);
            //if (apiResult.Status == ResultStatus.Success) {
            //    loginOutput.Session = apiResult.Result;
            //    if (!loginOutput.Session.openid.IsNullOrEmpty()) {
            //        /// 如果有注册会员则返回注册会员
            //        var userDetail = Resolve<IUserDetailService>()
            //            .GetSingle(r => r.OpenId == loginOutput.Session.openid);
            //        if (userDetail != null) {
            //            loginOutput.IsReg = true;
            //            var user = Resolve<IUserService>().GetSingle(userDetail.UserId);
            //            loginOutput.User = Resolve<IUserDetailService>().GetUserOutput(user.Id);
            //        }
            //    }
            //} else {
            //    var message = apiResult.Message;
            //    return ApiResult.Failure<LoginOutput>($"登陆到微信服务器失败:{message}", MessageCodes.ReremoteRequest);
            //}

            //return ApiResult.Success(loginOutput);
            return null;
        }

        public ApiResult<LoginOutput> PubLogin(LoginInput loginInput) {
            var loginOutput = new LoginOutput();
            var apiResult = _messageApiClient.PubLogin(loginInput);
            if (apiResult.Status == ResultStatus.Success) {
                loginOutput.Session = apiResult.Result;
                if (!loginOutput.Session.openid.IsNullOrEmpty()) {
                    // 如果有注册会员则返回注册会员
                    //var userDetail = Resolve<IUserDetailService>().GetSingle(r => r.OpenId == loginOutput.Session.openid);
                    //if (userDetail != null) {
                    //    loginOutput.IsReg = true;
                    //    var user = Resolve<IUserService>().GetSingle(userDetail.UserId);
                    //    loginOutput.User = Resolve<IUserDetailService>().GetUserOutput(user.Id);
                    //}
                }
            } else {
                var message = apiResult.Message;
                return ApiResult.Failure<LoginOutput>($"登陆到微信服务器失败:{message}", MessageCodes.ReremoteRequest);
            }

            return ApiResult.Success(loginOutput);
        }
    }
}