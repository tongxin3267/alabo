using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Data.People.UserTypes;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Data.People.Internals.Domain.Entities
{
    /// <summary>
    /// 内部合伙人
    /// </summary>
    [ClassProperty(Name = "内部合伙人")]
    [BsonIgnoreExtraElements]
    [Table("People_ParentInternal")]
    public class ParentInternal : UserTypeAggregateRoot<ParentInternal>
    {
    }
}