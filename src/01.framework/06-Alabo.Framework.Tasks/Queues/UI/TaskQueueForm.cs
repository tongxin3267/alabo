using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Framework.Tasks.Queues.Domain.Entities;
using Alabo.Framework.Tasks.Queues.Domain.Enums;
using Alabo.Framework.Tasks.Queues.Domain.Servcies;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.UI.Design.AutoForms;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Tasks.Queues.UI
{
    /// <summary>
    ///     任务队列
    /// </summary>
    [ClassProperty(Name = "后台任务队列", Icon = IconFontawesome.dedent, Description = "后台任务队列",
        ListApi = "Api/TaskQueue/TaskQueueList",
        PageType = ViewPageType.List, PostApi = "Api/TaskQueue/TaskQueueList",
        SideBarType = SideBarType.TaskQueueSideBar)]
    public class TaskQueueForm : UIBase, IAutoForm
    {
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

        /// <summary>
        ///     转换成Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            var taskQueueId = ToId<long>(id);
            var taskQueueView = Resolve<ITaskQueueService>().GetViewById(taskQueueId);
            var model = AutoMapping.SetValue<TaskQueueForm>(taskQueueView);
            return ToAutoForm(model);
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            var cartView = AutoMapping.SetValue<TaskQueue>(model);
            var result = Resolve<ITaskQueueService>().AddOrUpdate(cartView);
            return new ServiceResult(result);
        }
    }
}