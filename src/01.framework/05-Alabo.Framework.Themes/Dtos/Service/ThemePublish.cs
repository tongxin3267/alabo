using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Core.Themes.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Validations;

namespace Alabo.App.Core.Themes.Dtos.Service {

    public class ThemePublish {

        /// <summary>
        /// 模板
        /// </summary>
        public Theme Theme { get; set; }

        /// <summary>
        /// 站点页面列表
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Display(Name = "站点页面")]
        public List<ThemePage> PageList { get; set; }

        /// <summary>
        /// 当前租户
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Display(Name = "当前租户")]
        public string Tenant { get; set; }
    }
}