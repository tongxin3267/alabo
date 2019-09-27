using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Data.People.UserTypes;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Data.People.Counties.Domain.Entities
{
    [ClassProperty(Name = "区县合伙人")]
    [BsonIgnoreExtraElements]
    [AutoDelete(IsAuto = true)]
    [Table("People_County")]
    public class County : UserTypeAggregateRoot<County>
    {
    }
}