using Alabo.App.Core.Finance.Domain.CallBacks;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Shop.Activitys.Domain.Enum;
using Alabo.App.Shop.Activitys.Domain.Services;
using Alabo.App.Shop.Activitys.Modules.BuyPermision.Model;
using Alabo.App.Shop.Activitys.Modules.MemberDiscount.Callbacks;
using Alabo.App.Shop.Activitys.Modules.MemberDiscount.Model;
using Alabo.App.Shop.Activitys.Modules.TimeLimitBuy.Model;
using Alabo.App.Shop.Category.Domain.Entities;
using Alabo.App.Shop.Product.DiyModels;
using Alabo.App.Shop.Product.Domain.CallBacks;
using Alabo.App.Shop.Product.Domain.Dtos;
using Alabo.App.Shop.Product.Domain.Entities;
using Alabo.App.Shop.Product.Domain.Entities.Extensions;
using Alabo.App.Shop.Product.Domain.Enums;
using Alabo.App.Shop.Product.Domain.Repositories;
using Alabo.App.Shop.Product.ViewModels;
using Alabo.App.Shop.Store.Domain.Dtos;
using Alabo.App.Shop.Store.Domain.Services;
using Alabo.AutoConfigs;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.Helpers;
using Alabo.Mapping;
using Alabo.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Basic.Relations.Domain.Services;
using Alabo.Framework.Core.WebApis.Service;

namespace Alabo.App.Shop.Product.Domain.Services
{

    /// <summary>
    /// ProductService
    /// </summary>
    public class ProductService : ServiceBase<Entities.Product, long>, IProductService {

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="repository"></param>
        public ProductService(IUnitOfWork unitOfWork, IRepository<Entities.Product, long> repository)
            : base(unitOfWork, repository) {
        }

        /// <summary>
        /// 获取产品数量
        /// </summary>
        /// <returns></returns>
        public long GetProductCount() {
            var count = Resolve<IProductService>().Count();
            return count;
        }

        /// <summary>
        /// 通过店铺获取产品数量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public long GetProductByStoreCount(long id) {
            var count = Resolve<IProductService>().GetList().Where(l => l.StoreId == id).Count().ConvertToLong();
            return count;
        }

        /// <summary>
        ///     商品查询列表，次函数基本完成了商品列表的所有功能
        /// </summary>
        /// <param name="productApiInput"></param>
        public ProductItemApiOutput GetProductItems(ProductApiInput productApiInput) {
            var apiOutput = new ProductItemApiOutput();
            // 如果商城模式不正常，或者商品模式的货币类型不正常，则商品不显示
            var moneyTypesids = Resolve<IAutoConfigService>().MoneyTypes().Select(r => r.Id);
            productApiInput.PriceStyles = Resolve<IAutoConfigService>()
                .GetList<PriceStyleConfig>(r => r.Status == Status.Normal)
                .Where(r => moneyTypesids.Contains(r.MoneyTypeId)).ToList();
            var model = Repository<IProductRepository>().GetProductItems(productApiInput, out var count);
            var proIdList = model.Select(x => x.Id);
            var skuList = Resolve<IProductSkuService>().GetList(x => proIdList.Contains(x.ProductId));

            model.ForEach(r => {
                r.ThumbnailUrl = Resolve<IApiService>().ApiImageUrl(r.ThumbnailUrl);
                r.Price = decimal.Round(r.Price, 2);
                r.Stock = skuList.Where(x => x.ProductId == r.Id).Sum(x => x.Stock);
                r.DisplayPrice = Resolve<IProductService>().GetDisplayPrice(r.Price, r.PriceStyleId, 0M);//库存改为 sku库存的总和
            });

            apiOutput.ProductItems = model;
            apiOutput.TotalSize = count / productApiInput.PageSize + 1;
            apiOutput.StyleType = 1;
            apiOutput.PageIndex = productApiInput.PageIndex;
            return apiOutput;
        }

