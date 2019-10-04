using Alabo.Framework.Tasks.Queues.Domain.Enums;
using Alabo.Framework.Tasks.Schedules.Domain.Enums;
using System;
using ZKCloud.Open.Share.Enums;

namespace Alabo.Framework.Tasks.Queues.Models {

    /// <summary>
    ///     Class TaskModuleAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TaskModuleAttribute : Attribute {

        /// <summary>
        ///     Initializes a new instance of the <see cref="TaskModuleAttribute" /> class.
        /// </summary>
        /// <param name="id">Id标识</param>
        /// <param name="name">The name.</param>
        /// <exception cref="ArgumentException"></exception>
        public TaskModuleAttribute(string id, string name) {
            if (!Guid.TryParse(id, out var idGuid)) {
                throw new ArgumentException($"can not convert id {id} to guid.");
            }

            Id = idGuid;
            Name = name;
        }

        /// <summary>
        ///     模块Id，此Id具有需要全局唯一，为获取对应数据库配置的id
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        ///     模块名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     配置类型
        /// </summary>
        public Type ConfigurationType { get; set; }

        /// <summary>
        ///     分润关系所依赖的关系图
        /// </summary>
        public RelationshipType RelationshipType { get; set; }

        /// <summary>
        ///     分润方式
        /// </summary>
        public FenRunResultType FenRunResultType { get; set; } = FenRunResultType.Price;

        /// <summary>
        ///     分润维度说明
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        ///     分润
        /// </summary>
        public long SortOrder { get; set; }

        /// <summary>
        ///     Gets or sets the icon.
        ///     图标
        /// </summary>
        public string Icon { get; set; } = "flaticon-signs-2";

        /// <summary>
        ///     是否支持多个配置
        /// </summary>
        public bool IsSupportMultipleConfiguration { get; set; } = false;

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is support set distri ratio.
        ///     是否支持设置分润比例
        ///     某些维度不需要设置分润比例，则设置为false,在编辑页面将不显示分润比例。比如极差
        /// </summary>
        public bool IsSupportSetDistriRatio { get; set; } = true;

        /// <summary>
        ///     Gets or sets the circulation 类型.
        /// </summary>
        public TaskQueueType CirculationType { get; set; } = TaskQueueType.Once;

        /// <summary>
        ///     Gets or sets the execute 类型.
        /// </summary>
        public CallType ExecuteType { get; set; } = CallType.Common;
    }
}