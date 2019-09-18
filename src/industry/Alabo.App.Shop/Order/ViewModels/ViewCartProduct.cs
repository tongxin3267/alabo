using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;

namespace Alabo.App.Shop.Order.ViewModels {

    public class ViewCartProduct {

        /// <summary>
        ///     属性值ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     商品 Guid
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        ///     SkuId
        /// </summary>
        public long ProductSkuId { get; set; }

        /// <summary>
        ///     商品数量
        /// </summary>
        [Display(Name = "商品数量")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long Count { get; set; }
    }
}