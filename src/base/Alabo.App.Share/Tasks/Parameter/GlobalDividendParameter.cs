using System;
using Alabo.App.Core.Tasks.ResultModel;

namespace Alabo.App.Share.Tasks.Parameter {

    public class GlobalDividendParameter : TaskQueueParameterBase {

        /// <summary>
        ///金额(无效)
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 本轮起始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        public GlobalDividendParameter(int configurationId)
            : base(configurationId) {
        }
    }
}