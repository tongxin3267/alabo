using System;

namespace Alabo.Framework.Core.WebUis.Design.AutoArticles
{
    public class AutoArticle
    {
        /// <summary>
        ///     文章内容
        /// </summary>
        public AutoArticleItem ResultList { get; set; }

        /// <summary>
        ///     页面设置
        /// </summary>
        public AutoSetting Setting { get; set; }
    }

    public class AutoArticleItem
    {
        /// <summary>
        ///     图片
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        ///     新闻标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///     内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        ///     作者
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        ///     发表时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     浏览次数
        /// </summary>
        public int ViewCount { get; set; }
    }
}