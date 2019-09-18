﻿using System;
using System.Linq;
using Alabo.App.Core.Admin.Domain.Repositories;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Tenants.Callbacks;
using Alabo.App.Core.Tenants.Domains.Dtos;
using Alabo.App.Core.Tenants.UI;
using Alabo.App.Core.Themes.Domain.Enums;
using Alabo.App.Core.Themes.Domain.Services;
using Alabo.App.Core.Themes.Dtos;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Core.Enums.Enum;
using Alabo.Core.Extensions;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Base.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Runtime;
using Alabo.Tenants;
using Alabo.Tenants.Domain.Entities;
using Alabo.Tenants.Domain.Services;

namespace Alabo.App.Core.Tenants.Domains.Services {

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
            var dataBase = RuntimeContext.GetTenantDataBase(tenantInit.Tenant);
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
            var user = Resolve<IUserService>().GetSingle(r => r.UserName == "admin");
            if (user != null) {
                user.Mobile = tenantInit.Mobile;
                Resolve<IUserService>().Update(user);
            }
            return ServiceResult.Success;
        }

        public ServiceResult InitTenantTheme(TenantInit tenantInit) {
            var result = Check(tenantInit);
            if (!result.Succeeded) {
                return result;
            }

            // 初始化后台模板
            ClientPageInput pageInput = new ClientPageInput {
                ClientType = ClientType.PcWeb,
                Type = ThemeType.Admin
            };
            try {
                var allClientPages = Resolve<IThemePageService>().GetAllClientPages(pageInput);
                allClientPages.LastUpdate = DateTime.Now.AddMinutes(10).ConvertDateTimeInt();
            } catch (Exception ex) {
                return ServiceResult.FailedWithMessage("后台模板初始化出错" + ex.Message);
            }
            return ServiceResult.Success;
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

        public ServiceResult InitTenantDatabase(string tenant) {
            if (!TenantContext.IsTenant) {
                return ServiceResult.FailedWithMessage("非租户模式不能创建租户");
            }

            if (string.IsNullOrWhiteSpace(tenant)) {
                return ServiceResult.FailedWithMessage("租户不能为空");
            }

            var tenantName = RuntimeContext.GetTenantDataBase(tenant);
            var isExists = _tenantCreateRepository.IsExistsDatabase(tenantName);
            if (!isExists) {
                _tenantCreateRepository.CreateDatabase(tenantName);
            }
            isExists = _tenantCreateRepository.IsExistsDatabase(tenantName);
            if (isExists == false) {
                return ServiceResult.FailedWithMessage("数据库创建失败，没有 CREATE DATABASE 权限，请手动添加数据库用户的 CREATE DATABASE 权限");
            }
            return ServiceResult.Success;
        }

        /// <summary>
        /// 保存租户
        /// </summary>
        /// <param name="user"></param>
        private void SaveTenant(User.Domain.Entities.User user, TenantInput tenantInput) {
            // 此方法保存的数据需要切回到Master上.
            TenantContext.CurrentTenant = TenantContext.Master;
            var tenantService = Resolve<ITenantService>();
            var exists = tenantService.Exists(t => t.Name.ToLower() == user.UserName.ToLower());
            if (exists) {
                return;
            }

            var tenant = new Tenant {
                UserId = user.Id,
                UserName = user.UserName,
                Sign = user.UserName.Trim(),
                ServiceUrl = tenantInput.ServiceUrl,
                ClientUrl = tenantInput.ClientUrl,
                Name = user.GetUserName(),
                DatabaseName = RuntimeContext.GetTenantDataBase(user.UserName)
            };
            Resolve<ITenantService>().Add(tenant);
        }

        public ServiceResult Create(TenantInput tenantInput) {
            var user = Resolve<IUserService>().GetNomarlUser(tenantInput.UserId);
            if (user == null) {
                return ServiceResult.FailedWithMessage("用户不存在或状态不正常");
            }

            var find = Resolve<ITenantService>().GetSingle(r => r.UserId == user.Id);
            if (find != null) {
                return ServiceResult.FailedWithMessage("当前用户已是租户，不能重复创建");
            }

            var serviceConfig = Resolve<IAutoConfigService>()
                .GetList<TenantServiceHostConfig>(r => r.Id == tenantInput.TenantServiceHostConfigId)?.FirstOrDefault();
            if (serviceConfig == null) {
                return ServiceResult.FailedWithMessage("请选择Api服务器配置地址");
            }

            var clientConfig = Resolve<IAutoConfigService>()
                .GetList<TenantClientHostConfig>(r => r.Id == tenantInput.TenantClientHostConfigId)?.FirstOrDefault();
            if (clientConfig == null) {
                return ServiceResult.FailedWithMessage("请选择客服端访问地址");
            }

            var userName = user.UserName;
            tenantInput.ClientUrl = clientConfig.Url;
            tenantInput.ServiceUrl = serviceConfig.Url;

            try {
                var result = InitTenantDatabase(userName);
                if (result.Succeeded) {
                    SaveTenant(user, tenantInput);
                } else {
                    return result;//报错直接返回咯
                }
            } catch (Exception ex) {
                throw ex;
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
            var tenantName = RuntimeContext.GetTenantDataBase(tenant);
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
    }
}