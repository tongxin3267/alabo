using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Data.People.Stores.Domain.Entities;
using Alabo.Data.People.Stores.Domain.Entities.Extensions;
using Alabo.Data.People.Stores.Domain.Repositories;
using Alabo.Data.People.Stores.Domain.Services;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query;
using Alabo.Domains.Query.Dto;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.Address.Domain.Entities;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Industry.Shop.Activitys.Modules.GroupBuy.Model;
using Alabo.Industry.Shop.Deliveries.Domain.Dtos;
using Alabo.Industry.Shop.Deliveries.Domain.Entities;
using Alabo.Industry.Shop.Deliveries.Domain.Entities.Extensions;
using Alabo.Industry.Shop.Deliveries.Domain.Enums;
using Alabo.Industry.Shop.Deliveries.Domain.Repositories;
using Alabo.Industry.Shop.Deliveries.ViewModels;
using Alabo.Industry.Shop.Orders.Dtos;
using Alabo.Industry.Shop.Products.Domain.Repositories;
using Alabo.Industry.Shop.Products.Domain.Services;
using Alabo.Industry.Shop.Products.Dtos;
using Alabo.Mapping;
using MongoDB.Bson;

namespace Alabo.Industry.Shop.Deliveries.Domain.Services {

    /// <summary>
    /// </summary>
    public class ShopStoreService : ServiceBase, IShopStoreService {
        private const string _storeItemListCacheKey = "GetStoreItemListFromCache";

        private readonly IProductSkuRepository _productSkuRepository;

        /// <summary>
        /// </summary>
        /// <param name="dto"></param>

        public PagedList<ViewStore> GetViewStorePageList(PagedInputDto dto) {
            var query = new ExpressionQuery<Store> {
                PageIndex = (int)dto.PageIndex,
                PageSize = (int)dto.PageSize
            };
            query.OrderByDescending(r => r.Id);
            var stores = GetPagedList(query);
            IList<ViewStore> result = new List<ViewStore>();
            //店铺名称、等级、推荐人、是否自营、状态、创建时间
            //  var grades = Resolve<IAutoConfigService>().GetList<SupplierGradeConfig>();
            var parentUsers = Resolve<IUserService>().GetList(stores.Select(e => e.ParentUserId).ToList());
            foreach (var item in stores) {
                var viewStore = new ViewStore {
                    Id = item.Id,
                    ParentUserId = item.ParentUserId,
                    ParentUserName = parentUsers.FirstOrDefault(e => e.Id == item.ParentUserId)?.UserName,
                    Name = item.Name,
                    IsPlanform = item.IsPlanform,
                    //   Status = item.Status,
                    CreateTime = item.CreateTime
                };
                //if (grades != null || grades.FirstOrDefault(e => e.Id.Equals(item.GradeId)) != null)
                //{
                //    viewStore.SupplierGradeConfig = grades.FirstOrDefault(e => e.Id.Equals(item.GradeId));
                //    viewStore.GradeName = grades.FirstOrDefault(e => e.Id.Equals(item.GradeId))?.Name;
                //}

                result.Add(viewStore);
            }

            return PagedList<ViewStore>.Create(result, stores.RecordCount, stores.PageSize, stores.PageIndex);
        }

        /// <summary>
        ///     获取自营店铺
        ///     平台店铺，后台添加的时候，为平台商品
        /// </summary>
        public Store PlanformStore() {
            var planformUser = Resolve<IUserService>().PlanformUser();
            if (planformUser == null) {
                return null;
            }

            var store = GetSingle(r => r.UserId == planformUser.Id);
            // 如果店铺为空，则初始化店铺平台店铺
            if (store == null) {
                store = new Store {
                    UserId = planformUser.Id,
                    Name = "自营店铺",
                    ParentUserId = 0,
                    GradeId = Guid.Parse("72BE65E6-3A64-414D-972E-1A3D4A36F701"),
                    CreateTime = DateTime.Now,
                    Status = UserTypeStatus.Success
                };

                var context = Repository<IStoreRepository>().RepositoryContext;
                context.BeginTransaction();
                try {
                    Add(store);

                    context.SaveChanges();
                    context.CommitTransaction();
                } catch (Exception ex) {
                    context.RollbackTransaction();
                } finally {
                    context.DisposeTransaction();
                }

                //Resolve<IDeliveryTemplateService>().InitAllStoreDelivery();
            }

            store = GetSingle(r => r.UserId == planformUser.Id);

            return store;
        }

