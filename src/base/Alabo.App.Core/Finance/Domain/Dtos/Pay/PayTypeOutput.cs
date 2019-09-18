using System;
using System.Collections.Generic;
using Alabo.App.Core.Finance.Domain.Enums;

namespace Alabo.App.Core.Finance.Domain.Dtos.Pay {

    /// <summary>
    ///     返回的支付方式列表
    /// </summary>
    public class PayTypeOutput {

        /// <summary>
        ///     提示文字
        ///     比如
        ///     显示非人民币的支付文字
        ///     扣除积分20 现金30 虚拟币203
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        ///     支付订单Id
        /// </summary>
        public long PayId { get; set; } = 0;

        /// <summary>
        ///     支付金额
        /// </summary>
        public decimal Amount { get; set; } = 0;

        /// <summary>
        ///     支付方式列表
        /// </summary>
        public List<PayTypeList> PayTypeList { get; set; } = new List<PayTypeList>();
    }

    /// <summary>
    ///     支付方式列表
    /// </summary>
    public class PayTypeList {

        /// <summary>
        ///     支付方式的Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     支付方式类型
        /// </summary>
        public PayType PayType { get; set; }

        /// <summary>
        ///     支付方式名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     支付方式描述
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        ///     支付方式对应的图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///     支付方式在客户端对应的JS事件
        ///     如果客户端需要的JS事件，可以通过这里个方法来实现
        /// </summary>
        public string Method { get; set; }
    }
}