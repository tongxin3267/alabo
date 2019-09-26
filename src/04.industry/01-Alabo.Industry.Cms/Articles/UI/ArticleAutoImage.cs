using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using Alabo.App.Cms.Articles.Domain.Services;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoNews;
using Alabo.Domains.Entities;
using Alabo.Helpers;
using Alabo.UI;

namespace Alabo.App.Cms.Articles.UI {

    public class ArticleAutoImage : IAutoNews {

        public PagedList<AutoNewsItem> ResultList(object query, AutoBaseModel autoModel) {
            var model = Ioc.Resolve<IArticleService>()
                .GetPagedList(query, u => u.ChannelId == ObjectId.Parse("E02220001110000000000004"));
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

        public AutoSetting Setting() {
            var setting = new AutoSetting {
                Name = "图片",
                Icon = "fa fa-file-image-o"
            };
            return setting;
        }
    }
}