        /// <summary>
        ///     获取s the 会员 store.
        ///     获取会员店铺
        /// </summary>
        /// <param name="UserId">会员Id</param>
        public Store GetUserStore(long UserId) {
            var user = Resolve<IUserService>().GetNomarlUser(UserId);
            if (user == null) {
                return null;
            }

            var store = GetSingle(r => r.UserId == user.Id && r.Status == UserTypeStatus.Success);
            if (store != null) {
                // store.StoreExtension = store.Extension.ToObject<StoreExtension>();
            }

            return store;
        }

        /// <summary>
        /// </summary>
        /// <param name="storeId"></param>

        public StoreExtension GetStoreExtension(long storeId) {
            //TODO 店铺Id修改
            var store =Resolve<IStoreService>(). GetSingle(storeId);
            if (store == null) {
                return null;
            }

            return store.StoreExtension;


        }

        /// <summary>
        /// </summary>
        /// <param name="stroeId"></param>
        /// <param name="storeExtension"></param>

        public ServiceResult UpdateExtensions(long stroeId, StoreExtension storeExtension) {
            var store = GetSingle(r => r.Id == stroeId);
            if (store == null) {
                return ServiceResult.FailedWithMessage("店铺不存在,更新失败");
            }

            store.Extension = ObjectExtension.ToJson(storeExtension);
            if (Update(store)) {
                return ServiceResult.Success;
            }

            return ServiceResult.FailedWithMessage("更新到数据库失败");
        }

        /// <summary>
        /// </summary>
        /// <param name="store"></param>

        public ServiceResult AddOrUpdate(ViewStore store) {
            var user = Resolve<IUserService>().GetSingle(store.UserName);
            if (user == null) {
                return ServiceResult.FailedWithMessage("所属用户不存在");
            }

            if (user.Status != Status.Normal) {
                return ServiceResult.FailedWithMessage("该用户状态不正常");
            }
            var userStore = Resolve<IShopStoreService>().GetSingle(u => u.UserId == user.Id);
            if (userStore != null) {
                if (userStore.Id != store.Id) {
                    return ServiceResult.FailedWithMessage("该用户已经是供应商，无法重复添加");
                }
            }

            var find = Resolve<IShopStoreService>().GetByIdNoTracking(u => u.Id == store.Id);
            if (find == null) {
                find = new Store {
                    UserId = user.Id,
                    Name = store.Name,
                    GradeId = store.GradeId,
                    //   Status = store.Status,
                };
            } else {
                find.Name = store.Name;
                find.GradeId = store.GradeId;
                find.UserId = user.Id;
                //   find.Status = store.Status;
            }

            if (!store.ParentUserName.IsNullOrEmpty()) {
                var parentUser = Resolve<IUserService>().GetSingle(store.ParentUserName);
                if (parentUser == null) {
                    return ServiceResult.FailedWithMessage("推荐用户不存在");
                }
                find.ParentUserId = parentUser.Id;
            }

            // 如果没有扩展属性，初始化扩展属性
            if (find.Extension.IsNullOrEmpty()) {
                var storeExtension = new StoreExtension { };
                find.Extension = ObjectExtension.ToJson(storeExtension);
            }

            var result = ServiceResult.Success;
            if (find.Id == 0) {
                if (!Add(find)) {
                    result = ServiceResult.FailedWithMessage("供应商添加失败");
                }
            } else {
                if (!Update(find)) {
                    result = ServiceResult.FailedWithMessage("供应商更新失败");
                }
            }
            ObjectCache.Remove(_storeItemListCacheKey); // 清除店铺缓存
            return result;
        }

        public ViewStore GetView(long id) {
            ViewStore view = new ViewStore();
            var store = Resolve<IShopStoreService>().GetByIdNoTracking(id);

            if (store != null) {
                view = AutoMapping.SetValue<ViewStore>(store);
                var user = Resolve<IUserService>().GetSingle(r => r.Id == store.UserId);
                view.UserName = user?.UserName;
                if (store.ParentUserId > 0) {
                    view.ParentUserName = Resolve<IUserService>().GetSingle(r => r.Id == store.ParentUserId)?.UserName;
                }
            }
            return view;
        }

        /// <summary>
        /// </summary>
        public ViewStore GetViewStore(long Id) {
            var store = GetSingle(e => e.Id.Equals(Id));
            if (store == null) {
                //var stroreGrade = Resolve<IAutoConfigService>()
                //    .GetList<SupplierGradeConfig>(r => r.Status == Status.Normal);
                //var viewStore = new ViewStore
                //{
                //    GradeId = stroreGrade.FirstOrDefault().Id
                //};
                // return viewStore;
            }

            var user = Resolve<IUserService>().GetSingle(store.UserId);
            var parentUser = Resolve<IUserService>().GetSingle(store.ParentUserId);

            return new ViewStore {
                Name = store.Name,
                UserId = store.UserId,
                ParentUserId = store.ParentUserId,
                GradeId = store.GradeId,
                IsPlanform = store.IsPlanform,
                UserName = user?.UserName,
                ParentUserName = parentUser?.UserName,
                //  Status = store.Status
            };
        }

