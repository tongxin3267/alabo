using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Core.WebUis;
using MongoDB.Bson.Serialization.Attributes;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Market.PromotionalMaterial.Domain.Entities {

    /// <summary>
    ///     推广素材
    /// </summary>
    [BsonIgnoreExtraElements][Table("PromotionalMaterial_Material")]
    [ClassProperty(Name = "素材", Description = "素材", Icon = IconFlaticon.route,
        SideBarType = SideBarType.PromotionalMaterialSideBar)]
    public class Material : AggregateMongodbRoot<Material> {
        
        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        [Required(ErrorMessage = "状态不能为空")]
        [Field(ControlsType = ControlsType.DropdownList, DataSource = " Alabo.Domains.Enums.Status", Width = "100",
            IsShowAdvancedSerach = true, IsShowBaseSerach = true, ListShow = true, SortOrder = 5, GroupTabId = 2, IsTabSearch = true)]
        public Status Status { get; set; } = Status.Normal;

        /// <summary>
        ///     姓名
        /// </summary>
        [Display(Name = "标题")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, IsMain = true, ListShow = true, IsShowBaseSerach = true, EditShow = true, Link = "/Admin/Material/Edit?id=[[Id]]", GroupTabId = 1, SortOrder = 0)]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [Field(ControlsType = ControlsType.AlbumUploder, SortOrder = 1, IsImagePreview = true, ListShow = true,EditShow = true, Width = "15%")]
        [Display(Name = "图片")]
        public string Image { get; set; }

        /// <summary>
        ///     课程介绍
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, IsShowBaseSerach = true, EditShow = true, SortOrder = 2)]
        [Display(Name = "内容")]
        public string Intro { get; set; }


        public IEnumerable<ViewLink> ViewLinks()
        {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("编辑", "/Admin/Material/Edit?id=[[Id]]", Icons.Edit, LinkType.ColumnLink),
                new ViewLink("删除", "/Admin/Material/Delete?id=[[Id]]", Icons.Delete, LinkType.ColumnLinkDelete)
            };
            return quickLinks;
        }
    }
}