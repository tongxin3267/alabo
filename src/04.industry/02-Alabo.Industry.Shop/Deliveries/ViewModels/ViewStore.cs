using Alabo.App.Shop.Store.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Validations;

namespace Alabo.App.Shop.Store.ViewModels {

    /// <summary>
    /// </summary>
    /// <seealso cref="BaseViewModel" />
    [ClassProperty(Name = "店铺管理", Icon = "la la-users", Description = "店铺管理",
        SideBarType = SideBarType.SupplierSideBar)]
    public class ViewStore : UIBase, IAutoTable<ViewStore> {

        /// <summary>
        ///     Gets or sets Id标识
        /// </summary>
        /// <value>
        ///     Id标识
        /// </value>
        [Field(ListShow = false, EditShow = false, IsMain = true)]
        public long Id { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        [Display(Name = "名称")]
        [StringLength(20, ErrorMessage = ErrorMessage.MaxStringLength)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, TableDispalyStyle = TableDispalyStyle.Code, Width = "180",
            ListShow = true, IsMain = true, Link = "/Admin/User/Edit?id=[[UserId]]", SortOrder = 2)]
        public string Name { get; set; }

        /// <summary>
        ///     是否为平台,
        ///     平台只能有一个
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is planform; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "是否平台")]
        [Field(ControlsType = ControlsType.Switch, IsShowAdvancedSerach = true, Width = "180",
            ListShow = true, SortOrder = 7)]
        public bool IsPlanform { get; set; } = false;

        ///// <summary>
        /////     供应商状态
        ///// </summary>
        ///// <value>
        /////     The status.
        ///// </value>
        //[Display(Name = "供应商状态")]
        //[Field(ControlsType = ControlsType.DropdownList, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
        //    DataSource = "Alabo.App.Core.UserType.Domain.Enums.UserTypeStatus", Width = "180", ListShow = true,
        //    SortOrder = 3)]
        //[Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        //public UserTypeStatus Status { get; set; } = UserTypeStatus.Pending;

        /// <summary>
        ///     用户Id
        /// </summary>
        /// <value>
        ///     The user identifier.
        /// </value>
        [Display(Name = "用户名")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long UserId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the user.
        /// </summary>
        /// <value>
        ///     The name of the user.
        /// </value>
        [Display(Name = "用户名")]
        [NotMapped]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, Width = "180", IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            ListShow = true, SortOrder = 1, Link = "/Admin/User/Edit?id=[[UserId]]")]
        public string UserName { get; set; }

        /// <summary>
        ///     推荐人用户Id
        /// </summary>
        /// <value>
        ///     The parent user identifier.
        /// </value>
        public long ParentUserId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the parent user.
        /// </summary>
        /// <value>
        ///     The name of the parent user.
        /// </value>
        [Display(Name = "推荐人")]
        [Field(ControlsType = ControlsType.TextBox, Width = "180", IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            ListShow = true, Link = "/Admin/User/Edit?id=[[ParentUserId]]", SortOrder = 5)]
        public string ParentUserName { get; set; }

        /// <summary>
        ///     用户等级Id
        ///     每个类型对应一个等级
        /// </summary>
        /// <value>
        ///     The grade identifier.
        /// </value>
        [Display(Name = "等级")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public Guid GradeId { get; set; }

        ///// <summary>
        /////     Gets or sets the supplier grade configuration.
        ///// </summary>
        ///// <value>
        /////     The supplier grade configuration.
        ///// </value>
        //[Display(Name = "等级")]
        //[Field(ControlsType = ControlsType.DropdownList, Width = "180", ListShow = false,
        //    DataSource = "Alabo.App.Core.UserType.Modules.Supplier.SupplierGradeConfig", SortOrder = 4)]
        //public SupplierGradeConfig SupplierGradeConfig { get; set; }

        /// <summary>
        ///     Gets or sets the name of the grade.
        /// </summary>
        /// <value>
        ///     The name of the grade.
        /// </value>
        [Display(Name = "店铺等级")]
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Primary, Width = "180", ListShow = true,
            EditShow = false,
            SortOrder = 4)]
        public string GradeName { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        /// <value>
        ///     The create time.
        /// </value>
        [Display(Name = "创建时间")]
        [Field(ControlsType = ControlsType.DateTimeRang, Width = "180", ListShow = true, EditShow = false,
            SortOrder = 10)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     Views the links.
        /// </summary>

        public IEnumerable<ViewLink> ViewLinks() {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("供应商详情", "/Admin/Basic/Edit?Service=IstoreService&Method=GetView&id=[[Id]]", Icons.List,
                    LinkType.ColumnLink),
                new ViewLink("供应商添加", "/Admin/Basic/Edit?Service=IstoreService&Method=GetView", Icons.List,
                    LinkType.FormQuickLink)
            };
            return quickLinks;
        }

        /// <summary>
        /// 通用分页查询列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public PageResult<ViewStore> PageTable(object query, AutoBaseModel autoModel) {
            var list = Resolve<IShopStoreService>().GetPageList(query);
            return ToPageResult(list);
        }

        public List<TableAction> Actions() {
            var list = new List<TableAction>
            {
                ToLinkAction("供应商", "/ViewStore/list")
            };
            return list;
        }
    }
}