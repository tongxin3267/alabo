using System;

namespace Alabo.App.Core.UserType {

    [AttributeUsage(AttributeTargets.Class)]
    public class UserTypeModulesAttribute : Attribute {

        /// <summary>
        ///     模块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     模块的详细说明
        /// </summary>
        public string Intro { get; set; } = "请在模块特性里头填写活动详情";

        public string Icon { get; set; } = "icon-social-dribbble ";

        /// <summary>
        ///     配置名称（获取数据用）
        /// </summary>
        public string Config { get; set; }

        /// <summary>
        ///     图标对应的背景颜色
        /// </summary>
        public string BackGround { get; set; } = "bg-green-seagreen";

        /// <summary>
        ///     排序，从小到大排列
        /// </summary>
        public long SortOrder { get; set; } = 1000;
    }
}