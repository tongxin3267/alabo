using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities.Extensions;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Linq.Dynamic;
using Alabo.Users.Entities;
using Alabo.Validations;
using Newtonsoft.Json;

namespace Alabo.App.Asset.Pays.Domain.Entities.Extension
{
    /// <summary>
    ///     支付订单的扩展实体
    /// </summary>
    public class PayExtension : EntityExtension
    {
        /// <summary>
        ///     支付成功后跳转链接
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is group buy.
        ///     是否为拼团购买商品
        /// </summary>
        public bool IsGroupBuy { get; set; } = false;

        /// <summary>
        ///     是否来自于订购页面
        ///     订单从前台 /Order/Goods页面而来
        /// </summary>
        public bool IsFromOrder { get; set; } = false;

        /// <summary>
        ///     Gets or sets the buyer count.
        ///     拼团达标人数
        /// </summary>
        public long BuyerCount { get; set; } = 0;

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is group over.
        ///     拼团是否结束,0表示拼团没有结束，大于0表示拼团已结束
        ///     在支付时候：更新转态
        /// </summary>
        public long GroupOverId { get; set; } = 0;

        /// <summary>
        ///     商户订单号,64个字符以内、可包含字母、数字、下划线；需保证在商户端不重复
        /// </summary>
        [Display(Name = "订单号")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string TradeNo { get; set; }

        /// <summary>
        ///     订单标题
        /// </summary>
        [Display(Name = "订单标题")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Subject { get; set; }

        /// <summary>
        ///     订单总金额，单位为元，精确到小数点后两位，取值范围[0.01,100000000]。
        /// </summary>
        [Display(Name = "订单总金额")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Range(0, 100000, ErrorMessage = "金额不合法")]
        [JsonProperty("total_amount")]
        public decimal TotalAmount { get; set; }

        /// <summary>
        ///     支付时候的提示文字
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        ///     订单描述
        /// </summary>
        [Display(Name = "订单描述")]
        [Required(ErrorMessage = "订单描述")]
        public string Body { get; set; }

        /// <summary>
        ///     销售产品码，与支付宝签约的产品码名称。  注：目前仅支持FAST_INSTANT_TRADE_PAY
        /// </summary>
        [Display(Name = "销售产品码")]
        public string ProductCode { get; set; }

        /// <summary>
        ///     商户通知地址，口碑发消息给商户通知其是否对商品创建、修改、变更状态成功
        /// </summary>
        [Display(Name = "商户通知地址")]
        public string NotifyUrl { get; set; }

        /// <summary>
        ///     回调到商户的url地址
        /// </summary>
        [Display(Name = "商户回调地址")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     微信支付的时候，需要用到
        /// </summary>

        [Display(Name = "OpenId")]
        public string OpenId { get; set; }

        /// <summary>
        ///     支付的用户名，体系类数据
        /// </summary>
        [Display(Name = "用户名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string UserName { get; set; }

        /// <summary>
        ///     支付成功后执行的方法
        ///     注意以下几点：否则无法执行
        ///     1. 类型必须为void
        ///     2. 参数
        /// </summary>
        public BaseServiceMethod AfterSuccess { get; set; }

        /// <summary>
        ///     返回Sql脚本对象
        ///     注意以下几点
        ///     1. 返回类型必须为List<String>
        /// </summary>
        public BaseServiceMethod ExcecuteSqlList { get; set; }

        /// <summary>
        ///     支付失败后执行的方法
        ///     注意以下几点：否则无法执行
        ///     1. 类型必须为void
        ///     2. 参数
        /// </summary>
        public BaseServiceMethod AfterFail { get; set; }

        /// <summary>
        ///     支付账单的人，实际订单人
        ///     比如：会员权益购买
        /// </summary>
        public User OrderUser { get; set; }

        public TriggerType TriggerType { get; set; }
    }
}