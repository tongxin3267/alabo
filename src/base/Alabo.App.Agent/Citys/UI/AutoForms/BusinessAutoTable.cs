using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Core.User.Domain.Dtos;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Core.User.ViewModels;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Query;
using Alabo.Extensions;
using Alabo.UI;
using Alabo.UI.AutoForms;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;


namespace Alabo.App.Agent.Citys.UI.AutoForms
{
    /// <summary>
    /// </summary>
    /// <seealso cref="BaseViewModel" />
    [ClassProperty(Name = "实体商家", Icon = "la la-users", Description = "实体商家",
            SideBarType = SideBarType.SupplierSideBar)]
    public class BusinessAutoTable : UIBase, IAutoTable<BusinessAutoTable>
    {

        #region

        /// <summary>
        /// 用户Id
        /// </summary>
        [Display(Name = "用户Id")]
        [Field(ListShow = false, EditShow = false, IsMain = true)]
        public long UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        [NotMapped]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, Width = "180", IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            ListShow = true, SortOrder = 1, Link = "/Admin-city/User/Edit?id=[[UserId]]")]
        public string UserName { get; set; }

        [Display(Name = "实名")]
        [NotMapped]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, Width = "180", IsShowBaseSerach = true, IsShowAdvancedSerach = true,
         ListShow = true, SortOrder = 1, Link = "/Admin-city/User/Edit?id=[[UserId]]")]
        public string RealName { get; set; }


        /// <summary>
        ///     用户等级Id
        ///     每个类型对应一个等级
        /// </summary>
        /// <value>
        ///     The grade identifier.
        /// </value>
        [Display(Name = "用户等级")]
        [Field(ControlsType = ControlsType.DropdownList, LabelColor = LabelColor.Primary, IsShowBaseSerach = true, Width = "180", ListShow = false,
            EditShow = false, SortOrder = 4)]
        public Guid GradeId { get; set; }

        /// <summary>
        /// 实体店铺等级
        /// </summary>
        [Display(Name = "店铺等级")]
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Primary, Width = "180", ListShow = true,
            EditShow = false, SortOrder = 4)]
        public string GradeName { get; set; }

        /// <summary>
        ///     注册时间
        /// </summary>
        /// <value>
        ///     The create time.
        /// </value>
        [Display(Name = "注册时间")]
        [Field(ControlsType = ControlsType.DateTimeRang, Width = "180", ListShow = true, EditShow = false,
            SortOrder = 10)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 状态（启用，关闭）
        /// </summary>
        [Display(Name = "用户状态")]
        [Field(ControlsType = ControlsType.DropdownList, Width = "180", ListShow = true, EditShow = false,
         SortOrder = 10)]
        public string Status { get; set; }

        #endregion


        public List<TableAction> Actions()
        {
            return new List<TableAction>() {
                new TableAction("启用","Api/City/ChangeStatus?Status=1",TableActionType.ColumnAction),
                new TableAction("关闭","Api/City/ChangeStatus?Status=0",TableActionType.ColumnAction)
            };
        }

        public PageResult<BusinessAutoTable> PageTable(object query, AutoBaseModel autoModel)
        {
            var tb = new PagedList<BusinessAutoTable>();

            var Input = ToQuery<UserInput>();
            Input.ParentId = autoModel.BasicUser.Id;
            Input.PageSize = 50;
            var userList = Resolve<IUserService>().GetViewUserPageList(Input);
            if (userList.Count > 0)
            {
                userList.ForEach(p =>
                {
                    if (p.GradeId.ToString() == "72be65e6-3000-414d-972e-1a3d4a366001" ||
                    p.GradeId.ToString() == "6f7c8477-4d9a-486b-9fc7-8ce48609edfc" ||
                    p.GradeId.ToString() == "72be65e6-3000-414d-972e-1a3d4a366002")
                    {
                        var model = new BusinessAutoTable();

                        model.UserId = p.Id;
                        model.UserName = p.UserName;
                        model.GradeName = p.GradeName;
                        model.GradeId = p.GradeId;
                        model.Status = p.Status.GetDisplayName();
                        model.CreateTime = p.CreateTime;
                        tb.Add(model);
                    }
                });
            }
            return ToPageResult(tb);
        }
    }
}