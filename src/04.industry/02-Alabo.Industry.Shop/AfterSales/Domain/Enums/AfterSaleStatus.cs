using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.AfterSales.Domain.Enums
{
    /// <summary>
    /// 暂时只支持换货
    /// </summary>
    [ClassProperty(Name = "申请售后状态")]
    public enum AfterSaleStatus
    {
        /// <summary>
        ///     买家申请售后，进入售后审核
        /// </summary>
        [Display(Name = "申请售后中")]
        [LabelCssClass(BadgeColorCalss.Danger)]
        ApplyForAfterSale =0,


        /// <summary>
        ///     买家撤销售后,撤销后无法继续享受售后服务
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "撤销售后")]
        BuyerCancelAfterSale = 1,


        /// <summary>
        ///     卖家已经同意
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "已申请售后")]
        ApplyAfterSale = 2,
         
        /// <summary>
        ///     完成售后
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "已完成售后")]
        Refunded = 3




    }
}
