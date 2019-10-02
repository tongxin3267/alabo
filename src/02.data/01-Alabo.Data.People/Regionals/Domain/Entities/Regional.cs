using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Alabo.Data.People.UserTypes;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Data.People.Regionals.Domain.Entities
{
    /// <summary>
    /// 分公司
    /// </summary>

    [ClassProperty(Name = "分公司")]
    [BsonIgnoreExtraElements]
    [Table("People_Regional")]
    [AutoDelete(IsAuto = true)]
    public class Regional : UserTypeAggregateRoot<Regional>
    {
    }
}