        /// <summary>
        ///  GetProductItemsExt   商品查询列表，次函数基本完成了商品列表的所有功能
        /// </summary>
        /// <param name="productApiInput"></param>
        public ProductItemApiOutput GetProductItemsExt(ProductApiInput productApiInput) {
            var apiOutput = new ProductItemApiOutput();
            // 如果商城模式不正常，或者商品模式的货币类型不正常，则商品不显示
            var moneyTypesids = Resolve<IAutoConfigService>().MoneyTypes().Select(r => r.Id);
            productApiInput.PriceStyles = Resolve<IAutoConfigService>()
                .GetList<PriceStyleConfig>(r => r.Status == Status.Normal)
                .Where(r => moneyTypesids.Contains(r.MoneyTypeId)).ToList();
            var model = Repository<IProductRepository>().GetProductItems(productApiInput, out var count);
            var proIdList = model.Select(x => x.Id);
            var skuList = Resolve<IProductSkuService>().GetList(x => proIdList.Contains(x.ProductId));

            model.ForEach(r => {
                r.ThumbnailUrl = Resolve<IApiService>().ApiImageUrl(r.ThumbnailUrl);
                r.Price = decimal.Round(r.Price, 2);
                r.Stock = skuList.Where(x => x.ProductId == r.Id).Sum(x => x.Stock);
                r.DisplayPrice = Resolve<IProductService>().GetDisplayPrice(r.Price, r.PriceStyleId, 0M);//库存改为 sku库存的总和
            });

            apiOutput.ProductItems = model;
            apiOutput.TotalSize = count / productApiInput.PageSize + 1;
            apiOutput.StyleType = 1;
            return apiOutput;
        }

        /// <summary>
        ///     商品查询列表，次函数基本完成了商品列表的所有功能
        /// </summary>
        /// <param name="productApiInput"></param>
        public async Task<ProductItemApiOutput> GetProductItemsAsync(ProductApiInput productApiInput) {
            var apiOutput = new ProductItemApiOutput();
            // 如果商城模式不正常，或者商品模式的货币类型不正常，则商品不显示
            var moneyTypesids = Resolve<IAutoConfigService>().MoneyTypes().Select(r => r.Id);
            productApiInput.PriceStyles = Resolve<IAutoConfigService>()
                .GetList<PriceStyleConfig>(r => r.Status == Status.Normal)
                .Where(r => moneyTypesids.Contains(r.MoneyTypeId)).ToList();
            var model = Repository<IProductRepository>().GetProductItems(productApiInput, out var count);
            var proIdList = model.Select(x => x.Id);
            var skuList = Resolve<IProductSkuService>().GetList(x => proIdList.Contains(x.ProductId));

            model.ForEach(r => {
                r.ThumbnailUrl = Resolve<IApiService>().ApiImageUrl(r.ThumbnailUrl);
                r.Price = decimal.Round(r.Price, 2);
                r.Stock = skuList.Where(x => x.ProductId == r.Id).Sum(x => x.Stock);
                r.DisplayPrice = Resolve<IProductService>().GetDisplayPrice(r.Price, r.PriceStyleId, 0M);//库存改为 sku库存的总和
            });

            apiOutput.ProductItems = model;
            apiOutput.TotalSize = count / productApiInput.PageSize + 1;
            apiOutput.StyleType = 1;
            return await Task.FromResult(apiOutput);
        }

        /// <summary>
        ///     获取商品分类
        /// </summary>
        public List<Relation> GetProductClassList() {
            var productClassList = Resolve<IRelationService>().GetList(e =>
                e.Type == "Alabo.App.Shop.Product.Domain.CallBacks.ProductClassRelation"
                && e.Status == Status.Normal).OrderBy(r => r.SortOrder).ToList();
            return productClassList;
        }

