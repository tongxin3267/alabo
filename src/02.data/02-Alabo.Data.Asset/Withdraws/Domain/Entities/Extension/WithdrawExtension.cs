using Alabo.App.Asset.BankCards.Domain.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Asset.Withdraws.Domain.Entities.Extension
{
    /// <summary>
    ///     充值表
    /// </summary>
    [BsonIgnoreExtraElements]
    [ClassProperty(Name = "提现扩展", Icon = "fa fa-puzzle-piece", Description = "充值管理", PageType = ViewPageType.List)]
    public class WithdrawExtension : BaseViewModel
    {
        /// <summary>
        ///     银行卡信息
        /// </summary>
        public BankCard BankCard { get; set; }

        /// <summary>
        ///     会员备注
        /// </summary>
        [Display(Name = "会员备注")]
        public string UserRemark { get; set; }

        /// <summary>
        ///     ss
        ///     备注，此备注表示管理员备注，前台会员不可以修改
        /// </summary>
        [Display(Name = "备注")]
        public string Remark { get; set; }

        /// <summary>
        ///     失败原因
        /// </summary>
        public string FailuredReason { get; set; }
    }
}