using System.Collections.Generic;

namespace Alabo.App.Share.OpenTasks.Parameter {

    public class FenRunStagesResultParameter {

        /// <summary>
        /// 是否将积分变化更新至任务变更队列表
        /// </summary>
        public bool IsAddContributionChangeToQueue { get; set; }

        /// <summary>
        /// 是否将收入变化更新至任务变更队列表
        /// </summary>
        public bool IsAddIncomeChangeToQueue { get; set; }

        /// <summary>
        /// 分期结果
        /// </summary>
        public IList<FenRunResultParameter> FenRunResultParameterList { get; set; } = new List<FenRunResultParameter>();
    }
}