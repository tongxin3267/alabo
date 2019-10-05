using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Cloud.Wikis.Domain.Entities
{
    /// <summary>
    ///     迭代
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Wikis_Wiki")]
    [ClassProperty(Name = "Wiki", Description = "Wiki")]
    public class Wiki : AggregateMongodbUserRoot<Wiki>
    {
    }
}