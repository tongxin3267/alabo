using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;using MongoDB.Bson.Serialization.Attributes;
using Alabo.Core.UI;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Market.SuccessfulCases.Domains.Entities {

    /// <summary>
    ///     成功案例
    /// </summary>
    [BsonIgnoreExtraElements][Table("SuccessfulCases_Cases")]
    [ClassProperty(Name = "案例", Description = "案例", Icon = IconFlaticon.route,
        SideBarType = SideBarType.SuccessfulCasesSideBar)]
    public class Cases : AggregateMongodbRoot<Cases> {


        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        [Required(ErrorMessage = "状态不能为空")]
        [Field(ControlsType = ControlsType.DropdownList, DataSource = " Alabo.Domains.Enums.Status", Width = "100",
            IsShowAdvancedSerach = true, IsShowBaseSerach = true, ListShow = true, SortOrder = 5, GroupTabId = 2, IsTabSearch = true)]
        public Status Status { get; set; } = Status.Normal;

        /// <summary>
        ///     课程名称
        /// </summary>
        [Display(Name = "案例标题")]
        [Required(ErrorMessage = "标题不能为空")]
        [Field(ControlsType = ControlsType.TextBox, IsMain = true, ListShow = true, IsShowBaseSerach = true, EditShow = true, Link = "/Admin/Cases/Edit?id=[[Id]]", SortOrder = 0)]
        public string Name { get; set; }

        /// <summary>
        /// </summary>
        [Display(Name = "背景图片")]
        [Required(ErrorMessage = "背景图片不能为空")]
        [Field(ControlsType = ControlsType.AlbumUploder, SortOrder = 1, IsImagePreview = true, ListShow = true, EditShow = true, Width = "15%")]
        public string Image { get; set; }

        /// <summary>
        ///     详情
        /// </summary>
        [Display(Name = "案例详情")]
        [Required(ErrorMessage = "案例详情不能为空")]
        [Field(ControlsType = ControlsType.Editor, EditShow = true, SortOrder = 2)]
        [HelpBlock("编写案例详情")]
        public string Intro { get; set; }


        public IEnumerable<ViewLink> ViewLinks()
        {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("编辑", "/Admin/Cases/Edit?id=[[Id]]", Icons.Edit, LinkType.ColumnLink),
                new ViewLink("删除", "/Admin/Cases/Delete?id=[[Id]]", Icons.Delete, LinkType.ColumnLinkDelete)
            };
            return quickLinks;
        }
    }
}