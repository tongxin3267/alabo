using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Data.People.ShareHolders.Configs;
using Alabo.Data.People.UserTypes;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Data.People.ShareHolders.Domain.Entities
{
    /// <summary>
    /// 股东
    /// </summary>
    [ClassProperty(Name = "股东")]
    [BsonIgnoreExtraElements]
    [AutoDelete(IsAuto = true)]
    [Table("People_ShareHolder")]
    public class ShareHolder : UserTypeAggregateRoot<ShareHolder>
    {
    }
}