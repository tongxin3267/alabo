using System.Collections.Generic;
using Alabo.App.Core.Tasks.ResultModel;

namespace Alabo.App.Share.Share.Domain.Dtos {

    /// <summary>
    ///
    /// </summary>
    public class RewardModulesOutput {

        /// <summary>
        /// 数量
        /// </summary>
        public long Count { get; set; }

        /// <summary>
        ///
        /// </summary>
        public List<TaskModuleAttribute> ShareModules { get; set; }
    }
}