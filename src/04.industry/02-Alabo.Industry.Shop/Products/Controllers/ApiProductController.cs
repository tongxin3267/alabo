using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Shop.Activitys.Modules.MemberDiscount.Callbacks;
using Alabo.App.Shop.Product.DiyModels;
using Alabo.App.Shop.Product.Domain.CallBacks;
using Alabo.App.Shop.Product.Domain.Dtos;
using Alabo.App.Shop.Product.Domain.Enums;
using Alabo.App.Shop.Product.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Query;
using Alabo.Domains.Query.Dto;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Basic.Relations.Domain.Services;
using Alabo.Framework.Basic.Relations.Dtos;
using Alabo.Framework.Core.WebApis.Controller;
using Alabo.Framework.Core.WebApis.Filter;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Mapping;
using Alabo.Maps;
using Alabo.Randoms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Shop.Product.Controllers
{

    [ApiExceptionFilter]
    [Route("Api/Product/[action]")]
    public class ApiProductController : ApiBaseController//<Domain.Entities.Product, long>
    {
        public ApiProductController() : base() {
            //BaseService = Resolve<IProductService>();
        }

        /// <summary>
        ///     商品详情
        /// </summary>
        public ApiResult<ProductDetailView> ShowSync(long id, long userId = 0) {
            try {
                var result = Resolve<IProductService>().Show(id, userId);
                if (result.Item1.Succeeded) {
                    var productDetail = result.Item2.MapTo<ProductDetailView>();
                    var config = Resolve<IAutoConfigService>().GetValue<MemberDiscountConfig>();
                    var loginUser = Resolve<IUserService>().GetSingle(userId);
                    var isAdmin = Resolve<IUserService>().IsAdmin(userId);
                    productDetail.IsFrontShowPrice = true;

                    productDetail.DisplayPrice = Resolve<IProductService>().GetDisplayPrice(productDetail.Price, productDetail.PriceStyleId, 0M);
                    if (productDetail?.ProductExtensions?.ProductSkus?.Count > 0) {
                        productDetail.ProductExtensions.ProductSkus.Foreach(x => x.DisplayPrice = Resolve<IProductService>().GetDisplayPrice(productDetail.Price, productDetail.PriceStyleId, 0M));
                    }

                    return ApiResult.Success<ProductDetailView>(productDetail);
                } else {
                    return ApiResult.Failure<ProductDetailView>($"主库下单失败{result.Item1.ErrorMessages.Join()}");
                }
            } catch (Exception ex) {
                return ApiResult.Failure<ProductDetailView>(null, ex.Message);
            }
        }

        /// <summary>
        ///     商品详情
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <param name="userId">userid</param>
        [HttpGet]
        [Display(Description = "商品详情")]
        public ApiResult<ProductDetailView> Show(long id, long userId = 0) {
            ObjectCache.Remove("ApiProduct_" + id);
            return ObjectCache.GetOrSet(() => {
                var result = Resolve<IProductService>().Show(id, userId);
                if (result.Item1.Succeeded) {
                    var productDetail = result.Item2.MapTo<ProductDetailView>();
                    var config = Resolve<IAutoConfigService>().GetValue<MemberDiscountConfig>();
                    if ((AutoModel?.BasicUser?.Id ?? 0) != 0) {
                        userId = AutoModel.BasicUser.Id;
                    }
                    var loginUser = Resolve<IUserService>().GetSingle(userId);
                    var isAdmin = Resolve<IUserService>().IsAdmin(userId);
                    productDetail.IsFrontShowPrice = true;

                    productDetail.DisplayPrice = Resolve<IProductService>().GetDisplayPrice(productDetail.Price, productDetail.PriceStyleId, 0M);
                    if (productDetail?.ProductExtensions?.ProductSkus?.Count > 0) {
                        productDetail.ProductExtensions.ProductSkus.Foreach(x => x.DisplayPrice = Resolve<IProductService>().GetDisplayPrice(productDetail.Price, productDetail.PriceStyleId, 0M));
                    }
                    //bniuniu 不进行该判断
                    StringValues isTenant = string.Empty;

                    if (Request.Headers.TryGetValue("zk-tenant", out isTenant)) {
                        var tenant = Resolve<IUserService>().GetSingle(s => s.Mobile == isTenant.FirstOrDefault());
                        //如果有租户头 判断是否为空 ,如果不为空则表示有值
                        //if (isTenant.Where(s => !(string.IsNullOrEmpty(s) || s == "null"||s== "[object Null]")).Count() <= 0)
                        if (tenant == null)//不是租户 则判断是否显示价格
{
                            if (loginUser == null) {
                                productDetail.IsFrontShowPrice = false;
                                productDetail.PriceAlterText = config.PriceAlterText;
                            } else if (!isAdmin && loginUser.GradeId == Guid.Parse("72BE65E6-3000-414D-972E-1A3D4A366000")) {
                                productDetail.IsFrontShowPrice = config.IsFrontShowPrice;
                                productDetail.PriceAlterText = config.PriceAlterText;
                            }
                        }
                    } else {
                        //如果没有该头部 直接判断
                        if (loginUser == null) {
                            //未登录直接不允许查看
                            productDetail.IsFrontShowPrice = false;
                            productDetail.PriceAlterText = config.PriceAlterText;
                        } else if (!isAdmin && loginUser.GradeId == Guid.Parse("72BE65E6-3000-414D-972E-1A3D4A366000")) {
                            //如果不是管理员 且 会员等级为免费会员
                            productDetail.IsFrontShowPrice = config.IsFrontShowPrice;
                            productDetail.PriceAlterText = config.PriceAlterText;
                        }
                    }
                    return ApiResult.Success(productDetail);
                }

                return ApiResult.Failure<ProductDetailView>(result.Item1.ToString());
            }, "ApiProduct_" + id).Value;
        }

        /// <summary>
        ///     商品列表，对应zk-product-item组件
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "商品列表，对应zk-product-item")]
        public ApiResult<ProductItemApiOutput> List1([FromQuery] ProductApiInput parameter) {
            var apiOutput = Resolve<IProductService>().GetProductItems(parameter);
            return ApiResult.Success(apiOutput);
        }

        /// <summary>
        /// 更新已售数据
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>

        [Display(Description = "更新已售数据")]
        public ApiResult UpdateSoldCount(long productId) {
            var config = Resolve<IAutoConfigService>().GetValue<ProductConfig>();
            if (config.IsAutoUpdateSold) {
                var result = Resolve<IProductService>().GetSingle(productId);
                if (result != null) {
                    result.SoldCount += RandomHelper.Number(1, 20);
                    Resolve<IProductService>().Update(result);
                    return ApiResult.Success(result.SoldCount);
                }
            }
            return ApiResult.Success();
        }

        /// <summary>
        ///     商品列表，对应zk-product-item组件
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "商品列表，对应zk-product-item")]
        public ApiResult<ProductItemApiOutput> List([FromQuery] ProductApiInput parameter) {
            var apiOutput = Resolve<IProductService>().GetProductItems(parameter);
            var config = Resolve<IAutoConfigService>().GetValue<MemberDiscountConfig>();

            if ((AutoModel?.BasicUser?.Id ?? 0) != 0) {
                parameter.LoginUserId = AutoModel.BasicUser.Id;
            }
            var loginUser = Resolve<IUserService>().GetSingle(parameter.LoginUserId);
            var isAdmin = Resolve<IUserService>().IsAdmin(parameter.LoginUserId);
            apiOutput.IsFrontShowPrice = true;

            // 不进行该判断
            StringValues isTenant = string.Empty;
            if (Request.Headers.TryGetValue("zk-tenant", out isTenant)) {
                //如果有租户头 判断是否为空 ,如果不为空则表示有值
                var tenant = Resolve<IUserService>().GetSingle(s => s.Mobile == isTenant.FirstOrDefault());
                //如果有租户头 判断是否为空 ,如果不为空则表示有值
                //if (isTenant.Where(s => !(string.IsNullOrEmpty(s) || s == "null"||s== "[object Null]")).Count() <= 0)
                if (tenant == null)//不是租户 则判断是否显示价格
{
                    if (loginUser == null) {
                        apiOutput.IsFrontShowPrice = false;
                        apiOutput.PriceAlterText = config.PriceAlterText;
                    } else if (!isAdmin && loginUser.GradeId == Guid.Parse("72BE65E6-3000-414D-972E-1A3D4A366000")) {
                        apiOutput.IsFrontShowPrice = config.IsFrontShowPrice;
                        apiOutput.PriceAlterText = config.PriceAlterText;
                    }
                }
            } else {
                //如果没有该头部 直接判断
                if (loginUser == null) {
                    apiOutput.IsFrontShowPrice = false;
                    apiOutput.PriceAlterText = config.PriceAlterText;
                } else if (!isAdmin && loginUser.GradeId == Guid.Parse("72BE65E6-3000-414D-972E-1A3D4A366000")) {
                    apiOutput.IsFrontShowPrice = config.IsFrontShowPrice;
                    apiOutput.PriceAlterText = config.PriceAlterText;
                }
            }

            return ApiResult.Success(apiOutput);
        }

        /// <summary>
        /// ListExt
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "商品列表，对应zk-product-item")]
        public ApiResult<ProductItemApiOutput> ListExt([FromQuery] ProductApiInput parameter) {
            if (!string.IsNullOrEmpty(parameter.Keyword)) {
                //TODO:111 搜索关键字在这里进行存储,存储用户id,搜索时间,关键字,会员等级
            }

            var apiOutput = Resolve<IProductService>().GetProductItems(parameter);

            return ApiResult.Success(apiOutput);
        }

        /// <summary>
        ///     商品列表，对应zk-product-item组件
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "商品列表，对应zk-product-item")]
        public ApiResult<ProductItemApiOutput> GetListItem([FromQuery] ProductApiInput parameter) {
            //return null;
            var apiOutput = Resolve<IProductService>().GetProductItems(parameter);
            return ApiResult.Success(apiOutput);
        }

        /// <summary>
        ///     商品分类Api接口
        /// </summary>
        /// <param name="id">主键ID</param>
        [HttpGet]
        [Display(Description = "商品分类Api接口")]
        public ApiResult<IList<RelationApiOutput>> Class(long id) {
            return ObjectCache.GetOrSet(() => {
                var productClassList = Resolve<IProductService>().GetProductClassList();
                if (productClassList == null) {
                    return ApiResult.Failure<IList<RelationApiOutput>>("商品分类不存在");
                }

                IList<RelationApiOutput> result = new List<RelationApiOutput>();

                var fartherClassList = productClassList.Where(r => r.FatherId == 0).ToList();

                foreach (var item in fartherClassList) {
                    var fartherOutput = AutoMapping.SetValue<RelationApiOutput>(item);
                    fartherOutput.Icon = Resolve<IApiService>().ApiImageUrl(fartherOutput.Icon);
                    var childClassList = productClassList.Where(r => r.FatherId == item.Id); //子分类
                    foreach (var child in childClassList) {
                        var childOutput = AutoMapping.SetValue<RelationApiOutput>(child);
                        childOutput.Icon = Resolve<IApiService>().ApiImageUrl(childOutput.Icon);
                        fartherOutput.ChildClass.Add(childOutput);
                    }
                    result.Add(fartherOutput);
                }

                return ApiResult.Success(result);
            }, "ApiClass_" + id).Value;
        }

        [HttpGet]
        public ApiResult GetProductRelation() {
            var productClassList = Resolve<IProductService>().GetProductRelations();
            return ApiResult.Success(productClassList);
        }

        /// <summary>
        /// 根据分类ID获取商品列表
        /// </summary>
        /// <param name="relationId"></param>
        /// <returns></returns>
        [HttpGet]
        [Display(Description = "根据分类ID获取商品列表")]
        public ApiResult GetProductListByRelationId(long relationId) {
            var result = Resolve<IProductService>().GetProductsByRelationId(relationId);
            return ApiResult.Success(result);
        }

        [HttpGet]
        [Display(Description = "根据商品Id获取店铺信息")]
        public ApiResult GetStoreInfo(long productId) {
            var result = Resolve<IProductService>().GetStoreInfoByProductId(productId);
            return ApiResult.Success(result);
        }

        public ApiResult GetRelation(long productId) {
            var result = Resolve<IProductService>().GetRecommendProduct(productId);

            result = result.Select(s => {
                s.ThumbnailUrl = Resolve<IApiService>().ApiImageUrl(s.ThumbnailUrl);
                return s;
            }).ToList();
            return ApiResult.Success(result);
        }

        [HttpGet]
        [Display(Description = "商品管理")]
        public ApiResult<PageResult<Alabo.App.Shop.Product.Domain.Entities.Product>> ProductList([FromQuery] PagedInputDto parameter) {
            var query = new ExpressionQuery<Domain.Entities.Product> {
                PageIndex = (int)parameter.PageIndex,
                PageSize = (int)parameter.PageSize,
                EnablePaging = true
            };
            //query.And(e => e.StoreId == parameter.StoreId);
            if (parameter.StoreId > 0) {
                query.And(s => s.StoreId == parameter.StoreId);
            }

            if (parameter.Bn != null) {
                query.And(s => s.Bn.Contains(parameter.Bn));
            }

            if (parameter.Name != null) {
                query.And(s => s.Name.Contains(parameter.Name));
            }

            query.OrderByDescending(e => e.Id);

            var list = Resolve<IProductService>().GetPagedList(query);
            PageResult<Domain.Entities.Product> apiRusult = new PageResult<Domain.Entities.Product> {
                PageCount = list.PageCount,
                Result = list,
                RecordCount = list.RecordCount,
                CurrentSize = list.CurrentSize,
                PageIndex = list.PageIndex,
                PageSize = list.PageSize,
            };

            if (list.Count < 0) {
                return ApiResult.Success(new PageResult<Domain.Entities.Product>());
            }
            return ApiResult.Success(apiRusult);
            //return ApiResult.Success(list);
        }

        /// <summary>
        /// 审核商品
        /// </summary>
        /// <param name="input">审核信息</param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "商品审核")]
        public ApiResult AuditProduct([FromBody]AuditProductInput input) {
            try {
                var model = Resolve<IProductService>().GetByIdNoTracking(input.Id);

                if (input.State == ProductStatus.FailAudited) {
                    var productDetail = Resolve<IProductDetailService>().GetSingle(s => s.ProductId == input.Id);

                    productDetail.ProductDetailExtension.AidutMessage = input.AuditMessage;
                    productDetail.Extension = productDetail.ProductDetailExtension.ToJsons();
                    Resolve<IProductDetailService>().Update(productDetail); // 添加Shop_productdetai表
                }
                model.ProductStatus = input.State;

                //添加商品分类和标签
                Resolve<IRelationIndexService>().AddUpdateOrDelete<ProductClassRelation>(input.Id, string.Join(',', input.StoreClass));
                Resolve<IRelationIndexService>().AddUpdateOrDelete<ProductTagRelation>(input.Id, string.Join(',', input.Tags));
                Resolve<IProductService>().Update(model);
            } catch (Exception ex) {
                return ApiResult.Failure(ex.Message);
            }
            return ApiResult.Success();
        }

        /// <summary>
        /// 供应商商品下架
        /// </summary>
        /// <param name="Id">审核信息</param>
        /// <returns></returns>
        [HttpPost]
        [Display(Description = "商品下架")]
        public ApiResult SoldOutProduct([FromBody]long Id) {
            try {
                var model = Resolve<IProductService>().GetByIdNoTracking(Id);
                model.ProductStatus = ProductStatus.SoldOut;
                Resolve<IProductService>().Update(model);
            } catch (Exception ex) {
                return ApiResult.Failure(ex.Message);
            }
            return ApiResult.Success();
        }

        /// <summary>
        ///     商品列表，对应zk-product-item组件
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "商品列表，对应zk-product-item")]
        public async Task<ApiResult<ProductItemApiOutput>> ListAsync([FromQuery] ProductApiInput parameter) {
            var apiOutput = await Resolve<IProductService>().GetProductItemsAsync(parameter);

            return await Task.FromResult(ApiResult.Success(apiOutput));
        }
    }
}