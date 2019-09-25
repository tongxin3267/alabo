using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Validations;

namespace Alabo.App.Core.Themes.Dtos.Service {

    public class WidgeInput {

        /// <summary>
        ///     数据Id为空的时候，使用默认数据Id
        /// </summary>
        [Display(Name = "数据Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string DataId { get; set; }

        /// <summary>
        ///     模块数据
        /// </summary>
        [Display(Name = "模块Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string WidgetId { get; set; }

        /// <summary>
        ///     默认模块数据Id
        /// </summary>
        [Display(Name = "默认数据Id")]
        public string DefaultDataId { get; set; }
    }
}