using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.AutoConfigs;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Asset.Withdraws.Domain.Configs
{
    /// <summary>
    ///     Class WithdRawConfig.
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "提现设置", Icon = "fa fa-paw", PageType = ViewPageType.Edit, Description = "提现设置",
        SortOrder = 24, SideBarType = SideBarType.WithDrawSideBar)]
    public class WithddrawConfig : AutoConfigBase, IAutoConfig
    {
        /// <summary>
        ///     是否允许会员提现
        /// </summary>
        [Field(ControlsType = ControlsType.Switch)]
        [Display(Name = "允许提现")]
        [HelpBlock("是否允许会员提现")]
        public bool CanWithdRaw { get; set; } = true;

        /// <summary>
        ///     是否实名认证
        /// </summary>
        [Field(ControlsType = ControlsType.Switch)]
        [Display(Name = "提现前需实名认证")]
        [HelpBlock("提现前需实名认证")]
        public bool IsIdentity { get; set; } = true;

        /// <summary>
        ///     提现手续费（通用）
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox)]
        [HelpBlock("提现手续费（通用）")]
        [Display(Name = "手续费（通用）")]
        [Range(typeof(decimal), "0.00", "1", ErrorMessage = "提现手续费格式不正确")]
        public decimal Fee { get; set; } = 0.05m;

        /// <summary>
        ///     提现说明，将直接显示到会员提现页面
        /// </summary>
        [Field(ControlsType = ControlsType.TextArea)]
        [HelpBlock("提现说明，将直接显示到会员提现页面")]
        [Display(Name = "提现说明")]
        public string WithdRawIntro { get; set; } =
            @"1：提现（通用）手续费为5%。 2：提现成功后将在7个工作日内转款到您的银行卡上。 3：提现请确保您的银行卡信息是正确的。 4：提现请确保您的真实姓名、身份证、手机号码的准确性。";

        /// <summary>
        ///     不允许提现的原因
        /// </summary>
        [Field(ControlsType = ControlsType.TextArea)]
        [HelpBlock("不允许会员提现时候，会员提交提现申请的说明")]
        [Display(Name = "不允许提现的原因", Order = 10, GroupName = "基本设置", AutoGenerateField = false)]
        public string CanNotWithdrawIntro { get; set; } = "非提现时间，不允许会员提现，给您带来不便的抱歉深感遗憾";

        /// <summary>
        ///     最小提现额度
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic)]
        [HelpBlock("最小提现额度")]
        [Display(Name = "最小提现额度")]
        public decimal MinAmount { get; set; }

        /// <summary>
        ///     提现倍数
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic)]
        [Display(Name = "提现倍数")]
        [HelpBlock("限制提现额度为最小提现额度倍数,0表示提现额度不受倍数限制,比如100表示提现额度必须是100的倍数")]
        public decimal IsMinAmountMultiple { get; set; } = 100m;

        /// <summary>
        ///     当日最大提现额度
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic)]
        [HelpBlock("最大提现额度")]
        [Display(Name = "最大提现额度")]
        public decimal MaxAmount { get; set; } = 10000;

        /// <summary>
        ///     是否可连续体现
        /// </summary>
        [Field(ControlsType = ControlsType.Switch)]
        [Display(Name = "是否可连续提现")]
        [HelpBlock("即上一笔提现尚未完成则不可进行再次提现")]
        public bool CanSerialWithDraw { get; set; } = false;

        ///// <summary>
        ///// 提现扣除授信比例
        ///// </summary>
        //[Field(ControlsType = ControlsType.TextBox)]
        //[HelpBlock("比如客户的授信有2000元，提现有5000元，如果授信使用了600元，提现扣除授信比例是50%，则提现额度为=5000-600*0.5=4700，其中300还授信提现扣除授信比例")]
        //[Display(Name = "提现扣除授信比例")]
        //public decimal CredieDeductFee { get; set; } = 0.5m;

        /// <summary>
        ///     Sets the default.
        /// </summary>
        public void SetDefault()
        {
        }
    }
}