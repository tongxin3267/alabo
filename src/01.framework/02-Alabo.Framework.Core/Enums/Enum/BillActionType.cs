using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum
{
    /// <summary>
    ///     账单操作类型
    ///     可以在BillTypeConfig中自行启用或者增加
    ///     100-200之间为减少 颜色danger
    ///     200-300之间为增加
    ///     300-400自定义
    /// </summary>
    [ClassProperty(Name = "账单操作类型")]
    public enum BillActionType
    {
        /// <summary>
        ///     消费
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "消费")]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000101")]
        Shopping = 101,

        /// <summary>
        ///     理财
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "理财")]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000102")]
        Finance = 102,

        /// <summary>
        ///     转出
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "转出")]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000103")]
        TransferOut = 103,

        /// <summary>
        ///     还款
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "还款")]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000105")]
        Repayment = 105,

        /// <summary>
        ///     缴费
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "缴费")]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000106")]
        PayCost = 106,

        /// <summary>
        ///     捐款
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "捐款")]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000107")]
        Donation = 107,

        /// <summary>
        ///     提现
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000110")]
        [Display(Name = "提现")]
        Withdraw = 110,

        /// <summary>
        ///     系统减少
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000120")]
        [Display(Name = "系统减少")]
        SystemReduce = 120,

        /// <summary>
        ///     人为减少
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000130")]
        [Display(Name = "人为减少")]
        People = 130,

        /// <summary>
        ///     冻结
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000140")]
        [Display(Name = "冻结")]
        Treeze = 140,

        /// <summary>
        ///     解冻
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000150")]
        [Display(Name = "解冻")]
        DeductTreeze = 150,

        /// <summary>
        ///     充值
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000201")]
        [Display(Name = "充值")]
        Recharge = 201,

        /// <summary>
        ///     转入
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000202")]
        [Display(Name = "转入")]
        TransferIn = 202,

        /// <summary>
        ///     分润
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000203")]
        [Display(Name = "分润")]
        FenRun = 203,

        /// <summary>
        ///     系统增加
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000210")]
        [Display(Name = "系统增加")]
        SystemIncrease = 210,

        /// <summary>
        ///     人为增加
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000220")]
        [Display(Name = "人为增加")]
        PeopleIncrease = 220,

        /// <summary>
        ///     重置授信
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "重置授信")]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000304")]
        ResetAccount = 304,

        /// <summary>
        ///     报单注册
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "报单注册")]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000310")]
        Declaration = 310,

        /// <summary>
        ///     升级扣款
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "升级扣款")]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000320")]
        UpgradeDeductions = 320,

        /// <summary>
        ///     销售
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "销售")]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000330")]
        Sale = 330,

        /// <summary>
        ///     佣金捐款
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "佣金捐款")]
        [Field(IsDefault = true, GuidId = "E0000000-1478-49BD-BFC7-E00000000340")]
        Commission = 340,

        /// <summary>
        ///     自定义
        /// </summary>
        [Display(Name = "自定义")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Custommer = -1
    }
}