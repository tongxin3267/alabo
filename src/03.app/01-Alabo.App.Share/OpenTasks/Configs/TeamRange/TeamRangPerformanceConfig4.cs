using System.Collections.Generic;
using System.Linq;
using Alabo.App.Share.OpenTasks.Base;
using Alabo.App.Share.OpenTasks.Modules;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Data.Things.Orders.Extensions;
using Alabo.Data.Things.Orders.ResultModel;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Basic.Grades.Domain.Services;
using Alabo.Framework.Tasks.Queues.Models;
using Alabo.Framework.Tasks.Schedules.Domain.Enums;
using Alabo.Helpers;
using Alabo.Users.Dtos;
using Alabo.Web.Mvc.Attributes;
using Newtonsoft.Json;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Share.OpenTasks.Configs.TeamRange
{
    /// <summary>
    ///     Class TeamRangPerformanceConfig4.
    /// </summary>
    public class TeamRangPerformanceConfig4 : ShareBaseConfig
    {
        /// <summary>
        ///     Gets or sets the team range rate json.
        ///     团队极差
        /// </summary>
        [Field(ControlsType = ControlsType.Json, PlaceHolder = "请按会员等级升级点值的权重来设置，原则上等级越高，比例越高", ListShow = false,
            EditShow = true, ExtensionJson = "TeamRangeRateItems")]
        [JsonIgnore]
        public string TeamRangeRateJson { get; set; }

        /// <summary>
        ///     Gets or sets the team range rate items.
        /// </summary>
        public IList<TeamRangeRateItem> TeamRangeRateItems { get; set; } = new List<TeamRangeRateItem>();

        private static IList<TeamRangeRateItem> GetDefaultItems()
        {
            var gradeService = Ioc.Resolve<IGradeService>();
            var userGrades = gradeService.GetUserGradeList();
            var list = new List<TeamRangeRateItem>();
            foreach (var item in userGrades)
            {
                var TeamRangPerformance = new TeamRangeRateItem
                {
                    GradeId = item.Id
                };
                list.Add(TeamRangPerformance);
            }

            return list;
        }
    }

    /// <summary>
    ///     Class TeamRangeRateModule.
    /// </summary>
    [TaskModule("23B9A703-6B42-4005-800F-D1208905000d", "团队无级差绩效(二)", SortOrder = 999999,
        ConfigurationType = typeof(TeamRangPerformanceConfig4), IsSupportMultipleConfiguration = true,
        FenRunResultType = FenRunResultType.Price, IsSupportSetDistriRatio = false,
        Intro =
            "注意和团队无级差绩效(一)区别：自己拿自己的绩效，自己的绩效被自己拿！不同的等级之间绩效奖金会有所不同，比如总监A的绩效奖金为30%，经理B的绩效奖金为20%,业务员的绩效奖金为10%。那么经理可以拿到业务员的级差为10%，总监可以拿到经理的级差为10%，业务员的级差为20%",
        RelationshipType = RelationshipType.UserRecommendedRelationship)]
    public class TeamRangPerformance4Module : AssetAllocationShareModuleBase<TeamRangPerformanceConfig4>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TeamRangeRateModule" /> class.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="config">The configuration.</param>
        public TeamRangPerformance4Module(TaskContext context, TeamRangPerformanceConfig4 config)
            : base(context, config)
        {
        }

        /// <summary>
        ///     对module配置与参数进行基础验证，子类重写后需要显式调用并判定返回值，如返回值不为Success，则不再执行子类后续逻辑
        /// </summary>
        /// <param name="parameter">参数</param>
        public override ExecuteResult<ITaskResult[]> Execute(TaskParameter parameter)
        {
            var baseResult = base.Execute(parameter);
            if (baseResult.Status != ResultStatus.Success)
                return ExecuteResult<ITaskResult[]>.Cancel("未找到触发会员的Parent Map.");

            var userMap = Resolve<IUserMapService>().GetParentMapFromCache(ShareOrderUser.Id);
            var map = userMap.ParentMap.DeserializeJson<List<ParentMap>>();
            if (map == null) return ExecuteResult<ITaskResult[]>.Cancel("未找到触发会员的Parent Map.");
            IList<ITaskResult> resultList = new List<ITaskResult>();
            // 获取最大极差比例
            var maxRatio = Configuration.TeamRangeRateItems.Max(r => r.MaxRate);
            // 将自己加上去
            var parentMap = new ParentMap
            {
                UserId = ShareOrderUser.Id,
                ParentLevel = 0
            };
            map = map.AddBefore(parentMap).ToList();
            //已用过的极差比例
            var useRatio = 0.0m;

            for (var i = 0; i < TeamLevel; i++)
            {
                if (useRatio >= maxRatio) break; // 已使用的比例超过最大比例
                if (map.Count < i + 1) break;
                var item = map[i];
                base.GetShareUser(item.UserId, out var shareUser); //从基类获取分润用户
                var shareGrade = Resolve<IGradeService>().GetGrade(shareUser.GradeId);
                if (shareGrade == null) continue;
                var userRule = Configuration.TeamRangeRateItems.FirstOrDefault(r => r.GradeId == shareGrade.Id);
                if (userRule == null) continue;

                //当前分润用户最大极差
                var shareUserRate = userRule.MaxRate;

                //剩余极差比例=当前极差比例-已使用比例
                var ratio = shareUserRate - useRatio;
                if (ratio <= 0) continue;
                useRatio += ratio;
                var shareAmount = ratio * BaseFenRunAmount; // 极差分润
                CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration, resultList);
                if (shareUserRate > 0) break;
            }
            //分期

            return ExecuteResult<ITaskResult[]>.Success(resultList.ToArray());
        }
    }
}