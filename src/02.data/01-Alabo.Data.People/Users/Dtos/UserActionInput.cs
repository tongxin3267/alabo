using Alabo.Domains.Query.Dto;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Data.People.Users.Dtos
{
    /// <summary>
    ///     用户操作类型
    /// </summary>
    public class UserActionInput : ApiInputDto
    {
        /// <summary>
        ///     操作类型
        /// </summary>
        [Required(ErrorMessage = "操作类型必须输入")]
        public string Type { get; set; }
    }
}