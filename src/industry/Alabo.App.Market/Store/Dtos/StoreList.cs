using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.App.Core.Employes.Domain.Services;
using Alabo.App.Core.UserType.Domain.Enums;
using Alabo.App.Core.UserType.Modules.Supplier;
using Alabo.App.Shop.Store.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Mapping;
using Alabo.UI;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Market.Store.Dtos {

    [ClassProperty(Name = "供应商")]
    public class StoreList : UIBase, IAutoTable<StoreList> {

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
        [HelpBlock("填写供应商的名称")]
        public string Name { get; set; }

        /// <summary>
        ///     是否为平台,
        ///     平台只能有一个
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is planform; otherwise, <c>false</c>.
        /// </value>
        [Display(Name = "是否平台")]
        [HelpBlock("选择是否平台")]
        [Field(ControlsType = ControlsType.Switch, IsShowAdvancedSerach = true, Width = "180",
            ListShow = true, SortOrder = 7)]
        public bool IsPlanform { get; set; } = false;

        /// <summary>
        ///     联系号码
        /// </summary>
        [Display(Name = "联系号码")]
        [HelpBlock("填写供应商联系号码")]
        [Field(ControlsType = ControlsType.TextBox, IsShowAdvancedSerach = true, Width = "180",
            ListShow = true, SortOrder = 7)]
        public string Mobile { get; set; }

        /// <summary>
        ///     供应商状态
        /// </summary>
        /// <value>
        ///     The status.
        /// </value>
        [Display(Name = "供应商状态")]
        [Field(ControlsType = ControlsType.DropdownList, IsShowBaseSerach = true, IsShowAdvancedSerach = true,
            DataSource = "Alabo.App.Core.UserType.Domain.Enums.UserTypeStatus", Width = "180", ListShow = false, EditShow = false,
            SortOrder = 3)]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public UserTypeStatus Status { get; set; } = UserTypeStatus.Pending;

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
        [HelpBlock("填写供应商的用户名，如不存在用户，则会自动注册")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, Width = "180", EditShow = true, ListShow = true, SortOrder = 1)]
        public string UserName { get; set; }

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

        /// <summary>
        ///     Gets or sets the supplier grade configuration.
        /// </summary>
        /// <value>
        ///     The supplier grade configuration.
        /// </value>
        [Display(Name = "等级")]
        [Field(ControlsType = ControlsType.DropdownList, Width = "180", ListShow = false, EditShow = false,
            DataSource = "Alabo.App.Core.UserType.Modules.Supplier.SupplierGradeConfig", SortOrder = 4)]
        public SupplierGradeConfig SupplierGradeConfig { get; set; }

        /// <summary>
        ///     Gets or sets the name of the grade.
        /// </summary>
        /// <value>
        ///     The name of the grade.
        /// </value>
        [Display(Name = "店铺等级")]
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Primary, Width = "180", ListShow = true,
            EditShow = false, SortOrder = 4)]
        public string GradeName { get; set; }

        /// <summary>
        /// 银行卡
        /// </summary>
        [Display(Name = "银行卡")]
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Primary, Width = "180", ListShow = false,
            EditShow = true, SortOrder = 4)]
        [HelpBlock("填写银行卡")]
        public string BankCard { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        /// <value>
        ///     The create time.
        /// </value>
        [Display(Name = "创建时间")]
        [Field(ControlsType = ControlsType.DateTimeRang, Width = "180", ListShow = true, EditShow = false, SortOrder = 10)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///     推荐人用户名
        /// </summary>
        [Display(Name = "推荐人用户名")]
        [HelpBlock("填写推荐人用户名")]
        [Field(ControlsType = ControlsType.TextBox, LabelColor = LabelColor.Primary, Width = "180", ListShow = true,
            EditShow = true, SortOrder = 4)]
        public string ParentUserName { get; set; }

        public List<TableAction> Actions() {
            var rsList = new List<TableAction>
            {
                ToLinkAction("编辑", "Edit", TableActionType.ColumnAction),
            };
            return rsList;
        }

        public PageResult<StoreList> PageTable(object query, AutoBaseModel autoModel) {
            //分配权限即可查看
            //if (autoModel.Filter == FilterType.Admin)
            //{
            //    var isAdmin = Resolve<IUserService>().IsAdmin(autoModel.BasicUser.Id);
            //    if (!isAdmin)
            //    {
            //        throw new ValidException("非管理员不能查看");
            //    }
            //}

            var stores = Resolve<IStoreService>().GetPageList(query);
            var result = new PagedList<StoreList>();
            foreach (var item in stores) {
                var view = AutoMapping.SetValue<StoreList>(item);
                view.Status = item.Status;

                result.Add(view);
            }

            return ToPageResult(result);
        }
    }
}