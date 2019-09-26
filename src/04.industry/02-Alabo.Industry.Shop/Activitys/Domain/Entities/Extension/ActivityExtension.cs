using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities.Extensions;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Industry.Shop.Activitys.Domain.Enum;

namespace Alabo.Industry.Shop.Activitys.Domain.Entities.Extension {

    /// <summary>
    ///     活动
    /// </summary>
    public class ActivityExtension : EntityExtension {

        /// <summary>
        ///     活动类型
        /// </summary>
        [Display(Name = "活动类型")]
        public ActivityType ActivityType { get; set; }

        /// <summary>
        ///     活动的主图（多张）
        /// </summary>
        [Display(Name = "主图")]
        public string Image { get; set; }

        /// <summary>
        ///     活动详细介绍
        /// </summary>
        [Display(Name = "活动规则介绍")]
        public string Intro { get; set; }

        /// <summary>
        ///     活动商品列表
        /// </summary>
        public IList<long> ProductIds { get; set; } = new List<long>();

        /// <summary>
        ///     活动显示信息或其他
        /// </summary>
        public ActivityDisplay Display { get; set; }
    }

    /// <summary>
    ///     活动显示信息或其他信息
    /// </summary>
    public class ActivityDisplay {

        /// <summary>
        ///     优惠方式：是否有减钱行为
        /// </summary>
        [Display(Name = "是否减钱")]
        public bool IsDecreaseMoney { get; set; } = false;

        /// <summary>
        ///     优惠方式： 减多少钱。当IsDecreaseMoney为true时，
        ///     该值才有意义。注意：该值单位为分，即100表示1元。
        /// </summary>
        [Display(Name = "减钱金额")]
        public long DecreaseMoney { get; set; }

        /// <summary>
        ///     优惠方式： 是否有打折行为
        /// </summary>
        [Display(Name = "是否打折")]
        public bool IsDiscount { get; set; } = false;

        /// <summary>
        ///     优惠方式： 折扣值。IsDiscount，
        ///     该值才有意义。注意：800表示8折。
        /// </summary>
        [Display(Name = "打几折")]
        public long DiscountRate { get; set; }

        /// <summary>
        ///     优惠方式： 是否有送礼品行为
        /// </summary>
        [Display(Name = "是否赠送礼品")]
        public bool IsSendGift { get; set; } = false;

        /// <summary>
        ///     优惠方式： 礼品名称。
        ///     当IsSendGift为true时，该值才有意义。
        /// </summary>
        [Display(Name = "礼品名称")]
        public string GiftName { get; set; }

        /// <summary>
        ///     优惠方式：礼品id，当IsSendGift为true时，该值才有意义。
        ///     1）只有填写真实的商品id时，才能生成物流单，并且在确定订单的页面上可以点击该商品名称跳转到商品详情页面。
        ///     2）当礼物为实物商品时(有宝贝id),礼物必须为上架商品,不能为虚拟商品,不能为拍卖商品,不能有sku,不符合条件的,不做为礼物。
        /// </summary>
        [Display(Name = "礼品id")]
        public long GiftProductId { get; set; }

        /// <summary>
        ///     是否赠送优惠券
        /// </summary>
        public bool IsDiscountCoupon { set; get; }

        /// <summary>
        ///     赠送优惠券信息
        /// </summary>
        public int DiscountCoupon { set; get; }

        /// <summary>
        ///     优惠方式： 是否有免邮行为
        /// </summary>
        [Display(Name = "是否免邮")]
        public bool IsFreePost { get; set; }

        /// <summary>
        ///     优惠方式： 免邮的排除地区，
        ///     即，除指定地区外，其他地区包邮。
        ///     当IsFreePost为true时，该值才有意义。
        ///     Json数据,不包邮地区id集合
        /// </summary>
        [Display(Name = "免邮地区id集合")]
        public string ExcludeArea { get; set; }

        /// <summary>
        ///     活动显示： 价格标签
        ///     商品详情页显示
        ///     比如：促销，限时打折、包邮、新店开张等
        /// </summary>
        [Display(Name = "价格标签")]
        public string PriceTag { get; set; }

        /// <summary>
        ///     价格标签的颜色
        /// </summary>
        [Display(Name = "价格标签颜色")]
        public string PriceTagColor { get; set; }

        /// <summary>
        ///     价格促销模板
        /// </summary>
        [Display(Name = "价格促销模板")]
        public string PriceTagTemplate { get; set; }

        /// <summary>
        ///     商品详情页显示位置,1都显示,2仅pc,3仅phone,4都不显示
        /// </summary>
        [Display(Name = "活动显示位置")]
        public ViewLocation ProdoctViewLocation { get; set; }

        /// <summary>
        ///     活动显示：商品详情页显示模板
        ///     不同的促销模块生成的方式可能不一样
        ///     比如满就送：满100元送5积分
        ///     比如满包邮：满100元免邮费
        /// </summary>
        [Display(Name = "商品促销模板")]
        public string ProductTemplate { get; set; }

        /// <summary>
        ///     活动显示：活动性情页模板
        ///     PC端
        /// </summary>
        [Display(Name = "活动详情页模板")]
        public string ShowTemplate { get; set; }

        /// <summary>
        ///     商品范围
        /// </summary>
        [Display(Name = "活动商品范围")]
        public ProductRange ProductRange { get; set; }

        /// <summary>
        ///     所属商城
        /// </summary>
        [Display(Name = "所属商城")]
        public Guid PriceStyleId { get; set; }

        /// <summary>
        ///     所属产品线
        /// </summary>
        [Display(Name = "所属产品线")]
        public int ProductLineId { get; set; }

        /// <summary>
        ///     标签文本
        /// </summary>
        public string PriceText { get; set; }
    }
}