using Alabo.Web.Mvc.Attributes;

namespace Alabo.Schedules.Enum {

    [ClassProperty(Name = "任务计划调用")]
    public enum TaskScheduleInvokeType : byte {

        /// <summary>
        ///     同步
        /// </summary>
        Sync = 1,

        /// <summary>
        ///     异步
        /// </summary>
        Async
    }
}