using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common;
using Alabo.App.Core.User;
using Alabo.App.Shop.Product.Domain.Dtos;
using Alabo.App.Shop.Product.Domain.Entities;
using Alabo.App.Shop.Product.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.Maps;
using ZKCloud.Open.ApiBase.Configuration;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Shop.Product.Controllers {

    [ApiExceptionFilter]
    [Route("Api/ProductLine/[action]")]
    public class ApiProductLineController : ApiBaseController<ProductLine, long> {

        public ApiProductLineController() : base() {
            BaseService = Resolve<IProductLineService>();
        }

        [HttpGet]
        public ApiResult Index([FromQuery] ProductLine parameter) {
            var query = new ExpressionQuery<ProductLine>();
            var model = Resolve<IProductLineService>().GetPagedList(query);
            return ApiResult.Success(model);
        }

        [HttpGet]
        public ApiResult Edit(long? id) {
            var productLine = Resolve<IProductLineService>().GetSingle(r => r.Id == id);
            if (productLine == null) {
                productLine = new ProductLine();
            }
            var rsProductLine = productLine.MapTo<ProductLineViewOut>();
            var productList = Resolve<IProductService>().GetList(rsProductLine.ProductIds);
            rsProductLine.ProductList = productList;

            return ApiResult.Success(rsProductLine);
        }

        [HttpPost]
        public ApiResult Edit([FromBody] ProductLine view) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(string.Empty, this.FormInvalidReason());
            }

            var productLine = Resolve<IProductLineService>().GetSingle(r => r.Id == view.Id) ?? new ProductLine();
            productLine.ProductIds = view.ProductIds;
            productLine.Name = view.Name;
            productLine.Intro = view.Intro;
            productLine.SortOrder = view.SortOrder;
            Resolve<IProductLineService>().AddOrUpdate(productLine, productLine.Id > 0);
            return ApiResult.Success("±£´æ³É¹¦");
        }
    }
}