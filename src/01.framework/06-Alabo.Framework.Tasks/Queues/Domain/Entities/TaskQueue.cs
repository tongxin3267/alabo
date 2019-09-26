using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Datas.Ef.SqlServer;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Tasks.Queues.Domain.Enums;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alabo.Framework.Tasks.Queues.Domain.Entities {

    /// <summary>
    ///     任务队列
    /// </summary>
    [ClassProperty(Name = "后台任务队列", Icon = IconFontawesome.dedent, Description = "后台任务队列", ListApi = "Api/TaskQueue/TaskQueueList",
        PageType = ViewPageType.List, PostApi = "Api/TaskQueue/TaskQueueList",
        SideBarType = SideBarType.TaskQueueSideBar)]
    [Table("Task_TaskQueue")]
    public class TaskQueue : AggregateDefaultUserRoot<TaskQueue> {
        [Display(Name = "模块标识")] public Guid ModuleId { get; set; }

        /// <summary>
        ///     队列执行类型
        /// </summary>
        [Display(Name = "执行类型")]
        [Field(ControlsType = ControlsType.DropdownList, EditShow = true, Width = "100", ListShow = true,
            SortOrder = 3)]
        public TaskQueueType Type { get; set; } = TaskQueueType.Once;

        [Display(Name = "参数")] public string Parameter { get; set; } = string.Empty;

        /// <summary>
        ///     内容
        /// </summary>
        [Display(Name = "内容")]
        public string Message { get; set; } = string.Empty;

        /// <summary>
        ///     已经执行的次数
        /// </summary>
        [Display(Name = "已执行次数")]
        [Field(ControlsType = ControlsType.Numberic, IsShowAdvancedSerach = true, IsTabSearch = true, EditShow = true,
            Width = "90", ListShow = true, IsMain = true, SortOrder = 500)]
        public int ExecutionTimes { get; set; } = 0;

        /// <summary>
        ///     最大执行次数限制
        /// </summary>
        [Display(Name = "最大执行次数")]
        [Field(ControlsType = ControlsType.Numberic, IsShowAdvancedSerach = true, IsTabSearch = true, EditShow = true,
            Width = "90", ListShow = true, IsMain = true, SortOrder = 600)]
        public int MaxExecutionTimes { get; set; } = 1;

        /// <summary>
        ///     状态
        ///     是否已经处理完毕（多次任务以达到任务次数为标准，无限期任务则一直为false）
        /// </summary>
        [Display(Name = "状态")]
        [Field(ControlsType = ControlsType.DropdownList, IsShowAdvancedSerach = true, IsTabSearch = true,
            DataSource = "Alabo.App.Core.Tasks.Domain.Enums.QueueStatus", EditShow = true, Width = "100",
            ListShow = true, SortOrder = 3)]
        public QueueStatus Status { get; set; } = QueueStatus.Pending;

        /// <summary>
        ///     设定的执行时间
        /// </summary>
        [Display(Name = "执行时间")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, SortOrder = 10003)]
        public DateTime ExecutionTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     执行的日期和时间，对于多次执行的任务，为最后一次执行时间
        /// </summary>
        [Display(Name = "结束时间")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, SortOrder = 10005)]
        public DateTime HandleTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     分润名称
        /// </summary>
        [NotMapped]
        [Display(Name = "分润名称")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "120",
            TableDispalyStyle = TableDispalyStyle.Code, ListShow = true, EditShow = true,
            SortOrder = 1)]
        public string ModuleName { get; set; }
    }

    /// <summary>
    /// Task_TaskQueue
    /// </summary>
    public class TaskQueueTableMap : MsSqlAggregateRootMap<TaskQueue> {

        protected override void MapTable(EntityTypeBuilder<TaskQueue> builder) {
            builder.ToTable("Task_TaskQueue");
        }

        protected override void MapProperties(EntityTypeBuilder<TaskQueue> builder) {
            //应用程序编号
            builder.HasKey(e => e.Id);
            builder.Ignore(e => e.UserName);
            builder.Ignore(e => e.ModuleName);
            builder.Ignore(e => e.Version);
        }
    }
}