using System;
using Alabo.Framework.Core.Enums.Enum;

namespace Alabo.Framework.Core.Reflections.Interfaces {

    /// <summary>
    ///     级联数据继承接口
    /// </summary>
    public interface IRelation {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class RelationPropertyAttribute : Attribute {
        public RelationType RelationType { get; set; }

        /// <summary>
        ///     只有一级类目，不能添加子级类目
        /// </summary>
        public bool IsOnlyRoot { get; set; } = false;
    }
}