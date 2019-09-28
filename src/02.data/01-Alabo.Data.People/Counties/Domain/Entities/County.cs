using Alabo.Data.People.UserTypes;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Data.People.Counties.Domain.Entities
{
    [ClassProperty(Name = "区县合伙人")]
    [BsonIgnoreExtraElements]
    [AutoDelete(IsAuto = true)]
    [Table("People_County")]
    public class County : UserTypeAggregateRoot<County>
    {
    }
}