using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.WebUis;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Cloud.School.SmallTargets.Domain.Entities {

    /// <summary>
    ///     小目标
    /// </summary>
    [BsonIgnoreExtraElements][Table("Market_SmallTarget")]
    [ClassProperty(Name = "小目标", Description = "小目标", Icon = IconFlaticon.background,
        SideBarType = SideBarType.RelationshipIndexSideBar)]
    public class SmallTarget : AggregateMongodbRoot<SmallTarget> {

        /// <summary>
        ///     目标名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     图标,Logo
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///     图片
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        ///     目标简介
        /// </summary>
        public string Intro { get; set; }

        /// <summary>
        ///     完成后的动画图片
        /// </summary>
        public string CompleteImage { get; set; }
    }
}