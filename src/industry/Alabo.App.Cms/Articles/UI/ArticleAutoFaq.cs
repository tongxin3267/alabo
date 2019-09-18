using MongoDB.Bson;
using Alabo.App.Cms.Articles.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Helpers;
using Alabo.UI;
using Alabo.UI.AutoNews;
using Alabo.UI.AutoFaqs;

namespace Alabo.App.Cms.Articles.UI {

    /// <summary>
    ///
    /// </summary>
    public class ArticleAutoFaq : IAutoNews {

        /// <summary>
        ///
        /// </summary>
        /// <param name="query"></param>
        public PagedList<AutoNewsItem> ResultList(object query, AutoBaseModel autoModel) {
            var model = Ioc.Resolve<IArticleService>()
                .GetPagedList(query, u => u.ChannelId == ObjectId.Parse("e02220001110000000000007"));
            var list = new PagedList<AutoNewsItem>();
            foreach (var item in model) {
                var temp = new AutoNewsItem {
                    Intro = item.Intro,
                    CreateTime = item.CreateTime,
                    Title = item.Title,
                    Url = $"/user/article?id={item.Id}&type=ArticleAutoArticle",
                    Image = item.ImageUrl,
                    ViewCount = item.ViewCount
                };
                list.Add(temp);
            }
            return list;
        }

        /// <summary>
        ///
        /// </summary>
        public AutoSetting Setting() {
            var setting = new AutoSetting {
                Name = "百问百答",
                Icon = "fa fa-file-image-o"
            };
            return setting;
        }
    }
}