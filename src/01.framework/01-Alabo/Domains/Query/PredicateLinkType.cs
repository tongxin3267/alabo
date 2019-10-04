using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Domains.Query {

    [ClassProperty(Name = "且  或")]
    public enum PredicateLinkType {

        /// <summary>
        ///     并且
        /// </summary>
        [Display(Name = "并且(and)")]
        [Field(Mark = "&&")]
        And = 1,

        /// <summary>
        ///     或者
        /// </summary>
        [Display(Name = "或者(Or)")]
        [Field(Mark = "||")]
        Or = 2
    }
}