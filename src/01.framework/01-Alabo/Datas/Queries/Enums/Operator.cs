using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Datas.Queries.Enums
{
    /// <summary>
    ///     查询操作符
    /// </summary>
    [ClassProperty(Name = "查询操作符")]
    public enum Operator
    {
        /// <summary>
        ///     等于
        /// </summary>
        [Display(Name = "等于")]
        [Field(Mark = "==")]
        Equal = 1,

        /// <summary>
        ///     不等于
        /// </summary>
        [Display(Name = "不等于")]
        [Field(Mark = "!=")]
        NotEqual = 2,

        /// <summary>
        ///     大于
        /// </summary>
        [Display(Name = "大于")]
        [Field(Mark = ">>")]
        Greater = 3,

        /// <summary>
        ///     大于等于
        /// </summary>
        [Display(Name = "大于等于")]
        [Field(Mark = ">=")]
        GreaterEqual = 4,

        /// <summary>
        ///     小于
        /// </summary>
        [Display(Name = "小于")]
        [Field(Mark = "<<")]
        Less = 5,

        /// <summary>
        ///     小于等于
        /// </summary>
        [Display(Name = "小于等于")]
        [Field(Mark = "<=")]
        LessEqual = 6,

        /// <summary>
        ///     头匹配
        /// </summary>
        [Display(Name = "头匹配")]
        [Field(Mark = "s%")]
        Starts = 10,

        /// <summary>
        ///     尾匹配
        /// </summary>
        [Display(Name = "尾匹配")]
        [Field(Mark = "e%")]
        Ends = 11,

        /// <summary>
        ///     模糊匹配
        /// </summary>
        [Display(Name = "模糊匹配")]
        [Field(Mark = "c%")]
        Contains = 12
    }
}