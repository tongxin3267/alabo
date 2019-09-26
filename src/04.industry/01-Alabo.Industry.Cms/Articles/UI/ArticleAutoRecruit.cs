﻿using Alabo.Domains.Entities;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoNews;
using Alabo.Helpers;
using Alabo.Industry.Cms.Articles.Domain.Services;
using MongoDB.Bson;

namespace Alabo.Industry.Cms.Articles.UI
{
    //招聘，暫定的十大神器使用
    public class ArticleAutoRecruit : IAutoNews
    {
        public PagedList<AutoNewsItem> ResultList(object query, AutoBaseModel autoModel)
        {
            var model = Ioc.Resolve<IArticleService>()
                .GetPagedList(query, u => u.ChannelId == ObjectId.Parse("e02220001110000000000008"));
            var list = new PagedList<AutoNewsItem>();
            foreach (var item in model)
            {
                var temp = new AutoNewsItem
                {
                    Intro = item.Intro,
                    CreateTime = item.CreateTime,
                    Title = item.Title,
                    Url = $"/user/article/edit?id={item.Id}",
                    Image = item.ImageUrl,
                    ViewCount = item.ViewCount
                };
                list.Add(temp);
            }

            return list;
        }

        public AutoSetting Setting()
        {
            var setting = new AutoSetting
            {
                Name = "十大神器",
                Icon = "flaticon-network"
            };
            return setting;
        }
    }
}