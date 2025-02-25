﻿using Alabo.App.Share.OpenTasks.Base;
using Alabo.App.Share.OpenTasks.Modules;
using Alabo.Data.Things.Orders.Extensions;
using Alabo.Data.Things.Orders.ResultModel;
using Alabo.Framework.Tasks.Queues.Models;
using Alabo.Framework.Tasks.Schedules.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Share.OpenTasks.Configs.UserRecommendedRelationship
{
    /// <summary>
    ///     Class RebateConfig.
    ///     自身返利
    /// </summary>
    /// <seealso cref="ShareBaseConfig" />
    public class RebateConfig : ShareBaseConfig
    {
    }

    /// <summary>
    ///     Class RebateModul.
    /// </summary>
    [TaskModule(Id, "自身返利",
        ConfigurationType = typeof(RebateConfig), SortOrder = 999990,
        IsSupportMultipleConfiguration = true, FenRunResultType = FenRunResultType.Price,
        RelationshipType = RelationshipType.UserRecommendedRelationship,
        Intro = "会员商城消费时候，返还自己资产，比如返还积分，返还现金。比如消费100元，返自己10积分，比如注册赠送100积分")]
    public class RebateModule : AssetAllocationShareModuleBase<RebateConfig>
    {
        public const string Id = "0734225B-08C0-4229-9B66-530DB777B29A";

        public const string ModuleName = "自身返利";

        public RebateModule(TaskContext context, RebateConfig config)
            : base(context, config)
        {
        }

        public override ExecuteResult<ITaskResult[]> Execute(TaskParameter parameter)
        {
            var baseResult = base.Execute(parameter);
            if (baseResult.Status != ResultStatus.Success) {
                return baseResult;
            }

            IList<ITaskResult> resultList = new List<ITaskResult>();

            decimal shareAmount = 0;
            base.GetShareUser(ShareOrder.UserId, out var shareUser); //从基类获取分润用户
            var ratio = Convert.ToDecimal(Ratios[0]);
            shareAmount = BaseFenRunAmount * ratio; //分润金额
            CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration, resultList);

            return ExecuteResult<ITaskResult[]>.Success(resultList.ToArray());
        }
    }
}