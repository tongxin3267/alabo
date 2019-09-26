using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Alabo.App.Core.Tasks.Domain.Enums;
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
    /// 裂变与培育
    /// </summary>
    public class NLevelDistributionCultivateConfig : ShareBaseConfig {

        /// <summary>
        /// 会员等级
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true, EditShow = true, SortOrder = 1,
            DataSource = "Alabo.App.Core.User.Domain.Callbacks.UserGradeConfig")]
        [Display(Name = "会员等级")]
        [HelpBlock("必须设置会员等级，获得基础分润和培育分润的会员，等级需一直.改规则只针对会员有效")]
        public Guid UserGradeId { get; set; }

        /// <summary>
        /// 分润比例
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, SortOrder = 1)]
        [Display(Name = "分润基础比例")]
        [HelpBlock("以分润基数为依托，分润基础比例,0.1表示10%,比如分润基数为300元,如果分润基础比例为0.1,则符合条件的会员可得300*0.1=30元的分润")]
        public decimal BaseRatio { get; set; } = 0.1m;

        /// <summary>
        /// 分润比例
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, SortOrder = 1)]
        [Display(Name = "培育比例一代")]
        [HelpBlock("以分润基数为依托,培育比例,0.01表示1%.")]
        public decimal CultivateRatio { get; set; } = 0.01m;

        /// <summary>
        /// 分润比例
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, SortOrder = 1)]
        [Display(Name = "培育比例二代")]
        [HelpBlock("以分润基数为依托,培育比例,0.01表示1%.")]
        public decimal CultivateTwoRatio { get; set; } = 0.01m;
    }

    /// <summary>
    /// Class NLevelDistributionModule.
    /// </summary>
    [TaskModule("BD717F8D-A000-4409-9A05-507E0AE50001", "裂变与培育", SortOrder = 999999,
        ConfigurationType = typeof(NLevelDistributionCultivateConfig), IsSupportMultipleConfiguration = true,
        FenRunResultType = Core.Tasks.Domain.Enums.FenRunResultType.Price, IsSupportSetDistriRatio = false,
        Intro = "裂变与培育，支持不同等级的会员培育，培育对象必须得是同等级。比如A,B为系统的联合创始人,A在B的上面,B享受基础分润,A享受培育分润",
        RelationshipType = RelationshipType.UserRecommendedRelationship)]
    public class NLevelDistributionCultivateModule : AssetAllocationShareModuleBase<NLevelDistributionCultivateConfig> {

        public NLevelDistributionCultivateModule(TaskContext context, NLevelDistributionCultivateConfig config)
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

            var parentUserIds = map.OrderBy(r => r.ParentLevel).Select(r => r.UserId);
            if (Configuration.UserGradeId.IsGuidNullOrEmpty()) {
                return ExecuteResult<ITaskResult[]>.Cancel("会员等级设置错误");
            }

            var allUserGradeIds = Resolve<IGradeService>().GetUserGradeList().Select(r => r.Id);
            if (!allUserGradeIds.Contains(Configuration.UserGradeId)) {
                return ExecuteResult<ITaskResult[]>.Cancel("会员等级不存在，不是有效的会员等级");
            }

            var shareUsersList = Resolve<IUserService>()
                .GetList(r => r.GradeId == Configuration.UserGradeId && parentUserIds.Contains(r.Id)).ToList();
            if (!shareUsersList.Any()) {
                return ExecuteResult<ITaskResult[]>.Cancel("符合条件的会员不存在");
            }

            var shareUsersListIds = shareUsersList.Select(r => r.Id).ToList();

            var count = 0;
            IList<ITaskResult> resultList = new List<ITaskResult>();
            foreach (var parentId in parentUserIds) {
                if (shareUsersListIds.Contains(parentId)) {
                    base.GetShareUser(parentId, out var shareUser); //从基类获取分润用户
                    if (shareUser == null) {
                        continue;
                    }

                    count++;
                    //基础分润
                    if (count == 1) {
                        var shareAmount = BaseFenRunAmount * Configuration.BaseRatio; //基础分润
                        CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration, resultList);
                    }

                    //培育分润
                    if (count == 2) {
                        var shareAmount = BaseFenRunAmount * Configuration.CultivateRatio; //培育分润
                        CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration,
                            resultList); //构建分润参数
                    }

                    //培育分润
                    if (count == 3) {
                        var shareAmount = BaseFenRunAmount * Configuration.CultivateTwoRatio; //培育分润
                        CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration,
                            resultList); //构建分润参数
                    }
                }

                if (count >= 3) {
                    break;
                }
            }

            return ExecuteResult<ITaskResult[]>.Success(resultList.ToArray());
        }
    }
}