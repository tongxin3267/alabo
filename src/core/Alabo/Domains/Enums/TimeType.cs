using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Domains.Enums
{
    /// <summary>
    ///     分润分期方式 目前无效 待用
    ///     http://www.w3school.com.cn/sql/func_datepart.asp
    ///     https://www.cnblogs.com/hyd1213126/p/5828464.html
    /// </summary>
    [ClassProperty(Name = "分润分期方式")]
    public enum TimeType
    {
        /// <summary>
        ///     小时
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Field(Mark = "")] [Display(Name = "全部")]
        NoLimit = 1,

        /// <summary>
        ///     小时
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Field(Mark = "hh")] [Display(Name = "小时")]
        Hours = 2,

        /// <summary>
        ///     天
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Field(Mark = "dd")] [Display(Name = "天")]
        Day = 3,

        /// <summary>
        ///     星期
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Field(Mark = "wk")] [Display(Name = "星期")]
        Week = 4,

        /// <summary>
        ///     月
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Field(Mark = "mi")] [Display(Name = "月")]
        Month = 5,

        /// <summary>
        ///     季度
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Field(Mark = "mi")] [Display(Name = "季度")]
        Quarter = 6,

        /// <summary>
        ///     半年
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Field(Mark = "")] [Display(Name = "半年")]
        HalfYear = 7,

        /// <summary>
        ///     半年
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Field(Mark = "yy")] [Display(Name = "年")]
        Year = 8,

        /// <summary>
        ///     分钟
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Field(Mark = "mi")] [Display(Name = "分钟")]
        Minute = 11,

        /// <summary>
        ///     自定义时间
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "自定义")]
        Customer = 100
    }
}