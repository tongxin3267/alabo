using System.Collections.Generic;

namespace Alabo.Tables.Dtos
{
    /// <summary>
    ///     v2响应数据
    /// </summary>
    public class SmsEntity
    {
        /// <summary>
        ///     结果代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     结果说明
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     成功数量
        /// </summary>
        public string Total { get; set; }

        /// <summary>
        ///     返回的短信响应实体
        /// </summary>
        public IList<Smses> Smses { get; set; }

        /// <summary>
        ///     当前账户余额（条）
        /// </summary>
        public string Balance { get; set; }
    }

    /// <summary>
    ///     返回的短信响应实体
    /// </summary>
    public class Smses
    {
        /// <summary>
        ///     手机号
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        ///     消息ID，后续状态报告的返回以消息ID与手机号码作为依据，消息ID与手机号码确定唯一短信发送
        /// </summary>
        public string SmsId { get; set; }

        /// <summary>
        ///     客户自定义消息ID，手机号码作为依据，确定唯一
        /// </summary>
        public string CustomSmsId { get; set; }
    }
}