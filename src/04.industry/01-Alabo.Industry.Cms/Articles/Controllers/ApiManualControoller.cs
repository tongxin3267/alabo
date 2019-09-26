using System.Collections.Generic;
using System.Linq;
using Alabo.Extensions;
using Alabo.Framework.Basic.Relations.Domain.Services;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Industry.Cms.Articles.Domain.Dto;
using Alabo.Industry.Cms.Articles.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Industry.Cms.Articles.Controllers
{
    /// <summary>
    ///     文章项目的Api
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/Manual/[action]")]
    public class ApiManualControoller : ApiBaseController
    {
        [HttpGet]
        public ApiResult GetManualRelation()
        {
            var relationList = Resolve<IRelationService>().GetList(u =>
                u.Type == "Alabo.App.Cms.Articles.Domain.CallBacks.ChannelNoteBookClassRelation");
            var result = relationList.OrderBy(u => u.Id);
            return ApiResult.Success(result);
        }

        [HttpGet]
        public ApiResult GetListByRelationId(long id)
        {
            var model = Resolve<IArticleService>().GetList(u => u.RelationId == id);
            return ApiResult.Success(model);
        }

        [HttpGet]
        public ApiResult GetManualListByRelation()
        {
            var relationList = Resolve<IRelationService>().GetList(u =>
                u.Type == "Alabo.App.Cms.Articles.Domain.CallBacks.ChannelNoteBookClassRelation");
            var relations = relationList.OrderBy(u => u.Id);
            var model = Resolve<IArticleService>().GetList(u => u.ChannelId == "e02220001110000000000012".ToObjectId());
            var result = new List<ManualOutput>();
            foreach (var item in relations)
            {
                var list = model.Where(u => u.RelationId == item.Id).ToList();
                var view = new ManualOutput
                {
                    Article = list,
                    RelationName = item.Name,
                    RelationId = item.Id
                };
                result.Add(view);
            }

            return ApiResult.Success(result);
        }
    }
}