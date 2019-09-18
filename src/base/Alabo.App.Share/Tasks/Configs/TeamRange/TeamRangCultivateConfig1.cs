using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.Tasks.Extensions;
using Alabo.App.Core.Tasks.ResultModel;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Share.Tasks.Base;
using Alabo.App.Share.Tasks.Modules;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Share.Tasks.Configs.TeamRange {

    /// <summary>
    /// Class TeamRangCultivateConfig1.
    /// </summary>
    public class TeamRangCultivateConfig1 : ShareBaseConfig {

        /// <summary>
        /// Gets or sets the team range rate json.
        /// 团队极差
        /// </summary>
        [Field(ControlsType = ControlsType.Json, PlaceHolder = "请按会员等级升级点值的权重来设置，原则上等级越高，比例越高,等级配置不能重复", ListShow = false, EditShow = true, ExtensionJson = "TeamRangCultivateItems")]
        [Display(Name = "团队育成比例设置")]
        [JsonIgnore]
        public string TeamRangCultivateJson { get; set; }

        /// <summary>
        /// 育成兼容方式
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, SortOrder = 2)]
        [Display(Name = "同等级育成兼容")]
        [HelpBlock("同等级育成兼容:只有相同的等级才可以拿育成奖励；关闭表示高等级兼容低等级：级别高的可以拿级别低的育成奖励")]
        public bool IsAllowUserSelf { get; set; } = true;

        /// <summary>
        /// Gets or sets the team range rate items.
        /// </summary>
        public IList<TeamRangCultivateItem> TeamRangCultivateItems { get; set; } = new List<TeamRangCultivateItem>();

        private static IList<TeamRangCultivateItem> GetDefaultItems() {
            var gradeService = Alabo.Helpers.Ioc.Resolve<IGradeService>();
            var userGrades = gradeService.GetUserGradeList();
            var list = new List<TeamRangCultivateItem>();
            foreach (var item in userGrades) {
                var TeamRangCultivate = new TeamRangCultivateItem {
                    GradeId = item.Id
                };
                list.Add(TeamRangCultivate);
            }
            return list;
        }
    }

    /// <summary>
    /// Class TeamRangCultivateModule.
    /// </summary>
    [TaskModule("4066EC30-10E5-41F6-8821-37CB2DC5CC2E", "岗位极差育成收益一(按比例)", SortOrder = 999999,
        ConfigurationType = typeof(TeamRangCultivateConfig1), IsSupportMultipleConfiguration = true,
        FenRunResultType = Core.Tasks.Domain.Enums.FenRunResultType.Price, IsSupportSetDistriRatio = false,
        Intro = "不同岗位间的最高提成比例会有所不同，岗位低的会员不能拿完所有的绩效，多余的部分给岗位高的。比如公司有业务员、经理、总监三个岗位，直接销售一个订单",
        RelationshipType = RelationshipType.UserRecommendedRelationship)]
    public class TeamRangCultivateModule1 : AssetAllocationShareModuleBase<TeamRangCultivateConfig1> {

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamRangCultivateModule"/> class.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="config">The configuration.</param>
        public TeamRangCultivateModule1(TaskContext context, TeamRangCultivateConfig1 config)
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

            // foreach (var teamRangCultivateItem in Configuration.TeamRangCultivateItems)
            //  {
            // 将自己加上去

            ParentMap parentMap = new ParentMap {
                UserId = ShareOrderUser.Id,
                ParentLevel = 0
            };
            map = map.AddBefore(parentMap).ToList();
            var a = 0;
            var longAmount = 0.0m;
            var level = 0;
            for (var i = 0; i < base.TeamLevel;) {
                if (a >= 6) {
                    break;
                }
                if (map.Count < i + 1) {
                    break;
                }
                //var item = map[i];
                //var grade = Resolve<IGradeService>().GetGrade(teamRangCultivateItem.GradeId);
                //base.GetShareUser(item.UserId, out var shareUser);

                var item = map[i];
                base.GetShareUser(item.UserId, out var shareUser);//从基类获取分润用户
                var grade = Resolve<IGradeService>().GetGrade(shareUser.GradeId);
                var shareUserRule = Configuration.TeamRangCultivateItems.FirstOrDefault(r => r.GradeId == grade.Id);
                if (shareUserRule == null) {
                    continue;
                }
                var sumAmount = shareUserRule.FristAmount + shareUserRule.SecondAmount;
                if (grade == null) {
                    continue;
                }

                //if (grade == null)
                //{
                //    break;
                //}
                if (longAmount < sumAmount) {
                    i = i + 1;
                    level = 0;
                    longAmount = sumAmount;

                    continue;
                } else {
                    i = i + 1;
                    level++;
                    if (longAmount == sumAmount) {
                        if (level == 1) {
                            var intro = $"{grade.Name}育成一代";
                            var shareAmount = BaseFenRunAmount * shareUserRule.FristAmount;
                            if (shareAmount > 0) {
                                CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration,
                                    resultList, intro);
                                a = a + 1;
                            }
                        }
                        if (level == 2) {
                            var intro = $"{grade.Name}育成二代";
                            var shareAmount = BaseFenRunAmount * shareUserRule.SecondAmount;
                            if (shareAmount > 0) {
                                CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration,
                                    resultList, intro);
                                a = a + 1;
                            }
                        }
                        if (level >= 3) {
                            continue;
                        }
                    } else {
                        continue;
                    }
                }
            }

            // }
            return ExecuteResult<ITaskResult[]>.Success(resultList.ToArray());
        }
    }

    /// <summary>
    /// Class TeamRangeRuleItem.
    /// 团队极差
    /// </summary>
    [ClassProperty(Name = "团队极差比例设置")]
    public class TeamRangCultivateItem1 {

        /// <summary>
        /// 等级Id
        /// 等级权重，按照会员等级升级点来设置，升级点越高权重越高
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true, EditShow = true, DataSource = "UserGradeConfig", SortOrder = 1)]
        [Display(Name = "等级权重")]
        public Guid GradeId { get; set; }

        ///// <summary>
        ///// 分润比例
        ///// </summary>
        //[Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, SortOrder = 1)]
        //[Display(Name = "分润基础比例")]
        //[HelpBlock("以分润基数为依托，分润基础比例,0.1表示10%,比如分润基数为300元,如果分润基础比例为0.1,则符合条件的会员可得300*0.1=30元的分润")]
        //public decimal BaseRatio { get; set; } = 0.1m;

        /// <summary>
        /// 直推最高金额
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, SortOrder = 2)]
        [Display(Name = "育成一代比例")]
        public decimal FristAmount { get; set; } = 0.0m;

        /// <summary>
        /// 间推最高金额
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, SortOrder = 3)]
        [Display(Name = "育成二代比例")]
        public decimal SecondAmount { get; set; } = 0.0m;
    }
}

