using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Industry.Shop.Activitys.Domain.Entities;
using Alabo.Industry.Shop.Activitys.Domain.Enum;
using Alabo.Industry.Shop.Activitys.Dtos;
using Alabo.Industry.Shop.Activitys.Extensions;
using Alabo.UI.Design.AutoForms;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using Microsoft.AspNetCore.Http;

namespace Alabo.Industry.Shop.Activitys.Modules.TimeLimitBuy.Model
{
    /// <summary>
    ///     限时购
    /// </summary>
    [ClassProperty(Name = "限时购")]
    [ActivityModule(Name = "拼团", ActivityType = ActivityType.BehaviorActivity, Icon = "fa fa-recycle",
        IsSupportSigleProduct = true, IsSupportSku = true, IsSupportMaxStock = true,
        BackGround = "dashboard-stat yellow-crusta  ",
        Intro = "配置拼团策略，人多优惠大。",
        FatherId = 130001
    )]
    public class TimeLimitBuyActivity : BaseViewModel, IActivity
    {
        /// <summary>
        ///     预售开始时间
        /// </summary>
        [Display(Name = "开始时间")]
        [Field(ControlsType = ControlsType.DateTimePicker, ListShow = true, EditShow = true)]
        [HelpBlock("请设置您准备开始的抢购时间，抢购时间应预售活动结束后")]
        public DateTime StartTime { get; set; }

        /// <summary>
        ///     预售结束时间
        /// </summary>
        [Display(Name = "结束时间")]
        [Field(ControlsType = ControlsType.DateTimePicker, ListShow = true, EditShow = true)]
        [HelpBlock("请设置您准备结束抢购的时间，抢购结束时间与抢购开始时间相隔不宜低于24小时")]
        public DateTime EndTime { get; set; }

        public AutoForm GetAutoForm(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     get default value
        /// </summary>
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
            var model = rules.ToObject<TimeLimitBuyActivity>();
            if (model == null) return ServiceResult.FailedWithMessage("活动规则数据异常");
            if (model.StartTime >= model.EndTime) return ServiceResult.FailedWithMessage("抢购开始时间必须小于且不等于结束时间");
            return new ServiceResult(true);
        }
    }
}