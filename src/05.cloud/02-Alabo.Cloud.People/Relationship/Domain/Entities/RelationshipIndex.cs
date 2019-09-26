using System;
using System.ComponentModel.DataAnnotations.Schema;
using MongoDB.Bson.Serialization.Attributes;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Market.Relationship.Domain.Entities {

    /// <summary>
    ///     会员关系网
    ///     后期可拓展各种用户类型关系网
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Cloud_People_RelationshipIndex")]
    [ClassProperty(Name = "会员关系网", Description = "查看会员的关系网", Icon = IconFlaticon.route,
        SideBarType = SideBarType.RelationshipIndexSideBar)]
    public class RelationshipIndex : AggregateMongodbUserRoot<RelationshipIndex> {

        /// <summary>
        ///     配置类型
        ///     与UserRelationshipIndexConfig相互对应
        /// </summary>
        public Guid ConfigId { get; set; }

        /// <summary>
        ///     上级用户Id
        /// </summary>
        public long ParentId { get; set; } = 0;
    }
}