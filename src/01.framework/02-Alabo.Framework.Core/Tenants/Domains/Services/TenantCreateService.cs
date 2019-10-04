using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Core.Admins.Services;
using Alabo.Framework.Core.Tenants.Domains.Dtos;
using Alabo.Framework.Core.Tenants.Domains.Repositories;
using Alabo.Helpers;
using Alabo.Runtime;
using Alabo.Tables.Domain.Services;
using Alabo.Tenants;
using Alabo.Tenants.Domain.Services;
using Alabo.Users.Services;

namespace Alabo.Framework.Core.Tenants.Domains.Services {

    public class TenantCreateService : ServiceBase, ITenantCreateService {
        private readonly ITenantCreateRepository _tenantCreateRepository;

        public TenantCreateService(IUnitOfWork unitOfWork, ITenantCreateRepository tenantCreateRepository)
            : base(unitOfWork) {
            _tenantCreateRepository = tenantCreateRepository;
        }

        public ServiceResult DeleteTenant(TenantInit tenantInit) {
            var result = Check(tenantInit);
            if (!result.Succeeded) {
                return result;
            }

            if (tenantInit.Tenant == "master") {
                return ServiceResult.FailedWithMessage("主库不能初始化");
            }

            var dataBase = RuntimeContext.GetTenantMongodbDataBase(tenantInit.Tenant);
            // 删除数据库
            _tenantCreateRepository.DeleteDatabase(dataBase);
            return ServiceResult.Success;
        }

        public ServiceResult InitTenantDefaultData(TenantInit tenantInit) {
            var result = Check(tenantInit);
            if (!result.Succeeded) {
                return result;
            }

            // 初始化站点信息
            Resolve<ITenantService>().InitSite();

            if (HttpWeb.Site == null) {
                return ServiceResult.FailedWithMessage("站点信息初始化错误");
            }
            //初始化默认数据
            Resolve<IAdminService>().DefaultInit(tenantInit.IsTenant);
            // 修改管理员的账号
            var user = Resolve<IAlaboUserService>().GetSingle(r => r.UserName == "admin");
            if (user != null) {
                user.Mobile = tenantInit.Mobile;
                Resolve<IAlaboUserService>().Update(user);
            }

            return ServiceResult.Success;
        }

        public ServiceResult InitTenantTheme(TenantInit tenantInit) {
            var result = Check(tenantInit);
            if (!result.Succeeded) {
                return result;
            }
            //DOTO  2019年9月21日重构
            // 初始化后台模板
            //ClientPageInput pageInput = new ClientPageInput {
            //    ClientType = ClientType.PcWeb,
            //    Type = ThemeType.Admin
            //};
            //try {
            //    var allClientPages = Resolve<IThemePageService>().GetAllClientPages(pageInput);
            //    allClientPages.LastUpdate = DateTime.Now.AddMinutes(10).ConvertDateTimeInt();
            //} catch (Exception ex) {
            //    return ServiceResult.FailedWithMessage("后台模板初始化出错" + ex.Message);
            //}
            return ServiceResult.Success;
        }

        public ServiceResult InitTenantDatabase(string tenant) {
            if (!TenantContext.IsTenant) {
               //  return ServiceResult.FailedWithMessage("非租户模式不能创建租户");
            }

            if (string.IsNullOrWhiteSpace(tenant)) {
                return ServiceResult.FailedWithMessage("租户不能为空");
            }

            var tenantName = RuntimeContext.GetTenantMongodbDataBase(tenant);
            var isExists = _tenantCreateRepository.IsExistsDatabase(tenantName);
            if (!isExists) {
                _tenantCreateRepository.CreateDatabase(tenantName);
            }

            isExists = _tenantCreateRepository.IsExistsDatabase(tenantName);
            if (isExists == false) {
                return ServiceResult.FailedWithMessage($"数据库创建失败，用户{RuntimeContext.Current.WebsiteConfig.MsSqlDbConnection.UserName}没有 CREATE DATABASE 权限，请手动添加数据库用户的 CREATE DATABASE 权限");
            }

            return ServiceResult.Success;
        }

        public ServiceResult HaveTenant(string tenant) {
            if (tenant.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("租户标识不能为空");
            }

            var find = Resolve<ITenantService>().GetSingle(r => r.Sign == tenant);
            if (find == null) {
                return ServiceResult.FailedWithMessage("租户不存在");
            }

            var tenantName = RuntimeContext.GetTenantMongodbDataBase(tenant);
            var isExists = _tenantCreateRepository.IsExistsDatabase(tenantName);
            if (!isExists) {
                return ServiceResult.FailedWithMessage("租户数据库不存在");
            }

            return ServiceResult.Success;
        }

        public ServiceResult NoTenant(string tenant) {
            if (tenant.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("租户标识不能为空");
            }

            var isExists = _tenantCreateRepository.IsExistsDatabase(tenant);
            if (isExists) {
                return ServiceResult.FailedWithMessage("租户数据库已存在");
            }

            return ServiceResult.Success;
        }

        public string Token(string tenant, string siteId) {
            var key = tenant + siteId;
            var token =
                $"{key.ToMd5HashString().Substring(1, 20)}{key.ToMd5HashString().Substring(15, 10)}{tenant.ToMd5HashString().Substring(5, 10)}";
            return token.ToLower().Trim();
        }

        private ServiceResult Check(TenantInit tenantInit) {
            var tenantToken = HttpWeb.HttpContext.Request.Headers["zk-tenant"];
            if (TenantContext.CurrentTenant == TenantContext.Master) {
                return ServiceResult.FailedWithMessage("主库不能初始化");
            }

            if (tenantToken == "master") {
                return ServiceResult.FailedWithMessage("主库不能初始化");
            }

            if (tenantToken.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("租户头不能为空");
            }

            if (tenantInit.Mobile.IsNullOrEmpty()) {
                return ServiceResult.FailedWithMessage("手机号码不能为空");
            }

            if (!Resolve<IOpenService>().CheckMobile(tenantInit.Mobile)) {
                return ServiceResult.FailedWithMessage("手机号码格式不正确");
            }

            if (tenantToken != tenantInit.Tenant) {
                return ServiceResult.FailedWithMessage("租户标识输入错误");
            }

            var siteId = RuntimeContext.Current.WebsiteConfig.OpenApiSetting.Id;
            var token = Token(tenantInit.Tenant, siteId);
            if (token != tenantInit.Token) {
                return ServiceResult.FailedWithMessage("Token加密不正确");
            }

            return ServiceResult.Success;
        }
    }
}