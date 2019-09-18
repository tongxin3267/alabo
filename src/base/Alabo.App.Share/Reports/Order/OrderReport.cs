using Quartz;
using System.Threading.Tasks;
using Alabo.App.Core.Reports;
using Alabo.App.Core.Reports.Domain.Services;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.Dependency;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Schedules.Job;

namespace Alabo.App.Open.Reports.Order {

    public class OrderReport : IReportModel {

        /// <summary>
        /// 待付款
        /// </summary>
        public long WaitingBuyerPay { get; set; }

        /// <summary>
        /// 代发货
        /// </summary>
        public long WaitingSellerSendGoods { get; set; }

        /// <summary>
        /// 已完成
        /// </summary>
        public long Success { get; set; }
    }

    public class OrderReportSchedule : JobBase {

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
            var orderReport = new OrderReport();

            var sql = $"select COUNT(1) from ZKShop_Order where OrderStatus={(int)OrderStatus.Success}";
            orderReport.Success = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0);
            sql = $"select COUNT(1) from ZKShop_Order where OrderStatus={(int)OrderStatus.WaitingBuyerPay} ";
            orderReport.WaitingBuyerPay = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0);

            sql = $"select COUNT(1) from ZKShop_Order where OrderStatus={(int)OrderStatus.WaitingSellerSendGoods}  ";
            orderReport.WaitingSellerSendGoods = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0);

            //添加统计数据到数据库
            scope.Resolve<IReportService>().AddOrUpdate<OrderReport>(orderReport);
        }
    }
}