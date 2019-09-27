using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Data.People.Cities.Domain.Enums
{
    /// <summary>
    /// 城市线分类
    /// </summary>
    public enum CityLineType
    {
        /// <summary>
        /// 一线城市
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "一线城市")]
        One = 1,

        /// <summary>
        /// 一线城市
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "二线城市")]
        Two = 2,

        /// <summary>
        /// 一线城市
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "三线城市")]
        Three = 3,

        /// <summary>
        /// 一线城市
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Metal)]
        [Display(Name = "四线城市")]
        Four = 4,
    }
}