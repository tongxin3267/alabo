using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Shop.Activitys.Domain.Entities;
using Alabo.App.Shop.Activitys.Domain.Enum;
using Alabo.App.Shop.Activitys.Domain.Repositories;
using Alabo.App.Shop.Activitys.Domain.Services;
using Alabo.App.Shop.Activitys.Dtos;
using Alabo.App.Shop.Activitys.Extensions;
using Alabo.App.Shop.Activitys.Modules.BuyPermision.Model;
using Alabo.App.Shop.Activitys.Modules.GroupBuy.Model;
using Alabo.App.Shop.Activitys.Modules.MemberDiscount.Model;
using Alabo.App.Shop.Activitys.Modules.PreSells.Model;
using Alabo.App.Shop.Activitys.Modules.ProductNumberLimit.Dtos;
using Alabo.App.Shop.Activitys.Modules.TimeLimitBuy.Model;
using Alabo.App.Shop.Activitys.ViewModels;
using Alabo.App.Shop.Order.Domain.Dtos;
using Alabo.App.Shop.Order.Domain.Repositories;
using Alabo.App.Shop.Product.Domain.Entities.Extensions;
using Alabo.App.Shop.Product.Domain.Services;
using Alabo.App.Shop.Store.Domain.Dtos;
using Alabo.Cache;
using Alabo.Core.Enums.Enum;
using Alabo.Core.WebUis.Design.AutoForms;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Mapping;
using Alabo.Users.Entities;

namespace Alabo.App.Shop.Activitys.Services
{
    /// <summary>
    /// activity api service
    /// </summary>
    public class ActivityApiService : ServiceBase, IActivityApiService
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ActivityApiService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// get list by product activity type
        /// </summary>
        /// <param name="activityType"></param>
        /// <param name="productIds"></param>
        /// <returns></returns>
        public IList<Activity> GetList(ProductActivityType activityType, IList<long> productIds)
        {
            var key = activityType.GetDisplayResourceTypeName();
            return Resolve<IActivityService>().GetList(u => u.Key == key && productIds.Contains(u.ProductId));
        }

        /// <summary>
        /// get view for activity
        /// </summary>
        /// <param name="activityInput"></param>
        /// <returns></returns>
        public ActivityEditOutput GetView(ActivityEditInput activityInput)
        {
            //check
            var type = activityInput.Key.GetTypeByFullName();
            if (type == null)
            {
                throw new ArgumentException($"类型{ activityInput.Key }不存在，请确定Url是否正确");
            }
            var instance = Activator.CreateInstance(type);
            if (!(instance is IActivity))
            {
                throw new ArgumentException("该类型不属于活动实体");
            }
            var activityEntityInstance = instance as IActivity;

            //check product
            if (activityInput.ProductId <= 0)
            {
                throw new ArgumentException("商品不存在");
            }
            var product = Resolve<IProductService>().GetSingle(activityInput.ProductId);
            if (product == null)
            {
                throw new ArgumentException("商品不存在");
            }

            //check activity
            var model = Resolve<IActivityService>().GetSingle(e => e.ProductId == activityInput.ProductId && e.Key == type.FullName);
            if (activityInput.Id > 0)
            {
                model = Resolve<IActivityService>().GetSingle(e => e.Id == activityInput.Id);
            }
            //builder
            var view = AutoMapping.SetValue<ActivityEditOutput>(activityInput);
            if (model != null)
            {
                view.Activity = model;
                view = AutoMapping.SetValue(model, view);
                //user range 
                view.UserRange = model.LimitGradeId.IsGuidNullOrEmpty()
                    ? UserRange.AllUser
                    : UserRange.ByUserGrade;
                //time range
                view.DateTimeRange = $"{model.StartTime.ToTimeString()} / {model.EndTime.ToTimeString()}";
                //support sigle product
                var attribute = Resolve<IActivityAdminService>().GetActivityModuleAttribute(type.FullName);
                if (attribute != null && attribute.IsSupportSigleProduct)
                {
                    activityInput.ProductId = model.ActivityExtension.ProductIds.FirstOrDefault();
                    view.ProductId = activityInput.ProductId;
                }
            }
            else
            {
                view.Activity = new Activity
                {
                    Key = type.FullName,
                    Status = ActivityStatus.Processing,
                    ProductId = activityInput.ProductId
                };
                view.UserRange = UserRange.AllUser;
                view.DateTimeRange = $"{DateTime.Now.AddDays(1).Date.ToTimeString()} / {DateTime.Now.AddDays(7).Date.ToTimeString()}";
            }

            //get default value
            view.ActivityRules = activityEntityInstance.GetDefaultValue(activityInput, view.Activity);
            if (view.ActivityRules == null && !view.Activity.Value.IsNullOrEmpty())
            {
                view.ActivityRules = JsonConvert.DeserializeObject(view.Activity.Value, type);
            }

            //get form
            try
            {
                view.AutoForm = activityEntityInstance.GetAutoForm(view.ActivityRules);
            }
            catch (Exception)
            { }

            if (view.AutoForm == null)
            {
                view.AutoForm = view.ActivityRules == null
                        ? AutoFormMapping.Convert(type.FullName)
                        : AutoFormMapping.Convert(view.ActivityRules);
            }

            return view;
        }

