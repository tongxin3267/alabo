using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.Deliveries.Domain.Enums {

    /// <summary>
    ///     发货时间
    /// </summary>
    [ClassProperty(Name = "发货时间")]
    public enum DeliveryTimeType {

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "4小时内")]
        DeliveryTimeTypea = 0,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "8小时内")]
        DeliveryTimeTypeb = 1,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "12小时内")]
        DeliveryTimeTypec = 2,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "16小时内")]
        DeliveryTimeTyped = 3,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "20小时内")]
        DeliveryTimeTypee = 4,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "1天内")]
        DeliveryTimeTypef = 5,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "2天内")]
        DeliveryTimeTypeg = 6
    }
}