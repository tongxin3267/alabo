using Alabo.AutoConfigs;

namespace _01_Alabo.Cloud.Core.AppVersion.Domain.Configs {

    public class AppVersionConfig : IAutoConfig {

        /// <summary>
        /// 是否开启 版本检测更新
        /// 如果不开启则不进行版本检测,也不推更新
        /// </summary>
        public bool IsEnble { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 更新内容
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 更新链接 完整的url :http://www.domian.com/file/name.fix
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 默认初始数据
        /// </summary>
        public void SetDefault() {
            IsEnble = false;
            Version = string.Empty;
            Note = string.Empty;
            Url = string.Empty;
        }
    }
}