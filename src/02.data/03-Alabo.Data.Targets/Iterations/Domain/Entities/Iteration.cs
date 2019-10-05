using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Cloud.Targets.Iterations.Domain.Entities
{
    /// <summary>
    ///     迭代
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Target_Iteration")]
    [ClassProperty(Name = "迭代", Description = "目标迭代")]
    public class Iteration : AggregateMongodbUserRoot<Iteration>
    {
    }
}