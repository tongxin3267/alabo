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

namespace Alabo.App.Open.Tasks.Configs.UserRecommendedRelationship {

    /// <summary>
    /// 高等级绩效
    /// </summary>
    public class HighLevelPerformanceConfig : ShareBaseConfig {

        /// <summary>
        /// 会员等级
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true, EditShow = true, SortOrder = 1, DataSource = "Alabo.App.Core.User.Domain.Callbacks.UserGradeConfig")]
        [Display(Name = "基准等级(包含）")]
        [HelpBlock("必须设置基准等级，当分润会员的等级>=基准等级时，可获得绩效奖励")]
        public Guid UserGradeId { get; set; }

        /// <summary>
        /// 包含下单用户
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, SortOrder = 2)]
        [Display(Name = "包含下单用户")]
        [HelpBlock("包含下单用户:则下单会员可获得高等级绩效。如下单会员是总监，则高等级绩效被下单会员拿走。如果关闭，则不包含下单会员")]
        public bool IsAllowUserSelf { get; set; } = false;
    }

    /// <summary>
    /// Class NLevelDistributionModule.
    /// </summary>
    [TaskModule("BD717555-A777-8809-9005-507E0AE59991", "高等级绩效", SortOrder = 999999, ConfigurationType = typeof(HighLevelPerformanceConfig), IsSupportMultipleConfiguration = true,
      FenRunResultType = Core.Tasks.Domain.Enums.FenRunResultType.Price,
      Intro = "高等级绩效，当分润会员的等级>=基准等级时，可获得绩效奖励,比如经理以上级别（包括经理、总监、总经理、股东）可获得经理绩效，所有总监以上级别（包括总监、总经理、股东）可获得总监绩效",
      RelationshipType = RelationshipType.UserRecommendedRelationship)]
    public class HighLevelPerformanceModule : AssetAllocationShareModuleBase<HighLevelPerformanceConfig> {

        public HighLevelPerformanceModule(TaskContext context, HighLevelPerformanceConfig config)
            : base(context, config) {
        }

        public override ExecuteResult<ITaskResult[]> Execute(TaskParameter parameter) {
            var baseResult = base.Execute(parameter);
            if (baseResult.Status != ResultStatus.Success) {
                return ExecuteResult<ITaskResult[]>.Cancel("基础验证未通过" + baseResult.Message);
            }

            var userMap = Resolve<IUserMapService>().GetParentMapFromCache(base.ShareOrderUser.Id);
            var map = userMap.ParentMap.DeserializeJson<List<ParentMap>>();
            if (map == null) {
                return ExecuteResult<ITaskResult[]>.Cancel("未找到触发会员的Parent Map.");
            }

            var parentUserIds = map.OrderBy(r => r.ParentLevel).Select(r => r.UserId).ToList();
            if (Configuration.IsAllowUserSelf) {
                parentUserIds = parentUserIds.AddBefore(this.ShareOrderUser.Id).ToList();
            }
            if (Configuration.UserGradeId.IsGuidNullOrEmpty()) {
                return ExecuteResult<ITaskResult[]>.Cancel("会员等级设置错误");
            }

            var allUserGradeIds = Resolve<IGradeService>().GetUserGradeList().Select(r => r.Id);

            var allGrades = Resolve<IGradeService>().GetUserGradeList();
            var configGrade = allGrades.FirstOrDefault(r => r.Id == Configuration.UserGradeId);
            if (configGrade == null) {
                return ExecuteResult<ITaskResult[]>.Cancel("会员等级不存在，不是有效的会员等级");
            }

            allGrades = allGrades.Where(r => r.Contribute >= configGrade.Contribute).ToList(); // 获取高等级的

            var shareUsersList = Resolve<IUserService>().GetList(parentUserIds).ToList();
            if (!shareUsersList.Any()) {
                return ExecuteResult<ITaskResult[]>.Cancel("符合条件的会员不存在");
            }

            var shareUsersListIds = shareUsersList.Select(r => r.Id).ToList();
            var allGradeIds = allGrades.Select(r => r.Id).ToList();

            IList<ITaskResult> resultList = new List<ITaskResult>();
            foreach (var parentId in parentUserIds) {
                if (shareUsersListIds.Contains(parentId)) {
                    base.GetShareUser(parentId, out var shareUser);//从基类获取分润用户
                    if (shareUser != null) {
                        // 符合等级条件
                        if (allGradeIds.Contains(shareUser.GradeId)) {
                            var shareAmount = BaseFenRunAmount * this.Ratios[0].ToDecimal();//绩效奖励
                            CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration, resultList);
                            break;
                        }
                    }
                }
            }
            return ExecuteResult<ITaskResult[]>.Success(resultList.ToArray());
        }
    }
}