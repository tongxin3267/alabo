using System;
using System.Collections.Generic;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Industry.Cms.Articles.Domain.Entities;
using Alabo.Industry.Cms.Articles.Domain.Services;
using Alabo.UI;
using Alabo.UI.Design.AutoLists;
using MongoDB.Bson;

namespace Alabo.Industry.Cms.Articles.UI.AutoForm
{
    public class ArticleHelpAutoList : UIBase, IAutoList
    {
        public PageResult<AutoListItem> PageList(object query, AutoBaseModel autoModel)
        {
            var dic = query.ToObject<Dictionary<string, string>>();

            dic.TryGetValue("loginUserId", out var userId);
            dic.TryGetValue("pageIndex", out var pageIndexStr);
            var pageIndex = pageIndexStr.ToInt64();
            if (pageIndex <= 0) {
                pageIndex = 1;
            }

            var temp = new ExpressionQuery<Article>
            {
                EnablePaging = true,
                PageIndex = (int) pageIndex,
                PageSize = 15
            };
            temp.And(e => e.ChannelId == ObjectId.Parse("e02220001110000000000003"));
            var model = Resolve<IArticleService>().GetPagedList(temp);
            var users = Resolve<IUserDetailService>().GetList();
            var list = new List<AutoListItem>();
            foreach (var item in model)
            {
                var apiData = new AutoListItem
                {
                    Title = item.Title,
                    Intro = item.Intro,
                    Value = item.Author,
                    Image = "http://s-test.qiniuniu99.com" + item.ImageUrl,
                    Id = item.Id,
                    Url = $"/pages/index?path=articles_help_view&id={item.Id}"
                };
                list.Add(apiData);
            }

            return ToPageList(list, model);
        }

        public Type SearchType()
        {
            throw new NotImplementedException();
        }
    }
}