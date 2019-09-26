using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.App.Core.Common.Domain.Dtos;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Employes.Domain.Dtos;
using Alabo.App.Core.Employes.Domain.Services;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Shop.Category.Domain.Services;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.App.Shop.Product.Domain.Services;
using Alabo.App.Shop.Store.Domain.Dtos;
using Alabo.App.Shop.Store.Domain.Entities.Extensions;
using Alabo.App.Shop.Store.Domain.Services;
using Alabo.AutoConfigs;
using Alabo.Framework.Core.WebUis.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Basic.Relations.Domain.Services;
using Alabo.Mapping;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;
using Alabo.UI;

namespace Alabo.App.Shop.Store.Controllers {

    /// <summary>
    ///
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/ShopStore/[action]")]
    public class ApiStoreController : ApiBaseController<Domain.Entities.Store, long> {

        /// <summary>
        ///
        /// </summary>
        public ApiStoreController() : base() {
            BaseService = Resolve<IShopStoreService>();
        }

        /// <summary>
        /// 获取店铺分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<RelationApiOutput>> GetProductTags(long userId) {
            if (userId == 0) {
                return ApiResult.Failure<List<RelationApiOutput>>("用户ID不能为空");
            }

            var result = Resolve<IRelationService>().GetClass(typeof(Product.Domain.CallBacks.ProductTagRelation).Name, userId)
                .ToList();
            return ApiResult.Success(result);
        }

        /// <summary>
        /// 获取店铺分类
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<RelationApiOutput>> GetStoreClass(long userId) {
            if (userId == 0) {
                return ApiResult.Failure<List<RelationApiOutput>>("用户ID不能为空");
            }
            var ret = ApiResult.Success(Resolve<IRelationService>().GetClass(typeof(Product.Domain.CallBacks.ProductClassRelation).Name, userId).ToList());

            return ret;
        }

        /// <summary>
        /// 获取店铺分类
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="catList"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult SaveStoreCategories([FromQuery]long storeId, [FromBody]List<StoreCategory> catList) {
            var list = new List<StoreCategory>();
            list.AddRange(catList);
            while (list.Count > 0) {
                if (list[0].Id == Guid.Empty) {
                    list[0].Id = Guid.NewGuid();
                }

                list.AddRange(list[0].Children);
                list.Remove(list[0]);
            }

            var extend = Resolve<IShopStoreService>().GetStoreExtension(storeId);

            extend.StoreCategories = catList;

            return ApiResult.Success(Resolve<IShopStoreService>().UpdateExtensions(storeId, extend));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<PagedList<ProductBrief>> GetProductList([FromQuery] ProductListInput input) {
            var query = new ExpressionQuery<Product.Domain.Entities.Product>();

            query.And(e => e.StoreId == input.StoreId);
            query.PageIndex = (int)input.PageIndex;
            query.PageSize = (int)input.PageSize;
            query.EnablePaging = true;
            query.OrderByDescending(e => e.Id);
            var list = Resolve<IProductService>().GetPagedList(query);
            if (list.Count < 0) {
                return ApiResult.Success(new PagedList<ProductBrief>());
            }

            var catDict = Resolve<ICategoryService>().GetList().ToDictionary(c => c.Id);

            var ret = list.Select(prd => {
                var brief = new ProductBrief();

                AutoMapping.SetValue(prd, brief);

                brief.CategoryName = catDict.ContainsKey(prd.CategoryId) ? catDict[prd.CategoryId].Name : "未定义";

                return brief;
            });

            return ApiResult.Success(PagedList<ProductBrief>.Create(ret, list.RecordCount, list.PageSize, list.PageIndex));
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public ApiResult DeleteProduct(long id) {
            Resolve<IProductAdminService>().Delete(id);
            return ApiResult.Success();
        }

        /// <summary>
        /// 商品编辑
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult EditProduct([FromBody]ProductEditOuput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason());
            }

            var result = Resolve<IStoreProductService>().EditProduct(parameter);
            return ToResult(result);
        }

        /// <summary>
        /// 商品编辑
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<ProductEditOuput> GetProductView([FromQuery]ProductEditInput parameter) {
            try {
                var result = Resolve<IStoreProductService>().GetProductView(parameter);
                return ApiResult.Success(result);
            } catch (Exception ex) {
                return ApiResult.Failure<ProductEditOuput>(ex.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet]
        public ApiResult<List<CategoryBrief>> GetChildCategories([FromQuery]Guid parentId) {
            var list = Resolve<ICategoryService>().GetList(c => c.PartentId == parentId);

            var ret = list.Select(c => new CategoryBrief {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return ApiResult.Success(ret);
        }

        /// <summary>
        /// 供应商登录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<RoleOuput> Login([FromBody]UserOutput userOutput) {
            return null;
            //var result = Resolve<IEmployeeService>().Login(userOutput, () => {
            //    var storeInfo = Resolve<IStoreService>().GetSingle(s => s.UserId == userOutput.Id);
            //    return (storeInfo != null, FilterType.Store);
            //});
            //return ZKCloud.Open.ApiBase.Models.ApiResult.Success(result);
        }

        [HttpGet]
        public ApiResult GetStoreOrdersToExcel(StoreOrdersToExcel model) {
            var webSite = Resolve<IAutoConfigService>().GetValue<WebSiteConfig>();
            var store = Resolve<IShopStoreService>().GetSingle(u => u.UserId == model.UserId);
            if (store == null) {
                return ApiResult.Failure("非法操作");
            }

            var query = new ExpressionQuery<Order.Domain.Entities.Order> {
                PageIndex = 1,
                PageSize = 15
            };
            query.And(u => u.StoreId == store.Id);
            query.And(u => u.OrderStatus == model.Status);
            var view = Resolve<IOrderService>().GetPagedList(query);
            var orders = new List<Order.Domain.Entities.Order>();
            foreach (var item in view) {
                TimeSpan ts = DateTime.Now.Subtract(item.CreateTime);
                if (ts.Days < model.Days) {
                    orders.Add(item);
                }
            }

            view.Result = orders;
            var modelType = "Order".GetTypeByName();
            var result = Resolve<IAdminTableService>().ToExcel(modelType, view);
            var url = webSite.ApiImagesUrl + "/wwwroot/excel/" + result.Item3;
            return ApiResult.Success(url);
        }
    }
}