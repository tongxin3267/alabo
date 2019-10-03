using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Industry.Shop.Activitys.Domain.Entities;
using Alabo.Industry.Shop.Activitys.Dtos;
using Alabo.UI.Design.AutoForms;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using Microsoft.AspNetCore.Http;

namespace Alabo.Industry.Shop.Activitys.Modules.PreSells.Model
{
    /// <summary>
    ///     预售活动设置
    /// </summary>
    [ClassProperty(Name = "预售活动设置")]
    public class PreSellsActivity : BaseViewModel, IActivity
    {
        /// <summary>
        ///     预售价格
        /// </summary>
        [Display(Name = "预售价格")]
        [Field(ControlsType = ControlsType.Decimal, ListShow = true, EditShow = true)]
        [HelpBlock("请输入您准备预售的商品价格，商品价格与市场价差距应不高于的5%")]
        public decimal PreSellPrice { get; set; }

        /// <summary>
        ///     预售开始时间
        /// </summary>
        [Display(Name = "预售开始时间")]
        [Field(ControlsType = ControlsType.DateTimePicker, ListShow = true, EditShow = true)]
        [HelpBlock("请设置您准备开始的预售时间，确定预售开始后，预售商品供应量充足")]
        public DateTime PreSellStartTime { get; set; }

        /// <summary>
        ///     预售结束时间
        /// </summary>
        [Display(Name = "预售结束时间")]
        [Field(ControlsType = ControlsType.DateTimePicker, ListShow = true, EditShow = true)]
        [HelpBlock("设置您准备结束预售的时间，预售结束时间与预售开始时间相隔不宜低于24小时")]
        public DateTime PreSellEndTime { get; set; }

        /// <summary>
        ///     固定发货时间
        /// </summary>
        [Display(Name = "发货时间")]
        [Field(ControlsType = ControlsType.DateTimePicker, ListShow = true, EditShow = true)]
        [HelpBlock("请设置您的发货时间，发货时间应在订单付款后的24小时内，若在节假日发货时间不宜超过付款后的72小时")]
        public DateTime DeliverTime { get; set; }

        public AutoForm GetAutoForm(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     get default value
        /// </summary>
        /// <returns></returns>
        public object GetDefaultValue(ActivityEditInput activityEdit, Activity activity)
        {
            return null;
        }

        public ServiceResult SetValue(HttpContext httpContext)
        {
            throw new NotImplementedException();
        }

        public ServiceResult SetValueOfRule(object rules)
        {
            var model = rules.ToObject<PreSellsActivity>();
            if (model == null) {
                return ServiceResult.FailedWithMessage("活动规则数据异常");
            }

            if (model.PreSellStartTime >= model.PreSellEndTime) {
                return ServiceResult.FailedWithMessage("预售开始时间必须小于且不等于结束时间");
            }

            return new ServiceResult(true);
        }
    }
}