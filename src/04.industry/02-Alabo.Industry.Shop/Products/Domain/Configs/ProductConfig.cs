using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;using MongoDB.Bson.Serialization.Attributes;
using Alabo.App.Core.Common;
using Alabo.AutoConfigs;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Shop.Product.Domain.CallBacks {

    /// <summary>
    ///     商品图片上传设置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "商品发布规则", GroupName = "后台商品发布,图片设置", Icon = "fa fa-cloud-upload", Description = "商品图片上传",
        SortOrder = 400, SideBarType = SideBarType.ProductSideBar)]
    public class ProductConfig : BaseViewModel, IAutoConfig
    {

        /// <summary>
        /// 是否自动更新销售数量
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1)]
        [Display(Name = "是否自动更新销售数量")]
        [HelpBlock("是否自动更新销售数量,访问商品详情页每次刷新自动更新销售数据")]
        public bool IsAutoUpdateSold { get; set; } = true;

        /// <summary>
        /// 默认货号
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1)]
        [Display(Name = "默认货号")]
        [HelpBlock("请输入货号名称，默认值为ZK")]
        public string Bn { get; set; } = "ZK";

        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 1)]
        [Display(Name = "默认进货价")]
        [HelpBlock("请输入默认进货价的值，值必须大于等于0，默认值为100")]
        public decimal PurchasePrice { get; set; } = 100;

        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 1)]
        [Display(Name = "默认成本价")]
        [HelpBlock("请输入默认成本价的值，值必须大于等于0，默认值为100")]
        public decimal CostPrice { get; set; } = 100;

        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 1)]
        [Display(Name = "默认市场价")]
        [HelpBlock("请输入默认市场价的值，值必须大于等于0，默认值为100")]
        public decimal MarketPrice { get; set; } = 100;

        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 1)]
        [Display(Name = "默认销售价")]
        [HelpBlock("请输入默认销售价的值，值必须大于等于0，默认值为100")]
        public decimal Price { get; set; } = 100;

        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 1)]
        [Display(Name = "默认重量")]
        [HelpBlock("请输入默认重量的值，值必须大于等于0，默认值为100")]
        public decimal Weight { get; set; } = 100;

        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 1)]
        [Display(Name = "默认库存")]
        [HelpBlock("请输入默认库存的值，值必须大于等于0，默认值为100")]
        public long Stock { get; set; } = 100;

        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 1)]
        [Display(Name = "默认销量")]
        [HelpBlock("请输入默认销量的值，值必须大于等于0，默认值为100")]
        public long SoldCount { get; set; } = 100;

        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 1)]
        [Display(Name = "默认浏览量")]
        [HelpBlock("请输入默认浏览量的值，值必须大于等于0，默认值为100")]
        public long ViewCount { get; set; } = 100;

        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 1)]
        [Display(Name = "默认喜欢量")]
        [HelpBlock("请输入默认喜欢量的值，值必须大于等于0，默认值为100")]
        public long LikeCount { get; set; } = 100;

        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 1)]
        [Display(Name = "默认收藏量")]
        [HelpBlock("请输入默认收藏量的值，值必须大于等于0，默认值为100")]
        public long FavoriteCount { get; set; } = 100;

        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 2)]
        [Display(Name = "宽比高比例")]
        [HelpBlock("请输入宽比高比例的值，值必须大于等于0，默认值为1")]
        public decimal WidthThanHeight { get; set; } = 1;

        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 2)]
        [Display(Name = "列表页缩略图宽度")]
        [HelpBlock("请输入列表页缩略图宽度的值，值必须大于等于0，默认值为320")]
        public decimal ThumbnailWidth { get; set; } = 160;

        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 2)]
        [Display(Name = "详情页橱窗图宽度")]
        [HelpBlock("请输入详情页橱窗图宽度的值，值必须大于等于0，默认值为520")]
        public decimal ShowCaseWidth { get; set; } = 520;

        [Field(ControlsType = ControlsType.Numberic, GroupTabId = 2)]
        [Display(Name = "缩略图生成质量")]
        [HelpBlock("请输入缩略图生成质量的值，值必须大于等于0，默认值为100")]
        public decimal ThumbnailGeneratedQuality { get; set; } = 100;

        [Field(ControlsType = ControlsType.Switch, GroupTabId = 2)]
        [Display(Name = "是否生成水印")]
        [HelpBlock("是否允许生成水印")]
        public bool IsShuiYin { get; set; } = false;

        // [Field("价格登录后显示", ControlsType.Switch)]
        //public bool PricesShowAfterLogin  { get; set; } = false;

        // [Field("非会员可购物", ControlsType.Switch)]
        //public bool NoMemeberCanShopping { get; set; } = false;

        //// [Field("库存减少方式", ControlsType.TextBox)]
        ////public StotckReductionWay ReductionWay { get; set; } = StotckReductionWay.ReductionWhenPay;

        // [Field("商品上传自动审核", ControlsType.TextBox)]
        //public bool IsAutoCheck { get; set; } = true;

        // [Field("开启手机详情页", ControlsType.TextBox)]
        //public bool IsMobileIntro { get; set; } = true;

        // [Field("常见商品单位","克,千克,吨,包,件,盒,袋,立方米,米,套,码", ControlsType.DropdownList)]
        //public string Unit { get; set; }

        // [Field("货号默认前缀", ControlsType.TextBox)]
        //public string BnDefaultPrefix { get; set; } = "ZK";

        // [Field("默认库存数量", ControlsType.TextBox)]
        //public string DefaulStock { get; set; } = "100";

        // [Field("宽比高比例", ControlsType.TextBox)]
        //public int Id { get; set; } = 1;

        public void SetDefault() {
        }
    }
}