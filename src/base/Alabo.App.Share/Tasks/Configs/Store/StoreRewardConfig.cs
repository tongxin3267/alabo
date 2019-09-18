using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Tasks.Extensions;
using Alabo.App.Core.Tasks.ResultModel;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Share.Tasks.Base;
using Alabo.App.Share.Tasks.Modules;
using Alabo.App.Shop.Order.Domain.Services;
using Alabo.App.Shop.Store.Domain.Services;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Share.Tasks.Configs.Store {

    /// <summary>
    /// Class StoreRewardConfig.
    /// 自身返利
    /// </summary>
    /// <seealso cref="Alabo.App.Share.Tasks.Base.ShareBaseConfig" />
    public class StoreRewardConfig : ShareBaseConfig
    {
    }

    /// <summary>
    /// Class RebateModul.
    /// </summary>
    [TaskModule(Id, "推荐供应商分润",
        ConfigurationType = typeof(StoreRewardConfig), SortOrder = 999990,
        IsSupportMultipleConfiguration = true, FenRunResultType = Core.Tasks.Domain.Enums.FenRunResultType.Price,
        RelationshipType = Core.Tasks.Domain.Enums.RelationshipType.UserRecommendedRelationship,
        Intro = "推荐供应商时分润")]
    public class StoreRewardConfigModule : AssetAllocationShareModuleBase<StoreRewardConfig>
    {
        public const string Id = "0734225B-08C0-4009-9B66-530DB887B200";

        public const string ModuleName = "供应商分润";

        public StoreRewardConfigModule(TaskContext context, StoreRewardConfig config)
            : base(context, config)
        {
        }

        public override ExecuteResult<ITaskResult[]> Execute(TaskParameter parameter)
        {
            var baseResult = base.Execute(parameter);
            if (baseResult.Status != ResultStatus.Success)
            {
                return baseResult;
            }

            // 同时下多个供应商订单的时候，分开
            var order = Resolve<IOrderService>().GetSingle(r => r.Id == OrderId);
            if (order == null)
            {
                return ExecuteResult<ITaskResult[]>.Cancel("订单不存在");
            }

            var store = Resolve<IStoreService>().GetSingle(r => r.Id == order.StoreId);
            if (store == null)
            {
                return ExecuteResult<ITaskResult[]>.Cancel("店铺不存在");
            }

            var storeUser = Resolve<IUserService>().GetNomarlUser(store.UserId);
            if (storeUser == null)
            {
                return ExecuteResult<ITaskResult[]>.Cancel("店铺用户不存在");
            }
            var storeParentUser = Resolve<IUserService>().GetNomarlUser(storeUser.ParentId);
            if (storeParentUser == null)
            {
                return ExecuteResult<ITaskResult[]>.Cancel("店铺推荐用户不存在");
            }
            IList<ITaskResult> resultList = new List<ITaskResult>();

            decimal shareAmount = 0;
            base.GetShareUser(storeParentUser.Id, out var shareUser); //从基类获取分润用户

            var ratio = Convert.ToDecimal(Ratios[0]);
            shareAmount = base.BaseFenRunAmount * ratio; //分润金额
            CreateResultList(shareAmount, base.ShareOrderUser, shareUser, parameter, Configuration, resultList);

            return ExecuteResult<ITaskResult[]>.Success(resultList.ToArray());
        }
    }
}