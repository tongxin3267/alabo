using _01_Alabo.Cloud.Core.AppVersion.Domain.Enums;

namespace _01_Alabo.Cloud.Core.AppVersion.Dtos {

    public class AppVersionOutput {

        /// <summary>
        /// 升级标志
        /// </summary>
        public AppVersionStatus Status { get; set; }

        /// <summary>
        /// 更新内容
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 下载链接
        /// </summary>
        public string Url { get; set; }
    }
}