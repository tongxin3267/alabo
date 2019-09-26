using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities.Extensions;

namespace Alabo.Industry.Shop.Products.Domain.Entities.Extensions
{
    /// <summary>
    ///     商品扩展详情
    /// </summary>
    public class ProductDetailExtension : EntityExtension
    {
        /// <summary>
        ///     店铺分类
        /// </summary>
        public List<long> StoreClass { get; set; } = new List<long>();

        /// <summary>
        ///     商品标签
        /// </summary>
        public List<long> Tags { get; set; } = new List<long>();

        /// <summary>
        ///     海外商品
        /// </summary>
        [Display(Name = "海外商品")]
        public bool IsOverseas { get; set; } = false;

        /// <summary>
        ///     是否需要收货地址
        /// </summary>
        [Display(Name = "是否需要收货地址")]
        public bool NeedBuyerAddress { get; set; } = true;

        /// <summary>
        ///     商品简介
        /// </summary>
        [Display(Name = "商品简介")]
        public string Brief { get; set; }

        /// <summary>
        ///     表示商品的体积，如果需要使用按体积计费的运费模板，一定要设置这个值。该值的单位为立方米（m3），如果是其他单位，请转换成成立方米
        /// </summary>
        [Display(Name = "体积")]
        [Range(0, 99999999, ErrorMessage = "商品体积必须为大于等于0的数字")]
        public decimal Size { get; set; }

        /// <summary>
        ///     是否包邮
        /// </summary>
        [Display(Name = "是否包邮")]
        public bool IsShipping { get; set; } = false;

        [Display(Name = "备注")] public string Remark { get; set; }

        /// <summary>
        ///     商品条形码
        /// </summary>
        [Display(Name = "商品条形码")]
        public string BarCode { get; set; }

        /// <summary>
        ///     是否有折扣
        /// </summary>
        [Display(Name = "是否有折扣")]
        public int HasDiscount { get; set; }

        /// <summary>
        ///     是否有发票
        /// </summary>
        [Display(Name = "是否有发票")]
        public bool HasInvoice { get; set; } = false;

        /// <summary>
        ///     是否有保修
        /// </summary>
        [Display(Name = "是否有保修")]
        public bool HasWarranty { get; set; } = false;

        /// <summary>
        ///     是否是虚拟商品
        /// </summary>
        [Display(Name = "是否是虚拟商品")]
        public bool IsVirtual { get; set; } = false;

        /// <summary>
        ///     售后服务模板
        /// </summary>
        [Display(Name = "售后服务模板")]
        public Guid AfterSaleId { get; set; }

        /// <summary>
        ///     商品副标题
        /// </summary>
        [Display(Name = "商品副标题")]
        public string ProductSubTitle { get; set; }

        /// <summary>
        ///     商品短标题
        /// </summary>
        [Display(Name = "商品短标题")]
        public string ProductSortTitle { get; set; }

        /// <summary>
        ///     审核拒绝消息内容
        /// </summary>
        [Display(Name = "审核拒绝消息内容")]
        public string AidutMessage { get; set; }
    }
}