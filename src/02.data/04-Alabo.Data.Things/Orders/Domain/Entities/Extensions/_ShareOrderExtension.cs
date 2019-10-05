using Alabo.Domains.Entities.Extensions;
using System;
using System.Collections.Generic;

namespace Alabo.Data.Things.Orders.Domain.Entities.Extensions
{
    /// <summary>
    ///     Class ShareOrderExtension.
    ///     分润订单扩展
    /// </summary>
    /// <seealso cref="Alabo.Domains.Entities.Extensions.EntityExtension" />
    public class ShareOrderExtension : EntityExtension
    {
        /// <summary>
        ///     Gets or sets the task message.
        ///     模块执行信息记录
        /// </summary>
        /// <value>The task message.</value>
        public IList<TaskMessage> TaskMessage { get; set; } = new List<TaskMessage>();

        /// <summary>
        ///     备注
        /// </summary>
        public string Remark { get; set; }
    }

    /// <summary>
    ///     执行消息
    /// </summary>
    public class TaskMessage
    {
        /// <summary>
        ///     模块Id
        /// </summary>
        public Guid ModuleId { get; set; }

        /// <summary>
        ///     类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        ///     模块名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        ///     配置名称
        /// </summary>
        public string ConfigName { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;

        public string Message { get; set; }
    }
}