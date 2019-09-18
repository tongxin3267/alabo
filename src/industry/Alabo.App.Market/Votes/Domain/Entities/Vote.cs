using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using Alabo.App.Market.UserRightss.Domain.Entities;
using Alabo.Domains.Entities;

namespace Alabo.App.Market.Votes.Domain.Entities {

    /// <summary>
    /// 投票
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Market_Vote")]
    public class Vote : AggregateMongodbUserRoot<Vote> {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}