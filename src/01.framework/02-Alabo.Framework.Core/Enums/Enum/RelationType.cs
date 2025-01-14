﻿using Alabo.Web.Mvc.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Framework.Core.Enums.Enum {

    [ClassProperty(Name = "关联类型")]
    public enum RelationType {

        /// <summary>
        ///     分类类型
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "分类类型")]
        ClassRelation = 1,

        /// <summary>
        ///     标签类型
        /// </summary>
        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "标签类型")]
        TagRelation = 2
    }
}