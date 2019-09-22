using System.Collections.Generic;

namespace Alabo.App.Core.Tasks.ResultModel {

    /// <summary>
    /// 共享分润结果
    /// </summary>
    public class SharedFenRunResult {

        /// <summary>
        /// 分润结果
        /// </summary>
        public IList<ShareResult> ShareResults { get; set; }
    }
}