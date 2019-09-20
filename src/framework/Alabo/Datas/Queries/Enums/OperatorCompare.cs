using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Datas.Queries.Enums
{
    /// <summary>
    ///     比较操作符
    /// </summary>
    [ClassProperty(Name = "比较操作符")]
    public enum OperatorCompare
    {
        /// <summary>
        ///     等于
        /// </summary>
        [Display(Name = "等于")] [Field(Mark = "=")]
        Equal = 1,

        /// <summary>
        ///     不等于
        /// </summary>
        [Display(Name = "不等于")] [Field(Mark = "!=")]
        NotEqual = 2,

        /// <summary>
        ///     大于
        /// </summary>
        [Display(Name = "大于")] [Field(Mark = ">>")]
        Greater = 3,

        /// <summary>
        ///     大于等于
        /// </summary>
        [Display(Name = "大于等于")] [Field(Mark = ">=")]
        GreaterEqual = 4,

        /// <summary>
        ///     小于
        /// </summary>
        [Display(Name = "小于")] [Field(Mark = "<<")]
        Less = 5,

        /// <summary>
        ///     小于等于
        /// </summary>
        [Display(Name = "小于等于")] [Field(Mark = "<=")]
        LessEqual = 6
    }
}