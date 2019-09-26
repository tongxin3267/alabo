using Alabo.Framework.Core.Enums.Enum;

namespace Alabo.App.Asset.Pays.Dtos {

    public class PayCallBack {

        /// <summary>
        /// 支付Id
        /// </summary>
        public long PayId { get; set; }

        /// <summary>
        ///     支付成功以后前端跳转的Url
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 会员OpenId
        /// </summary>
        public string OpenId { get; set; }

        /// <summary>
        /// 客户端类型
        /// </summary>
        public ClientType ClientType { get; set; }
    }
}