using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.Tasks.Extensions;
using Alabo.App.Core.Tasks.ResultModel;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Open.Tasks.Base;
using Alabo.App.Open.Tasks.Modules;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Users.Dtos;
using ZKCloud.Open.ApiBase.Models;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Open.Tasks.Configs.TeamRange {

    /// <summary>
    /// Class TeamRangCultivateConfig.
    /// </summary>
    public class TeamRangCultivateConfig : ShareBaseConfig {

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
    [TaskModule("23B9A703-6B42-4bbb-810F-01208CC0C999", "岗位极差育成收益(按比例)", SortOrder = 999999,
        ConfigurationType = typeof(TeamRangCultivateConfig), IsSupportMultipleConfiguration = true,
        FenRunResultType = Core.Tasks.Domain.Enums.FenRunResultType.Price, IsSupportSetDistriRatio = false,
        Intro = "不同岗位间的最高提成比例会有所不同，岗位低的会员不能拿完所有的绩效，多余的部分给岗位高的。比如公司有业务员、经理、总监三个岗位，直接销售一个订单",
        RelationshipType = RelationshipType.UserRecommendedRelationship)]
    public class TeamRangCultivateModule : AssetAllocationShareModuleBase<TeamRangCultivateConfig> {

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamRangCultivateModule"/> class.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="config">The configuration.</param>
        public TeamRangCultivateModule(TaskContext context, TeamRangCultivateConfig config)
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
            List<ParentMap> mapList = new List<ParentMap>();
            var userMap = Resolve<IUserMapService>().GetParentMapFromCache(base.ShareOrderUser.Id);
            var map = userMap.ParentMap.DeserializeJson<List<ParentMap>>();
            if (map == null) {
                return ExecuteResult<ITaskResult[]>.Cancel("未找到触发会员的Parent Map.");
            }

            IList<ITaskResult> resultList = new List<ITaskResult>();
            long lvKey = 0;//用于判断级差
            long userIdKey = 0;//第一个营业部userID要记录
            var iKey = false;
            var firLv = false;

            foreach (var teamRangCultivateItem in Configuration.TeamRangCultivateItems) {
                // 将自己加上去
                ParentMap parentMap = new ParentMap {
                    UserId = ShareOrderUser.Id,
                    ParentLevel = 0
                };

                map = map.AddBefore(parentMap).ToList();

                for (var i = 0; i < base.TeamLevel; i++) {
                    var level = 0;
                    var tempLv = 0;
                    if (map.Count < i + 1) {
                        break;
                    }
                    var item = map[i];
                    var grade = Resolve<IGradeService>().GetGrade(teamRangCultivateItem.GradeId);
                    if (grade == null) {
                        break;
                    }

                    var gradeList = Resolve<IAutoConfigService>().GetList<UserGradeConfig>();

                    base.GetShareUser(item.UserId, out var shareUser);
                    var shareGradeContribute = gradeList.FirstOrDefault(u => u.Id == shareUser.GradeId).Contribute;

                    //如果是营业部 则不再判断其他等级
                    if (shareGradeContribute >= 500000) {
                        level = 4;
                        iKey = true;

                        if (i == 0) {
                            userIdKey = shareUser.Id;
                        }
                    }

                    if (shareGradeContribute >= 500000) {
                        level = 4;
                    }
                    if (shareGradeContribute >= 100000 && shareGradeContribute < 500000) {
                        level = 3;
                    }
                    if (shareGradeContribute >= 10000 && shareGradeContribute < 100000) {
                        level = 2;
                    }
                    if (shareGradeContribute >= 0 && shareGradeContribute < 10000) {
                        level = 1;
                    }

                    if (i == 0) {
                        lvKey = level;//获得下单人等级
                    }

                    if (level < lvKey) {
                        continue;
                    }

                    if (i >= 4) {
                        break;
                    }

                    #region 区分判断

                    var tempContribute = gradeList.FirstOrDefault(u => u.Id == teamRangCultivateItem.GradeId).Contribute;
                    if (tempContribute >= 500000) {
                        tempLv = 4;
                    }
                    if (tempContribute >= 100000 && tempContribute < 500000) {
                        tempLv = 3;
                    }
                    if (tempContribute >= 10000 && tempContribute < 100000) {
                        tempLv = 2;
                    }
                    if (tempContribute >= 0 && tempContribute < 10000) {
                        tempLv = 1;
                    }

                    #endregion 区分判断

                    if (level == tempLv) {
                        if (userIdKey == shareUser.Id && i != 0) {
                            continue;
                        }

                        if (level == 0) {
                            continue;
                        }

                        var mapSingle = mapList.FirstOrDefault(u => u.UserId == level);
                        if (mapSingle == null) {
                            ParentMap temp = new ParentMap {
                                UserId = level,
                                ParentLevel = 1   //计算该等级出现次数
                            };
                            //如果第一个加入的值为营业厅 则不判断其他
                            if (mapList.Count() == 0 && level == 4) {
                                iKey = true;
                            }

                            //如果等级小于报单人 则跳过
                            if (lvKey <= level) {
                                mapList.Add(temp);
                            }

                            continue;
                        } else {
                            var num = mapList.FirstOrDefault(u => u.UserId == level).ParentLevel;
                            mapList.Remove(mapSingle);
                            ParentMap temp = new ParentMap {
                                UserId = level,
                                ParentLevel = num + 1   //用于计算该等级出现次数
                            };
                            mapList.Add(temp);

                            mapSingle.ParentLevel = num + 1;
                        }
                        var gradeName = Resolve<IGradeService>().GetGrade(shareUser.GradeId);
                        if (mapSingle.ParentLevel == 2) {
                            if (lvKey > mapSingle.UserId) {
                                continue;
                            }

                            lvKey = mapSingle.UserId;

                            var intro = $"{gradeName.Name}育成一代";
                            var shareAmount = BaseFenRunAmount * teamRangCultivateItem.FristAmount;
                            if (shareAmount > 0) {
                                CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration,
                                    resultList, intro);
                            }
                        }
                        if (mapSingle.ParentLevel == 3 && mapSingle.UserId == 4) {
                            var intro = $"{gradeName.Name}育成二代";
                            var shareAmount = BaseFenRunAmount * teamRangCultivateItem.SecondAmount;
                            if (shareAmount > 0) {
                                CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration,
                                    resultList, intro);
                            }
                            break;
                        }
                        if (mapSingle.ParentLevel >= 3) {
                            break;
                        }
                    }
                }
            }

            return ExecuteResult<ITaskResult[]>.Success(resultList.ToArray());
        }
    }

    /// <summary>
    /// Class TeamRangeRuleItem.
    /// 团队极差
    /// </summary>
    [ClassProperty(Name = "团队极差比例设置")]
    public class TeamRangCultivateItem {

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