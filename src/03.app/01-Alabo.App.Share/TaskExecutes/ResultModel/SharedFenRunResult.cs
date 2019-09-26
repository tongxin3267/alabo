using System.Collections.Generic;
using Alabo.Data.Things.Orders.ResultModel;

namespace Alabo.App.Share.TaskExecutes.ResultModel {

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