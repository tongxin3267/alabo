namespace Alabo.Data.People.Users.Dtos
{
    /// <summary>
    ///     检测手机号码的输入
    /// </summary>
    public class CheckMobileInput
    {
        /// <summary>
        ///     手机号码
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        ///     用户id
        /// </summary>
        public long UserId { get; set; }
    }
}