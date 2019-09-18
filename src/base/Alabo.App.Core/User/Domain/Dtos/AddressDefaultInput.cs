using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;

namespace Alabo.App.Core.User.Domain.Dtos {

    /// <summary>
    ///     默认地址修改
    /// </summary>
    public class AddressDefaultInput {

        /// <summary>
        ///     地址Id
        /// </summary>
        [Display(Name = "地址Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Id { get; set; }

        /// <summary>
        ///     Gets or sets the login user identifier.
        ///     仅作为数据接收，不存储
        /// </summary>
        /// <value>
        ///     The login user identifier.
        /// </value>
        [Display(Name = "用户Id")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long UserId { get; set; }
    }
}