        /// <summary>
        ///     获取商品分类
        /// </summary>
        public List<Relation> GetProductRelations() {
            var productClassList = Resolve<IRelationService>().GetList(e =>
                e.Type == "Alabo.App.Shop.Product.Domain.CallBacks.ProductClassRelation"
                && e.Status == Status.Normal).OrderBy(r => r.SortOrder).ToList();
            var result = new List<Relation>();
            foreach (var item in productClassList) {
                if (item.FatherId > 0) {
                    continue;
                }
                result.Add(item);
            }
            return result;
        }

        public List<Entities.Product> GetProductsByRelationId(long relationId) {
            var model = Resolve<IRelationIndexService>().GetList(u => u.RelationId == relationId);
            var productList = Resolve<IProductService>().GetList();
            var result = new List<Entities.Product>();
            foreach (var item in model) {
                var temp = productList.FirstOrDefault(u => u.Id == item.EntityId);
                result.Add(temp);
            }
            return result;
        }

        /// <summary>
        /// 获得商品详情
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userId"></param>
        public Entities.Product GetShow(long id, long userId) {
            //product
            var product = GetSingle(r => r.Id == id);
            if (product == null) {
                return product;
            }
            //details
            product.Detail = Resolve<IProductDetailService>().GetSingle(e => e.ProductId == product.Id);
            if (product.Detail == null) {
                return null;
            }
            var apiService = Resolve<IApiService>();
            product.Detail.MobileIntro = apiService.ConvertToApiImageUrl(product.Detail.MobileIntro);
            product.Detail.Intro = apiService.ConvertToApiImageUrl(product.Detail.Intro);
            var website = Resolve<IAutoConfigService>().GetValue<WebSiteConfig>();

            var productThums = new List<ProductThum>();

            if (!product.Detail.ImageJson.IsNullOrEmpty()) {
                productThums = product.Detail.ImageJson.DeserializeJson<List<ProductThum>>();

                foreach (var item in productThums) {
                    item.OriginalUrl = apiService.ApiImageUrl(item.OriginalUrl);
                    item.ShowCaseUrl = apiService.ApiImageUrl(item.ShowCaseUrl);
                    item.ThumbnailUrl = apiService.ApiImageUrl(item.ThumbnailUrl);
                }
            }

            //product extension
            product.ProductExtensions = new ProductExtensions {
                Store = Resolve<IShopStoreService>().GetSingle(e => e.Id == product.StoreId),
                ProductCategory = product.Detail.PropertyJson.DeserializeJson<Category.Domain.Entities.Category>(),
                ProductThums = productThums
            };
            // 处理商品不显示规格和属性的
            if (product.ProductExtensions.ProductCategory != null && product.ProductExtensions.ProductCategory.SalePropertys != null) {
                var salePropertys = product.ProductExtensions.ProductCategory.SalePropertys;
                var newSalePropertys = new List<CategoryProperty>();
                foreach (var item in salePropertys) {
                    var saleItem = item;
                    saleItem.PropertyValues = item.PropertyValues.Where(r => r.IsCheck).ToList();
                    saleItem.PropertyValues.ForEach(r => {
                        if (r.ValueAlias.IsNullOrEmpty()) {
                            r.ValueAlias = r.ValueName;
                        }
                    });
                    newSalePropertys.Add(saleItem);
                }
                product.ProductExtensions.ProductCategory.SalePropertys = newSalePropertys;
            }
            product.ThumbnailUrl = apiService.ApiImageUrl(product.ThumbnailUrl);
            product.SmallUrl = apiService.ApiImageUrl(product.SmallUrl);

            //product all activities.
            var allActivities = Resolve<IActivityService>()
                .GetList(a => a.ProductId == id && a.IsEnable == true).ToList();
            var activityRules = allActivities.Select(a => new ProductActivity { Id = a.Id, Name = a.Name, Key = a.Key, Value = a.Value }).ToList();
            //user permissions
            var userPermissions = new UserPermissions();
            var key = ProductActivityType.BuyPermission.GetDisplayResourceTypeName();
            var buyPermissionActivity = activityRules.Find(a => a.Key == key);
            if (buyPermissionActivity != null) {
                //set
                var user = Resolve<IUserService>().GetSingle(userId);
                if (user == null) {
                    userPermissions.IsMemberLeverBuy = false;
                    userPermissions.IsMemberLeverView = false;
                }
                var rules = buyPermissionActivity.Value.ToObject<BuyPermisionActivity>();
                if (rules != null && user != null) {
                    userPermissions = rules.MapTo<UserPermissions>();
                    if (rules.MemberLeverBuyPermissions?.Count > 0 && !rules.MemberLeverBuyPermissions.Contains(user.GradeId)) {
                        userPermissions.IsMemberLeverBuy = false;
                    }
                    if (rules.MemberLeverViewPermissions?.Count > 0 && !rules.MemberLeverViewPermissions.Contains(user.GradeId)) {
                        userPermissions.IsMemberLeverView = false;
                    }
                }
                //remove
                activityRules.Remove(buyPermissionActivity);
            }
            product.ProductActivityExtension = new ProductActivityExtension {
                UserPermissions = userPermissions
            };

            //member price
            var allGradePrices = new List<SkuGradePriceItem>();
            var productSkus = Resolve<IProductSkuService>().GetList(e => e.ProductId == product.Id).ToList();
            key = ProductActivityType.MemberDiscount.GetDisplayResourceTypeName();
            var memberDiscountActivity = activityRules.Find(a => a.Key == key);
            if (memberDiscountActivity != null && productSkus.Count > 0) {
                //show front grade
                var showGradeIds = new List<string>();
                var showGrade = Resolve<IAutoConfigService>().GetValue<MemberDiscountConfig>();
                if (showGrade != null && !showGrade.GradeIds.IsNullOrEmpty()) {
                    showGradeIds = showGrade.GradeIds.Split(new char[] { ',' }).ToList();
                }
                var rules = memberDiscountActivity.Value.ToObject<MemberDiscountActivity>();
                productSkus.Foreach(item => {
                    var tempSku = rules.DiscountList.Find(d => d.ProductSkuId == item.Id);
                    if (tempSku == null || tempSku.GradeItems.Count <= 0) {
                        return;
                    }
                    var tempSkuGrades = tempSku.GradeItems.MapTo<List<SkuGradePriceItem>>();
                    item.GradePriceList = tempSkuGrades.Where(g => showGradeIds.Exists(u => u == g.Id.ToString())).ToList();

                    allGradePrices.AddRange(item.GradePriceList);
                });
                //remove
                activityRules.Remove(memberDiscountActivity);
            }
            product.ProductExtensions.ProductSkus = productSkus;
            product.Stock = productSkus.Sum(s => s.Stock);
            //grade price for product view
            var userGradePrices = new List<UserGradePriceView>();
            allGradePrices.GroupBy(g => g.Id)
                .Foreach(grade => {
                    var tempList = grade.OrderBy(g => g.Price).ToList();
                    if (tempList.Count <= 0) {
                        return;
                    }
                    userGradePrices.Add(new UserGradePriceView {
                        Name = tempList.First().Name,
                        LowPrice = tempList.First().Price,
                        HighPrice = tempList.Last().Price
                    });
                });

            //add to activity extension
            product.ProductActivityExtension.UserGradePrices = userGradePrices;
            product.ProductActivityExtension.Activitys = activityRules;

            return product;
        }

