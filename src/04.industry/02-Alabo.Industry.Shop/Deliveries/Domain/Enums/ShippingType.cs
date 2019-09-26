using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.Deliveries.Domain.Enums {

    /// <summary>
    ///     物流方式
    /// </summary>
    [ClassProperty(Name = "物流方式")]
    public enum ShippingType {

        /// <summary>
        ///     包含EMS、快递、平邮等
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "买家承担邮费")]
        BuyerPay = 0,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "卖家包邮")]
        SupplierPay = 1,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "虚拟发货")]
        Virtual = 2,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "预约配送")]
        Reserve = 3
    }
}