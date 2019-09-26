using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.Tasks.Extensions;
using Alabo.App.Core.Tasks.ResultModel;
using Alabo.App.Open.Tasks.Base;
using Alabo.App.Open.Tasks.Modules;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Users.Dtos;
using ZKCloud.Open.ApiBase.Models;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Open.Tasks.Configs.TeamRange {

    /// <summary>
    /// Class TeamRangeRateConfig.
    /// </summary>
    public class TeamRangeRateConfig : ShareBaseConfig {

        /// <summary>
        /// Gets or sets the team range rate json.
        /// 团队极差
        /// </summary>
        [Field(ControlsType = ControlsType.Json, PlaceHolder = "请按会员等级升级点值的权重来设置，原则上等级越高，比例越高", ListShow = false, EditShow = true, ExtensionJson = "TeamRangeRateItems")]
        [JsonIgnore]
        public string TeamRangeRateJson { get; set; }

        /// <summary>
        /// Gets or sets the team range rate items.
        /// </summary>
        public IList<TeamRangeRateItem> TeamRangeRateItems { get; set; } = new List<TeamRangeRateItem>();

        private static IList<TeamRangeRateItem> GetDefaultItems() {
            var gradeService = Alabo.Helpers.Ioc.Resolve<IGradeService>();
            var userGrades = gradeService.GetUserGradeList();
            var list = new List<TeamRangeRateItem>();
            foreach (var item in userGrades) {
                var teamRangeRate = new TeamRangeRateItem {
                    GradeId = item.Id
                };
                list.Add(teamRangeRate);
            }
            return list;
        }
    }

    /// <summary>
    /// Class TeamRangeRateModule.
    /// </summary>
    [TaskModule("23B9A703-6B42-4915-816F-D1208CC7C00B", "团队级差收益(按比例)", SortOrder = 999999, ConfigurationType = typeof(TeamRangeRateConfig), IsSupportMultipleConfiguration = true,
         FenRunResultType = Core.Tasks.Domain.Enums.FenRunResultType.Price, IsSupportSetDistriRatio = false,
         Intro = "不同的等级之间提成比例会有所不同，比如总监A的提成比例为30%，经理B的提成比例为20%,业务员的提成比例为10%。那么经理可以拿到业务员的级差为10%，总监可以拿到经理的级差为10%，业务员的级差为20%",
         RelationshipType = RelationshipType.UserRecommendedRelationship)]
    public class TeamRangeRateModule : AssetAllocationShareModuleBase<TeamRangeRateConfig> {

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamRangeRateModule"/> class.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="config">The configuration.</param>
        public TeamRangeRateModule(TaskContext context, TeamRangeRateConfig config)
            : base(context, config) {
        }

        /// <summary>
        /// 对module配置与参数进行基础验证，子类重写后需要显式调用并判定返回值，如返回值不为Success，则不再执行子类后续逻辑
        /// </summary>
        /// <param name="parameter">参数</param>
        public override ExecuteResult<ITaskResult[]> Execute(TaskParameter parameter) {
            var baseResult = base.Execute(parameter);
            if (baseResult.Status != ResultStatus.Success) {
                return ExecuteResult<ITaskResult[]>.Cancel("未找到触发会员的Parent Map.");
            }

            var userMap = Resolve<IUserMapService>().GetParentMapFromCache(base.ShareOrderUser.Id);
            var map = userMap.ParentMap.DeserializeJson<List<ParentMap>>();
            if (map == null) {
                return ExecuteResult<ITaskResult[]>.Cancel("未找到触发会员的Parent Map.");
            }
            IList<ITaskResult> resultList = new List<ITaskResult>();
            // 获取最大极差比例
            var maxRate = Configuration.TeamRangeRateItems.Max(r => r.MaxRate);
            for (var i = 0; i < base.TeamLevel; i++) {
                if (map.Count < i + 1) {
                    break;
                }
                var item = map[i];
                base.GetShareUser(item.UserId, out var shareUser);//从基类获取分润用户
                var shareGrade = Resolve<IGradeService>().GetGrade(shareUser.GradeId);
                if (shareGrade == null) {
                    continue;
                }
                var userRule = Configuration.TeamRangeRateItems.FirstOrDefault(r => r.GradeId == shareGrade.Id);
                if (userRule == null) {
                    continue;
                }
                //当前分润用户最大极差
                var shareUserRate = userRule.MaxRate;
                //剩余极差比例
                var ratio = maxRate - shareUserRate;
                if (ratio <= 0) {
                    continue;
                }
                var shareAmount = ratio * BaseFenRunAmount; // 极差分润
                CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration, resultList);
            }
            //分期

            return ExecuteResult<ITaskResult[]>.Success(resultList.ToArray());
        }
    }

    /// <summary>
    /// Class TeamRangeRuleItem.
    /// 团队极差
    /// </summary>
    [ClassProperty(Name = "团队极差比例设置")]
    public class TeamRangeRateItem {

        /// <summary>
        /// 等级Id
        /// 等级权重，按照会员等级升级点来设置，升级点越高权重越高
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true, EditShow = true, DataSource = "UserGradeConfig", SortOrder = 1)]
        [Display(Name = "等级权重")]
        public Guid GradeId { get; set; }

        /// <summary>
        /// 极差比例
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, SortOrder = 2)]
        [Display(Name = "极差比例")]
        public decimal MaxRate { get; set; } = 0.0m;
    }
}