﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Cms.Articles.Domain.Dto;
using Alabo.App.Cms.Articles.Domain.Services;
using Alabo.Core.WebApis.Controller;
using Alabo.Core.WebApis.Filter;
using Alabo.App.Core.Themes.DiyModels.Links;
using Alabo.App.Core.Themes.DiyModels.Lists;
using Alabo.Core.Enums.Enum;
using Alabo.Core.WebApis.Controller;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Cms.Articles.Controllers {

    [ApiExceptionFilter]
    [Route("Api/Help/[action]")]
    public class ApiHelpController : ApiBaseController {

        public ApiHelpController() : base() {
        }

        /// <summary>
        /// Pc端帮助中心导航
        /// </summary>

        [HttpGet]
        [Display(Description = "Pc端帮助中心导航")]
        public ApiResult<IList<LinkGroup>> Nav() {
            var result = Resolve<IArticleService>().GetHelpNav();
            return ApiResult.Success(result);
        }

        /// <summary>
        ///客服列表
        /// </summary>
        /// <param name="parameter">参数</param>

        [HttpGet]
        [Display(Description = "客服列表")]
        public ApiResult<ListOutput> List(ArticleInput parameter) {
            parameter.ChannelId = ChannelType.Help.GetFieldAttribute().GuidId;
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
                    Url = $"Api/Transfer/Get?loginUserId={parameter.LoginUserId}&id={item.Id}"
                };
                apiOutput.ApiDataList.Add(apiData);
            }
            return ApiResult.Success(apiOutput);
        }
    }
}