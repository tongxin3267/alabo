using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Share.Tasks.Enums {

    [ClassProperty(Name = "有效水品")]
    public enum EffectivityLevel {

        /// <summary>
        /// 全部
        /// </summary>
        [Display(Name = "全部")]
        All = 0,

        /// <summary>
        /// 奇数代
        /// </summary>
        [Display(Name = "奇数代")]
        Odd = 1,

        /// <summary>
        /// 偶数代
        /// </summary>
        [Display(Name = "偶数代")]
        Even = 2
    }
}