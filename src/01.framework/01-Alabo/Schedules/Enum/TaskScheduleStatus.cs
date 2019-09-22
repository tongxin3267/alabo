using Alabo.Web.Mvc.Attributes;

namespace Alabo.Schedules.Enum
{
    [ClassProperty(Name = "任务计划状态")]
    public enum TaskScheduleStatus : byte
    {
        Running = 1,
        Stoped,
        Idle,
        Suspend,
        Unknown = 255
    }
}