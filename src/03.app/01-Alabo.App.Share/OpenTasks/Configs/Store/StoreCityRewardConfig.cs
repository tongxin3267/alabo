using Alabo.App.Core.Tasks.Extensions;
using Alabo.App.Core.Tasks.ResultModel;
using Alabo.App.Open.Tasks.Base;
using Alabo.App.Open.Tasks.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Open.Tasks.Configs.Store {

    /// <summary>
    /// Class StoreCityRewardConfig.
    /// 自身返利
    /// </summary>
    /// <seealso cref="Alabo.App.Open.Tasks.Base.ShareBaseConfig" />
    public class StoreCityRewardConfig : ShareBaseConfig {
    }

    /// <summary>
    /// Class RebateModul.
    /// </summary>
    [TaskModule(Id, "城市供应商分润",
        ConfigurationType = typeof(StoreCityRewardConfig), SortOrder = 999990,
        IsSupportMultipleConfiguration = true, FenRunResultType = Core.Tasks.Domain.Enums.FenRunResultType.Price,
        RelationshipType = Core.Tasks.Domain.Enums.RelationshipType.UserRecommendedRelationship,
        Intro = "城市供应商分润")]
    public class StoreCityRewardConfigModule : AssetAllocationShareModuleBase<StoreCityRewardConfig> {
        public const string Id = "0734225B-0000-4009-9B66-530DB887B200";

        public const string ModuleName = "供应商分润";

        public StoreCityRewardConfigModule(TaskContext context, StoreCityRewardConfig config)
            : base(context, config) {
        }

        public override ExecuteResult<ITaskResult[]> Execute(TaskParameter parameter) {
            var baseResult = base.Execute(parameter);
            if (baseResult.Status != ResultStatus.Success) {
                return baseResult;
            }

            //TODO 2019年9月24日 供应商订单重构
            // 同时下多个供应商订单的时候，分开
            //var order = Resolve<IOrderService>().GetSingle(r => r.Id == OrderId);
            //if (order == null) {
            //    return ExecuteResult<ITaskResult[]>.Cancel("订单不存在");
            //}

            //var store = Resolve<IStoreService>().GetSingle(r => r.Id == order.StoreId);
            //if (store == null) {
            //    return ExecuteResult<ITaskResult[]>.Cancel("店铺不存在");
            //}

            //var storeUser = Resolve<IUserService>().GetNomarlUser(store.UserId);
            //if (storeUser == null) {
            //    return ExecuteResult<ITaskResult[]>.Cancel("店铺用户不存在");
            //}
            //var storeParentUser = Resolve<IUserService>().GetNomarlUser(storeUser.ParentId);
            //if (storeParentUser == null) {
            //    return ExecuteResult<ITaskResult[]>.Cancel("店铺推荐用户不存在");
            //}
            //IList<ITaskResult> resultList = new List<ITaskResult>();

            //decimal shareAmount = 0;
            //base.GetShareUser(storeParentUser.Id, out var shareUser); //从基类获取分润用户

            //var ratio = Convert.ToDecimal(Ratios[0]);
            //shareAmount = base.BaseFenRunAmount * ratio; //分润金额
            //CreateResultList(shareAmount, base.ShareOrderUser, shareUser, parameter, Configuration, resultList);

            // return ExecuteResult<ITaskResult[]>.Success(resultList.ToArray());
            return null;
        }
    }
}