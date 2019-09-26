using MongoDB.Bson;
using System.Collections.Generic;
using Alabo.App.Cms.Articles.Domain.Dto;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.App.Cms.Articles.ViewModels;
using Alabo.Framework.Core.WebUis.Models.Links;
using Alabo.Domains.Services;

namespace Alabo.App.Cms.Articles.Domain.Services {

    public interface IArticleService : IService<Article, ObjectId> {
        /// <summary>
        /// 前台 文章列表
        /// </summary>

        List<ArticleItem> GetArticleList(ArticleInput articleInput);

        /// <summary>
        /// Gets the article.
        /// </summary>
        /// <param name="id">Id标识</param>

        Article GetArticle(string id);

        /// <summary>
        /// 获取帮助中心导航菜单
        /// </summary>

        IList<LinkGroup> GetHelpNav();
    }
}