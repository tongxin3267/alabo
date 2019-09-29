using System.Collections.Generic;
using Alabo.Domains.Services;
using Alabo.Framework.Core.WebUis.Models.Links;
using Alabo.Industry.Cms.Articles.Domain.Dto;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using Alabo.Industry.Cms.Articles.ViewModels;
using MongoDB.Bson;

namespace Alabo.Industry.Cms.Articles.Domain.Services
{
    public interface IArticleService : IService<Article, ObjectId>
    {
        /// <summary>
        ///     前台 文章列表
        /// </summary>
        List<ArticleItem> GetArticleList(ArticleInput articleInput);

        /// <summary>
        ///     Gets the article.
        /// </summary>
        /// <param name="id">Id标识</param>
        Article GetArticle(string id);

        /// <summary>
        ///     获取帮助中心导航菜单
        /// </summary>
        IList<LinkGroup> GetHelpNav();
    }
}