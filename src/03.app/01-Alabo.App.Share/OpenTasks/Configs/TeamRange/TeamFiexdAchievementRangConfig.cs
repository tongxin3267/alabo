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
using Alabo.Helpers;
using Alabo.Users.Dtos;
using Alabo.Web.Mvc.Attributes;
using Newtonsoft.Json;
using ZKCloud.Open.ApiBase.Models;
using User = Alabo.Users.Entities.User;

namespace Alabo.App.Share.OpenTasks.Configs.TeamRange {

    using User = User;

    /// <summary>
    /// Class TeamFiexdAchievementRangConfig.
    /// </summary>
    public class TeamFiexdAchievementRangConfig : ShareBaseConfig {

        /// <summary>
        /// Gets or sets the team range rate json.
        /// 团队极差
        /// </summary>
        [Field(ControlsType = ControlsType.Json, PlaceHolder = "请按会员等级升级点值的权重来设置，原则上等级越高，比例越高", ListShow = false, EditShow = true, ExtensionJson = "TeamFiexdAchievementRangItems")]
        [JsonIgnore]
        [Display(Name = "团队极差")]
        public string TeamFiexdAchievementRangJson { get; set; }

        /// <summary>
        /// Gets or sets the team range rate items.
        /// </summary>
        public IList<TeamFiexdAchievementRangItem> TeamFiexdAchievementRangItems { get; set; } = new List<TeamFiexdAchievementRangItem>();

        private static IList<TeamFiexdAchievementRangItem> GetDefaultItems() {
            var gradeService = Ioc.Resolve<IGradeService>();
            var userGrades = gradeService.GetUserGradeList();
            var list = new List<TeamFiexdAchievementRangItem>();
            foreach (var item in userGrades) {
                var TeamFiexdAchievementRang = new TeamFiexdAchievementRangItem {
                    GradeId = item.Id
                };
                list.Add(TeamFiexdAchievementRang);
            }
            return list;
        }
    }

    /// <summary>
    /// Class TeamFiexdAchievementRangModule.
    /// </summary>
    [TaskModule("23B9A703-6B42-4915-810F-01208CC0C000", "岗位绩效收益(按固定金额)", SortOrder = 999999, ConfigurationType = typeof(TeamFiexdAchievementRangConfig), IsSupportMultipleConfiguration = true,
         FenRunResultType = FenRunResultType.Price,
         Intro = "不同岗位间的最高提成比例会有所不同，岗位低的会员不能拿完所有的绩效，多余的部分给岗位高的。比如公司有业务员、经理、总监三个岗位，直接销售一个订单，最高奖金分别为100,200,300。如果是业务员A成单，业务员A奖金100，经理奖金100,总监奖金100。如果是经理B成单，业务员A奖金0，经理B奖金200,总监C奖金100。如果是总监C成单，业务员A奖金0，经理B奖金0,总监C奖金300。",
         RelationshipType = RelationshipType.UserRecommendedRelationship)]
    public class TeamFiexdAchievementRangModule : AssetAllocationShareModuleBase<TeamFiexdAchievementRangConfig> {

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamFiexdAchievementRangModule"/> class.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="config">The configuration.</param>
        public TeamFiexdAchievementRangModule(TaskContext context, TeamFiexdAchievementRangConfig config)
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
            //检查分润比例
            var distriRatio = Configuration.DistriRatio.Split(',');

            // 以结构A->B->C->D，D下单为列子
            for (var i = 0; i < distriRatio.Count(); i++) {
                if (map.Count < i + 1 || i > 2) {
                    break;
                }
                //当前级别奖励
                var ratio = distriRatio[i].ConvertToDecimal();
                var levelRadio = ratio;
                if (ratio <= 0) {
                    continue;
                }
                var item = map[i];
                var shareGuids = new List<Guid>();  // 已分配的等级，极差分配中，同一等级只能分配一次
                base.GetShareUser(item.UserId, out var shareUser);//从基类获取分润用户
                ExecuteLevelAmount(resultList, shareUser, i + 1, parameter, ref levelRadio, ref shareGuids);

                if (levelRadio > 0 && i < 2) {
                    // 第一层分配有剩，开始第二层分配
                    base.GetShareUser(shareUser.ParentId, out shareUser);//从基类获取分润用户
                    if (shareUser.GradeId == Guid.Parse("cc873faa-749b-449b-b85a-c7d26f626feb")) {
                        ExecuteLevelAmount(resultList, shareUser, i + 1, parameter, ref levelRadio, ref shareGuids);
                    }
                    if (levelRadio > 0 && i < 1) {
                        // 第二层分配有剩，开始第三层分配
                        base.GetShareUser(shareUser.ParentId, out shareUser);//从基类获取分润用户
                        if (shareUser.GradeId == Guid.Parse("cc873faa-749b-449b-b85a-c7d26f626feb")) {
                            ExecuteLevelAmount(resultList, shareUser, i + 1, parameter, ref levelRadio, ref shareGuids);
                        }
                    }
                }
            }
            //分期

