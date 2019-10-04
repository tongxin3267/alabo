using Alabo.Domains.Query.Dto;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.WebApis.Dtos {

    public class DiyBaseInput : ApiInputDto {

        /// <summary>
        ///     Diy对应的Key
        /// </summary>
        [Display(Name = "Diy对应的Key")]
        [Required(ErrorMessage = "请输入Diykey")]
        public string DiyKey { get; set; }
    }
}