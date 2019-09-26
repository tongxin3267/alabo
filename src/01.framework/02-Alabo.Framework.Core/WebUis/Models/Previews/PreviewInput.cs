using System.ComponentModel.DataAnnotations;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Core.WebUis.Models.Previews {

    [ClassProperty(Name = "链接")]
    public class PreviewInput {

        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Id { get; set; }

        /// <summary>
        ///     数据ID
        /// </summary>
        public string DataId { get; set; }

        /// <summary>
        ///     客户端当前登录用户的Id
        /// </summary>
        public long LoginUserId { get; set; }
    }
}