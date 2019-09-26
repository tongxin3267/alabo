using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Cms.Articles.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoArticles;
using Alabo.Framework.Core.WebUis.Design.AutoNews;
using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Mapping;
using Alabo.UI;

namespace Alabo.App.Cms.Articles.UI {

    public class ArticleAutoArticle : IAutoArticle, IAutoNews {



        public AutoArticleItem ResultList(string Id) {
            var model = Ioc.Resolve<IArticleService>().GetSingle(u => u.Id == Id.ToObjectId());
            if (model == null) {
                return new AutoArticleItem();
            }
            var result = AutoMapping.SetValue<AutoArticleItem>(model);
            result.Image = model.ImageUrl;
            result.Content = Ioc.Resolve<IApiService>().ConvertToApiImageUrl(result.Content);
            return result;
        }

        public PagedList<AutoNewsItem> ResultList(object query, AutoBaseModel autoModel) {
            return null;
        }

        public AutoSetting Setting() {
            var setting = new AutoSetting {
                Name = "文章详情",
                Icon = "flaticon-network"
            };
            return setting;
        }
    }
}