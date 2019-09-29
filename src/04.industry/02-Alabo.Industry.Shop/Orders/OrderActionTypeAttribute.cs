using System;
using Alabo.UI.Enum;

namespace Alabo.Industry.Shop.Orders
{
    /// <summary>
    ///     订单操作方式
    ///     更加状态自动判断订单是否可以操作
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class OrderActionTypeAttribute : Attribute
    {
        /// <summary>
        ///     界面的操作方法
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        ///     操作名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     简介
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        ///     显示颜色
        /// </summary>
        public string Color { get; set; } = "metal";

        /// <summary>
        ///     允许操作的状态，多个状态用,隔开
        /// </summary>
        public string AllowStatus { get; set; }

        /// <summary>
        ///     使用者,
        ///     -1:未使用(默认)
        ///     0:用户
        ///     1:店家
        ///     2:管理员
        ///     3:线下商店
        /// </summary>
        public int AllowUser { get; set; } = -1;

        /// <summary>
        ///     操作类型 用于前端判断
        /// </summary>
        public string Type { get; set; }


        /// <summary>
        ///     图标
        /// </summary>
        public FontAwesomeIcon Icon { get; set; } = FontAwesomeIcon.Neuter;
    }
}