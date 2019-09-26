using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Basic.Notifications
{
    public class MessageResult
    {
        public ResultType Type { get; set; }

        public string Message { get; set; }
    }

    [ClassProperty(Name = "返回类型")]
    public enum ResultType : byte
    {
        Success = 1,
        Faild,
        Error
    }
}