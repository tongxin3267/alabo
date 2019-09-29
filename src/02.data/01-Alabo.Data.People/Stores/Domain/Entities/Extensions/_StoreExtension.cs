using Alabo.Domains.Entities.Extensions;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Data.People.Stores.Domain.Entities.Extensions
{
    /// <summary>
    ///     店铺拓展属性
    /// </summary>
    public class StoreExtension : EntityExtension
    {
        /// <summary>
        ///     店铺分类
        /// </summary>
        [Display(Name = "店铺分类")]
        public List<StoreCategory> StoreCategories { get; set; } = new List<StoreCategory>();

        /// <summary>
        ///     店铺所属的类目
        /// </summary>
        [Display(Name = "店铺所属的类目")]
        public List<Guid> CategoryIds { get; set; }

        /// <summary>
        ///     详细介绍
        /// </summary>
        [Field(ControlsType = ControlsType.TextArea, GroupTabId = 1, SortOrder = 15)]
        [Display(Name = "详细介绍")]
        [HelpBlock("详细介绍")]
        public string Intro { get; set; }
    }
}