using Alabo.App.Core.User.Domain.Services;
using Alabo.Schedules;
using Alabo.UI.AutoTasks;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.User.UI {

    /// <summary>
    /// 自动服务方法
    /// </summary>
    [ClassProperty(Name = "重新生成组织架构图")]
    public class UserMapAutoTask : IAutoTask {

        public AutoTask Init() {
            return new AutoTask("重新生成组织架构图", TaskQueueModuleId.UserParentUpdate, typeof(IUserMapService), "UpdateAllUserParentMap");
        }
    }

    /// <summary>
    /// 重新生成二维码
    /// </summary>
    [ClassProperty(Name = "重新生成二维码")]
    public class QrCodeAutoTask : IAutoTask {

        public AutoTask Init() {
            return new AutoTask("重新生成二维码", TaskQueueModuleId.UserQrcodeCreate, typeof(IUserDetailService), "CreateAllUserQrCode");
        }
    }

    /// <summary>
    /// 奖金池重新统计
    /// </summary>
    [ClassProperty(Name = "奖金池重新统计")]
    public class BonusPoolAutoTask : IAutoTask {

        public AutoTask Init() {
            return new AutoTask("奖金池重新统计", TaskQueueModuleId.BonusPoolReport, typeof(IShareOrderReportService), "Report");
        }
    }

    /// <summary>
    /// 用户等级Kpi统计
    /// </summary>
    //[ClassProperty(Name = "用户等级Kpi统计")]
    //public class UserGradeKpiAutoTask : IAutoTask {
    //    public AutoTask Init() {
    //        return new AutoTask("用户等级Kpi统计", TaskQueueModuleId.UserGradeKpiReport, typeof(IGradeKpiService), "KpiOperatorAllUser");
    //    }
    //}

    ///// <summary>
    ///// 订单支付成功
    ///// </summary>
    //[ClassProperty(Name = "订单支付成功")]
    //public class AfterOrderPayAutoTask : IAutoTask {
    //    public AutoTask Init() {
    //        return new AutoTask("订单支付成功", TaskQueueModuleId.AfterOrderPay, typeof(IShareOrderReportService), "Report");
    //    }
    //}

    ///// <summary>
    ///// 共享分润任务
    ///// </summary>
    //[ClassProperty(Name = "共享分润任务")]
    //public class SharedAccountAutoTask : IAutoTask {
    //    public AutoTask Init() {
    //        return new AutoTask("共享分润任务", TaskQueueModuleId.SharedAccountModuleGuid, typeof(IShareOrderReportService), "Report");
    //    }
    //}

    ///// <summary>
    ///// 共享分润任务
    ///// </summary>
    //[ClassProperty(Name = "内部合伙人关系梳理")]
    //public class ParnterAutoTask : IAutoTask {
    //    public AutoTask Init() {
    //        return new AutoTask("内部合伙人关系梳理", TaskQueueModuleId.ParnterModuleGuid, typeof(IShareOrderReportService), "Report");
    //    }
    //}
}