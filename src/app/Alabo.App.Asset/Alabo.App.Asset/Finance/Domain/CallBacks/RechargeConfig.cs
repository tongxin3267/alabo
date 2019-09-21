using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Finance.Domain.CallBacks {

    [ClassProperty(Name = "充值设置", Icon = "fa fa-plus-square", Description = "充值设置", SortOrder = 23,
        SideBarType = SideBarType.RechargeSideBar)]
    [NotMapped]
    public class RechargeConfig : AutoConfigBase, IAutoConfig {

        [Field(ControlsType = ControlsType.Switch, ListShow = true)]
        [Display(Name = "开启充值")]
        [HelpBlock("是否允许会员充值")]
        [Required]
        public bool Openrecharge { get; set; } = true;

        [Field(ControlsType = ControlsType.Switch, ListShow = true)]
        [Display(Name = "开启线上支付")]
        [HelpBlock("是否允许会员线上支付")]
        [Required]
        public bool Openonlinepayment { get; set; } = true;

        [Field(ControlsType = ControlsType.Switch, ListShow = true)]
        [Display(Name = "开启银行转账")]
        [HelpBlock("是否允许会员银行转账")]
        [Required]
        public bool Openbanktransfer { get; set; } = true;

        [Field(ControlsType = ControlsType.Switch, ListShow = true)]
        [Display(Name = "开启微信转账")]
        [HelpBlock("是否允许会员微信转账")]
        [Required]
        public bool OpenWeChattransfer { get; set; } = true;

        [Field(ControlsType = ControlsType.Switch, ListShow = true)]
        [Display(Name = "开启支付宝转账")]
        [HelpBlock("是否允许会员支付宝转账")]
        [Required]
        public bool OpenAlipaytransfer { get; set; } = true;

        [Field(ControlsType = ControlsType.Numberic, ListShow = true)]
        [Display(Name = "充值最小金额")]
        [HelpBlock("最小充值金额")]
        [Required]
        public decimal RechargeAmountMin { get; set; }

        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [Display(Name = "收款微信号")]
        [HelpBlock("请输入收款微信号，默认值为14554111")]
        [Required]
        public string ReceivablesMicroSignal { get; set; }

        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [Display(Name = "收款微信账户名")]
        [HelpBlock("请输入收款微信账户名，默认值为中酷")]
        [Required]
        public string ReceivingmicroName { get; set; }

        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [Display(Name = "收款支付宝账户")]
        [HelpBlock("请输入收款支付宝账户，默认值为48945")]
        [Required]
        public string PayPalaccountreceivable { get; set; }

        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [Display(Name = "收款支付宝名称")]
        [HelpBlock("请输入收款支付宝名称，默认值为中酷")]
        [Required]
        public string ReceivablesAlipayName { get; set; }

        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [Display(Name = "收款银行信息")]
        [HelpBlock("收款银行信息，公司收款银行卡信息")]
        [Required]
        public string ReceivablesBankInto { get; set; }

        [Field(ControlsType = ControlsType.Numberic, ListShow = true)]
        [Display(Name = "手续费")]
        [HelpBlock("手续费，0.1为10%")]
        [Required]
        public decimal ServiceRate { get; set; } = 0M;

        public void SetDefault() {
        }
    }
}