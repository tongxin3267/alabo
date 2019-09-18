using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Api.Controller;
using Alabo.App.Core.Api.Filter;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Core.Enums.Enum;
using Alabo.Core.Regex;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Mapping;
using ZKCloud.Open.ApiBase.Models;
using Alabo.RestfulApi;
using Alabo.UI;
using Alabo.UI.AutoForms;
using System.Linq;

namespace Alabo.App.Core.User.Controllers {

    [ApiExceptionFilter]
    [Route("Api/UserAddress/[action]")]
    public class ApiUserAddressController : ApiBaseController<UserAddress, ObjectId> {

        public ApiUserAddressController() : base() {
            BaseService = Resolve<IUserAddressService>();
        }

        /// <summary>
        ///     前端H5 vant组件地址json格式
        /// </summary>
        [HttpGet]
        [Display(Description = "vant组件地址json格式")]
        public ApiResult<VantAddress> VantAddress() {
            if (BaseService == null) {
                return ApiResult.Failure<VantAddress>("请在控制器中定义BaseService");
            }
            var result = Resolve<IUserAddressService>().GetVantAddress();
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     Gets the reg form.
        /// </summary>
        [HttpGet]
        [Display(Description = "获取添加地址视图")]
        public ApiResult<AutoForm> GetAddAddressForm() {
            var view = AutoFormMapping.Convert<UserAddress>();
            return null;
        }

        /// <summary>
        ///     更新、添加、保存收货地址
        /// </summary>
        [HttpPost]
        [Display(Description = "更新、添加、保存收货地址")]
        [ApiAuth]
        public ApiResult SaveOrderAddress([FromBody] AddressInput parameter) {
            if (parameter == null) {
                return ApiResult.Failure("所在地区不能为空");
            }

            var regex = RegexHelper.ChinaMobile.IsMatch(parameter.Mobile);
            if (!regex) {
                return ApiResult.Failure("联系电话格式不正确");
            }
            if (parameter.Mobile.IsNullOrEmpty()) {
                return ApiResult.Failure("联系电话不能为空");
            }

            if (parameter.Address.IsNullOrEmpty()) {
                return ApiResult.Failure("详细地址不能为空");
            }

            if (parameter.Name.IsNullOrEmpty()) {
                return ApiResult.Failure("收货人姓名不能为空");
            }

            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var serviceResult = Resolve<IUserAddressService>().SaveOrderAddress(parameter);
            return ToResult(serviceResult);
        }

        /// <summary>
        ///     更新、添加、保存会员备案地址(会员中心地址)
        /// </summary>
        [HttpPost]
        [Display(Description = "备案地址修改")]
        [ApiAuth]
        public ApiResult SaveUserInfoAddress([FromBody] UserInfoAddressInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            parameter.Type = AddressLockType.UserInfoAddress;
            var serviceResult = Resolve<IUserAddressService>().SaveUserInfoAddress(parameter);
            return ToResult(serviceResult);
        }

        /// <summary>
        ///     获取用户所有地址,包括备案地址、收货地址、发货地址
        /// </summary>
        [HttpGet]
        [Display(Description = "获取用户地址数据")]
        [ApiAuth]
        public ApiResult<List<UserAddress>> Get([FromQuery] long loginUserId) {
            var result = Resolve<IUserAddressService>().GetAllList(loginUserId)
                .Where(x => x.Type != AddressLockType.CustomeShopOrderAddress).ToList();    // 自定义商城自动下单的地址排除掉不显示
            return ApiResult.Success(result);
        }

        /// <summary>
        ///     删除用户地址
        /// </summary>
        [HttpDelete]
        [Display(Description = "删除用户地址")]
        [ApiAuth]
        public ApiResult Delete([FromQuery] long loginUserId, string id) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }
            var serviceResult = Resolve<IUserAddressService>().Delete(loginUserId, id.ToObjectId());
            return ToResult(serviceResult);
        }

        /// <summary>
        ///     设置默认地址
        /// </summary>
        [HttpPost]
        [Display(Description = "设置默认地址")]
        [ApiAuth]
        public ApiResult SetDefault([FromBody] AddressDefaultInput parameter) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason(), MessageCodes.ParameterValidationFailure);
            }

            var serviceResult = Resolve<IUserAddressService>().SetDefault(parameter.UserId, parameter.Id.ToObjectId());
            return ToResult(serviceResult);
        }

        /// <summary>
        ///     Id值为空获取默认地址  如果没有默认地址则返回空   Id值存在则获取与Id相同的地址
        /// </summary>
        /// <param name="id">Id标识</param>
        /// <param name="loginUserId">登录会员Id</param>
        [ApiAuth]
        [HttpGet]
        [Display(Description = "id值为空获取默认地址 如果没有默认地址则返回值为空 id 值存在则获取与id相同的地址")]
        public ApiResult Single([FromQuery] string id, long loginUserId) {
            var result = Resolve<IUserAddressService>().GetUserAddress(id.ToObjectId(), loginUserId);
            if (result != null) {
                var regionName = Resolve<IRegionService>().GetSingle(u => u.RegionId == result.RegionId);
                result.AddressDescription = regionName.Name;
                return ApiResult.Success(result);
            }
            return ApiResult.Failure("没有数据");
        }
    }
}