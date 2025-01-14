﻿using Alabo.Data.People.UserTypes;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Data.People.ServiceCenters.Domain.Entities
{
    /// <summary>
    /// 合作公司
    /// </summary>

    [ClassProperty(Name = "合作公司")]
    [BsonIgnoreExtraElements]
    [Table("People_ServiceCenter")]
    [AutoDelete(IsAuto = true)]
    public class ServiceCenter : UserTypeAggregateRoot<ServiceCenter>
    {
    }
}