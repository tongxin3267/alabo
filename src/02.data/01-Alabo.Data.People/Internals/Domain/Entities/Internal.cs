﻿using Alabo.Data.People.UserTypes;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Data.People.Internals.Domain.Entities
{
    /// <summary>
    /// 内部合伙人
    /// </summary>
    [ClassProperty(Name = "内部合伙人")]
    [BsonIgnoreExtraElements]
    [Table("People_ParentInternal")]
    public class Internal : UserTypeAggregateRoot<Internal>
    {
    }
}