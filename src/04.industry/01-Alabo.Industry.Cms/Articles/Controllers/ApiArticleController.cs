using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Cms.Articles.Domain.Dto;
using Alabo.App.Cms.Articles.Domain.Services;
using Alabo.App.Cms.Articles.ViewModels;
using Alabo.Core.WebApis.Controller;
using Alabo.App.Core.Api.Domain.Service;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.Common.Domain.CallBacks;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Themes.DiyModels.Lists;
using Alabo.App.Core.Themes.Extensions;
using Alabo.App.Core.User;
using Alabo.AutoConfigs;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Cms.Articles.Controllers {

    /// <summary>
    /// 文章项目的Api
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/Article/[action]")]
    public class ArticleApiController : ApiBaseController<Domain.Entities.Article, ObjectId> {
        /// <summary>
        /// The automatic configuration manager
        /// </summary>

        /// <summary>
        /// The user manager
        /// </summary>

        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleApiController"/> class.
        /// </summary>
        public ArticleApiController() : base() {
            this.BaseService = Resolve<IArticleService>();
        }

        /// <summary>
        /// 获取注册协议
        /// </summary>
        [HttpGet]
        [Display(Description = "注册协议")]
        // [ApiView(typeof(Domain.Entities.Article), PageType = ViewPageType.Data)]
        public ApiResult Agreement() {
            var model = Resolve<IAutoConfigService>().GetValue<WebSiteConfig>();
            if (model == null) {
                return null;
            }
            var result = new Domain.Entities.Article {
                Content = model.ServiceAgreement,
            };

            return ApiResult.Success(result);
        }

        /// <summary>
        /// 内容详情页面
        /// </summary>
        /// <param name="id">Id标识</param>

        [HttpGet]
        [Display(Description = "内容详情页面")]
        public ApiResult<Domain.Entities.Article> ArticleDetail(string id) {
            //如果前端没有传ID则返回用户协议
            //为方便前端暂时使用该组件传输用户协议
            if (id.IsNullOrEmpty() || id == "undefined") {
                var web = Resolve<IAutoConfigService>().GetValue<WebSiteConfig>();
                if (web == null) {
                    return null;
                }
                var result = new Domain.Entities.Article {
                    Content = web.ServiceAgreement,
                };
                return ApiResult.Success(result);
            }
            var model = Resolve<IArticleService>().GetSingle(e => e.Id == id.ToObjectId());
            if (model == null) {
                return ApiResult.Failure<Domain.Entities.Article>("内容不存在");
            }

            model.ViewCount++;
            Resolve<IArticleService>().Update(model);

            model.Content = Resolve<IApiService>().ConvertToApiImageUrl(model.Content);
            return ApiResult.Success(model);
        }

        /// <summary>
        /// Users the notice list.
        /// </summary>
        /// <param name="parameter">参数</param>

        [HttpGet]
        [Display(Description = "公告")]
        public ApiResult<ListOutput> UserNoticeList(ArticleInput parameter) {
            parameter.ChannelId = ChannelType.UserNotice.GetFieldAttribute().GuidId;
            var model = Resolve<IArticleService>().GetArticleList(parameter);
            ListOutput apiOutput = new ListOutput {
                TotalSize = model.Count() / parameter.PageSize
            };
            foreach (var item in model) {
                var apiData = new ListItem {
                    Title = $"{item.Title}",
                    Intro = $"{item.SubTitle} {item.CreateTime:yyyy-MM-dd hh:ss}",
                    Extra = $"{ item.CreateTime:yyyy-MM-dd hh:ss}",
                    Id = item.Id,
                    Url = $"Api/Transfer/Get?loginUserId={parameter.LoginUserId}&id={item.Id}"
                };
                apiOutput.ApiDataList.Add(apiData);
            }
            return ApiResult.Success(apiOutput);
        }

        /// <summary>
        /// 头条列表
        /// </summary>
        /// <param name="parameter">参数</param>

        [HttpGet]
        [Display(Description = "头条列表")]
        public ApiResult<ListOutput> TopLineList(ArticleInput parameter) {
            parameter.ChannelId = ChannelType.TopLine.GetFieldAttribute().GuidId;
            var webSite = Resolve<IAutoConfigService>().GetValue<WebSiteConfig>();
            var model = Resolve<IArticleService>().GetArticleList(parameter);
            var articleList = new List<ArticleItem>();
            if (!parameter.RelationIds.IsNullOrEmpty()) {
                var relationList = Resolve<IRelationIndexService>().GetList(u => u.RelationId.ToString() == parameter.RelationIds);
                relationList.Foreach(u => {
                    var temp = model.FirstOrDefault(z => z.RelationId == u.EntityId);
                    articleList.Add(temp);
                });
                model = articleList;
            }
            ListOutput apiOutput = new ListOutput {
                TotalSize = model.Count() / parameter.PageSize
            };
            foreach (var item in model) {
                var apiData = new ListItem {
                    Title = $"{item.Title}",
                    Intro = $"{item.SubTitle} {item.CreateTime:yyyy-MM-dd hh:ss}",
                    Extra = $"{item}",
                    Id = item.Id,
                    Url = $"/pages/index?path=articles_topline_show&id={item.Id}".ToClientUrl(parameter.ClientType),
                    Image = Resolve<IApiService>().ApiImageUrl(item.ImageUrl)
                };
                apiOutput.ApiDataList.Add(apiData);
            }
            return ApiResult.Success(apiOutput);
        }

        /// <summary>
        /// 头条列表
        /// </summary>
        /// <param name="parameter">参数</param>

        [HttpGet]
        [Display(Description = "头条列表")]
        public ApiResult<ListOutput> TenementTopLineList(ArticleInput parameter) {
            parameter.ChannelId = ChannelType.TopLine.GetFieldAttribute().GuidId;
            var model = Resolve<IArticleService>().GetArticleList(parameter);
            var articleList = new List<ArticleItem>();
            if (!parameter.RelationIds.IsNullOrEmpty()) {
                var relationList = Resolve<IRelationIndexService>().GetList(u => u.RelationId.ToString() == parameter.RelationIds);
                relationList.Foreach(u => {
                    var temp = model.FirstOrDefault(z => z.RelationId == u.EntityId);
                    articleList.Add(temp);
                });
                model = articleList;
            }
            ListOutput apiOutput = new ListOutput {
                TotalSize = model.Count() / parameter.PageSize
            };
            foreach (var item in model) {
                var apiData = new ListItem {
                    Title = $"{item.Title}",
                    Intro = $"{item.SubTitle} {item.CreateTime:yyyy-MM-dd hh:ss}",
                    Extra = $"{item}",
                    Id = item.Id,
                    Url = $"/pages/index?path=articles_topline_show&id={item.Id}".ToClientUrl(parameter.ClientType),
                    Image = $"{item.ImageUrl}"
                };
                apiOutput.ApiDataList.Add(apiData);
            }
            return ApiResult.Success(apiOutput);
        }

        /// <summary>
        /// 文章列表
        /// </summary>
        /// <param name="parameter">参数</param>

        [HttpGet]
        [Display(Description = "文章列表")]
        public ApiResult<ListOutput> ArticleList(ArticleInput parameter) {
            parameter.ChannelId = ChannelType.Article.GetFieldAttribute().GuidId;
            var model = Resolve<IArticleService>().GetArticleList(parameter);

            var articleList = new List<ArticleItem>();
            if (!parameter.RelationIds.IsNullOrEmpty()) {
                var relationList = Resolve<IRelationIndexService>().GetList(u => u.RelationId.ToString() == parameter.RelationIds);
                relationList.Foreach(u => {
                    var temp = model.FirstOrDefault(z => z.RelationId == u.EntityId);
                    articleList.Add(temp);
                });
                model = articleList;
            }

            ListOutput apiOutput = new ListOutput {
                TotalSize = model.Count() / parameter.PageSize
            };
            foreach (var item in model) {
                var apiData = new ListItem {
                    Title = $"{item.Title}",
                    Intro = $"{item.SubTitle} {item.CreateTime:yyyy-MM-dd hh:ss}",
                    Extra = $"{item.CreateTime:yyyy-MM-dd hh:ss}",
                    Id = item.Id,
                    Url = $"Api/Transfer/Get?loginUserId={parameter.LoginUserId}&id={item.Id}"
                };
                apiOutput.ApiDataList.Add(apiData);
            }
            return ApiResult.Success(apiOutput);
        }

        /// <summary>
        /// Helps the list.
        /// </summary>
        /// <param name="parameter">参数</param>

        [HttpGet]
        [Display(Description = "客服列表")]
        public ApiResult<ListOutput> HelpList(ArticleInput parameter) {
            parameter.ChannelId = ChannelType.Help.GetFieldAttribute().GuidId;
            var model = Resolve<IArticleService>().GetArticleList(parameter);

            var articleList = new List<ArticleItem>();
            if (!parameter.RelationIds.IsNullOrEmpty()) {
                var relationList = Resolve<IRelationIndexService>().GetList(u => u.RelationId.ToString() == parameter.RelationIds);
                relationList.Foreach(u => {
                    var temp = model.FirstOrDefault(z => z.RelationId == u.EntityId);
                    articleList.Add(temp);
                });
                model = articleList;
            }

            ListOutput apiOutput = new ListOutput {
                TotalSize = model.Count() / parameter.PageSize
            };
            foreach (var item in model) {
                var apiData = new ListItem {
                    Title = $"{item.Title}",
                    Intro = $"{item.SubTitle} {item.CreateTime:yyyy-MM-dd hh:ss}",
                    Extra = $"{item.CreateTime:yyyy-MM-dd hh:ss}",
                    Id = item.Id,
                    Url = $"/pages/index?path=articles_show&id={item.Id}"
                };
                apiOutput.ApiDataList.Add(apiData);
            }
            return ApiResult.Success(apiOutput);
        }

        /// <summary>
        /// 帮助内容
        /// </summary>
        /// <param name="id">Id标识</param>

        [HttpGet]
        [Display(Description = "帮助内容")]
        public ApiResult<Domain.Entities.About> AboutDetail(string id) {
            //var model = Resolve<IAboutService>().GetSingle(e => e.Id == id);
            var model = Resolve<IAutoConfigService>().GetList<Domain.Entities.About>().FirstOrDefault(e => e.Id == id.ToObjectId());
            if (model == null) {
                return ApiResult.Failure<Domain.Entities.About>("内容不存在");
            }

            model.Content = Resolve<IApiService>().ConvertToApiImageUrl(model.Content);
            return ApiResult.Success(model);
        }

        [HttpGet]
        [Display(Description = "获取帮助列表")]
        public ApiResult GetHelpList() {
            var model = Resolve<IArticleService>()
                .GetList(u => u.ChannelId == ObjectId.Parse("e02220001110000000000003"));
            return ApiResult.Success(model);
        }

        /// <summary>
        /// 头条分类
        /// </summary>
        [HttpGet]
        [Display(Description = "头条分类")]
        public ApiResult ArticleClassify() {
            var relationIndex = Resolve<IRelationIndexService>().GetList(u => u.Type == "Alabo.App.Cms.Articles.Domain.CallBacks.ChannelTopLineClassRelation");
            var relation = Resolve<IRelationService>().GetList(u =>
                u.Type == "Alabo.App.Cms.Articles.Domain.CallBacks.ChannelTopLineClassRelation");
            var relationOutput = new List<TopLineClassOutput>();
            foreach (var item in relation) {
                var apiOutput = new TopLineClassOutput {
                    ClassName = item.Name,
                    RelationId = item.Id,
                    EntityId = item.FatherId
                };
                relationOutput.Add(apiOutput);
            }

            return ApiResult.Success(relationOutput);
        }

        /// <summary>
        /// 头条分类
        /// </summary>
        [HttpGet]
        [Display(Description = "头条标签")]
        public ApiResult ArticleTag() {
            var relationIndex = Resolve<IRelationIndexService>().GetList(u => u.Type == "Alabo.App.Cms.Articles.Domain.CallBacks.ChannelTopLineTagRelation");
            var relation = Resolve<IRelationService>().GetList(u =>
                u.Type == "Alabo.App.Cms.Articles.Domain.CallBacks.ChannelTopLineTagRelation");
            var relationOutput = new List<TopLineClassOutput>();
            foreach (var item in relation) {
                relationOutput.Add(new TopLineClassOutput {
                    ClassName = item.Name,
                    RelationId = item.Id,
                });
            }

            return ApiResult.Success(relationOutput);
        }

        /// <summary>
        /// 头条分类
        /// </summary>
        [HttpGet]
        [Display(Description = "帮助分类")]
        public ApiResult<List<HelpClassOutput>> HelpClassify() {
            var relationIndex = Resolve<IRelationIndexService>().GetList(u => u.Type == typeof(Domain.CallBacks.ChannelHelpClassRelation).FullName);
            var relation = Resolve<IRelationService>().GetList(u => u.Type == typeof(Domain.CallBacks.ChannelHelpClassRelation).FullName);
            var relationOutput = new List<HelpClassOutput>();
            var apiService = Resolve<IApiService>();
            foreach (var item in relation) {
                if (item.FatherId > 0) {
                    continue;
                }
                var apiOutput = new HelpClassOutput {
                    Name = item.Name,
                    Url = $"/pages/index?path=articles_list&relationIds={item.Id}",
                    Image = apiService.ApiImageUrl(item.Icon)
                };
                relationOutput.Add(apiOutput);
            }

            return ApiResult.Success(relationOutput);
        }

        /// <summary>
        /// 热门问答
        /// </summary>
        [HttpGet]
        [Display(Description = "热门问答")]
        public ApiResult<ListOutput> TopHelpList() {
            var parameter = new ArticleInput() {
                ChannelId = ChannelType.Help.GetFieldAttribute().GuidId,
                ClientType = ClientType.WapH5,
                OrderType = 1,
                TotalCount = 5
            };

            var model = Resolve<IArticleService>().GetArticleList(parameter);
            ListOutput apiOutput = new ListOutput {
                TotalSize = model.Count() / parameter.PageSize
            };
            foreach (var item in model) {
                var apiData = new ListItem {
                    Title = $"{item.Title}",
                    Intro = $"{item.SubTitle} {item.CreateTime:yyyy-MM-dd hh:ss}",
                    Extra = $"{item.CreateTime:yyyy-MM-dd hh:ss}",
                    Id = item.Id,
                    Url = $"/pages/index?path=articles_show&id={item.Id}"
                };
                apiOutput.ApiDataList.Add(apiData);
            }
            return ApiResult.Success(apiOutput);
        }

        public ApiResult Delete(string id) {
            var result = Resolve<IArticleAdminService>().Delete(id);
            if (result.Item1.Succeeded) {
                return ApiResult.Success();
            }
            return ApiResult.Failure("删除失败");
        }
    }
}