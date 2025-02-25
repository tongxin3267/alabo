﻿using Alabo.Domains.Enums;
using Alabo.Framework.Core.Reflections.Interfaces;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.Products.Domain.Configs
{
    /// <summary>
    ///     控制面板中 商品标签
    /// </summary>
    [ClassProperty(Name = "商品标签", Icon = "fa fa-book", Description = "商品标签", PageType = ViewPageType.List)]
    //SideBarGroup = "Shop", SideBar = "SideBar", Mark = 1)]
    public class ProductTagRelation : IRelation
    {
    }

    /// <summary>
    ///     控制面板中 商品分类
    ///     Alabo.App.Shop.Product.Domain.CallBacks.ProductCalssRelation
    /// </summary>
    [ClassProperty(Name = "商品分类", Icon = "fa fa-database", Description = "商品分类", PageType = ViewPageType.List, Mark = 0)]
    public class ProductClassRelation : IRelation
    {
    }
}