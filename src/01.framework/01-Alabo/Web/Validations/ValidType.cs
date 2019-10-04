using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Web.Validations {

    /// <summary>
    ///     RemoteAttribute 验证特性名称
    /// </summary>
    [ClassProperty(Name = "验证类型")]
    public enum ValidType {

        /// <summary>
        ///     邮箱验证
        /// </summary>
        [Display(Name = "邮箱验证", Description = "")]
        Email = 1,

        /// <summary>
        ///     手机验证
        /// </summary>
        Moblie = 2,

        /// <summary>
        ///     身份证号
        /// </summary>
        IdentityCard = 3,

        /// <summary>
        ///     整数
        /// </summary>
        Digits = 4,

        /// <summary>
        ///     整数或小数，比如10.02,12等
        /// </summary>
        Decimal = 5,

        /// <summary>
        ///     Url
        /// </summary>
        Url = 6,

        /// <summary>
        ///     The ip
        /// </summary>
        Ip = 7,

        /// <summary>
        ///     验证用户名格式
        ///     判断用户名是否存在，以及用户名状态是否正常
        /// </summary>
        UserName = 58,

        /// <summary>
        ///     验证推荐人是否存在
        ///     如果推荐人名称为空的时候，不验证，不为空时验证
        /// </summary>
        ParentUserName = 59,

        /// <summary>
        ///     自定义正则表达式
        /// </summary>
        Customer = 100
    }
}