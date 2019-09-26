using System;
using Alabo.Data.Things.Orders.Domain.Entities;
using Alabo.Users.Entities;

namespace Alabo.Data.Things.Orders.ResultModel
{
    /// <summary>
    ///     分润执行后的结果
    /// </summary>
    public class ShareResult
    {
        /// <summary>
        ///     订单用户
        /// </summary>
        public User OrderUser { get; set; }

        /// <summary>
        ///     分润用户
        /// </summary>
        public User ShareUser { get; set; }

        /// <summary>
        ///     分润订单
        /// </summary>

        public ShareOrder ShareOrder { get; set; }

        /// <summary>
        ///     货币类型ID
        /// </summary>
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     模块Id
        /// </summary>
        public Guid ModuleId { get; set; }

        /// <summary>
        ///     介绍
        /// </summary>
        public string Intro { get; set; }

        public long ModuleConfigId { get; set; }

        public decimal Amount { get; set; }

        /// <summary>
        ///     是否开启短信通知
        /// </summary>
        public bool SmsNotification { get; set; }

        /// <summary>
        ///     短信通知模板
        /// </summary>
        public string SmsIntro { get; set; }
    }
}