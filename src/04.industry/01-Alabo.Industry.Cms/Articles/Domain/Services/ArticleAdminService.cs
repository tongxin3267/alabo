using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Cms.Articles.Domain.Entities;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.Relations.Domain.Services;

namespace Alabo.App.Cms.Articles.Domain.Services {

    public class ArticleAdminService : ServiceBase<Article, ObjectId>, IArticleAdminService {

        public Tuple<ServiceResult, Entities.Article> Delete(string id) {
            var result = ServiceResult.Success;
            var objId = id.ToObjectId();
            try {
                var article = Resolve<IArticleAdminService>().Delete(u => u.Id == objId);
                if (article == true) {
                    return Tuple.Create(result, new Article());
                }
            } catch (Exception ex) {
                Console.WriteLine(ex);
            }

            return Tuple.Create(ServiceResult.FailedWithMessage("删除失败"), new Entities.Article());
        }

        public List<Article> GetArticleList() {
            var view = Resolve<IArticleAdminService>().GetList();
            return view.ToList();
        }

        public List<Article> GetArticleListByChannelType(ChannelType type, int pageSize = 0, int pageIndex = 0) {
            var channeId = Resolve<IChannelService>().GetChannelId(type);
            var view = GetList(u => u.ChannelId == channeId);
            return view.ToList();
        }

        /// <summary>
        /// 计算方法有问题
        /// </summary>

        public long GetMaxRelationId() {
            var articles = Resolve<IRelationIndexService>().GetList();
            var article = articles.OrderByDescending(r => r.RelationId).FirstOrDefault();
            if (article != null) {
                return article.RelationId + 1;
            } else {
                return 1;
            }
        }

        public PagedList<Article> GetPageList(object query, ObjectId channelId) {
            var pageList = Resolve<IArticleAdminService>().GetPagedList(query, r => r.ChannelId == channelId);
            return pageList;
        }

        public Article GetViewArticle(ObjectId id) {
            var view = Resolve<IArticleAdminService>().GetSingle(u => u.Id == id);
            if (view != null) {
                var channel = Resolve<IChannelService>().GetSingle(r => r.Id == view.ChannelId);
                if (channel != null) {
                    var classType = Resolve<IChannelService>().GetChannelClassType(channel);
                    var tagType = Resolve<IChannelService>().GetChannelTagType(channel);
                    view.Classes = Resolve<IRelationIndexService>().GetRelationIds(classType.FullName, view.RelationId); //文章分类
                    view.Tags = Resolve<IRelationIndexService>().GetRelationIds(tagType.FullName, view.RelationId); //文章标签
                }
            }
            return view;
        }

        public Tuple<ServiceResult, Entities.Article> AddOrUpdate(Article model, HttpRequest request) {
            var id = request.Form["Id"];
            var channelId = request.Form["ChannelId"];
            if (!id.ToString().IsNullOrEmpty()) {
                model.Id = id.ToString().ToSafeObjectId();
            }

            model.ChannelId = channelId.ToString().ToSafeObjectId();
            var channel = Resolve<IChannelService>().GetSingle(r => r.Id == model.ChannelId);
            if (channel == null) {
                Tuple.Create(ServiceResult.FailedWithMessage("频道不存在"), new Article());
            }

            var list = Resolve<IChannelService>().DataFields(channelId.ToString());
            var dic = new Dictionary<string, string>();

            foreach (var item in list) {
                var key = item.Key;
                string value = request.Form[key];
                dic.Add(key, value);
            }

            var find = Resolve<IArticleAdminService>().GetSingle(u => u.Id == model.Id);
            var result = true;
            if (find == null) {
                model.RelationId = GetMaxRelationId();
                result = Resolve<IArticleAdminService>().Add(model);
                if (result) {
                }
            } else {
                result = Resolve<IArticleAdminService>().Update(model);
            }

            model.AttachValue = dic.ToJson();
            // 如果为true
            if (result == true) {
                // 添加标签和分类
                var classType = Resolve<IChannelService>().GetChannelClassType(channel);
                var classIds = request.Form["Classes"].ToStr();
                var tagType = Resolve<IChannelService>().GetChannelTagType(channel);
                var tagIds = request.Form["Tags"].ToStr();
                Resolve<IRelationIndexService>()
                     .AddUpdateOrDelete(classType.FullName, model.RelationId, classIds);
                Resolve<IRelationIndexService>()
                    .AddUpdateOrDelete(tagType.FullName, model.RelationId, tagIds);
                return Tuple.Create(ServiceResult.Success, new Article());
            }

            DeleteCache();
            return Tuple.Create(ServiceResult.Failed, new Article());
        }

        private void DeleteCache() {
            ObjectCache.Remove("GetHelpNav");
        }

        public ArticleAdminService(IUnitOfWork unitOfWork, IRepository<Article, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}