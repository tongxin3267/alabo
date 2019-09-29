using Alabo.Data.Things.Goodss.Domain.Entities;
using Alabo.Data.Things.Goodss.Domain.Services;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Data.Things.Goodss.Controllers
{
    [ApiExceptionFilter]
    [Route("Api/GoodsLine/[action]")]
    public class ApiGoodsLineController : ApiBaseController<GoodsLine, ObjectId>
    {
        public ApiGoodsLineController()
        {
            BaseService = Resolve<IGoodsLineService>();
        }

        [HttpGet]
        public ApiResult Index([FromQuery] GoodsLine parameter)
        {
            var query = new ExpressionQuery<GoodsLine>();
            var model = Resolve<IGoodsLineService>().GetPagedList(query);
            return ApiResult.Success(model);
        }

        [HttpGet]
        public ApiResult Edit(string id)
        {
            var view = Resolve<IGoodsLineService>().GetEditView(id);
            return ApiResult.Success(view);
        }

        [HttpPost]
        public ApiResult Edit([FromBody] GoodsLine view)
        {
            if (!this.IsFormValid()) return ApiResult.Failure(string.Empty, this.FormInvalidReason());

            return ApiResult.Success("����ɹ�");
        }
    }
}