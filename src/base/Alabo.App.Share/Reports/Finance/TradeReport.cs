using Quartz;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alabo.App.Core.Finance.Domain.Enums;
using Alabo.App.Core.Reports;
using Alabo.App.Core.Reports.Domain.Services;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Core.Enums.Enum;
using Alabo.Dependency;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Schedules.Job;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Share.Reports.Finance {

    [ClassProperty(Name = "交易数据统计", Icon = "fa fa-building", Description = "交易数据统计,提现，充值，订单支付")]
    public class TradeReport : IReportModel {

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 失败比例
        /// </summary>
        public decimal Raido { get; set; } = 0.00000m;

        public TradeType TradeType { get; set; }
    }

    public class TradeReportSchedule : JobBase {

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
            var list = new List<TradeReport>();
            // 提现
            var sql = $"select sum(amount) from  Finance_Trade where Type={(int)TradeType.Withraw}";
            var sqlRaido = $"select sum(amount) from  Finance_Trade where Type={(int)TradeType.Withraw} and Status={(int)TradeStatus.Pending}";
            var totalAmount = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0);
            var failAmount = repositoryContext.ExecuteScalar(sqlRaido).ToStr().ToLong(0);
            if (totalAmount == 0L) {
                list.Add(new TradeReport { TradeType = TradeType.Withraw, Amount = totalAmount, Raido = 0M });
            } else {
                list.Add(new TradeReport { TradeType = TradeType.Withraw, Amount = totalAmount, Raido = (decimal)failAmount / (decimal)totalAmount });
            }

            // 充值
            sql = $"select sum(amount) from  Finance_Trade where Type={(int)TradeType.Recharge}";
            sqlRaido = $"select sum(amount) from  Finance_Trade where Type={(int)TradeType.Recharge} and Status={(int)TradeStatus.Pending}";
            totalAmount = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0);
            failAmount = repositoryContext.ExecuteScalar(sqlRaido).ToStr().ToLong(0);
            if (totalAmount == 0L) {
                list.Add(new TradeReport { TradeType = TradeType.Recharge, Amount = totalAmount, Raido = 0M });
            } else {
                list.Add(new TradeReport { TradeType = TradeType.Recharge, Amount = totalAmount, Raido = (decimal)failAmount / (decimal)totalAmount });
            }

            //总支付
            sql = $"select sum(amount) from finance_pay where Type={(int)CheckoutType.Order}";
            sqlRaido = $"select sum(amount) from finance_pay where Type={(int)CheckoutType.Order} and Status={(int)PayStatus.Failured}";
            totalAmount = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0);
            failAmount = repositoryContext.ExecuteScalar(sqlRaido).ToStr().ToLong(0);
            if (totalAmount == 0L) {
                list.Add(new TradeReport { TradeType = TradeType.Payment, Amount = totalAmount, Raido = 0M });
            } else {
                list.Add(new TradeReport { TradeType = TradeType.Payment, Amount = totalAmount, Raido = (decimal)failAmount / (decimal)totalAmount });
            }

            //添加统计数据到数据库
            scope.Resolve<IReportService>().AddOrUpdate<TradeReport>(list);
        }
    }
}