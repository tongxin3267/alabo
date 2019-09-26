using Alabo.AutoConfigs;

namespace Alabo.Framework.Basic.Address.Domain.Configs {

    /// <summary>
    /// 地址配置文件
    /// </summary>
    public class UserAddressConfig : IAutoConfig {

        /// <summary>
        /// 是否开启配置
        /// 如果不开启则不限制
        /// </summary>
        public bool IsEnble { get; set; }

        /// <summary>
        /// 最大地址个数 0表示不限制
        /// </summary>
        public long MaxNumber { get; set; }

        /// <summary>
        /// 提示文字,为什么限制地址不让更改
        /// </summary>
        public string EditTips { get; set; }

        /// <summary>
        /// 提示文字,为什么限制地址个数
        ///
        /// </summary>
        public string AddTips { get; set; }

        /// <summary>
        /// 设置默认值
        /// </summary>
        public void SetDefault() {
            this.IsEnble = false;
            this.MaxNumber = 0;
        }
    }
}