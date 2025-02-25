﻿using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Cloud.Wikis.Wikis.Domain.Entities
{
    /// <summary>
    ///     迭代
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Wikis_WikiHistory")]
    [ClassProperty(Name = "Wiki", Description = "Wiki")]
    public class WikiHistory : AggregateMongodbUserRoot<WikiHistory>
    {
        /// <summary>
        /// Wiki
        /// </summary>
        public Wiki Wiki { get; set; }
    }
}