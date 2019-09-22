using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Tasks.Domain.Entities {

    /// <summary>
    ///     后台作业任务
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Task_Schedule")]
    [ClassProperty(Name = "后台作业任务", Icon = IconFlaticon.file, Description = "后台作业任务,在后台系统执行的时间",
        SideBarType = SideBarType.ControlSideBar)]
    public class Schedule : AggregateMongodbRoot<Schedule> {

        /// <summary>
        ///     任务名称
        /// </summary>
        [Display(Name = "任务名称")]
        [Field(ControlsType = ControlsType.Numberic, IsShowAdvancedSerach = true,
            EditShow = true, Width = "150", ListShow = true, IsMain = true, IsShowBaseSerach = true, SortOrder = 1)]
        public string Name { get; set; }

        /// <summary>
        ///     类型
        /// </summary>
        [Display(Name = "类型")]
        [Field(ControlsType = ControlsType.Numberic, IsShowAdvancedSerach = true,
            EditShow = true, Width = "350", ListShow = true, IsMain = true, IsShowBaseSerach = true, SortOrder = 2000)]
        public string Type { get; set; }

        /// <summary>
        ///     间隔(秒)
        /// </summary>
        [Display(Name = "间隔(秒)")]
        [Field(ControlsType = ControlsType.Numberic, Width = "90", ListShow = true, SortOrder = 20)]
        public long Delay { get; set; } = 0;

        /// <summary>
        ///     最近记录
        /// </summary>
        [Field(ControlsType = ControlsType.ChildList,
            GroupTabId = 1, Width = "120", ListShow = true, SortOrder = 2000)]
        [Display(Name = "最近记录")]
        public List<ScheduleHistory> Historys { get; set; } = new List<ScheduleHistory>();

        /// <summary>
        ///     已经执行的次数
        /// </summary>
        [Display(Name = "已执行次数")]
        [Field(ControlsType = ControlsType.Numberic, EditShow = true,
            Width = "90", ListShow = true, IsMain = true, SortOrder = 50)]
        public int ExecutionTimes { get; set; } = 0;

        /// <summary>
        ///     执行的日期和时间，对于多次执行的任务，为最后一次执行时间
        /// </summary>
        [Display(Name = "最后执行时间")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, SortOrder = 101)]
        public DateTime HandleTime { get; set; } = DateTime.Now.AddYears(-100);
    }

    /// <summary>
    ///     历史记录
    /// </summary>
    [ClassProperty(Name = "最近记录", Icon = IconFlaticon.menu, SortOrder = 1, Description = "系统保留最近100条记录",
        SideBarType = SideBarType.SinglePageSideBar)]
    public class ScheduleHistory : BaseViewModel {

        /// <summary>
        ///     执行结果
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox,
            GroupTabId = 1, Width = "180", ListShow = true, SortOrder = 1)]
        [Display(Name = "执行结果")]
        public QueueStatus Status { get; set; }

        /// <summary>
        ///     消息
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox,
            GroupTabId = 1, Width = "380", ListShow = true, SortOrder = 2)]
        [Display(Name = "消息")]
        public string Message { get; set; }

        /// <summary>
        ///     执行时间
        /// </summary>
        [Field(IsShowBaseSerach = true, IsShowAdvancedSerach = true, ControlsType = ControlsType.TextBox,
            TableDispalyStyle = TableDispalyStyle.Code,
            GroupTabId = 1, Width = "180", ListShow = true, SortOrder = 2)]
        [Display(Name = "执行时间")]
        public DateTime CreateTime { get; set; }
    }
}