        /// <summary>
        ///     获取商品的价格显示方式
        /// </summary>
        /// <param name="price">价格</param>
        /// <param name="priceStyleId">商品模式Guid</param>
        /// <param name="productMinCashRate">商品自定义现金抵扣比例</param>
        public string GetDisplayPrice(decimal price, Guid priceStyleId, decimal productMinCashRate) {
            var priceStyle = "";
            var psList = Ioc.Resolve<IAutoConfigService>().GetList<PriceStyleConfig>();
            var priceStyleConfig = Alabo.Helpers.Ioc.Resolve<IAutoConfigService>()
                .GetList<PriceStyleConfig>(e => e.Id == priceStyleId).FirstOrDefault();
            if (priceStyleConfig == null) {
                return price.ToString("F2");
            }

            var moneyTypeConfig = Alabo.Helpers.Ioc.Resolve<IAutoConfigService>()
                .GetList<MoneyTypeConfig>(e => e.Id == priceStyleConfig.MoneyTypeId).FirstOrDefault();
            //货币类型不存在，或者状态不正常
            if (moneyTypeConfig == null) {
                return price.ToString("F2");
            }

            if (moneyTypeConfig.RateFee == 0) {
                moneyTypeConfig.RateFee = 1;
            }

            if (priceStyleConfig.PriceStyle == PriceStyle.CashProduct
                || priceStyleConfig.PriceStyle == PriceStyle.PointProduct
                || priceStyleConfig.PriceStyle == PriceStyle.VirtualProduct
                || priceStyleConfig.PriceStyle == PriceStyle.CreditProduct
                || priceStyleConfig.PriceStyle == PriceStyle.ShopAmount
                //|| priceStyleConfig.PriceStyle == PriceStyle.CashAndVirtual
                //|| priceStyleConfig.PriceStyle == PriceStyle.CashAndCredit
                ) {
                priceStyle = Math.Round(price / moneyTypeConfig.RateFee, 2).ToString("F2") + moneyTypeConfig.Unit;
            }

            if (priceStyleConfig.PriceStyle == PriceStyle.CashAndPoint
                || priceStyleConfig.PriceStyle == PriceStyle.CashAndVirtual
                || priceStyleConfig.PriceStyle == PriceStyle.CashAndCredit
                || priceStyleConfig.PriceStyle == PriceStyle.Customer
                ) {
                if (productMinCashRate != 0) {
                    priceStyleConfig.MinCashRate = productMinCashRate;
                }

                if (priceStyleConfig.MinCashRate == 1) {
                    priceStyle = Math.Round(price / moneyTypeConfig.RateFee, 2).ToString("F2") + "元";
                } else if (priceStyleConfig.MinCashRate == 0) {
                    priceStyle = Math.Round(price / moneyTypeConfig.RateFee, 2).ToString("F2") + moneyTypeConfig.Unit;
                } else {
                    var productPrice = Math.Round(price * priceStyleConfig.MinCashRate, 2).ToString("F2") + "元";
                    var othrePrice =
                        Math.Round(price * (1 - priceStyleConfig.MinCashRate) / moneyTypeConfig.RateFee, 2)
                            .ToString("F2") + moneyTypeConfig.Unit;
                    priceStyle = productPrice + "+" + othrePrice;
                }
            }

            return priceStyle;
        }

