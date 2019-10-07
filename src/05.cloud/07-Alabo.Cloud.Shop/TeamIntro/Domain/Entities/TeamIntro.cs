using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.WebUis;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Cloud.Shop.TeamIntro.Domain.Entities
{
    [BsonIgnoreExtraElements]
    [Table("TeamIntro_TeamIntro")]
    [ClassProperty(Name = "团队介绍", Description = "团队介绍", Icon = IconFlaticon.route)]
    public class TeamIntro : AggregateMongodbRoot<TeamIntro>
    {
        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        [Required(ErrorMessage = "状态不能为空")]
        [Field(ControlsType = ControlsType.DropdownList, DataSource = " Alabo.Domains.Enums.Status", Width = "100",
            IsShowAdvancedSerach = true, IsShowBaseSerach = true, ListShow = true, SortOrder = 5, GroupTabId = 2,
            IsTabSearch = true)]
        public Status Status { get; set; } = Status.Normal;

        /// <summary>
        ///     姓名
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, IsMain = true, ListShow = true, IsShowBaseSerach = true,
            EditShow = true, Link = "/Admin/Team/Edit?id=[[Id]]", SortOrder = 0)]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [Display(Name = "人物图片")]
        [Required(ErrorMessage = "人物图片不能为空")]
        [Field(ControlsType = ControlsType.AlbumUploder, SortOrder = 1, IsImagePreview = true, ListShow = true,
            EditShow = true, Width = "15%")]
        public string Image { get; set; }

        /// <summary>
        ///     课程介绍
        /// </summary>
        [Display(Name = "人物介绍")]
        [Required(ErrorMessage = "人物介绍不能为空")]
        [Field(ControlsType = ControlsType.Editor, EditShow = true, SortOrder = 2)]
        [HelpBlock("编辑人物介绍")]
        public string Intro { get; set; }

        /// <summary>
        ///     职位
        /// </summary>
        [Display(Name = "职位")]
        [Required(ErrorMessage = "职位不能为空")]
        [Field(ControlsType = ControlsType.TextBox, EditShow = true, SortOrder = 3)]
        public string Position { get; set; }

        public IEnumerable<ViewLink> ViewLinks()
        {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("编辑", "/Admin/Team/Edit?id=[[Id]]", Icons.Edit, LinkType.ColumnLink),
                new ViewLink("删除", "/Admin/Team/Delete?id=[[Id]]", Icons.Delete, LinkType.ColumnLinkDelete)
            };
            return quickLinks;
        }
    }
}