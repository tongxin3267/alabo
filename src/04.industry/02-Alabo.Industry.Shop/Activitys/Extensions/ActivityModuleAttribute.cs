using System;
using Alabo.App.Shop.Activitys.Domain.Enum;

namespace Alabo.App.Shop.Activitys.Extensions {

    /// <summary>
    ///     Class ActivityModuleAttribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ActivityModuleAttribute : Attribute {

        /// <summary>
        ///     模块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     模块的详细说明
        /// </summary>
        public string Intro { get; set; } = "请在模块特性里头填写活动详情";

        /// <summary>
        ///     Gets or sets the icon.
        /// </summary>
        public string Icon { get; set; } = "icon-social-dribbble ";

        /// <summary>
        ///     活动类型
        /// </summary>
        public ActivityType ActivityType { get; set; } = ActivityType.ProductActivity;

        /// <summary>
        ///     图标对应的背景颜色
        /// </summary>
        public string BackGround { get; set; } = "bg-green-seagreen";

        /// <summary>
        ///     排序，从小到大排列
        /// </summary>
        public long SortOrder { get; set; } = 1000;

        /// <summary>
        ///     活动模板页面
        ///     在前台浏览时候的活动模板页面
        ///     不设置时候，使用系统模板页面
        /// </summary>
        public string ActivityShowTemplate { get; set; } = "Activity_Show";

        /// <summary>
        ///     是否支持分类
        /// </summary>
        public bool IsSupportClass { get; set; } = false;

        /// <summary>
        ///     是否支持标签
        /// </summary>
        public bool IsSupportTag { get; set; } = false;

        /// <summary>
        ///     是否支持相册
        /// </summary>
        public bool IsSupportAblum { get; set; } = false;

        /// <summary>
        ///     是否支持多个商品
        /// </summary>
        public bool IsSupportMultipleProduct { get; set; } = false;

        /// <summary>
        ///     是否支持单个商品
        /// </summary>
        public bool IsSupportSigleProduct { get; set; } = false;

        /// <summary>
        ///     是否支持Sku修改商品
        /// </summary>
        public bool IsSupportSku { get; set; } = false;

        /// <summary>
        ///     是否支持减钱
        /// </summary>
        public bool IsSupportLessMoney { get; set; } = false;

        /// <summary>
        ///     是否支持打折
        /// </summary>
        public bool IsSupportDiscounted { get; set; } = false;

        /// <summary>
        ///     是否支持送礼品
        /// </summary>
        public bool IsSupportGift { get; set; } = false;

        /// <summary>
        ///     是否支持送卡券
        /// </summary>
        public bool IsSupportCard { get; set; } = false;

        /// <summary>
        /// 是否开启最大库存
        /// </summary>
        public bool IsSupportMaxStock { get; set; }

        /// <summary>
        ///     是否支持免邮费
        /// </summary>
        public bool IsSupportPostage { get; set; } = false;

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is support offer content.
        /// </summary>
        public bool IsSupportOfferContent { get; set; } = false;

        /// <summary>
        ///     是否在营销当中显示图标
        /// </summary>
        public bool IsShowIcon { get; set; } = true;

        /// <summary>
        ///     独立活动活动详情页
        ///     如果活动的IsOwnerShowTemplate为True   默认:活动详情页的模板页面 Activity_Show
        ///     命名规则：Activity_Show+_模板前缀
        ///     比如拼团模块PinTuanActivity的详情页视图为：Activity_Show_PinTuan.cshtml
        /// </summary>
        public bool IsOwnerShowTemplate { get; set; } = false;

        /// <summary>
        ///     独立活动活动列表页
        ///     //如果活动的IsOwnerShowTemplate为True   默认:活动详情页的模板页面 Activity_Show
        ///     命名规则：Activity_List+_模板前缀
        ///     比如拼团模块PinTuanActivity的详情页视图为：Activity_List_PinTuan.cshtml
        /// </summary>
        public bool IsOwnerListTemplate { get; set; } = false;

        /// <summary>
        ///     独立活动活动列表页
        ///     //如果活动的IsOwnerShowTemplate为True   默认:活动详情页的模板页面 Activity_Show
        ///     命名规则：Activity+_模板前缀
        ///     比如拼团模块PinTuanActivity的详情页视图为：Activity_PinTuan.cshtml
        ///     Activity/PinTuan
        /// </summary>
        public bool IsOwneIndexTemplate { get; set; } = false;

        /// <summary>
        ///     独立活动活动列表页
        ///     //如果活动的IsOwnerShowTemplate为True   默认:活动详情页的模板页面 Activity_Show
        ///     命名规则：Activity_List+_模板前缀
        ///     比如拼团模块PinTuanActivity的详情页视图为：Activity_Show_PinTuan.cshtml
        /// </summary>
        public bool IsOwneOrderTemplate { get; set; } = false;

        /// <summary>
        ///     Gets or sets the father identifier.
        ///     菜单的父Id
        /// </summary>
        public long FatherId { get; set; }

        /// <summary>
        ///     Gets or sets the full name.
        /// </summary>
        public string FullName { get; set; }
    }
}