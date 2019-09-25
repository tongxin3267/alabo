using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Shop.Product.Domain.Dtos {

    /// <summary>
    /// </summary>
    [ClassProperty(Name = "商品数据统计", Icon = "fa fa-puzzle-piece", Description = "商品数据统计",
        SideBarType = SideBarType.ProductSideBar)]
    public class ProductBaseReport : BaseViewModel {
        public long Id { get; set; }

        /// <summary>
        ///     小图URl，绝对地址。大小50X50，通过主图生成
        ///     小图URl,根据后台设置规则生成，商城通用访问或显示地址,通用用于列表页或首页显示用格式：“OriginalUrl_宽X高.文件后缀”,参考淘宝
        /// </summary>
        [Display(Name = "小图URl")]
        [Field(IsImagePreview = true, GroupTabId = 1, Width = "150", ListShow = true, EditShow = false, SortOrder = 3)]
        public string SmallUrl { get; set; }

        /// <summary>
        ///     商品名称
        /// </summary>
        [Display(Name = "商品名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, IsMain = true, Width = "150", ListShow = true,
            IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, EditShow = false, SortOrder = 3)]
        [StringLength(60, ErrorMessage = "60个字以内")]
        public string Name { get; set; }

        /// <summary>
        ///     商品货号
        /// </summary>
        [Display(Name = "货号")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, IsMain = true, Width = "150", ListShow = true,
            IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, EditShow = true, SortOrder = 3)]
        public string Bn { get; set; }

        /// <summary>
        ///     销售价，价格计算时通过这个价格来计算
        /// </summary>
        [Display(Name = "销售价")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, EditShow = true,
            SortOrder = 3)]
        [Range(0, 99999999, ErrorMessage = ErrorMessage.DoubleInRang)]
        public decimal Price { get; set; }

        /// <summary>
        ///     已销售数量，通过此字段实现销量排行功能
        /// </summary>
        [Display(Name = "销量")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, EditShow = true,
            SortOrder = 3)]
        public long SoldCount { get; set; }

        /// <summary>
        ///     商品查看数量，通过此字段实现浏览量排行功能，人气排序.通过ViewCount和SoldCount来计算商品的成交率
        /// </summary>
        [Display(Name = "浏览量")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, EditShow = true,
            SortOrder = 3)]
        public long ViewCount { get; set; }

        /// <summary>
        ///     用户喜欢次数
        /// </summary>
        [Display(Name = "喜欢量")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, EditShow = true,
            SortOrder = 3)]
        public long LikeCount { get; set; }

        /// <summary>
        ///     用户收藏次数
        /// </summary>
        [Display(Name = "收藏量")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, EditShow = true,
            SortOrder = 3)]
        public long FavoriteCount { get; set; }
    }
}