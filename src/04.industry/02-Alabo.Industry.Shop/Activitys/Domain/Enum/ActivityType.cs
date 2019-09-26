using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.Activitys.Domain.Enum {

    /// <summary>
    ///     活动类型
    /// </summary>
    [ClassProperty(Name = "活动类型")]
    public enum ActivityType {

        /// <summary>
        ///     针对商品类型的活动：比如打折、满就减
        /// </summary>
        [LabelCssClass("label-success")]
        [Display(Name = "针对商品类型的活动：比如打折、满就减")]
        ProductActivity = 1,

        /// <summary>
        ///     针对店铺类级别活动：比如包邮、优惠券、代金券
        /// </summary>
        [LabelCssClass("label-success")]
        [Display(Name = "针对店铺类级别活动：比如包邮、优惠券、代金券")]
        StoreActivity = 2,

        /// <summary>
        ///     行为类型类活动，比如一元夺宝、秒杀、拍卖、拼团
        ///     行为类活动，应该都包含分类、标签、配置等信息
        /// </summary>
        [LabelCssClass("label-success")]
        [Display(Name = "行为类型类活动，比如一元夺宝、秒杀、拍卖、拼团")]
        BehaviorActivity = 3,

        /// <summary>
        ///     优惠券
        ///     单独的样式，可以在商品购买时选择，亦可以在商品下单时选择，参考淘宝和平多多
        ///     在商品详情页：优惠券标签
        ///     在订单详情页：优惠券标签
        /// </summary>
        [LabelCssClass("label-success")]
        [Display(Name = "优惠券")]
        Coupons = 4
    }
}