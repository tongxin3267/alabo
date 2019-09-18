namespace Alabo.App.Core.User.Domain.Dtos {

    /// <summary>
    /// 第一次设支付密码
    /// </summary>
    public class FirstSetPassword {
        public long UserId { get; set; }

        public string PayPassword { get; set; }

        public string Code { get; set; }

        public string Mobile { get; set; }
    }
}