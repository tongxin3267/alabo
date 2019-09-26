namespace Alabo.App.Share.OpenTasks.Base {

    /// <summary>
    /// 模板信息
    /// </summary>
    public class TemplateRule {

        /// <summary>
        /// 日志模板
        /// </summary>
        public string LoggerTemplate { get; set; } = "会员{OrderUserName}消费，会员{ShareUserName}{AccountName}账户获得资产，金额为:{ShareAmount}";

        public string SmsTemplate { get; set; } = "会员{OrderUserName}消费，会员{ShareUserName}{AccountName}账户获得资产，金额为:{ShareAmount}";

        /// <summary>
        /// 是否开启短信通知
        /// </summary>

        public bool SmsNotification { get; set; } = false;
    }
}