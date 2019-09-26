using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Base.Services;
using Alabo.Domains.Services;
using Alabo.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Alabo.Core.Reflections.Interfaces;
using Alabo.Core.Reflections.Services;
using ZKCloud.Open.ApiBase.Configuration;
using ZKCloud.Open.ApiBase.Services;

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
    }
}