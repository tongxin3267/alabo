using Alabo.Domains.Enums;
using System;

namespace Alabo.Web.Mvc.Attributes
{
    /// <summary>
    ///     类属性特性信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum)]
    public class ClassPropertyAttribute : Attribute
    {
        public ClassPropertyAttribute() {
        }

        public ClassPropertyAttribute(string icon, string description) {
            this.Icon = icon;
            this.Description = description;
        }

        /// <summary>
        ///     显示在导航上的图标
        /// </summary>
        public string Icon { get; set; } = "flaticon-interface-8 ";

        /// <summary>
        /// 是否显示快捷操作按钮
        /// </summary>
        public bool IsShowQuickAction { get; set; } = true;

        /// <summary>
        ///     显示名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     视图样式，用于指定用来显示那个界面
        /// </summary>
        public ViewPageType PageType { get; set; } = ViewPageType.Edit;

        /// <summary>
        ///     显示说明,该值用于在用户界面中
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     该值用于在用户界面中对字段进行分组
        ///     多个分组用,隔开
        /// </summary>
        public string GroupName { get; set; } = string.Empty;

        /// <summary>
        ///     排序，从小到大排列
        /// </summary>
        public long SortOrder { get; set; } = 1000;

        /// <summary>
        ///     标记
        ///     比如Mark=0 表示分类
        ///     Mark=1 表示标签
        /// </summary>
        public int Mark { get; set; } = 0;

        /// <summary>
        ///     存储SQL语句用于AutoConfig 删除时验证
        /// </summary>
        public string Validator { get; set; }

        /// <summary>
        ///     验证信息
        /// </summary>
        public string ValidateMessage { get; set; }
    }
}