        /// <summary>
        ///     Gets the store item list from cache.
        ///     从缓存中，读取StoreItem对象
        /// 后期供应商多的时候，需要优化
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<StoreItem> GetStoreItemListFromCache() {
            if (!ObjectCache.TryGet(_storeItemListCacheKey, out List<StoreItem> result)) {
                result = new List<StoreItem>();
                var delivertyTemplates = Resolve<IDeliveryTemplateService>().GetList();
                var storeList = GetList(r => r.Status == UserTypeStatus.Success);
                foreach (var item in storeList) {
                    var storeItem = new StoreItem {
                        StoreId = item.Id,
                        StoreName = item.Name
                    };
                    var storeDelivertyTemplate = delivertyTemplates.Where(r => r.StoreId == item.Id);
                    storeDelivertyTemplate.Foreach(r => {
                        storeItem.ExpressTemplates.Add(new KeyValue(r.Id, r.TemplateName));
                    });

                    result.Add(storeItem);
                }

                if (result != null) {
                    ObjectCache.Set(_storeItemListCacheKey, result);
                }
            }

            return result;
        }

        /// <summary>
        /// </summary>
        /// <param name="query"></param>

        public PagedList<ViewStore> GetPageList(object query) {
            var stores = Resolve<IShopStoreService>().GetPagedList(query);
            var users = Resolve<IUserService>().GetList();
            var list = new List<ViewStore>();
            //var grades = Resolve<IAutoConfigService>().GetList<SupplierGradeConfig>();
            foreach (var item in stores) {
                var viewStore = AutoMapping.SetValue<ViewStore>(item);
                var user = users.FirstOrDefault(u => u.Id == item.UserId);
                // viewStore.SupplierGradeConfig = grades.FirstOrDefault(u => u.Id == item.GradeId);
                // viewStore.GradeName = grades.FirstOrDefault(u => u.Id == item.GradeId)?.Name;
                viewStore.UserName = Resolve<IUserService>().GetUserStyle(user);
                viewStore.ParentUserName = users.FirstOrDefault(u => u.Id == item.ParentUserId)?.UserName;
                list.Add(viewStore);
            }

            return PagedList<ViewStore>.Create(list, list.Count, stores.PageSize, stores.PageIndex);
        }

        /// <summary>
        ///     根据店铺Id列表，和商品skuId列表，输出店铺商品显示对象
        ///     用户购物车、客户下单
        ///     店铺数量多的情况下，改方法需要优化，店铺数量少的情况下，将店铺数据插入到了缓存了，所有性能较快
        ///     Gets the store product sku.
        /// </summary>
        /// <param name="storeProductSkuDtos">The store product sku dtos.</param>
        public Tuple<ServiceResult, StoreProductSku> GetStoreProductSku(StoreProductSkuDtos storeProductSkuDtos) {
            var serviceResult = ServiceResult.Success;
            var storeItemList = GetStoreItemListFromCache();
            var productSkuItemList = _productSkuRepository.GetProductSkuItemList(storeProductSkuDtos.ProductSkuIds);
            if (storeProductSkuDtos.IsGroupBuy) {
                //如果是拼团商品，从商品中读取，处理分润价、显示价、以及拼团价
                var product = Resolve<IProductService>().GetSingle(r => r.Id == storeProductSkuDtos.ProductId);
                if (product == null) {
                    return null;
                }

                var productActivity =
                    product.ProductActivityExtension.Activitys.First(r => r.Key == typeof(GroupBuyActivity).FullName);
                if (productActivity == null) {
                    return Tuple.Create(ServiceResult.FailedWithMessage("当前购买商品不是拼团商品"),
                        new StoreProductSku()); // 不是拼团商品
                }

                var groupBuyActivity = productActivity.Value.ToObject<GroupBuyActivity>();
                productSkuItemList.Foreach(r => {
                    var groupBuySku = groupBuyActivity.SkuProducts.FirstOrDefault(e => e.Id == r.ProductSkuId);
                    if (groupBuySku == null) {
                        serviceResult = ServiceResult.FailedWithMessage("当前Sku未设置拼团价格");
                    } else {
                        r.DisplayPrice = groupBuySku.GroupBuyDisplayPrice;
                        r.FenRunPrice = groupBuySku.FenRunPrice;
                        r.MaxPayPrice = groupBuySku.MaxPayPrice;
                        r.MinPayCash = groupBuySku.MinPayCash;
                        r.PlatformPrice = r.Price;
                        r.Price = groupBuySku.Price;
                    }
                });
            }

            if (storeItemList == null || storeItemList?.ToList()?.Count <= 0 || productSkuItemList == null ||
                productSkuItemList?.ToList()?.Count <= 0) {
                return null;
            }

            if (storeProductSkuDtos.IsApiImage) {
                productSkuItemList.ToList().ForEach(c => {
                    c.ThumbnailUrl = Resolve<IApiService>().ApiImageUrl(c.ThumbnailUrl);
                });
            }

            var storeProductSku = new StoreProductSku();
            storeItemList = storeItemList.Where(r => storeProductSkuDtos.StoreIds.Contains(r.StoreId)).ToList();
            foreach (var item in storeItemList) {
                item.ProductSkuItems = productSkuItemList.Where(r => r.StoreId == item.StoreId).ToList();
                storeProductSku.StoreItems.Add(item);
            }

            storeItemList = storeItemList.Where(r => r.ProductSkuItems.Count > 0); // 店铺商品数大于0的店铺，如果商品不存在，或者下架则不显示
            return Tuple.Create(serviceResult, storeProductSku);
        }

