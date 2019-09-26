using System.Text;
using Alabo.Helpers;

namespace Alabo.Events.Messages
{
    /// <summary>
    ///     消息事件
    /// </summary>
    public class MessageEvent : Event, IMessageEvent
    {
        /// <summary>
        ///     事件数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        ///     发送目标
        /// </summary>
        public string Target { get; set; }

        /// <summary>
        ///     回调
        /// </summary>
        public string Callback { get; set; }

        /// <summary>
        ///     输出日志
        /// </summary>
        public override string ToString()
        {
            var result = new StringBuilder();
            result.AppendLine($"事件标识: {Id}");
            result.AppendLine($"事件时间:{Extensions.Extensions.ToMillisecondString(Time)}");
            if (string.IsNullOrWhiteSpace(Target) == false) result.AppendLine($"发送目标:{Target}");

            if (string.IsNullOrWhiteSpace(Callback) == false) result.AppendLine($"回调:{Callback}");

            result.Append($"事件数据：{Json.ToJson(Data)}");
            return result.ToString();
        }
    }
}