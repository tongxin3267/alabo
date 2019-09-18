using System.Threading.Tasks;
using Alabo.App.Share.Reports.Admin;
using Alabo.App.Share.Reports.Finance;
using Alabo.App.Share.Reports.Order;
using Alabo.App.Share.Reports.Pay;
using Alabo.App.Share.Reports.Product;
using Alabo.App.Share.Reports.User.Model;
using Alabo.App.Share.Reports.User.Schedule;
using Alabo.Schedules.Job;

namespace Alabo.App.Share.Reports {

    /// <summary>
    /// 数据统计服务
    /// </summary>
    public static class ReportsSchedule {
        /// <summary>
        /// 添加后台数据统计
        /// </summary>

        public static async Task<Scheduler> AddReportAsync(this Scheduler scheduler) {
            //用户基础数据
            await scheduler.AddJobAsync<UserBaseSchedule>();

            //会员等级统计
            await scheduler.AddJobAsync<UserGradeSchedule>();
            //左侧菜单数据统计
            await scheduler.AddJobAsync<AdminSideBarReportSchedule>();
            // 交易额
            await scheduler.AddJobAsync<PayReportSchedule>();
            // 订单状态
            await scheduler.AddJobAsync<OrderReportSchedule>();
            //商品数量
            await scheduler.AddJobAsync<ProductReportSchedule>();
            // 分润数据
            await scheduler.AddJobAsync<RewardReportSchedule>();
            //TradeSchedule
            await scheduler.AddJobAsync<TradeReportSchedule>();
            return scheduler;
        }
    }
}