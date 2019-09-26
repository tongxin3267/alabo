using System.Collections.Generic;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Core.WebUis.Models.Lists
{
    /// <summary>
    ///     通过列表输出，对应前端Api接口
    /// </summary>
    [ClassProperty(Name = "链接")]
    public class ListOutput : BaseComponent
    {
        /// <summary>
        ///     样式格式，不通的数据可能显示不同的格式
        /// </summary>
        public int StyleType { get; set; } = 1;

        /// <summary>
        ///     返回数据源的总页数
        /// </summary>
        public long TotalSize { get; set; }

        /// <summary>
        ///     Api数据列表
        /// </summary>
        public List<ListItem> ApiDataList { get; set; } = new List<ListItem>();
    }

    public class ListItem
    {
        /// <summary>
        ///     主键ID
        /// </summary>
        public object Id { get; set; }

        /// <summary>
        ///     网址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     图标或图片
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        ///     标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     标题额外数据
        /// </summary>

        public string Extra { get; set; }

        /// <summary>
        ///     描述，可以通过拼接完成
        /// </summary>
        public string Intro { get; set; }
    }
}