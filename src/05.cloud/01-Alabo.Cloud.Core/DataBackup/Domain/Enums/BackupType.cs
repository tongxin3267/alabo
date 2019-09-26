using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace _01_Alabo.Cloud.Core.DataBackup.Domain.Enums {

    /// <summary>
    ///     备份方式
    /// </summary>
    [ClassProperty(Name = "备份方式")]
    public enum BackupType {

        /// <summary>
        ///     增量备份
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "增量备份")]
        Increment = 1,

        /// <summary>
        ///     完全备份
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "完全备份")]
        Completely = 2
    }
}