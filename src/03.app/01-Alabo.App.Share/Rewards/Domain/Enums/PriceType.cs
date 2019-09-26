using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Share.Rewards.Domain.Enums {

    /// <summary>
    /// 分润价格类型
    /// </summary>
    [ClassProperty(Name = "分润价格类型")]
    public enum PriceType {

        /// <summary>
        /// 售价
        /// </summary>
        [Display(Name = "销售价(商品Sku销售价）")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        Price = 0,

        /// <summary>
        /// 分润
        /// </summary>
        [Display(Name = "分润价(商品sku分润价）")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        FenRun = 1,

        /// <summary>
        /// 商品数
        /// </summary>
        [Display(Name = "商品数(订单商品总数)")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        ProductNum = 3,

        /// <summary>
        /// 订单数
        /// </summary>
        [Display(Name = "订单数(通常基数为1)")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        OrderNum = 4,

        /// <summary>
        /// 虚拟资产服务费
        /// 订单使用虚拟资产支付时候，产生的现金服务费，服务费比例在货币类型管理处设置
        /// </summary>
        [Display(Name = "虚拟资产服务费")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        OrderFeeAmount = 5,
    }
}