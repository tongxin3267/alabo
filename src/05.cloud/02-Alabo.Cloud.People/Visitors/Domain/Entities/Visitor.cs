using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Cloud.People.Visitors.Domain.Enums;
using Alabo.Domains.Entities;
using Alabo.Users.Enum;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Cloud.People.Visitors.Domain.Entities {

    /// <summary>
    /// 游客表
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Cloud_People_Visitor")]
    public class Visitor : AggregateMongodbRoot<Visitor> {
        public string OpenId { get; set; }

        public string Avator { get; set; }

        public Sex Sex { get; set; }

        public string Country { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public long FatherId { get; set; }

        /// <summary>
        /// 访问次数
        /// </summary>
        public long VisitorCount { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public VisitorType Type { get; set; }

        /// <summary>
        /// 访问的URL
        /// </summary>
        public string Url { get; set; }
    }
}