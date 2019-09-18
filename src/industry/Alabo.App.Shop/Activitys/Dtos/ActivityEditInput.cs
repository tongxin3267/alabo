

using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;

namespace Alabo.App.Shop.Activitys.Dtos {

    /// <summary>
    ///     Class ActivityEditInput.
    /// </summary>
    public class ActivityEditInput {

        /// <summary>
        ///     Gets or sets Id标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Gets or sets the key.
        /// </summary>
        [Display(Name = "活动类型")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Key { get; set; }

        /// <summary>
        ///     Gets or sets the product identifier.
        /// </summary>
        public long ProductId { get; set; }
    }
}