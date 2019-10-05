using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Data.Targets.Targets.Domain.Entities
{
    /// <summary>
    ///     目标事务历史记录
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Target_TargetHistory")]
    [ClassProperty(Name = "历史记录", Description = "目标事务历史记录")]
    public class TargetHistory : AggregateMongodbRoot<TargetHistory>
    {
        /// <summary>
        /// 目标Id
        /// </summary>
        public ObjectId TargetId { get; set; }

        /// <summary>
        /// 目标
        /// </summary>
        public Target Target { get; set; }
    }
}