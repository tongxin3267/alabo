using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Core.Enums.Enum
{
    /// <summary>
    ///     分润触发类型
    /// </summary>
    [ClassProperty(Name = "分润触发类型")]
    public enum TriggerType
    {
        /// <summary>
        ///     用户注册
        ///     用户注册(会员注册时触发）
        /// </summary>
        [Display(Name = "用户注册")] [LabelCssClass(BadgeColorCalss.Danger)]
        UserReg = 1,

        /// <summary>
        ///     订单
        ///     商城购物订单(会员在商城下单时)
        /// </summary>
        [Display(Name = "商城订单")] [LabelCssClass(BadgeColorCalss.Primary)]
        Order = 2,

        /// <summary>
        ///     充值
        ///     商城购物订单(会员在商城下单时)
        /// </summary>
        [Display(Name = "充值")] [LabelCssClass(BadgeColorCalss.Brand)]
        Recharge = 10,

        /// <summary>
        ///     提现
        /// </summary>
        [Display(Name = "提现")] [LabelCssClass(BadgeColorCalss.Info)]
        WithDraw = 11,

        /// <summary>
        ///     转账
        /// </summary>
        [Display(Name = "转账")] [LabelCssClass(BadgeColorCalss.Info)]
        Transfer = 12,

        /// <summary>
        ///     任务
        ///     比如签到，完成具体的任务等等
        /// </summary>
        [Display(Name = "任务")] [LabelCssClass(BadgeColorCalss.Metal)]
        Task = 20,

        /// <summary>
        ///     文章收藏、商品收藏
        ///     对应 FavoriteType 类型
        /// </summary>
        [Display(Name = "收藏")] [LabelCssClass(BadgeColorCalss.Metal)]
        Favorite = 21,

        /// <summary>
        ///     文章收藏、商品收藏
        ///     对应 CommentType 类型
        /// </summary>
        [Display(Name = "评论")] [LabelCssClass(BadgeColorCalss.Metal)]
        Comment = 22,

        /// <summary>
        ///     文章收藏、商品收藏
        ///     对应 FavoriteType 类型
        /// </summary>
        [Display(Name = "分享")] [LabelCssClass(BadgeColorCalss.Metal)]
        Share = 23,

        /// <summary>
        ///     用户升级
        ///     (用户升级时触发）
        /// </summary>
        [Display(Name = "用户升级")] [LabelCssClass(BadgeColorCalss.Warning)]
        UserUpgrade = 30,

        /// <summary>
        ///     内部连锁
        /// </summary>
        [Display(Name = "内部连锁")] [LabelCssClass(BadgeColorCalss.Metal)]
        Chain = 80,

        /// <summary>
        ///     其他
        ///     其他使用分润订单ShareOrder的金额）
        /// </summary>
        [Display(Name = "其他")] [LabelCssClass(BadgeColorCalss.Warning)]
        Other = 100
    }
}