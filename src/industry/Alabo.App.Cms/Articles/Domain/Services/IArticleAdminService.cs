﻿using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;

namespace Alabo.App.Cms.Articles.Domain.Services {

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