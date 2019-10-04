using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum {

    [ClassProperty(Name = "数据类型")]
    public enum ZkCloudDateType {

        #region 数值为10-100,基本控件

        /// <summary>
        ///     文本类型
        /// </summary>
        [Display(Name = "String")]
        [LabelCssClass(BadgeColorCalss.Success)]
        String = 0,

        /// <summary>
        ///     整数类型
        /// </summary>
        [Display(Name = "Long")]
        [LabelCssClass(BadgeColorCalss.Success)]
        Long = 1,

        /// <summary>
        ///     小数类型
        /// </summary>
        [Display(Name = "Decimal")]
        [LabelCssClass(BadgeColorCalss.Success)]
        Decimal = 2,

        /// <summary>
        ///     时间类型
        /// </summary>
        [Display(Name = "DateTime")]
        [LabelCssClass(BadgeColorCalss.Success)]
        DateTime = 3,

        // <summary>
        /// 判断类型
        /// </summary>
        [Display(Name = "Bool")]
        [LabelCssClass(BadgeColorCalss.Success)]
        Bool = 4,

        /// <summary>
        ///     枚举类型
        /// </summary>
        [Display(Name = "EnumType")]
        [LabelCssClass(BadgeColorCalss.Success)]
        EnumType = 5,

        /// <summary>
        ///     枚举类型
        /// </summary>
        [Display(Name = "Guid")]
        [LabelCssClass(BadgeColorCalss.Success)]
        Guid = 6,

        /// <summary>
        ///     特殊类型
        /// </summary>
        [Display(Name = "DeleteStates")]
        [LabelCssClass(BadgeColorCalss.Success)]
        DeleteStates = 100,

        /// <summary>
        ///     外键
        /// </summary>
        [Display(Name = "ForeignKey")]
        [LabelCssClass(BadgeColorCalss.Success)]
        ForeignKey = 101,

        #endregion 数值为10-100,基本控件

        /// <summary>
        ///     特殊类型
        /// </summary>
        [Display(Name = "SpecialType")]
        [LabelCssClass(BadgeColorCalss.Success)]
        SpecialType = -1
    }
}