        /// <summary>
        ///     Shows the specified identifier.
        ///     获取商品详情
        /// </summary>
        /// <param name="id">Id标识</param>
        /// <param name="userId"></param>
        public Tuple<ServiceResult, Entities.Product> Show(long id, long userId) {
            var product = GetShow(id, userId);
            if (product == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("商品不存在"), product);
            }
            if (product.ProductActivityExtension != null && product.ProductActivityExtension.UserPermissions != null) {
                if (!product.ProductActivityExtension.UserPermissions.IsMemberLeverView) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("您的用户等级不能浏览该商品"), product);
                }
            }
            if (product.ProductStatus != ProductStatus.Online) {
                return Tuple.Create(ServiceResult.FailedWithMessage("商品没有上架"), product);
            }

            var priceStyle = Resolve<IAutoConfigService>()
                .GetList<PriceStyleConfig>(r => r.Status == Status.Normal && r.Id == product.PriceStyleId)
                ?.FirstOrDefault();
            if (priceStyle == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("商品的商城模式已下架"), product);
            }

            var moneyConfig = Resolve<IAutoConfigService>().MoneyTypes()
                .FirstOrDefault(r => r.Id == priceStyle.MoneyTypeId);
            if (moneyConfig == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("商品的货币类型不正常"), product);
            }

            return Tuple.Create(ServiceResult.Success, product);
        }

        public StoreInfoOutput GetStoreInfoByProductId(long productId) {
            var product = GetSingle(u => u.Id == productId);
            if (product == null) {
                return null;
            }
            var storeInfo = Resolve<IShopStoreService>().GetSingle(u => u.Id == product.StoreId);
            // var grade = Resolve<IAutoConfigService>().GetList<SupplierGradeConfig>(u => u.Id == storeInfo.GradeId);
            var model = new StoreInfoOutput {
                Store = storeInfo,
                //    StoreGradeName = grade.FirstOrDefault().Name,
                User = Resolve<IUserService>().GetSingle(u => u.Id == storeInfo.UserId)
            };

            return model;
        }

        public IList<ProductItem> GetRecommendProduct(long productId) {
            var productList = Resolve<IProductService>().GetList(u => u.Id != productId && u.ProductStatus == ProductStatus.Online);
            var recommendProduct = productList.OrderBy(u => u.SoldCount).Take(20).ToList();

            IList<ProductItem> itemList = new List<ProductItem>();
            foreach (var item in recommendProduct) {
                var model = AutoMapping.SetValue<ProductItem>(item);
                itemList.Add(model);
            }

            itemList = itemList.OrderBy(u => u.SortOrder).ToList();
            return itemList;
        }

        /// <summary>
        /// get time limit buy products
        /// </summary>
        /// <returns></returns>
        public List<TimeLimitBuyItem> GetTimeLimitBuyList() {
            var key = ProductActivityType.TimeLimitBuy.GetDisplayResourceTypeName();
            var result = new List<TimeLimitBuyItem>();
            var allActivities = Resolve<IActivityService>()
                .GetList(a => a.Key == key && a.IsEnable == true).ToList();
            var productIds = allActivities.Select(a => a.ProductId).ToList();
            var products = Resolve<IProductService>().GetList(p => productIds.Contains(p.Id)).ToList();
            allActivities.ForEach(activity => {
                //rule
                var rules = activity.Value.ToObject<TimeLimitBuyActivity>();
                if (rules == null) {
                    return;
                }
                //product
                var currentDate = DateTime.Now;
                if (rules.StartTime > currentDate || rules.EndTime < currentDate) {
                    return;
                }
                var product = products.Find(p => p.Id == activity.ProductId);
                if (product == null) {
                    return;
                }
                var item = product.MapTo<TimeLimitBuyItem>();
                item.ThumbnailUrl = Resolve<IApiService>().ApiImageUrl(item.ThumbnailUrl);
                item.StartTime = rules.StartTime;
                item.EndTime = rules.EndTime;
                item.Stock = activity.MaxStock;
                item.SoldCount = activity.UsedStock;
                result.Add(item);
            });

            return result;
        }

        public PagedList<ProductToExcel> GetProductsToExcel(object query) {
            var products = Resolve<IProductService>().GetList();
            var productSkus = Resolve<IProductSkuService>().GetList();
            // var relation = Resolve<IRelationService>().GetList(u => u.Type == "");
            var result = new List<ProductToExcel>();
            foreach (var item in productSkus) {
                var product = products.FirstOrDefault(u => u.Id == item.ProductId);
                var view = new ProductToExcel();
                view.Bn = item.Bn;
                view.ProductName = product.Name;
                view.Stock = item.Stock;
                view.RelationName = item.PropertyValueDesc;
                view.Status = item.ProductStatus.GetDisplayName();
                view.MarketPrice = item.MarketPrice;
            }

            return null;
        }
    }
}