            return ExecuteResult<ITaskResult[]>.Success(resultList.ToArray());
        }

        /// <summary>
        /// Executes the level amount.
        /// </summary>
        /// <param name="shareUser">会员Id</param>
        /// <param name="levelAmount">剩余金额</param>
        /// <param name="taskResults">The task results.</param>
        /// <param name="level">层次</param>
        /// <param name="parameter"></param>
        /// <param name="shareGuids">maxShareAmount</param>
        private IList<ITaskResult> ExecuteLevelAmount(IList<ITaskResult> taskResults, User shareUser, int level, TaskParameter parameter, ref decimal levelAmount, ref List<Guid> shareGuids) {
            if (levelAmount <= 0) {
                levelAmount = 0m;
                return taskResults;
            }

            if (shareUser == null) {
                levelAmount = 0m;
                return taskResults;
            }
            var maxAmount = GetRadioByGrade(shareUser, level); // 当前等级最大分润金额
            if (shareGuids.Contains(shareUser.GradeId)) {
                return taskResults;
            }
            shareGuids.Add(shareUser.GradeId);
            var shareAmount = Math.Min(levelAmount, maxAmount);
            levelAmount = levelAmount - shareAmount;
            var intro = "直推";
            if (level == 2) {
                intro = "间推";
            }
            if (level == 3) {
                intro = "三层";
            }
            CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration, taskResults, intro);
            return taskResults;
        }

        /// <summary>
        /// 获取s the radio by grade.
        /// 获取等级分润比例
        /// </summary>
        /// <param name="shareUser">The share 会员.</param>
        /// <param name="level">The level.</param>
        private decimal GetRadioByGrade(User shareUser, int level) {
            var shareGrade = Resolve<IGradeService>().GetGrade(shareUser.GradeId);
            if (shareGrade == null) {
                return 0;
            }
            var radio = 0m;
            var rangeItem = Configuration.TeamFiexdAchievementRangItems.FirstOrDefault(r => r.GradeId == shareGrade.Id);
            if (rangeItem == null) {
                return radio;
            }
            if (level == 1) {
                return rangeItem.FristAmount;
            }
            if (level == 2) {
                return rangeItem.SecondAmount;
            }
            if (level == 3) {
                return rangeItem.ThreeAmount;
            }
            return radio;
        }
    }

    /// <summary>
    /// Class TeamRangeRuleItem.
    /// 团队极差
    /// </summary>
    [ClassProperty(Name = "团队极差比例设置")]
    public class TeamFiexdAchievementRangItem {

        /// <summary>
        /// 等级Id
        /// 等级权重，按照会员等级升级点来设置，升级点越高权重越高
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true, EditShow = true, DataSource = "UserGradeConfig", SortOrder = 1)]
        [Display(Name = "等级权重")]
        public Guid GradeId { get; set; }

        /// <summary>
        /// 直推最高金额
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, SortOrder = 2)]
        [Display(Name = "直推最高金额")]
        public decimal FristAmount { get; set; } = 0.0m;

        /// <summary>
        /// 间推最高金额
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, SortOrder = 3)]
        [Display(Name = "间推最高金额")]
        public decimal SecondAmount { get; set; } = 0.0m;

        /// <summary>
        /// 间推最高金额
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, SortOrder = 4)]
        [Display(Name = "第三级最高金额")]
        public decimal ThreeAmount { get; set; } = 0.0m;
    }
}