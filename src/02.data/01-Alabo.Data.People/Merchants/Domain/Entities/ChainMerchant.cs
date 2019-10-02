using System;
using System.Collections.Generic;
using System.Text;
using Alabo.Data.People.UserTypes;
using Alabo.Web.Mvc.Attributes;
using Dapper.Contrib.Extensions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Data.People.Merchants.Domain.Entities
{
    /// <summary>
    /// 连锁门店
    /// </summary>

    [ClassProperty(Name = "连锁门店")]
    [BsonIgnoreExtraElements]
    [Table("People_ChainMerchant")]
    [AutoDelete(IsAuto = true)]
    public class ChainMerchant : UserTypeAggregateRoot<ChainMerchant>
    {
        /// <summary>
        /// 连锁门店Id
        /// </summary>
        public ObjectId MerchantId { get; set; }
    }
}