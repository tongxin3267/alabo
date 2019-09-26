using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Validations;

namespace Alabo.Data.People.Employes.Dtos
{
    /// <summary>
    /// 获取Token
    /// </summary>
    public class GetLoginToken {

        /// <summary>
        /// 站点Id
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string SiteId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long UserId { get; set; }

        /// <summary>
        /// 用户Url
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string AdminUrl { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public double Timestamp { get; set; }

        /// <summary>
        /// Ip地址
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Ip { get; set; }

        /// <summary>
        /// 租户
        /// </summary>
        public string Tenant { get; set; }

        public string Token { get; set; }
    }
}