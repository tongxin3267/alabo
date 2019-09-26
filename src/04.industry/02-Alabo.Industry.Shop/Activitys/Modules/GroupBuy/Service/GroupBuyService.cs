using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis.Dtos;
using Alabo.Framework.Core.WebApis.Service;
using Alabo.Industry.Shop.Activitys.Domain.Entities;
using Alabo.Industry.Shop.Activitys.Domain.Enum;
using Alabo.Industry.Shop.Activitys.Domain.Services;
using Alabo.Industry.Shop.Activitys.Modules.GroupBuy.Dtos;
using Alabo.Industry.Shop.Activitys.Modules.GroupBuy.Model;
using Alabo.Industry.Shop.Orders.Domain.Services;
using Alabo.Industry.Shop.Products.Domain.Services;
using Alabo.Industry.Shop.Products.Dtos;

namespace Alabo.Industry.Shop.Activitys.Modules.GroupBuy.Service
{
    /// <summary>
    ///     Class GroupBuyService.
    /// </summary>
    public class GroupBuyService : ServiceBase, IGroupBuyService
    {
        public GroupBuyService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        ///     获取s the group buy product records.
        ///     获取拼团商品记录
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        public Tuple<ServiceResult, IList<GroupBuyProductRecord>> GetGroupBuyProductRecords(long productId)
        {
            IList<GroupBuyProductRecord> productRecords = new List<GroupBuyProductRecord>();
            var product = Resolve<IProductService>().GetSingle(r => r.Id == productId);
            if (product == null) return Tuple.Create(ServiceResult.FailedWithMessage("商品不存在"), productRecords);

            var activityId = product.ProductActivityExtension?.Activitys
                ?.FirstOrDefault(r => r.Key == typeof(GroupBuyActivity).FullName)?.Id;
            var activity = Resolve<IActivityService>().GetSingle(r => r.Id == activityId);
            if (activity == null) return Tuple.Create(ServiceResult.FailedWithMessage("拼团活动不存在"), productRecords);

            var groupBuyActivity = activity.Value.ToObject<GroupBuyActivity>();
            //所有活动记录
            var activityRecoreds = Resolve<IActivityRecordService>()
                .GetList(r => r.ActivityId == activity.Id && r.Status == ActivityRecordStatus.IsPay);

            foreach (var item in activityRecoreds.Where(r => r.ParentId == 0))
            {
                var groupBuyProductRecord = new GroupBuyProductRecord
                {
                    ActivityRecordId = item.Id
                };
                var productRecordUser = new GroupBuyRecordUser
                {
                    UserId = item.ActivityRecordExtension.User.Id,
                    UserName = item.ActivityRecordExtension.User.UserName,
                    Avator = Resolve<IApiService>().ApiUserAvator(item.ActivityRecordExtension.User.Id),
                    IsFather = true
                };
                groupBuyProductRecord.Users.Add(productRecordUser);
                var childRecords = activityRecoreds.Where(r => r.ParentId == item.Id);
                foreach (var child in childRecords)
                {
                    productRecordUser = new GroupBuyRecordUser
                    {
                        UserId = child.ActivityRecordExtension.User.Id,
                        UserName = child.ActivityRecordExtension.User.UserName,
                        Avator = Resolve<IApiService>().ApiUserAvator(child.ActivityRecordExtension.User.Id),
                        IsFather = false
                    };
                    groupBuyProductRecord.Users.Add(productRecordUser);
                }

                groupBuyProductRecord.TotalCount = groupBuyActivity.BuyerCount; // 拼团总人数
                groupBuyProductRecord.RemainCount = groupBuyActivity.BuyerCount - activityRecoreds.Count(); // 剩余人数
                groupBuyProductRecord.RemainTime = (item.CreateTime.AddDays(1) - DateTime.Now).TotalSeconds; // 剩余时间
                groupBuyProductRecord.EndTime = activity.EndTime.ToString("yyyy-MM-dd HH:mm:ss");
                //剩余人数不为0
                if (groupBuyProductRecord.RemainCount > 0) productRecords.Add(groupBuyProductRecord);
            }

            return Tuple.Create(ServiceResult.Success, productRecords);
        }

