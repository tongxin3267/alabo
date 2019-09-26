using System;
using System.Collections.Generic;
using Alabo.Industry.Shop.Categories.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Alabo.Industry.Shop.Products.Domain.Services {

    /// <summary>
    ///     ProductHelper 类为产品帮助工具类
    /// </summary>
    public static class ProductHelper {

        /// <summary>
        ///     将现金价格转换为 积分，授信，虚拟货币等，如果不是以上类型不转换
        /// </summary>
        /// <param name="product"></param>
        public static void PriceFromCash(this Entities.Product product) {
            //switch (product.PriceStyle)
            //{
            //    case PriceStyle.PointProduct:
            //    case PriceStyle.CreditProduct:
            //    case PriceStyle.VirtualProduct:
            //        var psc =
            //            Alabo.Helpers.Ioc.Resolve<IAutoConfigService>()
            //                .GetList<PriceStyleConfig>()
            //                .FirstOrDefault(p => p.PriceStyle == product.PriceStyle);
            //        var mtc =
            //             Alabo.Helpers.Ioc.Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>().First(p => p.Id == psc.MoneyTypeId);
            //        product.Price *= mtc.RateFee;
            //        break;
            //}
        }

        /// <summary>
        ///     将积分，授信，虚拟货币等 转换为现金,如果不是以上类型不转换
        /// </summary>
        /// <param name="product"></param>
        public static void PriceToCash(this Entities.Product product) {
            //switch (product.PriceStyle)
            //{
            //    case PriceStyle.PointProduct:
            //    case PriceStyle.CreditProduct:
            //    case PriceStyle.VirtualProduct:
            //        var psc =
            //            Alabo.Helpers.Ioc.Resolve<IAutoConfigService>()
            //                .GetList<PriceStyleConfig>()
            //                .FirstOrDefault(p => p.PriceStyle == product.PriceStyle);
            //        var mtc =
            //             Alabo.Helpers.Ioc.Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>().First(p => p.Id == psc.MoneyTypeId);
            //        product.Price /= mtc.RateFee;
            //        break;
            //}
        }

        #region 商品图片处理

        /// <summary>
        ///     商品属性处理
        /// </summary>
        /// <param name="product"></param>
        /// <param name="request"></param>
        /// <param name="category"></param>
        public static Entities.Product HanderProperty(Entities.Product product, HttpRequest request,
            Category category) {
            // product.ProductPropertys.AllPropertys = new List<PropertyItem>();
            //foreach (var item in category.SalePropertys) {
            //    //var values = category.PropertyValues.Where(r => r.PropertyId == item.Id);
            //    //foreach (var temp in values) {
            //    //    var value = request.Form["sale_" + temp.Id];
            //    //    if (!value.IsNullOrEmpty()) {
            //    //        var property = new PropertyItem {
            //    //            ValueName = temp.ValueName,
            //    //            PropertyGuid = item.Id
            //    //        };
            //    //        ;
            //    //        property.PropertyValueGuid = temp.Id;
            //    //        property.ValueAlias = temp.ValueAlias;//别名后期设置
            //    //        property.IsSale = true;
            //    //        product.ProductPropertys.AllPropertys.Add(property);
            //    //    }
            //    //}
            //}

            //foreach (var item in category.DisplayPropertys) {
            //    //复选框多条记录
            //    if (item.ControlsType == ControlsType.CheckBox) {
            //        //var values = category.PropertyValues.Where(r => r.PropertyId == item.Id);
            //        //foreach (var temp in values) {
            //        //    var value = request.Form["display_" + temp.Id];
            //        //    if (!value.IsNullOrEmpty()) {
            //        //        var property = new PropertyItem {
            //        //            ValueName = temp.ValueName,
            //        //            PropertyGuid = item.Id
            //        //        };
            //        //        ;
            //        //        property.PropertyValueGuid = temp.Id;
            //        //        property.IsSale = false;
            //        //        property.ValueAlias = temp.ValueAlias;
            //        //        product.ProductPropertys.AllPropertys.Add(property);
            //          // }
            //        }
            //    } else {
            //        //其他框一条记录
            //        var value = request.Form["display_" + item.Id];
            //        if (!value.IsNullOrEmpty()) {
            //            var property = new PropertyItem {
            //                ValueName = value,
            //                ValueAlias = value,
            //                PropertyGuid = item.Id,
            //                IsSale = false
            //            };
            //            product.ProductPropertys.AllPropertys.Add(property);
            //        }
            //    }
            //}
            ////   product.PropertyJson = product.ProductPropertys.ToJson();
            //return product;
            return null;
        }

        /// <summary>
        ///     商品属性处理
        /// </summary>
        /// <param name="product"></param>
        /// <param name="saleProperties"></param>
        /// <param name="displayProperties"></param>
        /// <param name="category"></param>
        public static void HanderProperty(Entities.Product product, List<Guid> saleProperties,
            List<Guid> displayProperties, Category category) {
            //product.ProductPropertys.AllPropertys = new List<PropertyItem>();
            //foreach (var item in category.SalePropertys) {
            //    var values = category.PropertyValues.Where(r => r.PropertyId == item.Id && saleProperties.Contains(r.Id));
            //    foreach (var temp in values) {
            //        var property = new PropertyItem {
            //            ValueName = temp.ValueName,
            //            PropertyGuid = item.Id
            //        };
            //        ;
            //        property.PropertyValueGuid = temp.Id;
            //        property.ValueAlias = temp.ValueAlias;//别名后期设置
            //        property.IsSale = true;
            //        product.ProductPropertys.AllPropertys.Add(property);
            //    }
            //}

            //foreach (var item in category.DisplayPropertys) {
            //    //复选框多条记录
            //    if (item.ControlsType == ControlsType.CheckBox) {
            //        var values = category.PropertyValues.Where(r => r.PropertyId == item.Id && displayProperties.Contains(r.Id));
            //        foreach (var temp in values) {
            //            var property = new PropertyItem {
            //                ValueName = temp.ValueName,
            //                PropertyGuid = item.Id
            //            };
            //            ;
            //            property.PropertyValueGuid = temp.Id;
            //            property.IsSale = false;
            //            property.ValueAlias = temp.ValueAlias;
            //            product.ProductPropertys.AllPropertys.Add(property);
            //        }
            //    } else {
            //        var value = displayProperties.Where(id => id.Equals(item.Id)).FirstOrDefault();
            //        if (!value.IsGuidNullOrEmpty()) {
            //            //其他框一条记录
            //            var property = new PropertyItem {
            //                ValueName = value.ToString(),
            //                ValueAlias = value.ToString(),
            //                PropertyGuid = item.Id,
            //                IsSale = false
            //            };
            //            product.ProductPropertys.AllPropertys.Add(property);
            //        }
            //    }
            //}
            //product.PropertyJson = product.ProductPropertys.ToJson();
            //  return null;
        }

        #endregion 商品图片处理
    }
}