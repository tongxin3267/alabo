using System;
using System.ComponentModel.DataAnnotations;
using Alabo.AutoConfigs;
using Alabo.Datas.Queries.Enums;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Mongo.Extension;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Industry.Shop.Products.Domain.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Alabo.Industry.Shop.Products.ViewModels
{
    public class ViewProductList : BaseViewModel
    {
        /// <summary>
        ///     商品所属类目ID
        /// </summary>
        [Required]
        [Display(Name = "商品所属类目ID")]
        public long Id { get; set; }

        [Display(Name = "供应商")]
        [Field(IsShowAdvancedSerach = true, IsShowBaseSerach = true, ControlsType = ControlsType.TextBox)]
        public string StoreName { get; set; }

        [JsonConverter(typeof(ObjectIdConverter))] public ObjectId StoreId { get; set; }

        [Display(Name = "商品名称")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true,
            Operator = Operator.Contains)]
        [Required(ErrorMessage = ErrorMessage.NameNotCorrect)]
        public string Name { get; set; }

        [Display(Name = "货号")]
        [Field(IsShowAdvancedSerach = true, IsShowBaseSerach = true, ControlsType = ControlsType.TextBox)]
        [Required(ErrorMessage = ErrorMessage.NameNotCorrect)]
        public string Bn { get; set; }

        [Display(Name = "商品条形码")] public string BarCode { get; set; }

        [Display(Name = "进货价")]
        [Range(0, 99999999, ErrorMessage = ErrorMessage.DoubleInRang)]
        public decimal PurchasePrice { get; set; }

        [Display(Name = "成本价")]
        [Range(0, 99999999, ErrorMessage = ErrorMessage.DoubleInRang)]
        public decimal CostPrice { get; set; }

        [Display(Name = "市场价")]
        [Range(0, 99999999, ErrorMessage = ErrorMessage.DoubleInRang)]
        public decimal MarketPrice { get; set; }

        [Display(Name = "销售价")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowAdvancedSerach = true)]
        [Range(0, 99999999, ErrorMessage = ErrorMessage.DoubleInRang)]
        public decimal Price { get; set; }

        [Display(Name = "重量")]
        [Range(0, 99999999, ErrorMessage = ErrorMessage.DoubleInRang)]
        public decimal Weight { get; set; }

        [Display(Name = "体积")]
        [Range(0, 99999999, ErrorMessage = ErrorMessage.DoubleInRang)]
        public decimal Size { get; set; }

        [Display(Name = "库存")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowAdvancedSerach = true)]
        [Range(0, 99999999, ErrorMessage = ErrorMessage.DoubleInRang)]
        public long Stock { get; set; }

        [Display(Name = "小图URl")] public string SmallUrl { get; set; }

        /// <summary>
        ///     商品价格模式的配置Id
        ///     与PriceStyleConfig 对应
        /// </summary>
        [Display(Name = "所属商城")]
        //[Field(IsShowBaseSerach = true, IsShowAdvancedSerach = true, ControlsType = ControlsType.DropdownList)]
        public Guid PriceStyleId { get; set; }

        public string PriceStyleName { get; set; }

        /// <summary>
        ///     已销售数量，通过此字段实现销量排行功能
        /// </summary>
        [Display(Name = "已销售数量，通过此字段实现销量排行功能")]
        public long SoldCount { get; set; }

        /// <summary>
        ///     商品查看数量，通过此字段实现浏览量排行功能，人气排序.通过ViewCount和SoldCount来计算商品的成交率
        /// </summary>
        [Display(Name = "商品查看数量")]
        public long ViewCount { get; set; }

        [Display(Name = "商品状态")]
        [Field(IsShowBaseSerach = true, IsShowAdvancedSerach = true, IsTabSearch = true,
            DataSource = "Alabo.App.Shop.Product.Domain.Enums.ProductStatus",
            ControlsType = ControlsType.DropdownList)]
        public ProductStatus ProductStatus { get; set; }

        [Display(Name = "是否包邮")]
        [Field(ControlsType = ControlsType.TextBox)]
        public int IsShipping { get; set; }

        /// <summary>
        ///     线上线下商品，OnlineToOffline枚举,Online = 0（线上）,Offline = 1(线下）,OnlineOrOffline = 2(线上或线下）
        /// </summary>
        [Display(Name = "线上线下商品")]
        [Field(ControlsType = ControlsType.TextBox)]
        public int OnlineToOffline { get; set; }

        /// <summary>
        ///     发布时间
        /// </summary>
        [Display(Name = "发布时间")]
        [DataType(DataType.Date)]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     定时上架时间
        /// </summary>
        [Display(Name = "定时上架时间")]
        [DataType(DataType.Date)]
        public DateTime ListTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     定时下架时间
        /// </summary>
        [Display(Name = "定时下架时间")]
        [DataType(DataType.Date)]
        public DateTime DownTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     审核时间
        /// </summary>
        [Display(Name = "审核时间")]
        [DataType(DataType.Date)]
        public DateTime CheckTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     最后更新时间,格式:yyyy-MM-dd HH:mm:ss
        /// </summary>
        [Display(Name = "最后更新时间")]
        [Field(IsShowBaseSerach = false, IsShowAdvancedSerach = false, ControlsType = ControlsType.DateTimeRang)]
        public DateTime ModifiedTime { get; set; } = DateTime.Now;

        [Display(Name = "排序")] public long SortOrder { get; set; }

        /// <summary>
        ///     Gets or sets the display price.
        /// </summary>
        /// <value>
        ///     The display price.
        /// </value>
        [Display(Name = "显示价格")]
        public string DisplayPrice { get; set; }

        public string GetSmallUrl
        {
            get
            {
                if (SmallUrl != null) {
                    return SmallUrl;
                }

                return Resolve<IAutoConfigService>().GetValue<WebSiteConfig>().NoPic;
            }
        }
    }
}