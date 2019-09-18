﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Dtos;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Themes.Dtos.Service;
using Alabo.App.Shop.Activitys.Modules.GroupBuy.Dtos;
using Alabo.App.Shop.Activitys.Modules.GroupBuy.Service;
using Alabo.App.Shop.Product.DiyModels;
using Alabo.App.Shop.Product.Domain.Dtos;
using Alabo.Domains.Enums;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;

namespace Alabo.App.Shop.Activitys.Modules.GroupBuy.Controller {

    /// <summary>
    ///     Class GroupBuyApiController.
    ///     拼团Api
    /// </summary>
    [ApiExceptionFilter]
    [Route("Api/GroupBuy/[action]")]
    public class GroupBuyApiController : ApiBaseController {

        /// <summary>
        /// </summary>
        public GroupBuyApiController() : base() {
        }

        /// <summary>
        ///     拼团记录,根据商品Id获取商品的拼团记录
        /// </summary>
        /// <param name="productId"></param>
        [HttpGet]
        [Display(Description = "拼团记录，根据商品id获取商品id获取商品的拼团记录")]
        public ApiResult<IList<GroupBuyProductRecord>> ProductRecord([FromQuery] long productId) {
            var result = Resolve<IGroupBuyService>().GetGroupBuyProductRecords(productId);
            if (result.Item1.Succeeded) {
                return ApiResult.Success(result.Item2);
            }

            return ApiResult.Failure<IList<GroupBuyProductRecord>>(result.Item1.ToString());
        }

        /// <summary>
        /// 拼团商品列表
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "拼团记录列表")]
        public ApiResult<ProductItemApiOutput> List([FromQuery] ApiBaseInput parameter) {
            var apiOutput = Resolve<IGroupBuyService>().GetProductItems(parameter);
            return ApiResult.Success(apiOutput);
        }

        /// <summary>
        ///     商品列表
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "拼团商品列表，对应zk-groupbuy")]
        public ApiResult<ProductItemApiOutput> GetListItem([FromQuery] WidgeInput parameter) {
            return null;
            //var apiOutput = Resolve<IProductService>().GetProductItems(parameter);
            //return ApiResult.Success(apiOutput);
        }

        /// <summary>
        /// 拼团商品列表
        /// </summary>
        /// <param name="parameter"></param>
        [HttpGet]
        [Display(Description = "拼团商品列表")]
        public ApiResult<ProductItemApiOutput> Products([FromQuery] ApiBaseInput parameter) {
            var apiOutput = Resolve<IGroupBuyService>().GetProductItems(parameter);
            return ApiResult.Success(apiOutput);
        }

        /// <summary>
        ///     订单拼团用户，根据订单Id获取订单拼团用户
        /// </summary>
        /// <param name="orderId">The product identifier.</param>
        [HttpGet]
        [Display(Description = "订单拼团用户，根据订单id获取订单拼团用户")]
        public ApiResult<IList<GroupBuyRecordUser>> OrderGroupUser([FromQuery] long orderId) {
            var result = Resolve<IGroupBuyService>().GetGrouyBuyUserByOrderId(orderId);
            if (result.Item1.Succeeded) {
                return ApiResult.Success(result.Item2);
            }

            return ApiResult.Failure<IList<GroupBuyRecordUser>>(result.Item1.ToString());
        }
    }
}