namespace Alabo.Data.People.Users.Dtos {

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