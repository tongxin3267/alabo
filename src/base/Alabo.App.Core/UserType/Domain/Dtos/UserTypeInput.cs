using System;
using Alabo.App.Core.UserType.Domain.Enums;
using Alabo.Domains.Enums;
using Alabo.Domains.Query.Dto;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Domain.Dtos {

    public class UserTypeInput : PagedInputDto {

        [Field(ControlsType = ControlsType.TextBox, PlaceHolder = "用户类型",
            IsShowAdvancedSerach = true, IsShowBaseSerach = false, SortOrder = 1)]
        public Guid UserTypeId { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, PlaceHolder = "输入用户名精确查询",
            IsShowAdvancedSerach = true, IsShowBaseSerach = true, SortOrder = 1)]
        public string UserName { get; set; }

        public long UserId { get; set; }

        [Field(ControlsType = ControlsType.TextBox, PlaceHolder = "输入名称精确查询",
            IsShowAdvancedSerach = true, IsShowBaseSerach = true, SortOrder = 1)]
        public string Name { get; set; } = string.Empty;

        public Guid GradeId { get; set; } = Guid.Empty;

        [Field(ControlsType = ControlsType.DropdownList, IsShowAdvancedSerach = true,
            DataSource = "Alabo.App.Core.UserType.Domain.Enums.UserTypeStatus", IsShowBaseSerach = false,
            SortOrder = 200)]
        public UserTypeStatus? Status { get; set; }

        [Field(ControlsType = ControlsType.TextBox, PlaceHolder = "输入推荐人用户名精确查询",
            IsShowAdvancedSerach = true, IsShowBaseSerach = true, SortOrder = 1)]
        public long ParentUserId { get; set; }

        public Guid typeId { get; set; }
    }
}