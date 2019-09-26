using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.App.Shop.Activitys.Domain.Entities;
using Alabo.App.Shop.Activitys.Dtos;
using Alabo.Core.WebUis.Design.AutoForms;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Maps;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Shop.Activitys.Modules.BuyPermision.Model
{
    /// <summary>
    /// 购买权限
    /// </summary>
    [ClassProperty(Name = "购买权限")]
    public class BuyPermisionActivity : BaseViewModel, IActivity
    {
        /// <summary>
        /// 单次最多购买
        /// </summary>
        [Display(Name = "单次最多购买")]
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = true)]
        [HelpBlock("用户单次购买此商品数量限制，0表示不限制")]
        public long SingleBuyCountMax { get; set; } = 0;

        /// <summary>
        /// 单次最低购买
        /// </summary>
        [Display(Name = "单次最低购买")]
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = true)]
        [HelpBlock("用户单次必须最少购买此商品数量限制，0表示不限制")]
        public long SingleBuyCountMin { get; set; } = 0;

        /// <summary>
        /// 用户购买此商品数量限制
        /// </summary>
        [Display(Name = "最多购买")]
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = true)]
        [HelpBlock("用户购买过的此商品数量限制（包括历史订单），0表示不限制")]
        public long TotalBuyCountMax { get; set; } = 0;

        /// <summary>
        /// 会员等级浏览权限
        /// </summary>
        [Display(Name = "会员等级浏览权限")]
        [Field(ControlsType = ControlsType.CheckBoxMultipl, ApiDataSource = "Api/Common/GetKeyValuesByAutoConfig?type=UserGradeConfig", ListShow = true, EditShow = true)]
        [HelpBlock("不设置默认全部会员等级")]
        public List<Guid> MemberLeverViewPermissions { get; set; }

        /// <summary>
        /// 会员等级购买权限
        /// </summary>
        [Display(Name = "会员等级购买权限")]
        [Field(ControlsType = ControlsType.CheckBoxMultipl , ApiDataSource = "Api/Common/GetKeyValuesByAutoConfig?type=UserGradeConfig", ListShow = true, EditShow = true)]
        [HelpBlock("不设置默认全部会员等级")]
        public List<Guid> MemberLeverBuyPermissions { get; set; }

        public AutoForm GetAutoForm(object obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// get default value
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
            var model = rules.ToObject<BuyPermisionActivity>();
            if (model == null)
            {
                return ServiceResult.FailedWithMessage("活动规则数据异常");
            }
            if (model.SingleBuyCountMax > 0 && (model.SingleBuyCountMin > model.SingleBuyCountMax))
            {
                return ServiceResult.FailedWithMessage("单次最低购买不能大于单次最多购买");
            }
            if (model.TotalBuyCountMax > 0 && (model.TotalBuyCountMax < model.SingleBuyCountMax))
            {
                return ServiceResult.FailedWithMessage("最多购买不能小于单次最多购买");
            }

            return new ServiceResult(true);
        }
    }
}