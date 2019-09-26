using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Share.OpenTasks.Base {

    public class BaseRule {

        /// <summary>
        /// 最小触发金额
        /// </summary>
        [Display(Name = "最小触发金额")]
        public decimal MinimumAmount { get; set; } = 0.0m;

        /// <summary>
        /// 最大触发金额
        /// </summary>
        [Display(Name = "最大触发金额")]
        public decimal MaxAmount { get; set; } = 0.0m;

        /// <summary>
        /// 维度说明
        /// 尽可能详细的描述该维度的分润规则,可以将书面探讨的方案拍照后保存进来，便于后期的测试与管理
        /// </summary>
        [Display(Name = "维度说明")]
        public string Intro { get; set; }
    }

    /// <summary>
    /// 价格限制方式
    /// </summary>
    [ClassProperty(Name = "价格限制方式")]
    public enum PriceLimitType {

        [Display(Name = "订单总金额")]
        [LabelCssClass(BadgeColorCalss.Danger)]
        OrderPrice = 0,

        [Display(Name = "商品单价")]
        [LabelCssClass(BadgeColorCalss.Danger)]
        ProductPrice = 1,
    }
}