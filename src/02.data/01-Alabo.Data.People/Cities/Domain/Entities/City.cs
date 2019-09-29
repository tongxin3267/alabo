using Alabo.Data.People.Cities.Domain.Enums;
using Alabo.Data.People.UserTypes;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Data.People.Cities.Domain.Entities
{
    /// <summary>
    /// 城市代理、城市合伙人
    /// </summary>
    [ClassProperty(Name = "城市合伙人")]
    [BsonIgnoreExtraElements]
    [AutoDelete(IsAuto = true)]
    [Table("People_City")]
    public class City : UserTypeAggregateRoot<City>
    {
        /// <summary>
        /// 城市线分类
        /// </summary>
        public CityLineType LineType { get; set; }
    }
}