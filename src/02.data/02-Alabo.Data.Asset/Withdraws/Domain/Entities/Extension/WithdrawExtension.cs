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
    }
}