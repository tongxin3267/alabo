using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Cloud.People.GradeInfos.Domain.Entities {

    /// <summary>
    ///   用户等级信息
    /// </summary>
    [ClassProperty(Name = "用户等级信息", Icon = "fa fa-puzzle-piece", Description = "用户等级信息", PageType = ViewPageType.List, ListApi = "Api/GradeInfo/UserList",
        SideBarType = SideBarType.GradeInfoSideBar, PostApi = "Api/GradeInfo/UserList")]
    [BsonIgnoreExtraElements]
    [Table("Cloud_People_GradeInfo")]
    public class GradeInfo : AggregateMongodbUserRoot<GradeInfo> {

        /// <summary>
        ///     直推会员个数
        /// </summary>
        [Display(Name = "直推会员个数")]
        [Field(ListShow = true, Width = "120", TableDispalyStyle = TableDispalyStyle.Code, SortOrder = 100)]
        public long RecomendCount { get; set; }

        /// <summary>
        ///     直推会员
        /// </summary>
        [Field(ControlsType = ControlsType.ChildList,
            GroupTabId = 1, Width = "120", ListShow = true, SortOrder = 101)]
        [Display(Name = "直推等级信息")]
        public IList<GradeInfoItem> RecomendGradeInfo { get; set; }

        /// <summary>
        ///     间推会员个数
        /// </summary>
        [Display(Name = "间推会员个数")]
        [Field(ListShow = true, Width = "120", TableDispalyStyle = TableDispalyStyle.Code, SortOrder = 110)]
        public long SecondCount { get; set; }

        /// <summary>
        ///     间接推荐等级信息
        /// </summary>
        [Display(Name = "间接等级信息")]
        [Field(ControlsType = ControlsType.ChildList,
            GroupTabId = 1, Width = "120", ListShow = true, SortOrder = 111)]
        public IList<GradeInfoItem> SecondGradeInfo { get; set; }

        /// <summary>
        ///     团队会员个数
        /// </summary>
        [Display(Name = "团队会员个数")]
        [Field(ListShow = true, Width = "120", TableDispalyStyle = TableDispalyStyle.Code, SortOrder = 120)]
        public long TeamCount { get; set; }

        /// <summary>
        ///     团队等级信息
        /// </summary>
        [Display(Name = "团队等级信息")]
        [Field(ControlsType = ControlsType.ChildList,
            GroupTabId = 1, Width = "120", ListShow = true, SortOrder = 121)]
        public IList<GradeInfoItem> TeamGradeInfo { get; set; }

        /// <summary>
        ///     最后更新时间
        /// </summary>
        [Display(Name = "更新时间")]
        [Field(ControlsType = ControlsType.DateTimePicker, ListShow = true, EditShow = false, SortOrder = 10002,
            Width = "160")]
        public DateTime ModifiedTime { get; set; } = DateTime.Now;
    }

    /// <summary>
    ///     用户直推会员数量
    /// </summary>
    [ClassProperty(Name = "等级信息", SideBarType = SideBarType.GradeInfoSideBar)]
    public class GradeInfoItem : BaseViewModel {

        /// <summary>
        ///     等级Guid
        /// </summary>
        [Display(Name = "等级Guid")]
        [Field(DisplayMode = DisplayMode.Grade, Width = "120", ListShow = true, EditShow = false, SortOrder = 1)]
        public Guid GradeId { get; set; }

        /// <summary>
        ///     等级数量
        /// </summary>
        [Display(Name = "等级数量")]
        [Field(TableDispalyStyle = TableDispalyStyle.Code, ListShow = true, Width = "120", EditShow = false, SortOrder = 1)]
        public long Count { get; set; } = 0;

        /// <summary>
        ///     用户Id
        /// </summary>
        [Display(Name = "用户Id")]
        public IEnumerable<long> UserIds { get; set; } = new List<long>();
    }
}