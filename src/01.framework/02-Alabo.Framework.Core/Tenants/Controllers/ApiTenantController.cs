using Alabo.Core.Tenants.Domains.Dtos;
using Alabo.Core.Tenants.Domains.Services;
using Alabo.Core.WebApis.Controller;
using Alabo.Extensions;
using Alabo.Runtime;
using Alabo.Tenants.Domain.Entities;
using Alabo.Tenants.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Core.Tenants.Controllers {

    [Route("Api/Tenant/[action]")]
    public class ApiTenantController : ApiBaseController<Tenant, ObjectId> {

        public ApiTenantController() : base() {
            BaseService = Resolve<ITenantService>();
        }

        #region 租户初始化默认数据

        /// <summary>
        /// 初始化所有的默认数据
        /// </summary>
        /// <param name="tenantInit"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ApiResult DeleteTenant([FromBody] TenantInit tenantInit) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason());
            }

            tenantInit.IsTenant = false;
            var result = Resolve<ITenantCreateService>().DeleteTenant(tenantInit);
            return ToResult(result);
        }

        #endregion 租户初始化默认数据

        #region 租户初始化默认数据

        /// <summary>
        /// 初始化所有的默认数据
        /// </summary>
        /// <param name="tenantInit"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ApiResult InitAll([FromBody] TenantInit tenantInit) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason());
            }

            tenantInit.IsTenant = false;
            var result = Resolve<ITenantCreateService>().InitTenantDefaultData(tenantInit);
            return ToResult(result);
        }

        #endregion 租户初始化默认数据

        #region 租户初始化默认数据

        /// <summary>
        /// 租户初始化默认数据
        /// </summary>
        /// <param name="tenantInit"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ApiResult InitDefault([FromBody] TenantInit tenantInit) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason());
            }
            tenantInit.IsTenant = true;
            var result = Resolve<ITenantCreateService>().InitTenantDefaultData(tenantInit);
            return ToResult(result);
        }

        #endregion 租户初始化默认数据

        #region 租户初始化模板数据

        /// <summary>
        /// 租户初始化模板数据
        /// </summary>
        /// <param name="tenantInit"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ApiResult InitTheme([FromBody] TenantInit tenantInit) {
            if (!this.IsFormValid()) {
                return ApiResult.Failure(this.FormInvalidReason());
            }

            var result = Resolve<ITenantCreateService>().InitTenantTheme(tenantInit);
            return ToResult(result);
        }

        #endregion 租户初始化模板数据

        #region 租户信息

        /// <summary>
        /// 检查租户不存在
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ApiResult NoTenant(string tenant) {
            var result = Resolve<ITenantCreateService>().NoTenant(tenant);
            return ToResult(result);
        }

        // <summary>
        /// 检查租户存在
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public ApiResult HaveTenant(string tenant) {
            var result = Resolve<ITenantCreateService>().HaveTenant(tenant);
            return ToResult(result);
        }

        /// <summary>
        /// 创建租户
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ApiResult CreateSassTenant([FromBody]Tenant tenant) {
            if (tenant == null) {
                return ApiResult.Failure("对象不能为空");
            }

            var result = Resolve<ITenantCreateService>().InitTenantDatabase(tenant.Sign);
            if (result.Succeeded) {
                tenant.DatabaseName = RuntimeContext.GetTenantDataBase(tenant.Sign);
                Resolve<ITenantService>().Add(tenant);
            }

            return ToResult(result);
        }

        #endregion 租户信息
    }
}