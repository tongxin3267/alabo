using Alabo.Web.Mvc.Attributes;

namespace Alabo.Schedules.Enum
{
    [ClassProperty(Name = "任务计划类型")]
    public enum TaskScheduleType
    {
        Native = 1,
        Proxy
    }
}