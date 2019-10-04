namespace Alabo.UI {

    /// <summary>
    ///     自动页面设置
    /// </summary>
    public class AutoSetting {

        /// <summary>
        ///     Api接口地址
        /// </summary>
        public string ApiUrl { get; set; }

        /// <summary>
        ///     页面名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        public string Icon { get; set; } = UIHelper.Icon;

        /// <summary>
        ///     简介
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        ///     有哪儿
        /// </summary>
        public string Color { get; set; }
    }
}