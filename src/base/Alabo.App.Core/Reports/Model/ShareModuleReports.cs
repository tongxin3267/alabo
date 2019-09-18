using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Reports.Model {

    [ClassProperty(Name = "分润配置数据", Icon = "fa fa-building")]
    public class ShareModuleReports : IReportModel {

        /// <summary>
        ///     分润配置总数
        /// </summary>
        public long ConfigCount { get; set; }

        /// <summary>
        ///     分润启用配置数
        /// </summary>
        public long EnableCount { get; set; }

        /// <summary>
        ///     分润模块数据
        /// </summary>
        public string ShareModuleList { get; set; }
    }
}