using _01_Alabo.Cloud.Core.SendSms.Domain.Enums;

namespace _01_Alabo.Cloud.Core.SendSms.Dtos
{
    public class SmsInput
    {
        /// <summary>
        ///     类型
        /// </summary>
        public SmsType Type { get; set; } = SmsType.Phone;

        /// <summary>
        ///     手机号码 逗号隔开
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        ///     消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     发送状态
        /// </summary>
        public SendState State { get; set; } = SendState.Root;
    }
}