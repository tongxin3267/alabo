using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Share.Rewards.Domain.Enums {

    /// <summary>
    /// 分润价格类型
    /// </summary>
    [ClassProperty(Name = "分润价格类型")]
    public enum PerformanceType {

        /// <summary>
        /// 全部
        /// </summary>
        [Display(Name = "全部")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        All = 0,

        /// <summary>
        /// 报单
        /// </summary>
        [Display(Name = "报单")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        Declaration = 1,

        /// <summary>
        /// 订单
        /// </summary>
        [Display(Name = "订单")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        Order = 2
    }
}