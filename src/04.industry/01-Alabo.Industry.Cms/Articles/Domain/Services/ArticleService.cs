using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Cms.Articles.Domain.CallBacks;
using Alabo.App.Cms.Articles.Domain.Dto;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.App.Cms.Articles.ViewModels;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Core.WebUis.Models.Links;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Enums;
using Alabo.Domains.Query;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.Relations.Domain.Services;
using Alabo.Mapping;

namespace Alabo.App.Cms.Articles.Domain.Services {

    public class ArticleService : ServiceBase<Article, ObjectId>, IArticleService {

        public List<ArticleItem> GetArticleList(ArticleInput articleInput) {
            var query = new ExpressionQuery<Article>();

            query.OrderByAscending(e => e.CreateTime);
            if (!articleInput.ChannelId.IsNullOrEmpty()) {
                query.And(r => r.ChannelId == articleInput.ChannelId.ToObjectId());
            }

            if (articleInput.OrderType == 0) {
                query.OrderByAscending(a => a.ViewCount);
            } else {
                query.OrderByDescending(a => a.ViewCount);
            }

            var list = GetList(query);
            List<ArticleItem> result = new List<ArticleItem>();
            foreach (var item in list) {
                if (articleInput.TotalCount > 0 && result.Count >= articleInput.TotalCount) {
                    break;
                }

                ArticleItem articleItem = AutoMapping.SetValue<ArticleItem>(item);
                articleItem.RelationId = item.RelationId;
                result.Add(articleItem);
            }

            return result;
        }

        public Article GetArticle(string id) {
            return GetSingleFromCache(id.ToObjectId());
        }

        /// <summary>
        /// 获取
        /// </summary>

        public IList<LinkGroup> GetHelpNav() {
            var cacheKey = "GetHelpNav";
            if (!ObjectCache.TryGet(cacheKey, out IList<LinkGroup> list)) {
                var helpChannelId = ChannelType.Help.GetFieldAttribute().GuidId.ToObjectId();
                var classList = Resolve<IRelationService>()
                    .GetList(r => r.Type == typeof(ChannelHelpClassRelation).FullName && r.Status == Status.Normal);
                var articleList = Resolve<IArticleService>()
                    .GetList(r => r.ChannelId == helpChannelId && r.Status == Status.Normal);
                var relationIndexList = Resolve<IRelationIndexService>()
                    .GetList(r => r.Type == typeof(ChannelHelpClassRelation).FullName).ToList();
                list = new List<LinkGroup>();
                foreach (var itemClass in classList) {
                    LinkGroup group = new LinkGroup {
                        Name = itemClass.Name
                    };

                    var relationIds = relationIndexList.Where(r => r.Type == itemClass.Type).Select(r => r.EntityId);
                    var articleItems = articleList.Where(r => relationIds.Contains(r.RelationId));
                    foreach (var item in articleItems) {
                        Link link = new Link {
                            Name = item.Title, Image = item.ImageUrl, Url = $"/articles/help/show?id={item.Id}"
                        };
                        group.Links.Add(link);
                    }

                    list.Add(group);
                }

                ObjectCache.Set(cacheKey, list);
            }
            return list;
        }

        public ArticleService(IUnitOfWork unitOfWork, IRepository<Article, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}