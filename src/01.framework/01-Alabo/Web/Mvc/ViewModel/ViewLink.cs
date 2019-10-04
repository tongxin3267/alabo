namespace Alabo.Web.Mvc.ViewModel {

    /// <summary>
    ///     视图页面链接
    /// </summary>
    public class ViewLink {

        /// <summary>
        ///     Initializes a new instance of the <see cref="ViewLink" /> class.
        /// </summary>
        public ViewLink() {
        }

        public ViewLink(string name, string url) {
            Name = name;
            Url = url;
        }

        /// <summary>
        ///     构造链接
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="url">The URL.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="linktype">The linktype.</param>
        public ViewLink(string name, string url, string icon, LinkType linktype) {
            Name = name;
            Url = url;
            Icon = icon;
            LinkType = linktype;
        }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     URL
        ///     支持参数
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     图标名称
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///     视图类型
        /// </summary>
        public LinkType LinkType { get; set; }
    }

    /// <summary>
    ///     链接类型
    /// </summary>
    public enum LinkType {

        /// <summary>
        ///     表格页面右上角快捷导航
        /// </summary>
        TableQuickLink = 1,

        /// <summary>
        ///     表单页面右上角快捷导航
        /// </summary>
        FormQuickLink = 2,

        /// <summary>
        ///     表格页面列表导航
        /// </summary>
        ColumnLink = 3,

        /// <summary>
        ///     表格页面列表导航
        ///     删除事件单独处理
        /// </summary>
        ColumnLinkDelete = 4
    }
}