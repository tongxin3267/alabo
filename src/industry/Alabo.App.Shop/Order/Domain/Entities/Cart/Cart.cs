using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;using MongoDB.Bson.Serialization.Attributes;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.Core.UI;
using Alabo.Datas.Queries.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Order.Domain.Entities {

    /// <summary>
    /// 购物车
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Order_Cart")]
    [ClassProperty(Name = "购物车", Icon = IconLineawesome.shopping_cart, ListApi = "Api/Cart/GetCart", PostApi = "Api/Cart/GetCart",
        SideBarType = SideBarType.FullScreen)]
    public class Cart : AggregateMongodbUserRoot<Cart> {

        /// <summary>
        /// 店铺Id
        /// </summary>
        public long StoreId { get; set; }

        /// <summary>
        /// 商品Id
        /// </summary>
        [Display(Name = "商品Id")]
        public long ProductId { get; set; }

        public long ProductSkuId { get; set; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true,
            ListShow = true, Operator = Operator.Contains)]
        [Display(Name = "商品名称")]
        public string ProductName { get; set; }

        /// <summary>
        ///     商品货号
        /// </summary>
        [Display(Name = "商品货号")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true,
            ListShow = true)]
        public string Bn { get; set; }

        /// <summary>
        ///     规格属性说明,属性、规格的文字说明 比如：绿色 XL
        /// </summary>
        [Display(Name = "规格属性")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true,
            ListShow = true)]
        public string PropertyValueDesc { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        [Display(Name = "销售价")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Display(Name = "数量")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        public long Count { get; set; }

        /// <summary>
        /// 状态
        /// 购物车不删除
        /// </summary>
        public Status Status { get; set; } = Status.Normal;

        /// <summary>
        /// 通用分页查询列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PageResult<Cart> PageTable(object query, AutoBaseModel autoModel)
        {
            var list = Resolve<ICartService>().GetPagedList(query);
            return ToPageResult(list);
        }
    }
}