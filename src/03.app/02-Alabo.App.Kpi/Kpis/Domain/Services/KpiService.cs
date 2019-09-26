using System.Linq;
using Alabo.App.Share.Kpi.Domain.CallBack;
using Alabo.App.Share.Kpi.Domain.Repositories;
using Alabo.App.Share.Kpi.ViewModels;
using Alabo.Data.People.Teams.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Exceptions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Users.Entities;

namespace Alabo.App.Share.Kpi.Domain.Services {

    using System;
    using System.Collections.Generic;
    using Alabo.Framework.Core.Enums.Enum;
    using Kpi = Entities.Kpi;

    /// <summary>
    ///
    /// </summary>
    public class KpiService : ServiceBase<Kpi, long>, IKpiService {

        /// <summary>
        /// The kpi repository
        /// </summary>
        private readonly IKpiRepository _kpiRepository;

        /// <summary>
        /// 加入记录，计算TotalValue的值
        /// </summary>
        /// <param name="kpi">The KPI.</param>
        /// <exception cref="Exception">请设置时间方式</exception>
        public void AddSingle(Kpi kpi) {
            if (Convert.ToInt16(kpi.Type) <= 0) {
                throw new ValidException("请设置时间方式");
            }
            var lastSingle = GetLastSingle(kpi);
            if (lastSingle != null) {
                kpi.TotalValue = lastSingle.TotalValue + kpi.Value; // 值叠加
            } else {
                kpi.TotalValue = kpi.Value;
            }

            Add(kpi);
        }

        /// <summary>
        /// 获取最后一条记录
        /// </summary>
        /// <param name="kpi"></param>
        public Kpi GetLastSingle(Kpi kpi) {
            return _kpiRepository.GetLastSingle(kpi);
        }

        /// <summary>
        /// 获取绩效列表
        /// </summary>
        /// <param name="query">查询</param>
        public PagedList<KpiView> GetKpiList(object query) {
            var config = Resolve<IAutoConfigService>().GetList<KpiAutoConfig>();
            var model = Resolve<IKpiService>().GetPagedList<KpiView>(query);
            model.ForEach(r => {
                r.Module = config?.FirstOrDefault(u => u.Id == r.ModuleId)?.Name;
            });
            return model;
        }

        /// <summary>
        /// 根据Kpi访问和用户获取团队方式
        /// </summary>
        /// <param name="kpiTeamType"></param>
        /// <param name="user">用户</param>
        public IList<User> GetUserByKpiTeamType(KpiTeamType kpiTeamType, User user) {
            var userList = new List<User>();
            if (user != null) {
                //会员本身
                if (kpiTeamType == KpiTeamType.UserSelf) {
                    userList.Add(user);
                }
                // 直推
                if (kpiTeamType == KpiTeamType.RecommendUser) {
                    if (user.ParentId > 0) {
                        var parentUser = Resolve<IUserService>().GetSingle(user.ParentId);
                        if (parentUser != null) {
                            userList.Add(parentUser);
                        }
                    }
                }

                // 团队
                if (kpiTeamType == KpiTeamType.TeamUser) {
                    userList = Resolve<ITeamService>().GetTeamByGradeId(user.Id, user.GradeId, true).ToList();
                }
            }

            return userList;
        }

        public KpiService(IUnitOfWork unitOfWork, IRepository<Kpi, long> repository) : base(unitOfWork, repository) {
            _kpiRepository = Repository<IKpiRepository>();
        }
    }
}