        public ShopStoreService(IUnitOfWork unitOfWork, IRepository<Store, long> repository) : base(unitOfWork, repository) {
            _productSkuRepository = Repository<IProductSkuRepository>();
        }

        /// <summary>
        ///     Counts the express fee.
        ///     根据客户选择的物流方式，商品重量，计算快递费用
        /// </summary>
        /// <param name="storeId">The stroe identifier.</param>
        /// <param name="templId">The express identifier.</param>
        /// <param name="userAddress">用户地址</param>
        /// <param name="weight">The weight.</param>
        public Tuple<ServiceResult, decimal> CountExpressFee(long storeId, ObjectId templId, UserAddress userAddress, decimal weight) {
            var expressFee = 0m; // 快递费用
            var express = Resolve<IDeliveryTemplateService>().GetSingle(t => t.StoreId == storeId && t.Id == templId);
            if (express == null) {
                return Tuple.Create(ServiceResult.FailedWithMessage("运费模板未找到"), expressFee);
            }
            //卖家承担运费的模版，运费为0
            if (express.TemplateType == DeliveryTemplateType.SellerBearTheFreight) {
                return Tuple.Create(ServiceResult.Success, expressFee);
            }
            //先检查区县，如果区县运费找到，则直接返回
            var regionTemplateFee = GetTemplateFeeByRegionId(express, userAddress.RegionId);
            if (regionTemplateFee != null) {
                expressFee = CountFeeByWeight(regionTemplateFee, weight);
                return Tuple.Create(ServiceResult.Success, expressFee);
            }

            // 如果区县未运费找到,再检查城市，如果城市运费找到，则直接返回
            regionTemplateFee = GetTemplateFeeByRegionId(express, userAddress.City);
            if (regionTemplateFee != null) {
                expressFee = CountFeeByWeight(regionTemplateFee, weight);
                return Tuple.Create(ServiceResult.Success, expressFee);
            }

            // 如果城市未运费找到,再检查省份，如果省份运费找到，则直接返回
            regionTemplateFee = GetTemplateFeeByRegionId(express, userAddress.Province);
            if (regionTemplateFee != null) {
                expressFee = CountFeeByWeight(regionTemplateFee, weight);
                return Tuple.Create(ServiceResult.Success, expressFee);
            }

            // 如果省份未运费找到,默认数据配置
            var regionFee = new RegionTemplateFee {
                StartFee = express.StartFee,
                EndFee = express.EndFee,
                StartStandard = express.StartStandard,
                EndStandard = express.EndStandard
            };
            expressFee = CountFeeByWeight(regionFee, weight);
            return Tuple.Create(ServiceResult.Success, expressFee);
        }

