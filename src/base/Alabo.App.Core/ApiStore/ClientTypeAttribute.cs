using System;
using Alabo.App.Core.Finance.Domain.Enums;

namespace Alabo.App.Core.ApiStore {

    /// <summary>
    ///     运行的终端类型
    ///     值与ClientType 对应
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ClientTypeAttribute : Attribute {

        /// <summary>
        ///     允许终端类型的状态，多个状态用,隔开
        /// </summary>
        public string AllowClient { get; set; }

        /// <summary>
        ///     支付方式简介
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///     Gets or sets the type of the pay.
        /// </summary>
        /// <value>
        ///     The type of the pay.
        /// </value>
        public PayType PayType { get; set; }

        /// <summary>
        ///     支付方式名称
        ///     通过Display特性获取
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     通过特性Filed获取
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     客户端操作方法
        ///     比如IOs唤起微信支付
        /// </summary>
        public string Method { get; set; }
    }
}