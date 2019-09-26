using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Framework.Basic.Grades.Domain.Entities {

    /// <summary>
    ///     用户升级记录
    /// </summary>
    [ClassProperty(Name = "升级记录", Icon = IconFontawesome.registered, SortOrder = 1, PageType = ViewPageType.List, PostApi = "Api/UpgradeRecord/UpgradeList", ListApi = "Api/UpgradeRecord/UpgradeList",
        SideBarType = SideBarType.GradeInfoSideBar)]
    [BsonIgnoreExtraElements]
    [Table("Task_UpgradeRecord")]
    public class UpgradeRecord : AggregateMongodbUserRoot<UpgradeRecord> {

        /// <summary>
        /// 升级记录类型
        /// </summary>
        [Display(Name = "升级类型")]
        [Field(SortOrder = 12, ListShow = true, Width = "150")]
        public UpgradeType Type { get; set; }

        /// <summary>
        ///     升级前等级名称
        /// </summary>
        [Field(DisplayMode = DisplayMode.Grade, SortOrder = 12, ListShow = true, Width = "150")]
        [Display(Name = "升级前等级")]
        public Guid BeforeGradeId { get; set; }

        /// <summary>
        ///     升级后等级名称
        /// </summary>
        [Field(DisplayMode = DisplayMode.Grade, SortOrder = 14, ListShow = true, Width = "150")]
        [Display(Name = "升级后等级名称")]
        public Guid AfterGradeId { get; set; }

        /// <summary>
        /// 关联队列Id
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 20, ListShow = true, Width = "150")]
        [Display(Name = "关联队列Id")]
        public long QueueId { get; set; }
    }
}