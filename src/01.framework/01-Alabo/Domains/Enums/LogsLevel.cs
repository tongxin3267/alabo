using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Domains.Enums
{
    /// <summary>
    ///     系统日志级别
    /// </summary>
    [ClassProperty(Name = "系统日志级别")]
    public enum LogsLevel
    {
        [LabelCssClass(BadgeColorCalss.Info)]
        [Display(Name = "信息")]
        Information = 1,

        [LabelCssClass(BadgeColorCalss.Warning)]
        [Display(Name = "警告")]
        Warning = 2,

        [LabelCssClass(BadgeColorCalss.Danger)]
        [Display(Name = "错误")]
        Error = 3,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "成功")]
        Success = 10

        //Critical = 5,
        //None = 6
    }
}