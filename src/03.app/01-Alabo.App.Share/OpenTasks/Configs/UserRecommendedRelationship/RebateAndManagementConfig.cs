using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Core.Tasks.Extensions;
using Alabo.App.Core.Tasks.ResultModel;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Open.Tasks.Base;
using Alabo.App.Open.Tasks.Modules;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Users.Dtos;
using ZKCloud.Open.ApiBase.Models;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Open.Tasks.Configs.UserRecommendedRelationship {

    /// <summary>
    /// Class DebtRebateConfig.
    /// 自身返利与管理收益
    /// </summary>
    /// <seealso cref="Alabo.App.Open.Tasks.Base.ShareBaseConfig" />
    public class RebateAndManagementConfig : ShareBaseConfig {

        /// <summary>
        /// 团队层数
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, SortOrder = 1)]
        [Display(Name = "团队层数", Description = "团队层数")]
        public long TeamLevel { get; set; } = 1;

        /// <summary>
        /// 分润比例
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, SortOrder = 1)]
        [Display(Name = "管理分红比例", Description = "管理分红比例,0.5表示50%")]
        public decimal ManagerRatio { get; set; } = 0.5m;
    }

    /// <summary>
    /// Class RebateModul.
    /// </summary>
    [TaskModule(Id, "自身返利与管理收益",
        ConfigurationType = typeof(RebateAndManagementConfig), SortOrder = 0,
        IsSupportMultipleConfiguration = true, FenRunResultType = Core.Tasks.Domain.Enums.FenRunResultType.Price,
        RelationshipType = Core.Tasks.Domain.Enums.RelationshipType.UserRecommendedRelationship,
        Intro = "自身返利与管理收益,比如A推荐了B,B推荐了C。C消费,C获得自身返利，A、B获得管理收益")]
    public class RebateAndManagementConfigModule : AssetAllocationShareModuleBase<RebateAndManagementConfig> {

        /// <summary>
        /// The identifier
        /// </summary>
        public const string Id = "0734225B-08C0-4009-9B66-530DB7770200";

        /// <summary>
        /// The module name
        /// </summary>
        public const string ModuleName = "自身返利与管理收益";

        /// <summary>
        /// Initializes a new instance of the
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="config">The configuration.</param>
        public RebateAndManagementConfigModule(TaskContext context, RebateAndManagementConfig config)
            : base(context, config) {
        }

        /// <summary>
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

            // 开始计算自身返利
            base.GetShareUser(ShareOrder.UserId, out var shareUser);//从基类获取分润用户
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