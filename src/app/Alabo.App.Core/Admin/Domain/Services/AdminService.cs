using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Alabo.App.Core.Admin.Domain.Dtos;
using Alabo.App.Core.Admin.Domain.Repositories;
using Alabo.App.Core.Admin.Extensions;
using Alabo.App.Core.Themes.Clients;
using Alabo.App.Core.User.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Base.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Helpers;
using Alabo.Initialize;
using ZKCloud.Open.ApiBase.Configuration;
using ZKCloud.Open.ApiBase.Services;
using Alabo.Runtime;

namespace Alabo.App.Core.Admin.Domain.Services {

    /// <summary>
    ///     Class AdminService.
    /// </summary>
    /// <seealso cref="Alabo.Domains.Services.ServiceBase" />
    /// <seealso cref="Alabo.App.Core.Admin.Domain.Services.IAdminService" />
    public class AdminService : ServiceBase, IAdminService {

        /// <summary>
        ///     The admin cache key
        /// </summary>
        private static readonly string _adminCacheKey = "AdminCacheKey_";

        private IServerApiClient _userApiClient;
        private RestClientConfiguration _restClientConfiugration;

        private IThemeClient _themeClient;

        /// <summary>
        /// 租户初始时候需要速度比较快
        /// </summary>
        /// <returns></returns>
        public void DefaultInit(bool isTenant = false) {
            if (!isTenant) {
                // 先更新数据库脚本,非租户时执行
                Resolve<ICatalogService>().UpdateDatabase();
            }

            //初始化实体类表结构
            Resolve<ITableService>().Init();

            // 其他项目的默认数据
            var dic = new Dictionary<string, long>();
            var types = Resolve<ITypeService>().GetAllTypeByInterface(typeof(IDefaultInit)).ToList();
            types.ForEach(item => {
                var attr = item.GetCustomAttribute<DefaultInitSortAttribute>();
                if (attr == null) {
                    attr = new DefaultInitSortAttribute();
                }
                if (!dic.ContainsKey(item.FullName)) {
                    dic.Add(item.FullName, attr.SortIndex);
                }
            });
            //asc sort
            var dicSort = from item in dic orderby item.Value ascending select item;
            foreach (var item in dicSort) {
                var time1 = DateTime.Now;
                var type = types.Find(t => t.FullName == item.Key);
                if (type == null) {
                    continue;
                }
                try {
                    var config = Activator.CreateInstance(type);
                    if (config is IDefaultInit set) {
                        try {
                            if (isTenant) {
                                if (set.IsTenant) {
                                    set.Init(); ;
                                }
                            } else {
                                // 非租户执行模式
                                set.Init(); ;
                            }
                        } catch (Exception ex) {
                            Trace.WriteLine(ex.Message);
                        }
                    }
                } catch (Exception ex) {
                    Trace.WriteLine(ex.Message);
                }

                var time2 = DateTime.Now;
            }
            Resolve<IAdminService>().ClearCache();
        }

        public AdminService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        /// <summary>
        ///     Clears the cache.
        /// </summary>
        public void ClearCache() {
            ObjectCache.Clear();
        }

        public async Task CheckData() {
            // 其他项目的默认数据
            var types = Resolve<ITypeService>().GetAllTypeByInterface(typeof(ICheckData));
            foreach (var item in types) {
                var config = Activator.CreateInstance(item);
                if (config is ICheckData set) {
                    set.Execute();
                    await set.ExcuteAsync();
                }
            }
        }

        public ServiceResult TruncateTable(TruncateInput truncateInput) {
            if (!HttpWeb.UserName.Equal("admin")) {
                return ServiceResult.FailedWithMessage("当前操作用户名非admin,不能进行该操作");
            }

            var user = Resolve<IUserService>().GetSingle(HttpWeb.UserId);
            if (user.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("用户状态不正常,不能进行该操作");
            }
            if (!user.UserName.Equal("admin")) {
                return ServiceResult.FailedWithMessage("当前操作用户名非admin,不能进行该操作");
            }

            if (!truncateInput.UserTable.Equals("User_User")) {
                return ServiceResult.FailedWithMessage("用户数据表填写出错,不能进行该操作");
            }
            if (!truncateInput.MongoTableName.Equals(RuntimeContext.Current.WebsiteConfig.MongoDbConnection.Database)) {
                return ServiceResult.FailedWithMessage("Mongodb数据库表填写出错,不能进行该操作");
            }
            if (!truncateInput.ScheduleTable.Equals("Task_Schedule")) {
                return ServiceResult.FailedWithMessage("调度作业表填写错误,不能进行该操作");
            }
            //if (!truncateInput.ProjectId.Equals(HttpWeb.Token.ProjectId.ToString())) {
            //    return ServiceResult.FailedWithMessage("项目Id填写错误,不能进行该操作");
            //}

            if (!truncateInput.Key.Equals(RuntimeContext.Current.WebsiteConfig.OpenApiSetting.Key)) {
                return ServiceResult.FailedWithMessage("秘钥填写错误,不能进行该操作");
            }

            if (!truncateInput.Mobile.Equals(Resolve<IOpenService>().OpenMobile)) {
                return ServiceResult.FailedWithMessage("平台预留手机号码填写错误,不能进行该操作");
            }

            //if (!truncateInput.CompanyName.Equals(Resolve<IOpenService>().Project.CompanyName)) {
            //    return ServiceResult.FailedWithMessage("公司名称填写错误,不能进行该操作");
            //}

            Repository<ICatalogRepository>().TruncateTable();

            // 先更新数据库脚本
            Resolve<ICatalogService>().UpdateDatabase();

            // 清空缓存
            ClearCache();
            Resolve<ITableService>().Log("清空数据");
            Resolve<IOpenService>().SendRaw(Resolve<IOpenService>().OpenMobile, "您的系统数据已清空，请悉知。");
            return ServiceResult.Success;
        }

        public TruncateInput TruncateView(object id) {
            if (id.ConvertToLong() == 10000) {
                // var project = Resolve<IOpenService>().Project;
                var truncateInput = new TruncateInput {
                    //  CompanyName = project.CompanyName,
                    // Mobile = project.Phone,
                    UserTable = "User_User",
                    ScheduleTable = "Task_Schedule",
                    Key = RuntimeContext.Current.WebsiteConfig.OpenApiSetting.Key,
                    // ProjectId = project.ProjectId.ToString(),
                };
                return truncateInput;
            }
            return new TruncateInput(); ;
        }
    }
}