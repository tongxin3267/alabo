using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Domains.Services.Report.Enums
{
    /// <summary>
    ///     报表运算符,支持等于和不等于两种方式即可
    /// </summary>
    public enum ReportOperator
    {
        /// <summary>
        ///     等于
        /// </summary>
        [Display(Name = "等于")] [Field(Mark = "==")]
        Equal = 1,

        /// <summary>
        ///     不等于
        /// </summary>
        [Display(Name = "不等于")] [Field(Mark = "!=")]
        NotEqual = 2
    }
}