using Alabo.Domains.Enums;
using Alabo.Framework.Core.Reflections.Interfaces;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.Deliveries.Domain.CallBacks {

    /// <summary>
    ///     店铺商品分类 （供应商（Supplier）添加商品时使用）
    /// </summary>
    [ClassProperty(Name = "店铺商品分类", Icon = "fa fa-shopping-cart", Description = "店铺商品分类", PageType = ViewPageType.List,
        SideBarType = SideBarType.SupplierSideBar)]
    //Mark = 1)]
    public class StoreClassRelation : IRelation {
    }
}