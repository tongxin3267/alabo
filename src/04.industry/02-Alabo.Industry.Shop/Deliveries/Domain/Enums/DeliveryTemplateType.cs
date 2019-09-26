using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Industry.Shop.Deliveries.Domain.Enums {

    [ClassProperty(Name = "买卖方所选运费模板类型")]
    public enum DeliveryTemplateType {

        /// 买家承担运费的模版
        /// </summary>
        [Display(Name = "买家承担运费的模版")]
        [LabelCssClass("text-default")]
        BuyerBearTheFreight = 0,

        // 卖家家承担运费的模版
        /// </summary>
        [Display(Name = "卖家承担运费的模版")]
        [LabelCssClass("text-default")]
        SellerBearTheFreight = 1
    }
}