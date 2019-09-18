using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Modules.Employees {

    /// <summary>
    ///     员工
    /// </summary>
    [UserTypeModules(Name = "员工", Icon = " icon-user ", SortOrder = 1001, Config = "EmployeesGradeConfig")]
    public class EmployeesUserType : IUserType {

        /// <summary>
        ///     用户性别
        /// </summary>
        [Display(Name = "性别")]
        [Field(ControlsType = ControlsType.RadioButton, SortOrder = 0, DataSource = "Alabo.Core.Enums.Enum.Sex",
            EditShow = true,
            Width = "12%")]
        public Sex Sex { get; set; }

        /// <summary>
        ///     员工编号
        /// </summary>
        [Display(Name = "员工编号")]
        [Required(ErrorMessage = "请填写员工编号")]
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 0, EditShow = true, Width = "12%")]
        public string UserNo { get; set; }

        /// <summary>
        ///     企业ID
        /// </summary>
        [Display(Name = "企业ID")]
        public long CompanyId { get; set; }

        /// <summary>
        ///     部门ID
        /// </summary>
        [Display(Name = "部门")]
        public string DepartmentId { get; set; }

        /// <summary>
        ///     员工的直接上级
        /// </summary>
        [Display(Name = "直接上级")]
        public long DirectSuperior { get; set; }

        /// <summary>
        ///     入职时间
        /// </summary>
        [Display(Name = "入职时间")]
        [Field(ControlsType = ControlsType.DateTimePicker, SortOrder = 0, EditShow = true, Width = "12%")]
        public DateTime EntryTime { get; set; }
    }
}