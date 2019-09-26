using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Query.Dto;

namespace Alabo.Core.WebApis.Dtos {

    public class DiyBaseInput : ApiInputDto {

        /// <summary>
        ///     Diy对应的Key
        /// </summary>
        [Display(Name = "Diy对应的Key")]
        [Required(ErrorMessage = "请输入Diykey")]
        public string DiyKey { get; set; }
    }
}