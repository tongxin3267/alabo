using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Cloud.Contracts.Domain.Entities
{
    [BsonIgnoreExtraElements]
    [Table("Contract_ContractRecord")]
    [ClassProperty(Name = "电子合同", Icon = "fa fa-cog", SortOrder = 1)]
    public class ContractRecord : AggregateMongodbUserRoot<ContractRecord>
    {
        /// <summary>
        ///     电子合同Id
        /// </summary>
        public ObjectId ContractId { get; set; }
    }
}