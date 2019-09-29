using System;
using Alabo.App.Share.TaskExecutes.ResultModel;

namespace Alabo.App.Share.OpenTasks.Parameter
{
    public class GlobalDividendParameter : TaskQueueParameterBase
    {
        public GlobalDividendParameter(int configurationId)
            : base(configurationId)
        {
        }

        /// <summary>
        ///     金额(无效)
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        ///     本轮起始时间
        /// </summary>
        public DateTime StartTime { get; set; }
    }
}