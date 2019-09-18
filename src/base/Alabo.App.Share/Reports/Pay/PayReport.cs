using Quartz;
using System.Threading.Tasks;
using Alabo.App.Core.Reports;
using Alabo.App.Core.Reports.Domain.Services;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Core.Enums.Enum;
using Alabo.Dependency;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Schedules.Job;

namespace Alabo.App.Share.Reports.Pay {

    public class PayReport : IReportModel {

        /// <summary>
        /// 总交易额
        /// </summary>
        public long TotalAmount { get; set; }

        /// <summary>
        /// 今日交易额
        /// </summary>
        public long TodayAmount { get; set; }

        /// <summary>
        /// 昨日交易额
        /// </summary>
        public long YesterdayAmount { get; set; }
    }

    public class PayReportSchedule : JobBase {

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
            var payReport = new PayReport();

            var sql = $"select COUNT(Amount) from finance_pay where Type={(int)CheckoutType.Order} and Status={(int)PayStatus.Success}";
            payReport.TotalAmount = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0);
            sql = $"select COUNT(Amount) from finance_pay where Type={(int)CheckoutType.Order} and Status={(int)PayStatus.Success} and DATEDIFF(DAY,CreateTime,GETDATE())=0 ";
            payReport.TodayAmount = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0);

            sql = $"select COUNT(Amount) from finance_pay where Type={(int)CheckoutType.Order} and Status={(int)PayStatus.Success} and DATEDIFF(DAY,CreateTime,GETDATE())=1 ";
            payReport.YesterdayAmount = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0);

            //添加统计数据到数据库
            scope.Resolve<IReportService>().AddOrUpdate<PayReport>(payReport);
        }
    }
}