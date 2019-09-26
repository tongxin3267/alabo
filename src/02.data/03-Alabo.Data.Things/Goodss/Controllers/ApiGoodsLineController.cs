using System;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Model;
using System.Linq;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Domains.Entities;
using Microsoft.AspNetCore.Mvc;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Core.Common;
using MongoDB.Bson;
using Alabo.App.Core.User;
using Alabo.RestfulApi;
using ZKCloud.Open.ApiBase.Configuration;
using Alabo.Domains.Services;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.Controllers;
using Alabo.Data.Things.Goodss.Domain.Entities;
using Alabo.Data.Things.Goodss.Domain.Services;
using Alabo.Data.Things.Goodss.Dtos;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.Maps;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Data.Things.Goodss.Controllers {

    [ApiExceptionFilter]
    [Route("Api/GoodsLine/[action]")]
    public class ApiGoodsLineController : ApiBaseController<GoodsLine, ObjectId> {

        public ApiGoodsLineController() : base() {
            BaseService = Resolve<IGoodsLineService>();
        }

        [HttpGet]
        public ApiResult Index([FromQuery] GoodsLine parameter) {
            var query = new ExpressionQuery<GoodsLine>();
            var model = Resolve<IGoodsLineService>().GetPagedList(query);
            return ApiResult.Success(model);
        }

        [HttpGet]
        public ApiResult Edit(string id) {
            var view = Resolve<IGoodsLineService>().GetEditView(id);
            return ApiResult.Success(view);
        }

        [HttpPost]
        public ApiResult Edit([FromBody] GoodsLine view) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(string.Empty, this.FormInvalidReason());
            }

            return ApiResult.Success("±£´æ³É¹¦");
        }
    }
}