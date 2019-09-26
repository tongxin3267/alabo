using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Themes.Domain.Enums;
using Alabo.Framework.Core.Enums.Enum;

namespace Alabo.App.Core.Themes.Dtos {

    /// <summary>
    ///     终端页面获取
    /// </summary>
    public class ClientPageInput {

        /// <summary>
        ///     终端配置
        /// </summary>
        [Display(Name = "终端配置")]
        public ClientType ClientType {
            get; set;
        }

        /// <summary>
        /// 类型
        /// </summary>
        public ThemeType Type { get; set; }
    }
}