        /// <summary>
        /// Adds the or update activity.
        /// </summary>
        public ServiceResult Save(ActivityEditOutput model)
        {
            //check and set  model value.
            var result = SetModelValue(model);
            if (!result.Succeeded)
            {
                return result;
            }
            var activity = model.Activity;
            var allActvities = Resolve<IActivityService>().GetList(a => a.Key == activity.Key).ToList();
            if (activity.Id > 0)
            {
                //update remove selft
                allActvities.RemoveAll(a => a.Id == activity.Id);
            }
            if (allActvities.Exists(a => a.ProductId == model.ProductId))
            {
                return ServiceResult.FailedWithMessage($"产品id:{model.ProductId} ,该商品已经存在相关设置");
            }

            //check product
            var product = Resolve<IProductService>().GetSingle(r => r.Id == model.ProductId);
            if (product == null)
            {
                return ServiceResult.FailedWithMessage("活动商品不存在");
            }
            if (product.ProductActivityExtension == null)
            {
                product.ProductActivityExtension = new ProductActivityExtension();
            }
            //check product repeat
            if (product.ProductActivityExtension.Activitys.FirstOrDefault(r => r.Key == activity.Key) != null
                && product.Id != model.ProductId)
            {
                return ServiceResult.FailedWithMessage("该商品已存在同类型的活动，不能重复添加");
            }
            activity.Name = product.Name;

            //transaction
            var context = Repository<IActivityRepository>().RepositoryContext;
            try
            {
                context.BeginTransaction();

                //save activity
                if (activity.Id <= 0)
                {
                    activity.Status = ActivityStatus.HasNotStarted;
                    Resolve<IActivityService>().Add(activity);
                }
                else
                {
                    Resolve<IActivityService>().UpdateNoTracking(activity);
                }

                // add activity to product for group by activity.
                if (activity.Key == typeof(GroupBuyActivity).FullName)
                {
                    product.ProductActivityExtension.IsGroupBuy = true;
                }

                //modify product
                var productActivity = AutoMapping.SetValue<ProductActivity>(activity);
                //productActivity.Value = result.ReturnObject;
                var productFindActivity = product.ProductActivityExtension.Activitys.FirstOrDefault(r => r.Key == model.Activity.Key);
                if (productFindActivity == null)
                {
                    product.ProductActivityExtension.Activitys.Add(productActivity);
                }
                else
                {
                    product.ProductActivityExtension.Activitys.Remove(productFindActivity);
                    product.ProductActivityExtension.Activitys.Add(productActivity);
                }
                Resolve<IProductService>().Update(r => { r.Activity = product.ProductActivityExtension.ToJson(); }, r => r.Id == product.Id);

                context.SaveChanges();
                context.CommitTransaction();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                context.RollbackTransaction();
                return ServiceResult.FailedWithMessage("数据保存失败");
            }
            finally
            {
                context.DisposeTransaction();
            }

            return result;
        }

