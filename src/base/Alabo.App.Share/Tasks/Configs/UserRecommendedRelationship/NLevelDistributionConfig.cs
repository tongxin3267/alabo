using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.Tasks.Extensions;
using Alabo.App.Core.Tasks.ResultModel;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Share.Tasks.Base;
using Alabo.App.Share.Tasks.Modules;
using Alabo.Extensions;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.App.Share.Tasks.Configs.UserRecommendedRelationship {

    /// <summary>
    /// 裂变分佣
    /// </summary>
    /// <seealso cref="Alabo.App.Share.Tasks.Base.ShareBaseConfig" />
    public class NLevelDistributionConfig : ShareBaseConfig {
    }

    /// <summary>
    /// Class NLevelDistributionModule.
    /// </summary>
    [TaskModule("BD717F8D-AD00-44C9-9AA5-597E0AE55011", "裂变分佣", SortOrder = 999999, ConfigurationType = typeof(NLevelDistributionConfig), IsSupportMultipleConfiguration = true,
      FenRunResultType = Core.Tasks.Domain.Enums.FenRunResultType.Price,
      Intro = "最常用的分润维度，基本上任何个客户都会使用到，应用最广，配置更灵活,根据会员关系图,计算下一级，上一级分润，包括常见的分润模型比如三级分销、三三复制,4M复制",
      RelationshipType = RelationshipType.UserRecommendedRelationship)]
    public class NLevelDistributionModule : AssetAllocationShareModuleBase<NLevelDistributionConfig> {

        /// <summary>
        /// Initializes a new instance of the <see cref="NLevelDistributionModule"/> class.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="config">The configuration.</param>
        public NLevelDistributionModule(TaskContext context, NLevelDistributionConfig config)
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
                return ExecuteResult<ITaskResult[]>.Cancel("基础验证未通过" + baseResult.Message);
            }

            var userMap = Resolve<IUserMapService>().GetParentMapFromCache(base.ShareOrderUser.Id);
            var map = userMap.ParentMap.DeserializeJson<List<ParentMap>>();
            if (map == null) {
                return ExecuteResult<ITaskResult[]>.Cancel("未找到触发会员的Parent Map.");
            }

            IList<ITaskResult> resultList = new List<ITaskResult>();
            for (var i = 0; i < Ratios.Count; i++) {
                if (map.Count < i + 1) {
                    break;
                }
                var item = map[i];
                base.GetShareUser(item.UserId, out var shareUser);//从基类获取分润用户
                if (shareUser == null) {
                    continue;
                }

                var ratio = Convert.ToDecimal(Ratios[i]);
                var shareAmount = BaseFenRunAmount * ratio;//分润金额
                CreateResultList(shareAmount, ShareOrderUser, shareUser, parameter, Configuration, resultList);//构建分润参数
            }
            return ExecuteResult<ITaskResult[]>.Success(resultList.ToArray());
        }
    }
}