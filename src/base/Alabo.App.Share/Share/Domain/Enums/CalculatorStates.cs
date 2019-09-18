using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Share.Share.Domain.Enums {

    [ClassProperty(Name = "启用状态")]
    public enum CalculatorStates {

        /// <summary>
        /// 启用
        /// </summary>
        [Display(Name = "启用")]
        [LabelCssClass(BadgeColorCalss.Success)]
        Q = 0,

        /// <summary>
        /// 停用
        /// </summary>
        [Display(Name = "停用")]
        [LabelCssClass(BadgeColorCalss.Warning)]
        T = 1,
    }
}