        /// <summary>
        /// set model value
        /// </summary>
        private ServiceResult SetModelValue(ActivityEditOutput model)
        {
            //check type
            var activity = model.Activity;
            var type = activity.Key.GetTypeByFullName();
            if (type == null)
            {
                return ServiceResult.FailedWithMessage("key不能为空，活动类型不存在");
            }
            var instance = Activator.CreateInstance(type);
            if (!(instance is IActivity))
            {
                return ServiceResult.FailedWithMessage("该类型不属于活动实体");
            }
            var activityEntityInstance = instance as IActivity;

            //set value
            var ruleResult = activityEntityInstance.SetValueOfRule(model.ActivityRules);
            if (!ruleResult.Succeeded)
            {
                return ServiceResult.FailedWithMessage(ruleResult.ErrorMessages);
            }
            model.ActivityRules = ruleResult.ReturnObject == null
                ? model.ActivityRules.ToObject(type)
                : ruleResult.ReturnObject;

            //user range
            if (model.UserRange == UserRange.AllUser)
            {
                activity.LimitGradeId = Guid.Empty;
            }
            //time range
            var timeRange = model.DateTimeRange.ToSplitList("/");
            if (timeRange == null || timeRange.Count != 2)
            {
                return ServiceResult.FailedWithMessage("活动时间范围填写出错");
            }
            activity.StartTime = timeRange[0].ConvertToDateTime();
            activity.EndTime = timeRange[1].ConvertToDateTime();
            if (activity.StartTime.Year < 1970 || activity.EndTime.Year < 1970)
            {
                return ServiceResult.FailedWithMessage("活动时间范围填写出错");
            }
            if (DateTime.Compare(activity.StartTime, activity.EndTime) > 0)
            {
                return ServiceResult.FailedWithMessage("活动结束时间需在活动开始时间之后");
            }
            //support single product
            var attribute = Resolve<IActivityAdminService>().GetActivityModuleAttribute(activity.Key);
            if (attribute != null && attribute.IsSupportSigleProduct)
            {
                if (model.ProductId > 0)
                {
                    activity.ActivityExtension.ProductIds.Add(model.ProductId);
                }
            }
            activity.ProductId = model.ProductId;
            activity.Extension = activity.ActivityExtension.ToJson();
            activity.Value = model.ActivityRules.ToJson();

            return ServiceResult.Success;
        }

        #region activity

        /// <summary>
        /// get current user price
        /// </summary>
        /// <param name="storeItems"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<ProductGradePrice> GetMemberDiscountPrice(List<StoreItem> storeItems, User user)
        {
            //get product ids
            var productGrades = new List<ProductGradePrice>();
            var productIds = new List<long>();
            storeItems.ForEach(store =>
            {
                var tempProductIds = store.ProductSkuItems.Select(p => p.ProductId);
                productIds.AddRange(tempProductIds);
            });
            productIds = productIds.Distinct().ToList();
            if (productIds.Count <= 0)
            {
                return productGrades;
            }

            //get all activities
            var currentOrderActivities = Resolve<IActivityApiService>().GetList(ProductActivityType.MemberDiscount, productIds).ToList();
            if (currentOrderActivities.Count <= 0)
            {
                return productGrades;
            }
            //loop
            currentOrderActivities.ForEach(item =>
            {
                //rule
                var rules = item.Value.ToObject<MemberDiscountActivity>();
                if (rules == null || rules.DiscountList == null || rules.DiscountList.Count <= 0)
                {
                    return;
                }
                rules.DiscountList.ForEach(discount =>
                {
                    if (discount.GradeItems != null && discount.GradeItems.Count > 0)
                    {
                        var gradeItem = discount.GradeItems.Find(g => g.Id == user.GradeId);
                        if (gradeItem != null && gradeItem.Price > 0)
                        {
                            productGrades.Add(new ProductGradePrice
                            {
                                GradeId = gradeItem.Id,
                                ProductId = item.ProductId,
                                ProductSkuId = discount.ProductSkuId,
                                MemberPrice = gradeItem.Price
                            });
                        }
                    }
                });
            });
            return productGrades;
        }

        /// <summary>
        /// check buy permission activity
        /// </summary>
        /// <param name="storeOrders"></param>
        /// <param name="user"></param>
        public ServiceResult CheckBuyPermissionActivity(IList<StoreOrderItem> storeOrders, User user)
        {
            var result = ServiceResult.Success;
            //get order products
            var orderProductCounts = GetOrderProducts(storeOrders);
            var orderProductIds = orderProductCounts.Select(o => o.ProductId).ToList();
            //get all activities
            var currentOrderActivities = Resolve<IActivityApiService>().GetList(ProductActivityType.BuyPermission, orderProductIds).ToList();
            if (currentOrderActivities.Count <= 0)
            {
                return result;
            }
            var userProductCounts = Repository<IOrderProductRepository>().GetUserProductCount(user.Id, orderProductIds);
            //loop proccess acitity
            foreach (var item in currentOrderActivities)
            {
                //rule
                var rules = item.Value.ToObject<BuyPermisionActivity>();
                if (rules == null)
                {
                    continue;
                }
                //order product count
                var order = orderProductCounts.Find(p => p.ProductId == item.ProductId);
                if (order == null)
                {
                    continue;
                }
                //buy count
                if ((rules.SingleBuyCountMin > 0 && order.Count < rules.SingleBuyCountMin)
                    || (rules.SingleBuyCountMax > 0 && order.Count > rules.SingleBuyCountMax))
                {
                    return ServiceResult.FailedWithMessage($"商品{order.ProductId}，最小购买数量为{rules.SingleBuyCountMin}件，最大购买数量为{rules.SingleBuyCountMax}件");
                }
                //get current product buy count
                var userProductCount = userProductCounts.Find(u => u.ProductId == order.ProductId);
                var buyCount = userProductCount == null ? order.Count : (order.Count + userProductCount.Count);
                if (rules.TotalBuyCountMax > 0 && buyCount > rules.TotalBuyCountMax)
                {
                    return ServiceResult.FailedWithMessage($"您购买的数量超过了活动限购的数量，该商品限购{rules.TotalBuyCountMax}件");
                }
                //member level permission
                if (rules.MemberLeverBuyPermissions?.Count > 0 && !rules.MemberLeverBuyPermissions.Contains(user.GradeId))
                {
                    return ServiceResult.FailedWithMessage("您的会员等级不可以购买此等级限购商品，请升级后再试");
                }
            }
            return result;
        }

