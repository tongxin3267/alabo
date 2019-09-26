namespace _01_Alabo.Cloud.Core.SendSms.Domain.Enums {

    public enum SendState {

        /// <summary>
        /// 初始状态 未发送
        /// </summary>
        Root = 0,

        /// <summary>
        /// 发送成功
        /// </summary>
        Success = 1,

        /// <summary>
        /// 发送失败
        /// </summary>
        Fail = 2,

        /// <summary>
        /// 该状态无效 用于查询所有
        /// </summary>
        All = 3
    }
}