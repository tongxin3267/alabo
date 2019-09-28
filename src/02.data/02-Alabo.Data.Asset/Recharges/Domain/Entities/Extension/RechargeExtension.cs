using Alabo.App.Asset.Pays.Domain.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.App.Asset.Recharges.Domain.Entities.Extension
{
    /// <summary>
    ///     充值表
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Finace_TradeRecharge")]
    [ClassProperty(Name = "充值管理", Icon = "fa fa-puzzle-piece", Description = "充值管理", PageType = ViewPageType.List)]
    public class RechargeExtension : BaseViewModel
    {
        /// <summary>
        ///     支付记录
        ///     线上支付时，支付记录必须填写，线下汇款不需要填写
        /// </summary>
        public Pay Pay { get; set; }

        public decimal CheckAmount { get; set; }

        /// <summary>
        ///     手续费
        /// </summary>
        [Display(Name = "手续费")]
        public decimal Fee { get; set; }

        /// <summary>
        ///     Gets or sets the bank 类型.
        ///     汇款银行
        ///     线下充值时，汇款银行必须填写
        /// </summary>
        [Display(Name = "汇款银行")]
        public BankType BankType { get; set; }

        /// <summary>
        ///     银行卡号
        ///     线下充值时，汇款银行必须填写
        /// </summary>
        [Display(Name = "银行卡号")]
        public string BankNumber { get; set; }

        /// <summary>
        ///     持卡人姓名
        /// </summary>
        [Display(Name = "持卡人姓名")]
        public string BankName { get; set; }
    }
}