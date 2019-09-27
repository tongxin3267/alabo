using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Industry.Shop.Products.Domain.Enums;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.Products.Dtos
{
    /// <summary>
    ///     商品Sku数据传输，一般购物车，下单等流程
    /// </summary>
    public class ProductSkuItem
    {
        /// <summary>
        ///     Gets or sets the store identifier.
        /// </summary>
        /// <value>
        ///     The store identifier.
        /// </value>
        public ObjectId StoreId { get; set; }

        /// <summary>
        ///     商品ID
        /// </summary>
        /// <value>
        ///     The product identifier.
        /// </value>
        public long ProductId { get; set; }

        /// <summary>
        ///     Gets or sets the product sku identifier.
        ///     商品SkuId
        /// </summary>
        /// <value>
        ///     The product sku identifier.
        /// </value>
        public long ProductSkuId { get; set; }

        /// <summary>
        ///     商品购买数量
        /// </summary>
        public long BuyCount { get; set; }

        /// <summary> 商品货号 </summary>
        public string Bn { get; set; }

        /// <summary>
        ///     商品名称
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///     缩略图的URL,通过主图生成
        ///     缩略图地址,根据后台设置规则生成，商城通用访问或显示地址,通用用于列表页或首页显示用格式：“OriginalUrl_宽X高.文件后缀”,参考淘宝
        /// </summary>
        [Display(Name = "缩略图的URL")]
        public string ThumbnailUrl { get; set; }

        /// <summary> 市场价 通常指商品商品的吊牌价格，一般用于显示 </summary>
        public decimal MarketPrice { get; set; } = 0;

        /// <summary> 销售价 用户购买的价格，和订单相关，生成订单的时候，使用这个价格 </summary>
        public decimal Price { get; set; }

        /// <summary> 平台价 平台显示的价格，和订单计算会员优惠相关，用于订单中计算会员和平台的价格优惠差值 </summary>
        public decimal PlatformPrice { get; set; } = 0M;

        /// <summary>
        ///     免邮费
        /// </summary>
        [Display(Name = "免邮费")]
        public bool IsFreeShipping { get; set; } = false;

        /// <summary> 商品的重量，用于按重量计费的运费模板。注意：单位为kg。只能传入数值类型（包含小数），不能带单位，单位默认为kg。 </summary>
        public decimal Weight { get; set; }

        /// <summary>
        ///     显示价格
        /// </summary>
        /// <value>
        ///     The display price.
        /// </value>
        public string DisplayPrice { get; set; }

        /// <summary> 分润价格 </summary>
        public decimal FenRunPrice { get; set; } = 0;

        /// <summary> 规格属性说明,属性、规格的文字说明 比如：绿色 XL </summary>
        public string PropertyValueDesc { get; set; }

        /// <summary>
        ///     Gets or sets the stock.
        /// </summary>
        /// <value>
        ///     The stock.
        /// </value>
        public long Stock { get; set; }

        /// <summary>
        ///     Gets or sets the price style identifier.
        ///     货币类型
        /// </summary>
        /// <value>
        ///     The price style identifier.
        /// </value>
        public Guid PriceStyleId { get; set; }

        /// <summary>
        ///     Gets or sets the maximum pay amount. 最高可支付的金额
        ///     虚拟资产，最高可使用虚拟资产支付的价格
        /// </summary>
        /// <value> The maximum pay amount. </value>
        public decimal MaxPayPrice { get; set; }

        /// <summary>
        ///     Gets or sets the minimum pay cash. 现金的最低支付额度
        /// </summary>
        /// <value> The minimum pay cash. </value>
        public decimal MinPayCash { get; set; }

        /// <summary>
        ///     运费模板
        /// </summary>
        public string DeliveryTemplateId { get; set; }

        /// <summary>
        ///     商品状态
        /// </summary>
        public ProductStatus ProductStatus { get; set; } = ProductStatus.Online;
    }
}