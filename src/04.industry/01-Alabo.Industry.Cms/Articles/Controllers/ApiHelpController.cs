using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.Extensions;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Framework.Core.WebUis.Models.Links;
using Alabo.Framework.Core.WebUis.Models.Lists;
using Alabo.Industry.Cms.Articles.Domain.Dto;
using Alabo.Industry.Cms.Articles.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Industry.Cms.Articles.Controllers {

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