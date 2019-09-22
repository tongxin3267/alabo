using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Alabo.App.Shop.Product.Domain.Entities.Extensions;
using Alabo.App.Shop.Product.Domain.Enums;
using Alabo.App.Shop.Product.ViewModels;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Datas.Queries.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Tenants;
using Alabo.Web.Mvc.ViewModel;
using Alabo.UI.AutoTables;
using Alabo.Extensions;
using Alabo.App.Shop.Product.Domain.Services;
using Alabo.App.Shop.Store.Domain.Services;
using Alabo.UI;
using Alabo.App.Shop.Product.Domain.Dtos;
using Alabo.Helpers;
using Alabo.Domains.Query;
using Alabo.Exceptions;

namespace Alabo.App.Shop.Product.Domain.Entities {

    [ClassProperty(Name = "供应商商品管理", Icon = "fa fa-puzzle-piece", Description = "供应商商品", PageType = ViewPageType.List, PostApi = "Api/Store/GetProductList", ListApi = "Api/Product/ProductList")]
    /// <summary>
    /// Class Product.
    /// </summary>
    public class ProductStore : UIBase, IAutoTable<Product> {

        #region 属性

        /// <summary>
        ///     供应商 Id，0 表示平台商品
        /// </summary>
        [Display(Name = "供应商")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, PlaceHolder = "请输入供应商",
            IsShowAdvancedSerach = true, DataField = "StoreId", GroupTabId = 2, IsMain = true, Width = "150",
            ListShow = true, SortOrder = 2, Link = "/Admin/Product/Edit?id=[[Id]]")]
        public long StoreId { get; set; }

        /// <summary>
        ///     商品价格模式的配置Id 与PriceStyleConfig 对应
        /// </summary>
        [Display(Name = "商品价格模式的配置Id")]
        public Guid PriceStyleId { get; set; }

