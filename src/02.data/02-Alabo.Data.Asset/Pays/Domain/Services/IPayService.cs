using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Alabo.App.Core.ApiStore;
using Alabo.App.Core.Finance.Domain.Dtos.Pay;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Core.Finance.Domain.Services {

    public interface IPayService : IService<Pay, long> {

        /// <summary>
        ///     第三方支付，或者余额账户现金支付
        /// </summary>
        /// <param name="payInput"></param>
        /// <param name="httpContext"></param>
        Tuple<ServiceResult, PayOutput> Pay(PayInput payInput, HttpContext httpContext = null);

        /// <summary>
        /// 微信app支付
        /// </summary>
        /// <param name="pay"></param>
        /// <param name="url"></param>
        /// <param name="serviceUrl"></param>
        /// <returns></returns>
        Tuple<ServiceResult, object> WechatAppPayment(ref Pay pay, string url, string serviceUrl);

        /// <summary>
        ///     获取所有支付方式的特性
        ///     通过缓存方式获取
        /// </summary>
        IList<ClientTypeAttribute> GetAllPayClientTypeAttribute();

        /// <summary>
        ///     获取可用的支付方式
        /// </summary>
        /// <param name="parameter"></param>
        Tuple<ServiceResult, PayTypeOutput> GetPayType(ClientInput parameter);

        /// <summary>
        ///     根据Id获取支付账单
        /// </summary>
        /// <param name="id">主键ID</param>
        Pay GetSingle(long id);

        /// <summary>
        ///     支付完成后的操作
        /// </summary>
        /// <param name="pay"></param>
        /// <param name="isSucess"></param>
        ServiceResult AfterPay(Pay pay, bool isSucess);

        /// <summary>
        ///     Gets the page list.
        /// </summary>
        /// <param name="query">查询</param>
        /// <returns>PagedList&lt;Pay&gt;.</returns>
        PagedList<Pay> GetPageList(object query);

        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="pay"></param>
        /// <param name="fee"></param>
        /// <param name="refundNo"></param>
        /// <returns></returns>
        Tuple<ServiceResult, string> Refund(ref Pay pay, int fee, string refundNo);
    }
}