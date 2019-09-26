using System;
using System.Collections.Generic;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace Alabo.Industry.Cms.Articles.Domain.Services {

    public interface IArticleAdminService : IService<Article, ObjectId> {
        /// <summary>
        /// 获取表Article中的最大Id
        /// </summary>

        long GetMaxRelationId();

        /// <summary>
        ///     根据ChannelType 获取文章
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        List<Entities.Article> GetArticleListByChannelType(ChannelType type, int pageSize = 0, int pageIndex = 0);

        Article GetViewArticle(ObjectId id);

        PagedList<Article> GetPageList(object query, ObjectId channelId);

        List<Article> GetArticleList();

        Tuple<ServiceResult, Entities.Article> Delete(string id);

        Tuple<ServiceResult, Entities.Article> AddOrUpdate(Article model, HttpRequest request);
    }
}