        /// <summary>
        ///     获取s the grouy buy 会员 by order identifier.
        ///     根据订单Id，获取商品订单记录
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        public Tuple<ServiceResult, IList<GroupBuyRecordUser>> GetGrouyBuyUserByOrderId(long orderId)
        {
            IList<GroupBuyRecordUser> orderRecordUsers = new List<GroupBuyRecordUser>();
            var order = Resolve<IOrderService>().GetSingle(r => r.Id == orderId);
            if (order == null) return Tuple.Create(ServiceResult.FailedWithMessage("订单不存在"), orderRecordUsers);

            var orderRecord = Resolve<IActivityRecordService>().GetSingle(r => r.OrderId == order.Id);
            if (orderRecord == null)
                return Tuple.Create(ServiceResult.FailedWithMessage("该订单不是拼团订单"), orderRecordUsers);
            //获取拼团记录订单
            var activityRecords = new List<ActivityRecord>();
            if (orderRecord.ParentId == 0)
                activityRecords = Resolve<IActivityRecordService>()
                    .GetList(r => r.ParentId == orderRecord.Id || r.Id == orderRecord.Id).ToList();
            else
                activityRecords = Resolve<IActivityRecordService>()
                    .GetList(r => r.ParentId == orderRecord.ParentId || r.Id == orderRecord.ParentId).ToList();

            var users = Resolve<IUserService>()
                .GetList(e => activityRecords.Select(r => r.UserId).Distinct().Contains(e.Id));

            #region//暂时传时间  过后修改

            var activity = Resolve<IActivityService>().GetSingle(u => u.Id == orderRecord.ActivityId);
            var endTime = activity.EndTime.ToString("yyyy-MM-dd HH:mm:ss");
            var groupBuyActivity = activity.Value.ToObject<GroupBuyActivity>();

            #endregion

            activityRecords.ForEach(r =>
            {
                var user = users.FirstOrDefault(e => e.Id == r.UserId);
                if (user != null)
                {
                    var groupBuyRecordUser = new GroupBuyRecordUser
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        Avator = Resolve<IApiService>().ApiUserAvator(user.Id),
                        ActivityRecordId = orderRecord.Id,
                        IsFather = r.ParentId == 0 ? true : false,
                        EndTime = endTime,
                        RemainCount = groupBuyActivity.BuyerCount - activityRecords.Count()
                    };
                    orderRecordUsers.Add(groupBuyRecordUser);
                }
            });

            return Tuple.Create(ServiceResult.Success, orderRecordUsers);
        }

        /// <summary>
        ///     获取虚拟订货商品列表
        /// </summary>
        /// <param name="parameter">参数</param>
        public ProductItemApiOutput GetProductItems(ApiBaseInput parameter)
        {
            var productApiInput = new ProductApiInput
            {
                ProductIds = GetAllProductIds().JoinToString(",")
            };
            var result = Resolve<IProductService>().GetProductItems(productApiInput);
            return result;
        }

        /// <summary>
        ///     通过缓存获取所有的拼团商品Id
        /// </summary>
        public List<long> GetAllProductIds()
        {
            return ObjectCache.GetOrSet(() =>
            {
                var idList = new List<long>();
                var activitys = Resolve<IActivityService>().GetList(r => r.Key == typeof(GroupBuyActivity).FullName);
                foreach (var item in activitys)
                    if (!item.Value.IsNullOrEmpty())
                    {
                        var groupBuyActivity = item.Value.ToObject<GroupBuyActivity>();
                        if (groupBuyActivity != null)
                            idList.Add(groupBuyActivity.SkuProducts.FirstOrDefault().ProductId);
                    }

                return idList;
            }, "GetAllProductIds", TimeSpan.FromMinutes(10)).Value;
        }
    }
}