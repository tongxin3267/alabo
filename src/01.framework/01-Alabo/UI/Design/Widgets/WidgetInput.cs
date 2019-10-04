using Alabo.Validations;
using System.ComponentModel.DataAnnotations;

namespace Alabo.UI.Design.Widgets {

    /// <summary>
    ///     模块参数获取
    /// </summary>
    public class WidgetInput {
        /// <summary>
        ///     模块类型
        /// </summary>

        [Display(Name = "模块类型")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Type { get; set; }

        /// <summary>
        ///     用Json的方式来传数据
        /// </summary>
        public string Json { get; set; }
    }
}