        /// <summary>
        /// check presell activity
        /// </summary>
        /// <param name="storeOrders"></param>
        public ServiceResult CheckPreSellActivity(IList<StoreOrderItem> storeOrders)
        {
            return CheckActivity(storeOrders, ProductActivityType.PreSells, item =>
            {
                //rule
                var rules = item.Value.ToObject<PreSellsActivity>();
                if (rules == null)
                {
                    return ServiceResult.Success;
                }
                var currentTime = DateTime.Now;
                if (rules.PreSellStartTime > currentTime || rules.PreSellEndTime < currentTime)
                {
                    return ServiceResult.FailedWithMessage($"商品{item.ProductId}，该商品不在预售时间范围内");
                }
                return ServiceResult.Success;
            });
        }

        /// <summary>
        /// check time limit buy activity
        /// </summary>
        /// <param name="storeOrders"></param>
        public ServiceResult CheckTimeLimitBuyActivity(IList<StoreOrderItem> storeOrders)
        {
            //get order products
            var orderProductCounts = GetOrderProducts(storeOrders);
            var orderProductIds = orderProductCounts.Select(o => o.ProductId).ToList();
            var productCounts = Repository<IOrderProductRepository>().GetProductCount(orderProductIds);
            var activities = new List<Activity>();
            var result = CheckActivity(storeOrders, ProductActivityType.TimeLimitBuy, item =>
            {
                //rule
                var rules = item.Value.ToObject<TimeLimitBuyActivity>();
                if (rules == null)
                {
                    return ServiceResult.Success;
                }
                var order = orderProductCounts.Find(o => o.ProductId == item.ProductId);
                if (order == null)
                {
                    return ServiceResult.Success;
                }

                var currentTime = DateTime.Now;
                if (rules.StartTime > currentTime || rules.EndTime < currentTime)
                {
                    return ServiceResult.FailedWithMessage($"商品{item.ProductId}，该商品不在限时购销售范围内");
                }
                var productCount = productCounts.Find(p => p.ProductId == item.ProductId);
                var totalCount = (productCount == null ? 0 : productCount.Count) + order.Count;
                if (item.MaxStock > 0 && totalCount > item.MaxStock)
                {
                    return ServiceResult.FailedWithMessage($"商品{item.ProductId}，该抢购商品库存不足");
                }
                item.UsedStock += order.Count;
                activities.Add(item);

                return ServiceResult.Success;
            });
            result.ReturnObject = activities;

            return result;
        }

        /// <summary>
        /// check activty code template
        /// </summary>
        /// <param name="storeOrders"></param>
        /// <param name="productActivityType"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        private ServiceResult CheckActivity(IList<StoreOrderItem> storeOrders, ProductActivityType productActivityType, Func<Activity, ServiceResult> func)
        {
            //get order products
            var orderProductCounts = GetOrderProducts(storeOrders);
            var orderProductIds = orderProductCounts.Select(o => o.ProductId).ToList();
            //get all activities
            var currentOrderActivities = Resolve<IActivityApiService>().GetList(productActivityType, orderProductIds).ToList();
            if (currentOrderActivities.Count <= 0)
            {
                return ServiceResult.Success;
            }
            //loop proccess acitity
            foreach (var item in currentOrderActivities)
            {
                var result = func(item);
                if (!result.Succeeded)
                {
                    return result;
                }
            }
            return ServiceResult.Success;
        }

        /// <summary>
        /// get order products
        /// </summary>
        /// <param name="storeOrders"></param>
        /// <returns></returns>
        private List<UserProductCount> GetOrderProducts(IList<StoreOrderItem> storeOrders)
        {
            var result = new List<UserProductCount>();
            storeOrders.Foreach(order =>
            {
                var temp = order.ProductSkuItems.Select(p => new UserProductCount
                {
                    Count = p.Count,
                    ProductId = p.ProductId
                }).ToList();
                result.AddRange(temp);
            });
            return result;
        }

        #endregion
    }
}