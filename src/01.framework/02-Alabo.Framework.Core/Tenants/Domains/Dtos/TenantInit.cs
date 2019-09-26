using System.ComponentModel.DataAnnotations;
using Alabo.Validations;

namespace Alabo.Core.Tenants.Domains.Dtos {

    /// <summary>
    /// 租户输入模型
    /// </summary>
    public class TenantInit {

        /// <summary>
        ///
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Tenant { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Mobile { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Name { get; set; }

        /// <summary>
        /// token
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Token { get; set; }

        /// <summary>
        /// 是否为租户
        /// </summary>
        public bool IsTenant { get; set; } = false;
    }
}