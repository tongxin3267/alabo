using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Query.Dto;
using Alabo.Validations;

namespace Alabo.Framework.Basic.Storages.Dtos {

    /// <summary>
    ///     文件上传
    /// </summary>
    public class UploadApiInput : ApiInputDto {

        /// <summary>
        ///     文件保存路径
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string SavePath { get; set; } = "/uploads/api";

        /// <summary>
        ///     上传文件类型
        /// </summary>
        public string FileType { get; set; } = ".gif,.jpg,.jpeg,.png,.webp,.svg";

        /// <summary>
        ///     上传文件大小
        /// </summary>
        public int FileSize { get; set; }
    }
}