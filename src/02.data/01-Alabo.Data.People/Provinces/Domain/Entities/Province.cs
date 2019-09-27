using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Data.People.Cities.Domain.CallBacks;
using Alabo.Data.People.UserTypes;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Data.People.Provinces.Domain.Entities
{
    /// <summary>
    /// 省代理
    /// </summary>
    [ClassProperty(Name = "省合伙人")]
    [BsonIgnoreExtraElements]
    [Table("People_Province")]
    [AutoDelete(IsAuto = true)]
    public class Province : UserTypeAggregateRoot<Province>
    {
    }
}