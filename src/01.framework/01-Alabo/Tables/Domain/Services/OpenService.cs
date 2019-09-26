using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Base.Client;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Helpers;
using ZKCloud.Open.ApiBase.Configuration;
using ZKCloud.Open.ApiBase.Models;
using ZKCloud.Open.ApiBase.Services;
using ZKCloud.Open.Message.Services;

namespace Alabo.Domains.Base.Services
{
    public class OpenService : ServiceBase, IOpenService
    {
        private static readonly string _CodeCacheKey = "CodeCacheKey_";
        private IAdminMeesageApiClient _adminMessageApiClient;
        private IOpenMessageClient _messageApiClient;
        private RestClientConfiguration _restClientConfiugration;
        private IServerApiClient _serverApiClient;

        public OpenService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public string OpenMobile => HttpWeb.Site.Phone;

        public ApiResult SendRaw(string mobile, string message)
        {
            //验证手机
            if (!CheckMobile(mobile)) return ApiResult.Failure();
            //验证消息是否为空
            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException(nameof(message));

            //先使用v2
            var res = Ioc.Resolve<ISmsService>().Sent(mobile, message);
            if (res.Code.ToUpper().Equal("SUCCESS")) return ApiResult.Success();
            //记录错误
            Ioc.Resolve<ITableService>().Log($"V2=>{mobile},{message};res=>{res.ToJson()}", LogsLevel.Error);
            //如果发送不成功尝试使用V1版本
            // 循环发送五次,确保发送率更高

            ////令牌未获取
            //if (_serverAuthenticationManager.Token == null || _serverAuthenticationManager.Token.Token.IsNullOrEmpty()) {
            //    var msg = "短信发送失败，未授权" + mobile + message;
            //    Ioc.Resolve<ITableService>().Log(msg, LogsLevel.Warning);
            //    // 重新授权一次，反正授权失败
            //    Authentication();
            //    BaseService();
            //}

            ////令牌过期
            //if (_serverAuthenticationManager.Token.ExpiresTime < DateTime.Now) {
            //    var msg = "短信发送失败，授权已过期" + mobile + message;
            //    Ioc.Resolve<ITableService>().Log(msg, LogsLevel.Warning);
            //    //令牌过期 重新授权一次
            //    Authentication();
            //    BaseService();
            //}

            ////如果两次判断令牌还没 则直接返回
            //if (_serverAuthenticationManager.Token == null || _serverAuthenticationManager.Token.Token.IsNullOrEmpty())
            //    return ApiResult.Failure();
            //var count = 0;
            //while (count < 5) {
            //    count++;
            //    //发送
            //    var result = _messageApiClient.SendRaw(_serverAuthenticationManager.Token.Token, mobile, message,
            //        HttpWeb.Ip);
            //    if (result.Status != ResultStatus.Success) {
            //        var msg =
            //            $"授权服务端短信发送失败:发送次数{count},{result.Message},_serverAuthenticationManager.Token.:{_serverAuthenticationManager.Token},_serverAuthenticationManager.Token.Token:{_serverAuthenticationManager.Token.Token}";
            //        Ioc.Resolve<ITableService>().Log(msg, LogsLevel.Error);
            //    } else {
            //        return ApiResult.Success();
            //    }
            //}

            return ApiResult.Failure();
        }

        public async Task SendRawAsync(string mobile, string message)
        {
            if (!CheckMobile(mobile)) return;

            if (string.IsNullOrWhiteSpace(message)) throw new ArgumentNullException(nameof(message));
            //先尝试发送V2版本
            var res = Ioc.Resolve<ISmsService>().Sent(mobile, message);
            if (res.Code.ToUpper().Equal("SUCCESS")) return;
            //记录V2发送的错误
            Ioc.Resolve<ITableService>().Log("V2=>" + res.ToJson(), LogsLevel.Error);
            ////如果v2发送失败则用v1
            //var result = await _messageApiClient.SendRawAsync(_serverAuthenticationManager.Token.Token,
            //    mobile,
            //    message, HttpWeb.Ip);
            //if (result.Status != ResultStatus.Success)
            //    throw new ServerApiException(result.Status, result.MessageCode, result.Message);
        }

        public bool CheckMobile(string mobile)
        {
            if (mobile.IsNullOrEmpty()) return false;

            if (mobile.Length != 11) return false;

            var rg = new Regex(@"^0?(13[0-9]|15[0-9]|18[0-9]|17[0-9]|19[0-9]|16[0-9]|14[0-9])[0-9]{8}$");
            var m = rg.Match(mobile);
            return m.Success;
        }

        #region 验证码

        /// <summary>
        ///     生成验证码
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public long GenerateVerifiyCode(string mobile)
        {
            //var messageAccount = Ioc.Resolve<IMessageAccountService>().GetSingle(projectId);
            //if (messageAccount == null) return 0;
            var cacheKey = _CodeCacheKey + $"{mobile}";
            ObjectCache.Remove(cacheKey);
            var code = new Random().Next(100001, 999999);
            var verifiyCode = new VerifiyCode
            {
                Code = code,
                CreateTime = DateTime.Now,
                Mobile = mobile
            };
            ObjectCache.Set(cacheKey, verifiyCode);
            return verifiyCode.Code;
        }

        /// <summary>
        ///     核对验证码
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public bool CheckVerifiyCode(string mobile, long code)
        {
            VerifiyCode verifiyCodeRes = null;
            var cacheKey = _CodeCacheKey + $"{mobile}";
            if (ObjectCache.TryGet(cacheKey, out VerifiyCode verifiyCode))
            {
                verifiyCodeRes = verifiyCode;
                if (verifiyCode.Code == code)
                {
                    ObjectCache.Remove(cacheKey);
                    if (verifiyCode.CreateTime > DateTime.Now.AddMinutes(-30)) return true;
                }
            }

            return false;
        }

        #endregion 验证码
    }

    public class VerifiyCode
    {
        public long Code { get; set; }
        public DateTime CreateTime { get; set; }
        public string Mobile { get; set; }
    }
}