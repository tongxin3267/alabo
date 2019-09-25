using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Reports.Domain.Entities {

    /// <summary>
    ///     通用配置
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Basic_Report")]
    [ClassProperty(Name = "通用配置")]
    public class Report : AggregateMongodbRoot<Report> {

        /// <summary>
        ///     所属应用名称
        /// </summary>
        [Display(Name = "所属应用名称")]
        public string AppName { get; set; }

        /// <summary>
        ///     配置键名
        /// </summary>
        [Display(Name = "配置键名")]
        public string Type { get; set; }

        /// <summary>
        ///     配置值（json）
        /// </summary>
        [Display(Name = "配置值")]
        public string Value { get; set; }

        /// <summary>
        ///     统计说明
        /// </summary>
        [Display(Name = "统计说明")]
        public string Summary { get; set; }

        /// <summary>
        ///     最后更新时间
        /// </summary>
        [Display(Name = "最后更新时间")]
        public DateTime LastUpdated { get; set; }
    }
}