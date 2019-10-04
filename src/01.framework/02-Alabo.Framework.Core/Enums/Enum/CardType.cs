using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum {

    [ClassProperty(Name = "身份卡类型")]
    public enum CardType {

        /// <summary>
        ///     身份证
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "身份证")]
        IdCard = 1,

        /// <summary>
        ///     护照
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "护照")]
        Passport = 2,

        /// <summary>
        ///     港澳通行证
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "港澳通行证")]
        Trafficpermit = 3,

        /// <summary>
        ///     营业执照
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "营业执照")]
        BusinessLicense = 4,

        /// <summary>
        ///     驾照
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "驾照")]
        DrivingLicense = 5
    }
}