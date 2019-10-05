using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Cloud.Wikis.Settings.Domain.Entities
{
    /// <summary>
    ///     项目
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Wikis_WikiProject")]
    [ClassProperty(Name = "Wiki", Description = "Wiki")]
    public class WikiProject : AggregateMongodbUserRoot<WikiProject>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
    }
}