using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alabo.App.Core.Reports;
using Alabo.App.Core.Reports.Domain.Services;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Dependency;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Schedules.Job;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Open.Reports.Finance {

    /// <summary>
    /// 分润数据统计
    /// </summary>
    [ClassProperty(Name = "分润数据统计", Icon = "fa fa-building", Description = "分润数据统计")]
    public class RewardReport : IReportModel {

        /// <summary>
        /// 统计数据
        /// </summary>
        public Guid MoneyTypeId { get; set; }

        public decimal Amount { get; set; }

        /// <summary>
        /// 比例
        /// </summary>
        public decimal Raido { get; set; } = 0.00000m;
    }

    /// <summary>
    /// 数据统计后台服务
    /// </summary>
    public class RewardReportSchedule : JobBase {

        /// <summary>
        /// 数据统计服务
        /// </summary>
        protected override async Task Execute(IJobExecutionContext context, IScope scope) {
            //获取底层访问容器对象

            //获取数据库操作对象
            var repositoryContext = scope.Resolve<IUserRepository>().RepositoryContext;
            if (repositoryContext == null) {
                return;
            }

            // 统计会员总数
            var sql = "select sum(Amount) MoneyTypeId from  Share_Reward";
            var totalAmount = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0);

            var list = new List<RewardReport>();
            //获取等级服务
            var moneyTypeSql = $"select sum(Amount) as Amount, MoneyTypeId from  Share_Reward  group by MoneyTypeId";

            using (var dr = repositoryContext.ExecuteDataReader(moneyTypeSql)) {
                while (dr.Read()) {
                    // 有bug
                    //list.Add(
                    //new RewardReport { MoneyTypeId = dr["MoneyTypeId"].ToGuid(), Amount = dr["Amount"].ToDecimal(), Raido = dr["Amount"].ToDecimal() / (decimal)totalAmount });
                }
            }

            //添加统计数据到数据库
            scope.Resolve<IReportService>().AddOrUpdate<RewardReport>(list);
        }
    }
}