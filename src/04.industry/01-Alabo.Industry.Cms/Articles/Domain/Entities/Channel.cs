using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Industry.Cms.Articles.Domain.Entities {

    /// <summary>
    ///     频道
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("CMS_Channel")]
    [ClassProperty(Name = "频道")]
    public class Channel : AggregateMongodbRoot<Channel> {

        // <summary>
        ///     频道名称
        /// </summary>
        [Display(Name = "频道名称")]
        public string Name { get; set; }

        /// <summary>
        ///     频道类型
        /// </summary>
        [Display(Name = "频道类型")]
        public ChannelType ChannelType { get; set; } = ChannelType.Article;

        /// <summary>
        ///     频道包含字段的 Json 数据
        ///     Alabo.App.Core.Common.Domain.CallBacks.DataField
        ///     List<DataField>
        /// </summary>
        [Display(Name = "频道包含字段的 Json 数据")]
        public string FieldJson { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        [Display(Name = "图标")]
        public string Icon { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        public Status Status { get; set; }

        /// <summary>
        ///     排序
        /// </summary>
        [Display(Name = "排序")]
        public long SortOrder { get; set; }
    }
}