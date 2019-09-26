using System.Collections.Generic;
using Alabo.Framework.Core.Enums.Enum;

namespace Alabo.App.Asset.Pays.Dtos
{
    /// <summary>
    ///     支付结果返回
    /// </summary>
    public class PayOutput
    {
        /// <summary>
        ///     账单处理状态
        /// </summary>
        public PayStatus Status { get; set; } = PayStatus.WaiPay;

        /// <summary>
        ///     返回信息，不同的支付方式，返回的结果不同
        ///     保存对象的json数据：比如支付宝支付保存 AlipayResponse.ToJson()
        ///     微信支付返回：WeChatPayResponse.ToJson()
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     支付Id
        /// </summary>
        public long PayId { get; set; }

        /// <summary>
        ///     签名
        /// </summary>
        public string Sign { get; set; }

        /// <summary>
        ///     支付实体的Id
        /// </summary>
        public IList<object> EntityIds { get; set; }

        /// <summary>
        ///     订单Id
        /// </summary>
        public object OrderId { get; set; }

        /// <summary>
        ///     支付成功以后前端跳转的Url
        /// </summary>
        public string Url { get; set; }
    }
}