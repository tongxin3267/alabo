using Alabo.Validations;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.WebUis.Dtos {

    /// <summary>
    ///     view form
    /// </summary>
    public class AutoFormInput {

        /// <summary>
        ///     type
        /// </summary>
        [Display(Name = "表单类型")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Type { get; set; }

        /// <summary>
        ///     数据类型Json格式
        /// </summary>
        [Display(Name = "数据model")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Model { get; set; }

        /// <summary>
        ///     根据type 找到的类型
        /// </summary>
        [JsonIgnore]
        public Type TypeFind { get; set; }

        /// <summary>
        ///     根据type 找到的实列
        /// </summary>
        [JsonIgnore]
        public object TypeInstance { get; set; }

        /// <summary>
        ///     查找的类型
        /// </summary>
        [JsonIgnore]
        public object ModelFind { get; set; }
    }
}