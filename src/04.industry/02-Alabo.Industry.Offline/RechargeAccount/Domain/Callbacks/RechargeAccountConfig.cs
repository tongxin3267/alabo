using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Alabo.AutoConfigs;
using Alabo.Core.WebApis;
using Alabo.Core.WebUis.Design.AutoForms;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Offline.RechargeAccount.Domain.Callbacks
{
    /// <summary>
    /// 储值规则
    /// </summary>
    [ClassProperty(Name = "储值规则", Description = "储值规则", PageType = ViewPageType.List)]
    public class RechargeAccountConfig : BaseViewModel, IAutoConfig
    {
        /// <summary>
        /// Id
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, EditShow = false)]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, IsShowBaseSerach = true)]
        [Display(Name = "描述")]
        [HelpBlock("请您输入描述")]
        public string Intro { get; set; }

        /// <summary>
        /// 储值金额
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = true)]
        [Display(Name = "储值金额")]
        [HelpBlock("请您输入储值金额")]
        public decimal StoreAmount { get; set; }

        /// <summary>
        /// 到账金额
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic,ListShow = true,EditShow = true)]
        [Display(Name = "到账金额")]
        [HelpBlock("请您输入到账金额")]
        public decimal ArriveAmount { get; set; }

        /// <summary>
        /// 赠送消费额
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = true)]
        [Display(Name = "赠送消费额")]
        [HelpBlock("请您输入赠送消费额")]
        public decimal GiveChangeAmount { get; set; }

        /// <summary>
        /// 赠送优惠券
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic, ListShow = false, EditShow = false)]
        [Display(Name = "赠送优惠券")]
        [HelpBlock("请您输入赠送优惠券")]
        public decimal GiveBuyAmount { get; set; } = 0m;


        /// <summary>
        /// 赠送积分
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, EditShow = true)]
        [Display(Name = "赠送积分")]
        [HelpBlock("请您输入赠送积分")]
        public decimal DiscountAmount { get; set; }


        public AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            AutoForm autoForm = new AutoForm();
            autoForm.AlertText = "用户可通过多种途径获取积分，积分可用于积分商城消费";
            autoForm.ButtomHelpText = new List<string>
            {
                "积分来源：一、本店出品消费；二、当面付支付；三、管理员手台手动添加；四、促销活动，比如大转盘、签到、红包雨等；",
                "消费用途：积分商城消费",
                "消费用途：积分商城消费",
                "消费用途：积分商城消费，消费用途：积分商城消费",
                "消费用途：积分商城消费，消费用途：积分商城消费",
                "消费用途：积分商城消费，消费用途：积分商城消费",
            };

            // 操作PriceStyleConfig
            return autoForm;
        }


        public void SetDefault()
        {

        }
    }
}
