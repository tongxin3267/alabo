using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Datas.Queries.Enums;
using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.Products.ViewModels
{
    /// <summary>
    ///     商品单元，用于商品列表页展示，商品首页展示
    /// </summary>
    public class ProductItem
    {
        /// <summary>
        ///     商品Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     商品名称
        /// </summary>
        [Display(Name = "商品名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, PlaceHolder = "请输入商品名称",
            Operator = Operator.Contains,
            IsShowAdvancedSerach = true, DataSource = "Alabo.App.Shop.Product.Domain.Entities", GroupTabId = 1,
            IsMain = true, Width = "150",
            ListShow = true, SortOrder = 1)] //, Link = "/Admin/Product/Edit?id=[[Id]]"
        [StringLength(60, ErrorMessage = "60个字以内")]
        public string Name { get; set; }

        /// <summary>
        ///     商品货号
        /// </summary>
        [Display(Name = "货号")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, PlaceHolder = "请输入货号",
            IsShowAdvancedSerach = true, DataSource = "Alabo.App.Shop.Product.Domain.Entities", GroupTabId = 3,
            IsMain = true, Width = "150",
            ListShow = true, SortOrder = 2, Link = "/Admin/Product/Edit?id=[[Id]]")]
        [StringLength(60, ErrorMessage = "60个字以内")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Bn { get; set; }

        /// <summary>
        ///     市场价格
        /// </summary>
        public decimal MarketPrice { get; set; }

        /// <summary>
        ///     销售价
        /// </summary>
        [Display(Name = "销售价")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.NumberRang, ListShow = true, Width = "120", SortOrder = 4)]
        [Range(0, 99999999, ErrorMessage = "商品销售价必须为大于等于0的数字")]
        public decimal Price { get; set; }

        /// <summary>
        ///     价格显示格式
        /// </summary>
        [Display(Name = "显示价格")]
        [Field(ListShow = true, Width = "110", SortOrder = 9)]
        public string DisplayPrice { get; set; }

        /// <summary>
        ///     商品计量单位
        /// </summary>
        public string PriceUnit { get; set; }

        /// <summary>
        ///     商品缩略图片
        /// </summary>
        [Display(Name = "缩略图的URL")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "120", SortOrder = 1)]
        public string ThumbnailUrl { get; set; }

        /// <summary>
        ///     商品评论
        /// </summary>
        public long ProductReviews { get; set; }

        /// <summary>
        ///     商品销量
        /// </summary>
        [Display(Name = "销量")]
        public long SoldCount { get; set; }

        /// <summary>
        ///     商品库存
        /// </summary>
        [Display(Name = "库存")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "120", SortOrder = 5)]
        [Range(0, long.MaxValue, ErrorMessage = "商品库存必须为大于等于0的整数")]
        public long Stock { get; set; }

        /// <summary>
        ///     商品价格模式的配置Id
        ///     与PriceStyleConfig 对应
        /// </summary>
        public Guid PriceStyleId { get; set; }

        /// <summary>
        ///     查看数量
        /// </summary>
        [Display(Name = "查看数量")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "120", SortOrder = 5)]
        public long ViewCount { get; set; }

        /// <summary>
        ///     是否关联（预售商城，购物券商城）
        /// </summary>
        public bool IsLinked { get; set; }

        /// <summary>
        ///     排序
        /// </summary>
        public long SortOrder { get; set; }
    }
}