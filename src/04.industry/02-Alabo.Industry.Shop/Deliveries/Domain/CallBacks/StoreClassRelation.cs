﻿using Alabo.App.Core.Common;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Store.Domain.CallBacks {

    /// <summary>
    ///     店铺商品分类 （供应商（Supplier）添加商品时使用）
    /// </summary>
    [ClassProperty(Name = "店铺商品分类", Icon = "fa fa-shopping-cart", Description = "店铺商品分类", PageType = ViewPageType.List,
        SideBarType = SideBarType.SupplierSideBar)]
    //Mark = 1)]
    public class StoreClassRelation : IRelation {
    }
}