        /// <summary>
        ///     Counts the express fee.
        ///     根据客户选择的物流方式，商品重量，计算快递费用
        /// </summary>
        /// <param name="storeId">The stroe identifier.</param>
        /// <param name="userAddress">用户地址</param>
        /// <param name="productSkuItems">The weight.</param>
        public Tuple<ServiceResult, decimal> CountExpressFee(long storeId, UserAddress userAddress, IList<ProductSkuItem> productSkuItems) {
            //total
            var totalExpressFee = 0m;
            //get all delivery template
            var deliveryTemplates = Resolve<IDeliveryTemplateService>().GetList(d => d.StoreId == storeId).ToList();
            var productGroups = productSkuItems.GroupBy(p => p.ProductId).ToList();
            productGroups.ForEach(product => {
                var tempDelivery = product.FirstOrDefault();
                var template = deliveryTemplates.FirstOrDefault(d => d.Id.ToString() == tempDelivery?.DeliveryTemplateId);
                if (template == null || template.TemplateType == DeliveryTemplateType.SellerBearTheFreight) {
                    return;
                }

                //delivery freight calculate type
                var value = 0M;
                if (template.CalculateType == DeliveryFreightCalculateType.ByCount) {
                    value = product.Sum(p => p.BuyCount);
                }
                if (template.CalculateType == DeliveryFreightCalculateType.ByWeight) {
                    value = product.Sum(p => p.IsFreeShipping ? 0 : (p.BuyCount * p.Weight) / 1000);
                }

                //region (现在用户只存了县级id (440105),县级找不到截前四位)
                var tempExpressFee = CountFeeByRegion(template, userAddress.RegionId, value);
                if (tempExpressFee < 0) {
                    //city
                    var cityId = userAddress.RegionId.ToString().Substring(0, 4).ToLong();
                    //if (userAddress.City > 0)
                    //{
                    //    cityId = userAddress.City;
                    //}
                    tempExpressFee = CountFeeByRegion(template, cityId, value);

                    if (tempExpressFee < 0) {
                        //province
                        var provinceId = userAddress.RegionId.ToString().Substring(0, 2).ToLong();
                        //if (userAddress.Province > 0)
                        //{
                        //    provinceId = userAddress.Province;
                        //}
                        tempExpressFee = CountFeeByRegion(template, provinceId, value);
                    }
                }
                if (tempExpressFee < 0) {
                    //default
                    var regionFee = new RegionTemplateFee {
                        StartFee = template.StartFee,
                        EndFee = template.EndFee,
                        StartStandard = template.StartStandard,
                        EndStandard = template.EndStandard
                    };
                    totalExpressFee += CountFeeByWeight(regionFee, value);
                } else {
                    totalExpressFee += tempExpressFee;
                }
            });

            return Tuple.Create(ServiceResult.Success, totalExpressFee);
        }

        /// <summary>
        /// CountFeeByRegion
        /// </summary>
        /// <param name="express"></param>
        /// <param name="regionId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private decimal CountFeeByRegion(DeliveryTemplate express, long regionId, decimal value) {
            var regionTemplateFee = GetTemplateFeeByRegionId(express, regionId);
            if (regionTemplateFee != null) {
                return CountFeeByWeight(regionTemplateFee, value);
            }
            return -1;
        }

        /// <summary>
        ///     Gets the template fee by region identifier.
        ///     根据区域Id，返回快递模板
        /// </summary>
        /// <param name="expressTemplate"></param>
        /// <param name="regionId">The region identifier.</param>
        private RegionTemplateFee GetTemplateFeeByRegionId(DeliveryTemplate expressTemplate, long regionId) {
            // 遍历所有的物流模板，从第一个开始
            foreach (var feeItem in expressTemplate.TemplateFees) // 遍历所有的区域
            {
                foreach (var id in feeItem.RegionList) {
                    if (id == regionId) {
                        return feeItem;
                    }
                }
            }

            return null;
        }

        /// <summary>
        ///     Counts the fee by weight.
        ///     根据重量计算区域运费
        /// </summary>
        /// <param name="regionFee">The delivery fee.</param>
        /// <param name="weight">The weight.</param>
        private decimal CountFeeByWeight(RegionTemplateFee regionFee, decimal weight) {
            //基础费用为首重
            var result = regionFee.StartFee;
            //判断是否超过首重
            if (weight > regionFee.StartStandard) {
                //续重重量（整数)
                var nextWeight = weight - regionFee.StartStandard;
                //nextWeight = Math.Ceiling(nextWeight); // 向上取整
                //排除续费为0，不需要运费的
                if (regionFee.EndStandard != 0) {
                    //加上续重费用，以续重为基础，加上超过其倍数的费用
                    var nextWeightFee = Math.Ceiling(nextWeight / regionFee.EndStandard);
                    result += nextWeightFee * regionFee.EndFee;
                    //如果超过首重的部分不能被续费重量整除，则向上取整，需要再加入单位续重
                    //if (nextWeight % regionFee.EndStandard != 0) {
                    //    result += regionFee.EndFee;
                    //}
                }
            }

            return result;
        }
    }
}