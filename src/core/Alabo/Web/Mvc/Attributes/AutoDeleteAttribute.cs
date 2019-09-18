using System;

namespace Alabo.Web.Mvc.Attributes
{
    /// <summary>
    ///     自动删除配置特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoDeleteAttribute : Attribute
    {
        /// <summary>
        ///     管理的实体
        ///     如果未配置关联的实体，则数据不能删除
        /// </summary>
        public Type EntityType { get; set; }

        /// <summary>
        ///     是否可以自动删除
        ///     如果entityType为空，且IsAuto=true的时候，可以自动删除数据
        /// </summary>
        public bool IsAuto { get; set; } = false;

        /// <summary>
        ///     关联的ID
        /// </summary>
        public string RelationId { get; set; }
    }
}