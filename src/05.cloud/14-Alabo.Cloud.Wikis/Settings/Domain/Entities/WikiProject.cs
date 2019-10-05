using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Cloud.Wikis.Settings.Domain.Entities
{
    /// <summary>
    ///     迭代
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Wikis_WikiProject")]
    [ClassProperty(Name = "Wiki", Description = "Wiki")]
    public class WikiProject : AggregateMongodbUserRoot<WikiProject>
    {
    }
}