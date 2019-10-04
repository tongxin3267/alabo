using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum {

    [ClassProperty(Name = "大小关系")]
    public enum QueryEnum {

        /// <summary>
        ///     等于
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "等于")]
        Equal = 0,

        /// <summary>
        ///     小于
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "小于")]
        LessThan = 1,

        /// <summary>
        ///     大于
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "大于")]
        GreaterThan = 2,

        /// <summary>
        ///     小于等于
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "小于等于")]
        LessThanOrEqual = 3,

        /// <summary>
        ///     大于等于
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "大于等于")]
        GreaterThanOrEqual = 4,

        /// <summary>
        ///     不等于
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "不等于")]
        NotEqual = 5,

        /// <summary>
        ///     开头等于
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "开头等于")]
        StartsWith = 6,

        /// <summary>
        ///     结尾等于
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "结尾等于")]
        EndsWith = 7,

        /// <summary>
        ///     处理Like的问题
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "包括")]
        Contains = 8,

        /// <summary>
        ///     处理In的问题
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "In")]
        StdIn = 9
    }
}