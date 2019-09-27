using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Data.People.Cities.Domain.CallBacks;
using Alabo.Data.People.UserTypes;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Data.People.Cities.Domain.Entities
{
    /// <summary>
    /// 城市代理、城市合伙人
    /// </summary>
    [ClassProperty(Name = "城市合伙人")]
    [BsonIgnoreExtraElements]
    [AutoDelete(IsAuto = true)]
    [Table("People_City")]
    public class City : UserTypeAggregateRoot<City>
    {
    }
}