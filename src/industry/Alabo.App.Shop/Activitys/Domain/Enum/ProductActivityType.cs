using System.ComponentModel.DataAnnotations;
using Alabo.App.Shop.Activitys.Modules.BuyPermision.Model;
using Alabo.App.Shop.Activitys.Modules.MemberDiscount.Model;
using Alabo.App.Shop.Activitys.Modules.PreSells.Model;
using Alabo.App.Shop.Activitys.Modules.TimeLimitBuy.Model;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Activitys.Domain.Enum
{
    /// <summary>
    /// product activity type
    /// </summary>
    [ClassProperty(Name = "商品活动类型")]
    public enum ProductActivityType
    {
        /// <summary>
        /// 购买权限
        /// </summary>
        [Display(Name = "购买权限", ResourceType = typeof(BuyPermisionActivity))]
        BuyPermission = 1,

        /// <summary>
        /// 会员折扣
        /// </summary>
        [Display(Name = "会员折扣", ResourceType = typeof(MemberDiscountActivity))]
        MemberDiscount = 2,

        /// <summary>
        /// 预售设置
        /// </summary>
        [Display(Name = "预售设置", ResourceType = typeof(PreSellsActivity))]
        PreSells = 3,

        /// <summary>
        /// 限时购
        /// </summary>
        [Display(Name = "限时购", ResourceType = typeof(TimeLimitBuyActivity))]
        TimeLimitBuy = 4
    }
}