        /// <summary>
        /// 运费模板ID
        /// </summary>
        [Display(Name = "运费模板ID")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string DeliveryTemplateId { get; set; }

        /// <summary>
        ///     商品类目
        /// </summary>
        [Display(Name = "商品类目")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
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
        [Display(Name = "商品名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, PlaceHolder = "请输入商品名称", Operator = Operator.Contains,
            IsShowAdvancedSerach = true, DataSource = "Alabo.App.Shop.Product.Domain.Entities", GroupTabId = 1, IsMain = true, Width = "150",
            ListShow = true, SortOrder = 1, Link = "/Admin/Product/Edit?id=[[Id]]")]
        [StringLength(60, ErrorMessage = "60个字以内")]
        public string Name { get; set; }

        /// <summary>
        ///     商品货号
        /// </summary>
        [Display(Name = "货号")]
        [Field(ControlsType = ControlsType.TextBox, IsShowBaseSerach = true, PlaceHolder = "请输入货号",
            IsShowAdvancedSerach = true, DataSource = "Alabo.App.Shop.Product.Domain.Entities", GroupTabId = 3, IsMain = true, Width = "150",
            ListShow = true, SortOrder = 2, Link = "/Admin/Product/Edit?id=[[Id]]")]
        [StringLength(60, ErrorMessage = "60个字以内")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
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
        [Field(ControlsType = ControlsType.NumberRang, ListShow = true, Width = "120", SortOrder = 4)]
        [Range(0, 99999999, ErrorMessage = "商品销售价必须为大于等于0的数字")]
        public decimal Price { get; set; }

        /// <summary>
        ///     价格显示方式，最终页面上的显示价格 页面上显示价格
        /// </summary>
        [Display(Name = "显示价格")]
        [Field(ListShow = true, Width = "110", SortOrder = 9)]
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
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "120", SortOrder = 5)]
        [Range(0, long.MaxValue, ErrorMessage = "商品库存必须为大于等于0的整数")]
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
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "120", SortOrder = 1)]
        public string ThumbnailUrl { get; set; }

        /// <summary>
        ///     Gets or sets the modified time.
        /// </summary>

        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "120", SortOrder = 10)]
        [Display(Name = "修改时间")]
        public DateTime ModifiedTime { get; set; }

        /// <summary>
        ///     Gets or sets the product status.
        /// </summary>
        [Display(Name = "商品状态")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, IsTabSearch = true, Width = "120", SortOrder = 10)]
        public ProductStatus ProductStatus { get; set; } = ProductStatus.Auditing;

        /// <summary>
        ///     Gets or sets the sort order.
        /// </summary>
        [Display(Name = "排序顺序")]
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
        ///     免邮费
        /// </summary>
        [Display(Name = "免邮费")]
        public bool IsFreeShipping { get; set; } = false;

        /// <summary>
        ///     用户收藏次数
        /// </summary>
        [Display(Name = "收藏量")]
        public long FavoriteCount { get; set; }

        [Display(Name = "所属商城")]
        [NotMapped]
        public string PriceStyleName { get; set; }

        /// <summary>
        ///     所属活动 ,使用Json保存
        /// </summary>
        [Field(ExtensionJson = "Alabo.App.Shop.Product.Domain.Entities.Extensions.ProductActivityExtension")]
        [Display(Name = "所属活动")]
        public string Activity { get; set; }

        #region 以下字段程序使用,不生成数据库

        /// <summary>
        ///     商品详情
        /// </summary>
        [Display(Name = "商品详情")]
        public ProductDetail Detail { get; set; }

        /// <summary>
        ///     商品所有属性，包括销售属性，和非销售属性 (非数据库字段)
        /// </summary>
        [Display(Name = "商品所有属性")]
        public ProductExtensions ProductExtensions { get; set; }

        /// <summary>
        ///     Gets the store.
        /// </summary>
        [Display(Name = "获取库存")]
        public Store.Domain.Entities.Store Store { get; internal set; }

        /// <summary>
        ///     商品活动扩展.
        /// </summary>
        [Display(Name = "商品活动扩展")]
        public ProductActivityExtension ProductActivityExtension { get; set; }

        #endregion 以下字段程序使用,不生成数据库

        #endregion 属性

        /// <summary>
        /// 构建自动表单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PageResult<Product> PageTable(object query, AutoBaseModel autoModel) {
            var model = new PagedList<Product>();

            var store = Resolve<IStoreService>().GetUserStore(autoModel.BasicUser.Id);
            if (store == null) {
                throw new ValidException("您不是供应商,暂无店铺");
            }
            model = Resolve<IProductService>().GetPagedList(query, r => r.StoreId == store.Id);

            return ToPageResult(model);
        }

        public List<TableAction> Actions() {
            var list = new List<TableAction>
            {
                ToLinkAction("商品添加", "/User/Product/Store/Edit",TableActionType.QuickAction),//供应商增加

                ToLinkAction("商品编辑", "/User/Product/Store/Edit",TableActionType.ColumnAction),//供应商编辑
                //ToLinkAction("商品活动", "/User/Activity/Store/Index",TableActionType.ColumnAction),//管理员活动
                ToLinkAction("删除商品", "/Api/Store/DeleteProduct",ActionLinkType.Delete,TableActionType.ColumnAction)//供应商删除
            };
            return list;
        }
    }

    //public class ProductStoreTableMap : MsSqlAggregateRootMap<ProductStore>
    //{
    //    protected override void MapTable(EntityTypeBuilder<ProductStore> builder)
    //    {
    //        builder.ToTable("ZKShop_Product");
    //    }

    //    protected override void MapProperties(EntityTypeBuilder<ProductStore> builder)
    //    {
    //        //应用程序编号
    //        builder.HasKey(e => e.Id);
    //        builder.Property(e => e.Name).HasMaxLength(250);
    //        builder.Ignore(e => e.ProductExtensions);
    //        builder.Ignore(e => e.ProductActivityExtension);
    //        builder.Ignore(e => e.Detail);
    //        builder.Ignore(e => e.Store);
    //        builder.Ignore(e => e.Version);
    //        if (TenantContext.IsTenant)
    //        {
    //            // builder.HasQueryFilter(r => r.Tenant == TenantContext.CurrentTenant);
    //        }
    //    }
    //}
}