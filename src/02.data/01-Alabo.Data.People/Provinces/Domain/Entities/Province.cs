using Alabo.Data.People.UserTypes;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

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