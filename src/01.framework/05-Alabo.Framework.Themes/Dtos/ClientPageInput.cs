using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Themes.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Themes.Dtos {

    /// <summary>
    ///     终端页面获取
    /// </summary>
    public class ClientPageInput {

        /// <summary>
        ///     终端配置
        /// </summary>
        [Display(Name = "终端配置")]
        public ClientType ClientType { get; set; }

        /// <summary>
        ///     类型
        /// </summary>
        public ThemeType Type { get; set; }
    }
}