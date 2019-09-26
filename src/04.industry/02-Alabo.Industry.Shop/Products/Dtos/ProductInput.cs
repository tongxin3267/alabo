using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Industry.Shop.Products.Domain.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.Products.Dtos
{
    public class ProductInput : PagedInputDto
    {
        /// <summary>
        ///     价格方式
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, PlaceHolder = "价格方式",
            IsShowAdvancedSerach = true, IsShowBaseSerach = true, SortOrder = 1)]
        [Display(Name = "价格方式")]
        public Guid? priceStyle { get; set; }

        /// <summary>
        ///     商品状态
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, IsShowAdvancedSerach = true,
            DataSource = "Alabo.App.Shop.Product.Domain.Enums.ProductStatus", IsShowBaseSerach = false,
            SortOrder = 200)]
        [Display(Name = "商品状态")]
        public ProductStatus? Status { get; set; }

        /// <summary>
        ///     产品线
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, PlaceHolder = "产品线", IsShowAdvancedSerach = true,
            IsShowBaseSerach = true, SortOrder = 1)]
        [Display(Name = "产品线")]
        public int productLineId { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, PlaceHolder = "品牌", IsShowAdvancedSerach = true,
            IsShowBaseSerach = true, SortOrder = 1)]
        [Display(Name = "品牌")]
        public string productBn { get; set; }

        /// <summary>
        ///     商品名称
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, PlaceHolder = "商品名称",
            IsShowAdvancedSerach = true, IsShowBaseSerach = true, SortOrder = 1)]
        [Display(Name = "商品名称")]
        public string productName { get; set; }

        /// <summary>
        ///     用户类型
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, PlaceHolder = "用户类型",
            IsShowAdvancedSerach = true, IsShowBaseSerach = true, SortOrder = 1)]
        [Display(Name = "用户类型")]
        public Guid? userTypeId { get; set; }
    }
}