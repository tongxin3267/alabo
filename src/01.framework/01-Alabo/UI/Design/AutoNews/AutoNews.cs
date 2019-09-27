using System;
using Alabo.Domains.Entities;

namespace Alabo.UI.Design.AutoNews
{
    public class AutoNews
    {
        /// <summary>
        ///     文章内容
        /// </summary>
        public PagedList<AutoNewsItem> ResultList { get; set; }

        /// <summary>
        ///     页面设置
        /// </summary>
        public AutoSetting Setting { get; set; }
    }

    public class AutoNewsItem
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
        ///     发表时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     链接地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        ///     新闻简介
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        ///     浏览次数
        /// </summary>
        public int ViewCount { get; set; }

        /// <summary>
        ///     来源
        /// </summary>
        public string Source { get; set; }
    }
}