using System;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Shop.Product.Domain.Enums;
using Alabo.Domains.Entities;

namespace Alabo.App.Shop.Product.ViewModels {

    /// <summary>
    ///     产品线模型
    /// </summary>
    public class ProductLineModel {
        public Domain.Entities.Product Product { get; set; }

        /// <summary>
        ///     商品价格模式
        /// </summary>
        public string PriceName { get; set; }

        /// <summary>
        ///     产品线信息
        /// </summary>
        public string Plinestring { get; set; }

        public long Id { get; set; }

        /// <summary>
        ///     供应商 Id，0 表示平台商品
        /// </summary>
        [Display(Name = "供应商")]
        public long StoreId { get; set; }

        /// <summary>
        ///     商品价格模式的配置Id 与PriceStyleConfig 对应
        /// </summary>

        public Guid PriceStyleId { get; set; }

        /// <summary>
        ///     商品类目
        /// </summary>
        [Display(Name = "商品类目")]
        public Guid CategoryId { get; set; }

        /// <summary>
        ///     商品品牌ID
        ///     商品的品牌Id存放在Store表中
        ///     关联店铺的品牌
        /// </summary>
        [Display(Name = "商品品牌")]
        public Guid BrandId { get; set; }

        /// <summary>
        ///     所在区县
        /// </summary>
        [Display(Name = "所在区县")]
        public long RegionId { get; set; }

        /// <summary>
        ///     商品名称
        /// </summary>

        public string Name { get; set; }

        /// <summary>
        ///     商品货号
        /// </summary>
        [Display(Name = "货号")]
        public string Bn { get; set; }

        /// <summary>
        ///     现金最低比例
        ///     优先级高于商城模式的现金比例，为空则使用商城模式的现金比例
        /// </summary>
        [Required]
        [Display(Name = "现金最低比例")]
        [Range(typeof(decimal), "0.00", "1", ErrorMessage = "现金比例格式不正确")]
        public decimal MinCashRate { get; set; } = 0;

        /// <summary>
        ///     商品进货价，指针对卖家的进货价格，该价格通常用于与卖家的货款的结算，比如货号为X002的衣服从供货商进货价位100元
        /// </summary>
        [Display(Name = "进货价")]
        [Range(0, 99999999, ErrorMessage = "商品进货价必须为大于等于0的数字")]
        public decimal PurchasePrice { get; set; }

        /// <summary>
        ///     商品成本价，指卖家的成本价格，该价格统称
        /// </summary>
        [Display(Name = "成本价")]
        [Range(0, 99999999, ErrorMessage = "商品成本价必须为大于等于0的数字")]
        public decimal CostPrice { get; set; }

        /// <summary>
        ///     市场价
        /// </summary>
        [Display(Name = "市场价")]
        [Range(0, 99999999, ErrorMessage = "商品市场价必须为大于等于0的数字")]
        public decimal MarketPrice { get; set; }

        /// <summary>
        ///     销售价，价格计算时通过这个价格来计算
        /// </summary>
        [Display(Name = "销售价")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(0, 99999999, ErrorMessage = "商品销售价必须为大于等于0的数字")]
        public decimal Price { get; set; }

        /// <summary>
        ///     价格显示方式，最终页面上的显示价格 页面上显示价格
        /// </summary>
        [Display(Name = "显示价格")]
        public string DisplayPrice { get; set; }

        /// <summary>
        ///     商品的重量，用于按重量计费的运费模板。注意：单位为kg。只能传入数值类型（包含小数），不能带单位，单位默认为kg。
        /// </summary>
        [Display(Name = "重量")]
        [Range(0, 99999999, ErrorMessage = "商品重量必须为大于等于0的数字")]
        public decimal Weight { get; set; }

        /// <summary>
        ///     商品库存,用商品所有的SKU库存 相加的总数
        /// </summary>
        [Display(Name = "库存")]
        [Range(0, 99999999, ErrorMessage = "商品库存必须为大于等于0的整数")]
        public long Stock { get; set; }

        /// <summary>
        ///     小图URl，绝对地址。大小50X50，通过主图生成
        ///     小图URl,根据后台设置规则生成，商城通用访问或显示地址,通用用于列表页或首页显示用格式：“OriginalUrl_宽X高.文件后缀”,参考淘宝
        /// </summary>
        [Display(Name = "小图URl")]
        public string SmallUrl { get; set; }

        /// <summary>
        ///     缩略图的URL,通过主图生成
        ///     缩略图地址,根据后台设置规则生成，商城通用访问或显示地址,通用用于列表页或首页显示用格式：“OriginalUrl_宽X高.文件后缀”,参考淘宝
        /// </summary>
        [Display(Name = "缩略图的URL")]
        public string ThumbnailUrl { get; set; }

        /// <summary>
        ///     发布时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     Gets or sets the modified time.
        /// </summary>

        public DateTime ModifiedTime { get; set; }

        /// <summary>
        ///     Gets or sets the product status.
        /// </summary>
        [Display(Name = "商品状态")]
        public ProductStatus ProductStatus { get; set; }

        /// <summary>
        ///     Gets or sets the sort order.
        /// </summary>
        public long SortOrder { get; set; }

        /// <summary>
        ///     已销售数量，通过此字段实现销量排行功能
        /// </summary>
        [Display(Name = "销量")]
        public long SoldCount { get; set; }

        /// <summary>
        ///     商品查看数量，通过此字段实现浏览量排行功能，人气排序.通过ViewCount和SoldCount来计算商品的成交率
        /// </summary>
        [Display(Name = "浏览量")]
        public long ViewCount { get; set; }

        /// <summary>
        ///     用户喜欢次数
        /// </summary>
        [Display(Name = "喜欢量")]
        public long LikeCount { get; set; }

        /// <summary>
        ///     用户收藏次数
        /// </summary>
        [Display(Name = "收藏量")]
        public long FavoriteCount { get; set; }

        [Display(Name = "所属商城")] public string PriceStyleName { get; set; }
    }
}