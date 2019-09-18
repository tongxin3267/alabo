﻿using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Entities;
using System;
using Alabo.App.Core.ApiStore.CallBacks;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.Domains.Base.Services;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Helpers;

namespace Alabo.App.Core.ApiStore.WeiXinMp.Clients {

    /// <summary>
    ///
    /// </summary>
    public class WeiXinMpClient : IWeixinMpClient {

        public JsApiTicketResult GetTicketByAccessToken() {
            var token = Ioc.Resolve<IWeixinMpClient>().GetToken();
            if (token == null) {
                throw new ValidException("access_token获取失败");
            }

            if (token.access_token.IsNullOrEmpty()) {
                Ioc.Resolve<ITableService>().Log("微信分享接口获取错误" + token.errmsg);
                throw new SystemException(token.errmsg);
            }

            return CommonApi.GetTicketByAccessToken(token.access_token);
            //return   Ioc.Resolve<IObjectCache>().GetOrSet(() =>
            //  {
            //      var token = Ioc.Resolve<IWeixinMpClient>().GetToken();
            //      if (token == null)
            //      {
            //          throw new ValidException("access_token获取失败");
            //      }

            //      if (token.access_token.IsNullOrEmpty())
            //      {
            //          Ioc.Resolve<ITableService>().Log("微信分享接口获取错误" + token.errmsg);
            //          throw new SystemException(token.errmsg);
            //      }

            //      return CommonApi.GetTicketByAccessToken(token.access_token);

            //  }, "GetTicketByAccessToken", TimeSpan.FromHours(1)).Value;
        }

        public AccessTokenResult GetToken() {
            var wexinConfig = Ioc.Resolve<IAutoConfigService>().GetValue<WeChatPaymentConfig>();
            var token = CommonApi.GetToken(wexinConfig.AppId, wexinConfig.AppSecret);

            return token;
        }

        public WeixinUserInfoResult GetUserInfo(string openId) {
            return CommonApi.GetUserInfo("", openId);
        }
    }
}