using System;
using System.Collections.Generic;
using System.Text;
using Alabo.Data.People.UserTypes;
using Alabo.Web.Mvc.Attributes;
using Dapper.Contrib.Extensions;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Data.People.Merchants.Domain.Entities
{
    /// <summary>
    /// 门店、线下门店
    /// </summary>

    [ClassProperty(Name = "门店")]
    [BsonIgnoreExtraElements]
    [Table("People_Merchant")]
    [AutoDelete(IsAuto = true)]
    public class Merchant : UserTypeAggregateRoot<Merchant>
    {
    }
}