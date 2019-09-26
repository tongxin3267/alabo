using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Data.Things.Goodss.Domain.Entities
{
    [ClassProperty(Name = "产品线", Icon = "fa fa-puzzle-piece", Description = "将相同的产品归集到一起，不同的产品线可以参与不同的分润规则")]
    [AutoDelete(IsAuto = true)]
    [Table("Things_GoodsLine")]
    public class GoodsLine : AggregateMongodbRoot<GoodsLine>
    {
        /// <summary>
        ///     产品线名称
        /// </summary>
        [Required(ErrorMessage = "请填写产品线名称")]
        [Display(Name = "名称")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, IsShowBaseSerach = true,
            GroupTabId = 1, IsMain = true, Width = "150", ListShow = true, SortOrder = 2,
            Link = "/Admin/ProductLine/Edit?Id=[[Id]]")]
        [HelpBlock("请填写产品线的名称")]
        public string Name { get; set; }

        /// <summary>
        ///     产品线介绍
        /// </summary>
        [Required(ErrorMessage = "请填写产品线介绍")]
        [Field(ControlsType = ControlsType.TextArea, GroupTabId = 1, ListShow = true, Width = "400", SortOrder = 3)]
        [Display(Name = "产品线介绍")]
        public string Intro { get; set; }

        /// <summary>
        ///     产品线商品
        /// </summary>
        public List<long> ProductIds { get; set; }
    }
}