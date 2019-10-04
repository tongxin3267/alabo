using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum {

    /// <summary>
    ///     地址锁定方式
    /// </summary>
    [ClassProperty(Name = "地址锁定方式")]
    public enum AddressLockType {

        /// <summary>
        ///     订单收货地址
        ///     根据订单的最终收货地址来锁定分润。如订单收货地址为：广东省东莞市南城区天安数码城，则南城区区代理、东莞市市代理、广东省省代理会按比例获得该订单的分润
        /// </summary>
        [Display(Name = "收货地址")]
        [LabelCssClass(BadgeColorCalss.Success)]
        OrderAddress = 1,

        /// <summary>
        ///     用户信息地址
        ///     根据会员中心用户填写的真实地址来锁定分润。如果会员的真实地址在广东省，则该会员任何交易相关的分润和广东省都有关系
        /// </summary>
        [Display(Name = "备案地址")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        UserInfoAddress = 2,

        /// <summary>
        ///     发货人的注册地址
        /// </summary>
        [Display(Name = "发货地址")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        DeliveryUserAddress = 3,

        /// <summary>
        ///     自定义商城订单地址
        /// </summary>
        [Display(Name = "自定义商城订单地址")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        CustomeShopOrderAddress = 4
    }
}