using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.App.Shop.Order.ViewModels;
using Alabo.App.Shop.Product.Domain.CallBacks;
using Alabo.Cloud.Shop.ShopSaleReport.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Mapping;
using ZKCloud.App.Core.User.Domain.Entities.Extensions;

namespace _07_Alabo.Cloud.Shop.ShopSaleReport.Services {

    /// <summary>
    /// 商城业绩统计
    /// </summary>
    public class ShopSaleReportService : ServiceBase, IShopSaleReportService {

        /// <summary>
        ///  TODO 2019年9月23日 重构注释 云应用商城业绩统计
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ShopSaleReportService(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public PagedList<ViewShopSale> GetShopSalePagedList(object query) {
            var pageList = Resolve<IUserMapService>().GetPagedList(query);
            var dictionary = query.DeserializeJson<Dictionary<string, string>>();
            dictionary.TryGetValue("type", out var type);
            var shopSaleList = new List<ViewShopSale>();
            var userIds = pageList.Select(r => r.UserId).Distinct().ToList();
            foreach (var item in pageList) {
                var view = new ViewShopSale();
                AutoMapping.SetValue(item, view);
                //item.ShopSaleExtension = item.ShopSaleInfo.DeserializeJson<ShopSaleExtension>();
                //view.ModifiedTime = item.ShopSaleExtension.ModifiedTime;
                //view.Id = item.UserId; //Id=用户Id
                //if (type == "user") {
                //    var result = GetDictionary(item.ShopSaleExtension.UserShopSale.PriceStyleSales);
                //    view.PriceStyleSales = result.Item1;
                //    AutoMapping.SetValue(item.ShopSaleExtension.UserShopSale, view);
                //}

                //if (type == "recomend") {
                //    var result = GetDictionary(item.ShopSaleExtension.RecomendShopSale.PriceStyleSales);
                //    view.PriceStyleSales = result.Item1;
                //    AutoMapping.SetValue(item.ShopSaleExtension.RecomendShopSale, view);
                //}

                //if (type == "second") {
                //    var result = GetDictionary(item.ShopSaleExtension.SecondShopSale.PriceStyleSales);
                //    view.PriceStyleSales = result.Item1;
                //    AutoMapping.SetValue(item.ShopSaleExtension.SecondShopSale, view);
                //}

                //if (type == "team") {
                //    var result = GetDictionary(item.ShopSaleExtension.TeamShopSale.PriceStyleSales);
                //    view.PriceStyleSales = result.Item1;
                //    AutoMapping.SetValue(item.ShopSaleExtension.TeamShopSale, view);
                //}

                shopSaleList.Add(view);
            }

            return PagedList<ViewShopSale>.Create(shopSaleList, pageList.RecordCount, pageList.PageSize,
                pageList.PageIndex);
        }

        /// <summary>
        ///     更新订单业绩
        ///     更新到表User_UserMap.ShopSale中
        /// </summary>
        /// <param name="orderId"></param>
        public void UpdateUserSaleInfo(long orderId) {
            var order = Resolve<IOrderService>().GetSingleWithProducts(orderId);
            if (order != null && order.OrderStatus != OrderStatus.WaitingBuyerPay &&
                order.OrderStatus != OrderStatus.Closed) {
                var userMap = Resolve<IUserMapService>().GetSingle(order.UserId);
                // 下单用户更新自身业绩
                //userMap.ShopSaleExtension.UserShopSale =
                //    SetSalfInfoByOrderId(userMap.ShopSaleExtension.UserShopSale, order); // 自身业绩
                //Resolve<IUserMapService>().UpdateShopSale(order.UserId, userMap.ShopSaleExtension);

                // 上一级用户更新推荐业绩
                var userConfig = Resolve<IAutoConfigService>().GetValue<TeamConfig>();
                if (userConfig.TeamLevel < 2) {
                    userConfig.TeamLevel = 2;
                }

                foreach (var parent in userMap.ParentMapList) {
                    if (parent.ParentLevel > userConfig.TeamLevel) {
                        break;
                    }

                    var parentUserMap = Resolve<IUserMapService>().GetSingle(parent.UserId);
                    if (parentUserMap != null) {
                        //// 团队业绩
                        //parentUserMap.ShopSaleExtension.TeamShopSale =
                        //    SetSalfInfoByOrderId(parentUserMap.ShopSaleExtension.TeamShopSale, order);
                        //if (parent.ParentLevel == 2) {
                        //    parentUserMap.ShopSaleExtension.SecondShopSale =
                        //        SetSalfInfoByOrderId(parentUserMap.ShopSaleExtension.SecondShopSale, order);
                        //}

                        //if (parent.ParentLevel == 1) {
                        //    parentUserMap.ShopSaleExtension.RecomendShopSale =
                        //        SetSalfInfoByOrderId(parentUserMap.ShopSaleExtension.RecomendShopSale, order);
                        //}

                        //Resolve<IUserMapService>().UpdateShopSale(parent.UserId, parentUserMap.ShopSaleExtension);
                    }
                }
            }
        }

        private Tuple<Dictionary<Guid, string>, string> GetDictionary(IEnumerable<PriceStyleSale> priceStyleSales) {
            var dictionary = new Dictionary<Guid, string>();
            var str = string.Empty;
            if (priceStyleSales != null) {
                priceStyleSales.Foreach(r => {
                    str =
                        $"订单({r.OrderCount})商品({r.ProductCount})总金额({r.PriceAmount})总分润({r.FenRunAmount})总支付({r.PaymentAmount})总服务费({r.FeeAmount})";
                    str = $"<code>{str}</code>";
                    dictionary.Add(r.PriceStyleId, str);
                    // str += $"{r.GradeName}({r.Count})  ";
                });
            }

            return Tuple.Create(dictionary, str);
        }

        private ShopSale SetSalfInfoByOrderId(ShopSale shopSale, Alabo.App.Shop.Order.Domain.Entities.Order order) {
            // 统计下单会员自身业绩数据
            shopSale.TotalOrderCount += 1; // 总订单数加1
            shopSale.TotalPaymentAmount += order.PaymentAmount;
            shopSale.TotalPriceAmount += order.TotalAmount;
            shopSale.TotalProductCount += order.TotalCount;
            shopSale.TotalExpressAmount += order.OrderExtension.OrderAmount.ExpressAmount;

            shopSale.TotalFeeAmount += order.OrderExtension.OrderAmount.FeeAmount; //总服务费

            var priceStyleConfigs =
                Resolve<IAutoConfigService>().GetList<PriceStyleConfig>(r => r.Status == Status.Normal);
            foreach (var priceStyle in priceStyleConfigs) {
                var productSkuItems = order.OrderExtension.ProductSkuItems.Where(r => r.PriceStyleId == priceStyle.Id);
                if (productSkuItems != null && productSkuItems.Count() > 0) {
                    // 所有商城的下单商品
                    var priceStyleProducts =
                        order.Products.Where(r => productSkuItems.Select(e => e.ProductSkuId).Contains(r.SkuId));
                    if (priceStyleProducts.Count() <= 0) {
                        continue;
                    }

                    var priceStyleSale = shopSale.PriceStyleSales.FirstOrDefault(r => r.PriceStyleId == priceStyle.Id);
                    if (priceStyleSale == null) {
                        priceStyleSale = new PriceStyleSale {
                            PriceStyleId = priceStyle.Id
                        };
                        shopSale.PriceStyleSales.Add(priceStyleSale);
                    }

                    shopSale.PriceStyleSales.Foreach(e => {
                        if (e.PriceStyleId == priceStyle.Id) {
                            e.PriceStyleName = priceStyle.Name;

                            e.OrderCount += 1; // 订单数加1
                            e.ProductCount += priceStyleProducts.Sum(r => r.Count); // 商品总数
                            e.PriceAmount += priceStyleProducts.Sum(r => r.TotalAmount); // 价格统计
                            e.FenRunAmount += priceStyleProducts.Sum(r => r.FenRunAmount); // 分润统计
                            e.PaymentAmount += priceStyleProducts.Sum(r => r.PaymentAmount); // 支付金额统计
                            e.FeeAmount +=
                                priceStyleProducts.Sum(r => r.OrderProductExtension.OrderAmount.FeeAmount); //服务费统计

                            shopSale.TotalFenRunAmount += priceStyleProducts.Sum(r => r.FenRunAmount); // 分润费用
                        }
                    });
                }
            }

            return shopSale;
        }

        public PagedList<ViewPriceStyleSale> GetViewPriceStyleSalePagedList(object query) {
            var pageList = Resolve<IUserMapService>().GetPagedList(query);
            var dictionary = query.DeserializeJson<Dictionary<string, string>>();
            dictionary.TryGetValue("type", out var type);
            dictionary.TryGetValue("priceStyle", out var _priceStyleId);
            var priceStyleId = _priceStyleId.ToGuid();
            var shopSaleList = new List<ViewPriceStyleSale>();
            var userIds = pageList.Select(r => r.UserId).Distinct().ToList();
            foreach (var item in pageList) {
                var view = new ViewPriceStyleSale();
                AutoMapping.SetValue(item, view);
                //item.ShopSaleExtension = item.ShopSaleInfo.DeserializeJson<ShopSaleExtension>();
                //view.ModifiedTime = item.ShopSaleExtension.ModifiedTime;
                //view.Id = item.UserId; //Id=用户Id
                //if (type == "user") {
                //    var priceStyleSales =
                //        item.ShopSaleExtension.UserShopSale.PriceStyleSales.FirstOrDefault(r =>
                //            r.PriceStyleId == priceStyleId);
                //    AutoMapping.SetValue(priceStyleSales, view);
                //}

                //if (type == "recomend") {
                //    var priceStyleSales =
                //        item.ShopSaleExtension.RecomendShopSale.PriceStyleSales.FirstOrDefault(r =>
                //            r.PriceStyleId == priceStyleId);
                //    AutoMapping.SetValue(priceStyleSales, view);
                //}

                //if (type == "second") {
                //    var priceStyleSales =
                //        item.ShopSaleExtension.SecondShopSale.PriceStyleSales.FirstOrDefault(r =>
                //            r.PriceStyleId == priceStyleId);
                //    AutoMapping.SetValue(priceStyleSales, view);
                //}

                //if (type == "team") {
                //    var priceStyleSales =
                //        item.ShopSaleExtension.TeamShopSale.PriceStyleSales.FirstOrDefault(r =>
                //            r.PriceStyleId == priceStyleId);
                //    AutoMapping.SetValue(priceStyleSales, view);
                //}

                shopSaleList.Add(view);
            }

            return PagedList<ViewPriceStyleSale>.Create(shopSaleList, pageList.RecordCount, pageList.PageSize,
                pageList.PageIndex);
        }
    }
}