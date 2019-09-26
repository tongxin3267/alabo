using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Datas.Queries.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Core.WebUis.Design.AutoForms;
using Alabo.Industry.Shop.Carts.Domain.Entities;
using Alabo.Industry.Shop.Carts.Domain.Services;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Industry.Shop.Carts.UI
{
    /// <summary>
    ///     购物车
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Order_Cart")]
    [ClassProperty(Name = "购物车", Icon = IconLineawesome.shopping_cart, ListApi = "Api/Cart/GetCart",
        PostApi = "Api/Cart/GetCart",
        SideBarType = SideBarType.FullScreen)]
    public class CartForm : UIBase, IAutoForm
    {
        /// <summary>
        ///     店铺Id
        /// </summary>
        public long StoreId { get; set; }

        /// <summary>
        ///     商品Id
        /// </summary>
        [Display(Name = "商品Id")]
        public long ProductId { get; set; }

        public long ProductSkuId { get; set; }

        /// <summary>
        ///     商品名称
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
        ///     销售价
        /// </summary>
        [Display(Name = "销售价")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        public decimal Price { get; set; }

        /// <summary>
        ///     数量
        /// </summary>
        [Display(Name = "数量")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        public long Count { get; set; }

        /// <summary>
        ///     状态
        ///     购物车不删除
        /// </summary>
        public Status Status { get; set; } = Status.Normal;

        /// <summary>
        ///     转换成Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            var cartId = ToId<long>(id);
            var cartView = Resolve<ICartService>().GetViewById(cartId);
            var model = AutoMapping.SetValue<CartForm>(cartView);
            return ToAutoForm(model);
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            var cartView = AutoMapping.SetValue<Cart>(model);
            var result = Resolve<ICartService>().AddOrUpdate(cartView);
            return new ServiceResult(result);
        }
    }
}