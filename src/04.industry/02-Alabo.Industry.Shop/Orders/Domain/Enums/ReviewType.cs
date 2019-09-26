using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.Orders.Domain.Enums {

    /// <summary>
    ///     评价方式 好评、中评、差评
    /// </summary>
    [ClassProperty(Name = "评价方式")]
    public enum ReviewType {

        /// <summary>
        ///     好评
        /// </summary>
        HightReview = 1,

        /// <summary>
        ///     中评
        /// </summary>
        MiddleReview = 2,

        /// <summary>
        ///     差评
        /// </summary>
        BadReview = 3
    }
}