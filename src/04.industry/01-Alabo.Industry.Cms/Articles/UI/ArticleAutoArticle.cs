using Alabo.Domains.Entities;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Framework.Core.WebUis;
using Alabo.Helpers;
using Alabo.Industry.Cms.Articles.Domain.Services;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.UI.Design.AutoArticles;
using Alabo.UI.Design.AutoNews;

namespace Alabo.Industry.Cms.Articles.UI
{
    public class ArticleAutoArticle : IAutoArticle, IAutoNews
    {
        public AutoArticleItem ResultList(string Id)
        {
            var model = Ioc.Resolve<IArticleService>().GetSingle(u => u.Id == Id.ToObjectId());
            if (model == null) {
                return new AutoArticleItem();
            }

            var result = AutoMapping.SetValue<AutoArticleItem>(model);
            result.Image = model.ImageUrl;
            result.Content = Ioc.Resolve<IApiService>().ConvertToApiImageUrl(result.Content);
            return result;
        }

        public AutoSetting Setting()
        {
            var setting = new AutoSetting
            {
                Name = "文章详情",
                Icon = "flaticon-network"
            };
            return setting;
        }

        public PagedList<AutoNewsItem> ResultList(object query, AutoBaseModel autoModel)
        {
            return null;
        }
    }
}