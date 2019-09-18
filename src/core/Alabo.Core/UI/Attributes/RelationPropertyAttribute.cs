using System;
using Alabo.Core.Enums.Enum;

namespace Alabo.Core.UI.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class RelationPropertyAttribute : Attribute
    {
        public RelationType RelationType { get; set; }

        /// <summary>
        ///     只有一级类目，不能添加子级类目
        /// </summary>
        public bool IsOnlyRoot { get; set; } = false;
    }
}