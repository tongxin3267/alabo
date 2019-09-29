using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.App.Share.Rewards.Domain.Enums
{
    [ClassProperty(Name = "封顶限制")]
    public enum UpperLimitType
    {
        /// <summary>
        ///     没有封顶
        /// </summary>
        [Display(Name = "无")] No = 1,

        /// <summary>
        ///     日封顶
        /// </summary>
        [Display(Name = "日封顶")] Days = 2,

        /// <summary>
        ///     周封顶
        /// </summary>
        [Display(Name = "周封顶")] Weeks = 3,

        /// <summary>
        ///     月封顶
        /// </summary>
        [Display(Name = "月封顶")] Months = 4,

        /// <summary>
        ///     年封顶
        /// </summary>
        [Display(Name = "年封顶")] Years = 5,

        /// <summary>
        ///     无限期
        /// </summary>
        [Display(Name = "无限期")] Undefined = 6
    }
}