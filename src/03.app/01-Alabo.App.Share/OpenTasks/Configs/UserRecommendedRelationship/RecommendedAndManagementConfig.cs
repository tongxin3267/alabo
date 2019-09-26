using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Share.OpenTasks.Base;
using Alabo.App.Share.OpenTasks.Modules;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Data.Things.Orders.Extensions;
using Alabo.Data.Things.Orders.ResultModel;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Tasks.Queues.Models;
using Alabo.Framework.Tasks.Schedules.Domain.Enums;
using Alabo.Users.Dtos;
using Alabo.Web.Mvc.Attributes;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Share.OpenTasks.Configs.UserRecommendedRelationship {

    /// <summary>
    /// 直推与管理收益
    /// </summary>
    /// <seealso cref="ShareBaseConfig" />
    [ClassProperty]
    public class RecommendedAndManagementConfig : ShareBaseConfig {

        /// <summary>
        /// 团队层数
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, SortOrder = 1)]
        [Display(Name = "团队层数", Description = "团队层数")]
        public long TeamLevel { get; set; } = 100;

        /// <summary>
        /// 分润比例
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, SortOrder = 1)]
        [Display(Name = "管理收益比例", Description = "管理收益比例,0.5表示50%")]
        public decimal ManagerRatio { get; set; } = 0.5m;
    }

    /// <summary>
    /// Class NLevelDistributionModule.
    /// </summary>
    [TaskModule(Id, ModelName, SortOrder = 999999, ConfigurationType = typeof(RecommendedAndManagementConfig), IsSupportMultipleConfiguration = true,
      FenRunResultType = FenRunResultType.Price,
      Intro = "直推与管理收益,比如A推荐了B,B推荐了C。C消费，B获得直推奖，A获得B直推奖的管理奖",
      RelationshipType = RelationshipType.UserRecommendedRelationship)]
    public class RecommendedAndManagementModule : AssetAllocationShareModuleBase<RecommendedAndManagementConfig> {

        /// <summary>
        /// The identifier
        /// </summary>
        public const string Id = "BD717F0D-AD00-4009-9005-597E0AE55000";

        /// <summary>
        /// The model name
        /// </summary>
        public const string ModelName = "直推与管理收益";

        //
        /// <summary>
        /// Initializes a new instance of the <see cref="NLevelDistributionModule"/> class.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="config">The configuration.</param>
        public RecommendedAndManagementModule(TaskContext context, RecommendedAndManagementConfig config)
            : base(context, config) {
        }

        /// <summary>
        /// 开始执行分润
        /// 对module配置与参数进行基础验证，子类重写后需要显式调用并判定返回值，如返回值不为Success，则不再执行子类后续逻辑
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>ExecuteResult&lt;ITaskResult[]&gt;.</returns>
        public override ExecuteResult<ITaskResult[]> Execute(TaskParameter parameter) {
            var baseResult = base.Execute(parameter);
            if (baseResult.Status != ResultStatus.Success) {
                return baseResult;
            }

            IList<ITaskResult> resultList = new List<ITaskResult>();

            // 开始计算直推奖
            //当前下单用户
            var user = Resolve<IUserService>().GetSingle(ShareOrder.UserId);
            base.GetShareUser(user.ParentId, out var shareUser);//从基类获取分润用户
            if (shareUser == null) {
                return ExecuteResult<ITaskResult[]>.Cancel("推荐用户不存在");
            }
            var ratio = Convert.ToDecimal(Ratios[0]);
            var shareAmount = BaseFenRunAmount * ratio;//分润金额
            CreateResultList(shareAmount, base.ShareOrderUser, shareUser, parameter, Configuration, resultList);//构建分润参数

            // 开始计算管理分红
            var userMap = Resolve<IUserMapService>().GetParentMapFromCache(shareUser.Id);
            var map = userMap.ParentMap.DeserializeJson<List<ParentMap>>();
            if (map == null) {
                return ExecuteResult<ITaskResult[]>.Cancel("未找到触发会员的Parent Map.");
            }

            for (var i = 0; i < map.Count; i++) {
                // 如果大于团队层数
                if (i + 1 > Configuration.TeamLevel) {
                    break;
                }
                var item = map[i];
                GetShareUser(item.UserId, out shareUser);//从基类获取分润用户
                if (shareUser == null) {
                    continue;
                }
                // 每上一级50%
                var itemRatio = Math.Pow(Convert.ToDouble(Configuration.ManagerRatio), Convert.ToDouble(i + 1)).ToDecimal() * ratio;
                if (itemRatio <= 0) {
                    continue;
                }

                shareAmount = BaseFenRunAmount * itemRatio;//分润金额

                CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration, resultList);//构建分润参数
            }
            return ExecuteResult<ITaskResult[]>.Success(resultList.ToArray());
        }
    }
}