using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Framework.Core.Enums.Enum
{
    [ClassProperty(Name = "区时")]
    public enum ZoneTime
    {
        /// <summary>
        ///     (UTC-12:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Dateline Standard Time -12:00")]
        UtcSubtract12 = -12,

        /// <summary>
        ///     (UTC-11:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Coordinated Universal Time -11:00")]
        UtcSubtract11 = -11,

        /// <summary>
        ///     (UTC-10:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Aleutian Standard Time -10:00")]
        UtcSubtract10 = -10,

        /// <summary>
        ///     (UTC-9:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Alaskan Standard Time -9:00")]
        UtcSubtract9 = -9,

        /// <summary>
        ///     (UTC-8:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Pacific Standard Time -8:00")]
        UtcSubtract8 = -8,

        /// <summary>
        ///     (UTC-7:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Mountain Standard Time -7:00")]
        UtcSubtract7 = -7,

        /// <summary>
        ///     (UTC-6:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Central Standard Time -6:00")]
        UtcSubtract6 = -6,

        /// <summary>
        ///     (UTC-5:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "SA Pacific Standard Time -5:00")]
        UtcSubtract5 = -5,

        /// <summary>
        ///     (UTC-4:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Paraguay Standard Time -4:00")]
        UtcSubtract4 = -4,

        /// <summary>
        ///     (UTC-3:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Tocantins Standard Time -3:00")]
        UtcSubtract3 = -3,

        /// <summary>
        ///     (UTC-2:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Mid-Atlantic Standard Time -2:00")]
        UtcSubtract2 = -2,

        /// <summary>
        ///     (UTC-1:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Azores Standard Time -1:00")]
        UtcSubtract1 = -1,

        /// <summary>
        ///     (UTC)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "UTC")]
        Utc = 0,

        /// <summary>
        ///     (UTC+1:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Central Europe Standard Time +1:00")]
        UtcAdd1 = 1,

        /// <summary>
        ///     (UTC+2:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Jordan Standard Time +2:00")]
        UtcAdd2 = 2,

        /// <summary>
        ///     (UTC+3:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Arab Standard Time +3:00")]
        UtcAdd3 = 3,

        /// <summary>
        ///     (UTC+4:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Arabian Standard Time +4:00")]
        UtcAdd4 = 4,

        /// <summary>
        ///     (UTC+5:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Ekaterinburg Standard Time +5:00")]
        UtcAdd5 = 5,

        /// <summary>
        ///     (UTC+6:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Central Asia Standard Time +6:00")]
        UtcAdd6 = 6,

        /// <summary>
        ///     (UTC+7:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "SE Asia Standard Time +7:00")]
        UtcAdd7 = 7,

        /// <summary>
        ///     (UTC+8:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "China Standard Time +8:00")]
        UtcAdd8 = 8,

        /// <summary>
        ///     (UTC+9:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Korea Standard Time +9:00")]
        UtcAdd9 = 9,

        /// <summary>
        ///     (UTC+10:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "West Pacific Standard Time +10:00")]
        UtcAdd10 = 10,

        /// <summary>
        ///     (UTC+11:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Norfolk Standard Time +11:00")]
        UtcAdd11 = 11,

        /// <summary>
        ///     (UTC+12:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "New Zealand Standard Time +12:00")]
        UtcAdd12 = 12,

        /// <summary>
        ///     (UTC+13:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Tonga Standard Time +13:00")]
        UtcAdd13 = 13,

        /// <summary>
        ///     (UTC+14:00)
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)] [Display(Name = "Line Islands Standard Time +14:00")]
        UtcAdd14 = 14
    }
}