//                    {
//                        if (teamRangCultivateItem.SecondAmount == 0)
//            {
//                if (shareUser.GradeId == teamRangCultivateItem.GradeId)
//                {
//                    level++;
//                    if (level == 1)
//                    {
//                        a = i;
//                    }
//                    if (level == 2)
//                    {
//                        b = i;
//                        c = b - a;
//                        if (c == 1)
//                        {
//                            var intro = $"{grade.Name}育成一代";
//                            var shareAmount = BaseFenRunAmount * teamRangCultivateItem.FristAmount;
//                            if (shareAmount > 0)
//                            {
//                                CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration,
//                                    resultList, intro);
//                            }
//                        }
//                    }

//                }
//            }
//            else
//            {
//                d=d+1;
//                level++;
//                if (level == 1)
//                {
//                    a = i;
//                }
//            }
//        }

//        else
//        {
//            level++;
//            if (level == 2)
//            {
//                b = i;
//                c = b - a;
//                if (c == 1)
//                {
//                    if (teamRangCultivateItem.SecondAmount > 0) {
//                        var intro = $"{grade.Name}育成一代";
//                    var shareAmount = BaseFenRunAmount * teamRangCultivateItem.FristAmount;
//                    if (shareAmount > 0)
//                    {
//                        CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration,
//                            resultList, intro);
//                    }
//                    }
//                }
//            }
//            if (level == 3)
//            {
//                d = d + 1;
//                b = i;
//                c = b - a;
//                if (c == 2)
//                {
//                    var intro = $"{grade.Name}育成二代";
//                    var shareAmount = BaseFenRunAmount * teamRangCultivateItem.SecondAmount;
//                    if (shareAmount > 0)
//                    {
//                        CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration,
//                            resultList, intro);
//                    }
//                }

//            }
//            if (level >= 3)
//            {
//                break;
//            }

//        }