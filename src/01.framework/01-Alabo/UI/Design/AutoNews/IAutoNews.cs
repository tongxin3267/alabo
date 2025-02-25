﻿using Alabo.Dependency;
using Alabo.Domains.Entities;

namespace Alabo.UI.Design.AutoNews {

    /// <summary>
    ///     自动新闻列表
    /// </summary>
    public interface IAutoNews : IScopeDependency {

        /// <summary>
        ///     文章详情内容
        /// </summary>
        /// <param name="query"></param>
        /// <param name="autoModel"></param>
        /// <returns></returns>
        PagedList<AutoNewsItem> ResultList(object query, AutoBaseModel autoModel);

        /// <summary>
        ///     页面设置
        /// </summary>
        AutoSetting Setting();
    }
}