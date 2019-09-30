using Alabo.App.Share.OpenTasks.Base;
using Alabo.App.Share.OpenTasks.Modules;
using Alabo.Cloud.People.Relationship.Domain.CallBacks;
using Alabo.Cloud.People.Relationship.Domain.Services;
using Alabo.Data.Things.Orders.Extensions;
using Alabo.Data.Things.Orders.ResultModel;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Tasks.Queues.Models;
using Alabo.Framework.Tasks.Schedules.Domain.Enums;
using Alabo.Web.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Cloud.People.Relationship.Share
{
    /// <summary>
    ///     裂变分佣
    /// </summary>
    /// <seealso cref="ShareBaseConfig" />
    public class RelationshipShareConfig : ShareBaseConfig
    {
        /// <summary>
        ///     升级前等级名称
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, SortOrder = 1,
            DataSourceType = typeof(UserRelationshipIndexConfig), ListShow = true, Width = "150")]
        [Display(Name = "会员关系图")]
        public Guid ConfigId { get; set; }
    }

    /// <summary>
    ///     Class RelationshipShareModule.
    /// </summary>
    [TaskModule("BD717F8D-AD00-44C9-9005-59700AE55001", "会员关系网裂分佣", SortOrder = 999999,
        ConfigurationType = typeof(RelationshipShareConfig), IsSupportMultipleConfiguration = true,
        FenRunResultType = FenRunResultType.Price,
        Intro = "会员关系网裂分佣，最常用的分润维度，规矩会员关系网设置",
        RelationshipType = RelationshipType.UserRecommendedRelationship)]
    public class RelationshipShareModule : AssetAllocationShareModuleBase<RelationshipShareConfig>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RelationshipShareModule" /> class.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="config">The configuration.</param>
        public RelationshipShareModule(TaskContext context, RelationshipShareConfig config)
            : base(context, config)
        {
        }

        public override ExecuteResult<ITaskResult[]> Execute(TaskParameter parameter)
        {
            var baseResult = base.Execute(parameter);
            if (baseResult.Status != ResultStatus.Success)
                return ExecuteResult<ITaskResult[]>.Cancel("基础验证未通过" + baseResult.Message);

            IList<ITaskResult> resultList = new List<ITaskResult>();

            // 检查关系图
            var userRelationshipConfigs = Resolve<IAutoConfigService>().GetList<UserRelationshipIndexConfig>();
            var config = userRelationshipConfigs.FirstOrDefault(r => r.IsEnable && r.Id == Configuration.ConfigId);
            if (config == null) return ExecuteResult<ITaskResult[]>.Cancel("关系图配置不存在");

            // 查找分润订单用户的，关系图索引
            var userRelationshipIndex = Resolve<IRelationshipIndexService>()
                .GetSingle(r => r.ConfigId == config.Id && r.UserId == ShareOrder.UserId);
            if (userRelationshipIndex == null) return ExecuteResult<ITaskResult[]>.Cancel("触发用户关系图索引不存在");

            // 根据关系索引获取分润用户
            base.GetShareUser(userRelationshipIndex.ParentId, out var shareUser); //从基类获取分润用户
            if (shareUser == null) return ExecuteResult<ITaskResult[]>.Cancel("分润用户不符合条件或不存在");

            // 一级分润比例
            if (Ratios.Count > 0)
            {
                var ratio = Convert.ToDecimal(Ratios[0]);
                var shareAmount = BaseFenRunAmount * ratio; //分润金额
                CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration, resultList);

                // 二级分润比例
                if (Ratios.Count > 1)
                {
                    // 二级关系图索引
                    var user = shareUser;
                    userRelationshipIndex = Resolve<IRelationshipIndexService>()
                        .GetSingle(r => r.ConfigId == config.Id && r.UserId == user.Id);
                    if (userRelationshipIndex == null) return ExecuteResult<ITaskResult[]>.Cancel("二级用户关系图索引不存在");

                    // 根据关系索引获取二级分润用户
                    base.GetShareUser(userRelationshipIndex.ParentId, out shareUser);
                    if (shareUser == null) return ExecuteResult<ITaskResult[]>.Cancel("二级分润用户不符合条件或不存在");

                    ratio = Convert.ToDecimal(Ratios[1]);
                    shareAmount = BaseFenRunAmount * ratio;
                    CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration, resultList);
                }
            }

            return ExecuteResult<ITaskResult[]>.Success(resultList.ToArray());
        }
    }
}