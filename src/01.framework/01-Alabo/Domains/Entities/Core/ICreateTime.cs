using System;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Domains.Entities.Core {

    public interface ICreateTime {

        /// <summary>
        ///     创建时间
        /// </summary>
        [Display(Name = "创建时间")]
        DateTime CreateTime { get; set; }
    }
}