using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Alabo.App.Asset.Accounts.Domain.Repositories;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Basic.Grades.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Industry.Shop.Categories.Domain.Entities;
using Alabo.Industry.Shop.Orders.Dtos;
using Alabo.Industry.Shop.Products.Domain.Configs;
using Alabo.Industry.Shop.Products.Domain.Entities;
using Alabo.Industry.Shop.Products.Domain.Enums;
using Alabo.Industry.Shop.Products.Domain.Repositories;
using Alabo.Industry.Shop.Products.Dtos;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Industry.Shop.Products.Domain.Services
{
    public class ProductSkuService : ServiceBase<ProductSku, long>, IProductSkuService
    {
        private readonly IProductSkuRepository _productSkuRepository;

        public ProductSkuService(IUnitOfWork unitOfWork, IRepository<ProductSku, long> repository) : base(unitOfWork,
            repository)
        {
            _productSkuRepository = Repository<IProductSkuRepository>();
        }

        /// <summary></summary>
        /// <param name="product"></param>
        /// <param name="skuList"></param>
        public ServiceResult AddUpdateOrDelete(Product product, List<ProductSku> skuList)
        {
            var result = ServiceResult.Failed;
            var oldList = Resolve<IProductSkuService>().GetList(m => m.ProductId == product.Id).ToList();
            if (skuList.Count == 0) {
                return ServiceResult.FailedWithMessage("商品规格为空");
            }

            var specnList = skuList.Select(r => r.SpecSn).IsDistinct();

            var addList = new List<ProductSku>();
            var updateList = new List<ProductSku>();

            foreach (var skuItem in skuList) //遍历上送的是增加还是修改
            {
                skuItem.ProductId = product.Id;
                if (string.IsNullOrEmpty(skuItem.SpecSn)) {
                    continue;
                }

                var find = oldList.FirstOrDefault(e => e.SpecSn.Contains(skuItem.SpecSn));
                if (find == null)
                {
                    // 暂注释此行 2019.03.23
                    // skuItem.SpecSn = GetSnSpec(skuItem.PropertyJson);
                    skuItem.DisplayPrice = Resolve<IProductService>()
                        .GetDisplayPrice(skuItem.Price, product.PriceStyleId, product.MinCashRate);
                    addList.Add(skuItem);
                }
                else
                {
                    //可根据实际业务情况增减列更新内容
                    find.BarCode = skuItem.BarCode;
                    find.Bn = skuItem.Bn;
                    find.CostPrice = skuItem.CostPrice;
                    find.MarketPrice = skuItem.MarketPrice;
                    find.Modified = skuItem.Modified;
                    find.Price = skuItem.Price;
                    find.ProductStatus = skuItem.ProductStatus;
                    find.PropertyJson = skuItem.PropertyJson;
                    find.PropertyValueDesc = skuItem.PropertyValueDesc;
                    find.PurchasePrice = skuItem.PurchasePrice;
                    find.Size = skuItem.Size;
                    find.Stock = skuItem.Stock;
                    find.FenRunPrice = skuItem.FenRunPrice;
                    find.StorePlace = skuItem.StorePlace;
                    find.Weight = skuItem.Weight;

                    //特殊处理
                    // 暂注释此行 2019.03.23
                    //find.SpecSn = GetSnSpec(skuItem.PropertyJson);
                    find.DisplayPrice = Resolve<IProductService>()
                        .GetDisplayPrice(skuItem.Price, product.PriceStyleId, product.MinCashRate);
                    updateList.Add(find);
                }
            }

            // 删除处理
            var deleteList = oldList.Where(r => !updateList.Select(e => e.Id).Contains(r.Id)).ToList();
            foreach (var item in deleteList) {
                Resolve<IProductSkuService>().Delete(e => e.Id == item.Id);
            }

            if (!addList.Select(r => r.SpecSn).IsDistinct()) {
                return ServiceResult.FailedWithMessage("商品规格重复添加");
            }
            // 编辑数据无需处理，EF会自动处理
            if (!updateList.Select(r => r.SpecSn).IsDistinct()) {
                return ServiceResult.FailedWithMessage("商品规格重复添加");
            }

            if (addList.Count > 0) {
                Resolve<IProductSkuService>().AddMany(addList);
            }

            return ServiceResult.Success;
        }

        /// <summary>
        ///     Automatics the update sku price.
        ///     后台自动更新商品Sku的价格
        ///     更加货币类型和价格自动更新显示价、最高现金价
        /// </summary>
        public void AutoUpdateSkuPrice(long productId = 0)
        {
            var skuPriceLsit = _productSkuRepository.GetSkuPrice(productId);
            // 删除缓存，确保数据最新
            ObjectCache.Remove("AutoConfigCacheKey_Alabo.App.Core.Finance.Domain.CallBacks.MoneyTypeConfig");
            ObjectCache.Remove("AutoConfigCacheKey_Alabo.App.Shop.Product.Domain.CallBacks.PriceStyleConfig");
            var moneyTypes = Resolve<IAutoConfigService>().MoneyTypes(); // 所有货币类型
            var priceSytles =
                Resolve<IAutoConfigService>()
                    .GetList<PriceStyleConfig>(r => r.Status == Status.Normal); // 状态不正常的商品，可能不支持价格类型

            #region 更新Sku价格显示

            var productSkuList = new List<ProductSku>();
            foreach (var item in skuPriceLsit)
            {
                var sku = new ProductSku
                {
                    Id = item.ProductSkuId,
                    DisplayPrice =
                        Resolve<IProductService>()
                            .GetDisplayPrice(item.Price, item.PriceStyleId, item.MinCashRate) // 显示价格
                };
                var priceStyleConfig = priceSytles.FirstOrDefault(r => r.Id == item.PriceStyleId);
                // 如果商品中有价格参数，则使员商品中的最小抵现价格
                if ((item.MinCashRate > 0) & (item.MinCashRate <= 1)) {
                    priceStyleConfig.MinCashRate = item.MinCashRate;
                }

                if (priceStyleConfig != null) {
                    try
                    {
                        var moneyConfig = moneyTypes.FirstOrDefault(r => r.Id == priceStyleConfig.MoneyTypeId);
                        if (moneyConfig.RateFee == 0) {
                            moneyConfig.RateFee = 1;
                        }

                        // 如果不是现金商品
                        if (priceStyleConfig.PriceStyle != PriceStyle.CashProduct)
                        {
                            sku.MinPayCash = Math.Round(item.Price * priceStyleConfig.MinCashRate, 2); // 最低可使用的现金资产
                            sku.MaxPayPrice =
                                Math.Round(item.Price * (1 - priceStyleConfig.MinCashRate) / moneyConfig.RateFee,
                                    2); // 最高可使用的现金资产
                        }
                        else
                        {
                            sku.MaxPayPrice = 0; // 现金商品，最高可使用的虚拟资产为0
                            sku.MinPayCash = item.Price; //现金商品，最低使用的现金为价格
                        }

                        productSkuList.Add(sku);
                    }
                    catch (Exception ex)
                    {
                        Trace.WriteLine(ex.Message);
                    }
                }
            }

            // 跟新价格
            _productSkuRepository.UpdateSkuPrice(productSkuList);

            #endregion 更新Sku价格显示

            var productids = skuPriceLsit.Select(r => r.ProductId).ToList();
            foreach (var item in productids)
            {
                var product = Resolve<IProductService>().GetSingle(r => r.Id == item);
                if (product != null)
                {
                    //TODO 生成二维码
                    // Resolve<IProductAdminService>().CreateQrcode(product.Id);
                    if (product.MinCashRate == 0)
                    {
                        var priceStyleConfig = priceSytles.FirstOrDefault(r => r.Id == product.PriceStyleId);
                        //product.MinCashRate = priceStyleConfig.MinCashRate;
                    }

                    product.DisplayPrice = Resolve<IProductService>()
                        .GetDisplayPrice(product.Price, product.PriceStyleId, product.MinCashRate); // 显示价格
                    Resolve<IProductService>().Update(r => { r.DisplayPrice = product.DisplayPrice; },
                        r => r.Id == product.Id);
                }
            }
        }

        /// <summary>
        ///     Gets the store money buy skus.
        ///     根据sku信息，获取店铺可是使用的资产信息
        /// </summary>
        /// <param name="productSkuItems">The product sku items.</param>
        public IEnumerable<OrderMoneyItem> GetStoreMoneyBuySkus(IEnumerable<ProductSkuItem> productSkuItems,
            long UserId)
        {
            var priceStyleIds = productSkuItems.Select(r => r.PriceStyleId).Distinct().ToList(); // 商品模式Id
            var priceSytles =
                Resolve<IAutoConfigService>()
                    .GetList<PriceStyleConfig>(r => r.Status == Status.Normal); // 状态不正常的商品，可能不支持价格类型
            var productStyleMoneyIds = priceSytles.Where(r => priceStyleIds.Contains(r.Id)).Select(r => r.MoneyTypeId)
                .Distinct().ToList(); // 获取商品中包括的货币类型

            var allMoneyTypes = Resolve<IAutoConfigService>().MoneyTypes();
            var productMoneyTypes =
                allMoneyTypes.Where(r =>
                    productStyleMoneyIds.Contains(r.Id) && r.Currency != Currency.Cny); // 现金商品不需要计算

            //获取所有资产账户
            var accountList = Repository<IAccountRepository>().GetUserAllAccount(UserId,
                productMoneyTypes.Select(r => r.Id).ToList().JoinToString("','")); // 获取所有可支付的货币类型
            var moneyItems = new List<OrderMoneyItem>();
            foreach (var moneyItemId in productStyleMoneyIds)
            {
                var moneyItemConfig = allMoneyTypes.FirstOrDefault(e => e.Id == moneyItemId);
                if (moneyItemConfig != null)
                {
                    var priceStyleConfigs = priceSytles.Where(e => e.MoneyTypeId == moneyItemConfig.Id);
                    if (priceStyleConfigs != null)
                    {
                        var account = accountList.FirstOrDefault(r => r.MoneyTypeId == moneyItemConfig.Id);
                        if (account != null)
                        {
                            var orderMoneyItem = new OrderMoneyItem
                            {
                                Name = moneyItemConfig.Name,
                                MoneyId = moneyItemConfig.Id,
                                Balance = account.Amount, // 余额
                                Description = $"{moneyItemConfig.Name}余额{account.Amount}{moneyItemConfig.Unit}"
                            };
                            foreach (var styleItem in priceStyleConfigs)
                            {
                                productSkuItems.Where(t => t.PriceStyleId == styleItem.Id).ToList().ForEach(r =>
                                {
                                    orderMoneyItem.MaxPayPrice += r.MaxPayPrice * r.BuyCount; // 最高可以使用虚拟资产支付
                                    orderMoneyItem.MinPayCash += r.MinPayCash * r.BuyCount; // 最低现金支付
                                });
                                if (orderMoneyItem.MaxPayPrice > account.Amount) {
                                    orderMoneyItem.MaxPayPrice = account.Amount; // 不能超过余额
                                }

                                orderMoneyItem.Title =
                                    $"使用{moneyItemConfig.Name}{orderMoneyItem.MaxPayPrice}{moneyItemConfig.Unit}";
                            }

                            if (orderMoneyItem.MaxPayPrice > 0) {
                                moneyItems.Add(orderMoneyItem);
                            }
                        }
                    }
                }
            }

            return moneyItems;
        }

        /// <summary>
        ///     获取商品会员等级价格
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public List<ProductSku> GetGradePrice(long productId)
        {
            //user grade
            var userGrades = Resolve<IGradeService>().GetUserGradeList().ToList();
            //get product skus
            var productSkus = _productSkuRepository.GetList(e => e.ProductId == productId).ToList();
            productSkus.Foreach(item =>
            {
                if (userGrades.Count <= 0) {
                    return;
                }
                //add default grade
                item.GradePriceList = userGrades.Select(u => new SkuGradePriceItem
                {
                    Id = u.Id,
                    Name = u.Name,
                    Price = u.Discount > 0 ? u.Discount * item.Price : item.Price
                    //FenRunPrice = (u.Discount > 0 ? u.Discount * item.Price : item.Price)
                }).ToList();
                //sku grade
                var skuGradeList = item.GradePrice.DeserializeJson<List<SkuGradePriceItem>>();
                item.GradePriceList.ForEach(grade =>
                {
                    var tempGrade = skuGradeList.Find(g => g.Id == grade.Id);
                    if (tempGrade != null) {
                        grade.Price = tempGrade.Price;
                    }
                    //grade.FenRunPrice = tempGrade.FenRunPrice;
                });
            });
            return productSkus;
        }

        /// <summary>
        ///     更新会员价
        /// </summary>
        /// <param name="productSkus"></param>
        public ApiResult UpdateGradePrice(List<ProductSku> productSkus)
        {
            //check
            var skuIds = productSkus.Select(s => s.Id).ToList();
            var productSkusOfData = new List<ProductSku>();
            if (skuIds.Count > 0) {
                productSkusOfData = Resolve<IProductSkuService>().GetList(s => skuIds.Contains(s.Id)).ToList();
            }

            if (productSkusOfData.Count <= 0) {
                return ApiResult.Failure("更新会员价失败，提交数据错误");
            }
            //save
            productSkusOfData.ForEach(item =>
            {
                var tempSku = productSkus.Find(s => s.Id == item.Id);
                if (tempSku != null) {
                    item.GradePrice = tempSku.GradePriceList.ToJson();
                }

                Resolve<IProductSkuService>().Update(item);
            });
            return ApiResult.Success();
        }

        private string GetSnSpec(string propertyJson)
        {
            var specSn = "";
            if (!string.IsNullOrEmpty(propertyJson)) //如果规则json不为空
            {
                var propertyList = propertyJson.DeserializeJson<List<CategoryPropertyValue>>();
                specSn = propertyList.Select(m => m.Id).Aggregate("", (current, proId) => current + (proId + "|"));
            }

            return specSn;
        }
    }
}