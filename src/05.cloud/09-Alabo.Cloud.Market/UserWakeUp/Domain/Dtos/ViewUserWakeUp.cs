using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.UI.AutoTables;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Market.UserWakeUp.Domain.Dtos {

    public class ViewUserWakeUp : UIBase, IAutoTable<ViewUserWakeUp> {

        /// <summary>
        /// 头像
        /// </summary>
        [Display(Name = "头像")]
        [Field(ControlsType = ControlsType.ImagePreview, Width = "150", ListShow = true, SortOrder = 1)]
        public string Avator { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = true, SortOrder = 2)]
        public string UserName { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = true, SortOrder = 3)]
        public string Name { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Display(Name = "手机号码")]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = true, SortOrder = 4)]
        public string Mobile { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = true, SortOrder = 5)]
        public string Email { get; set; }

        /// <summary>
        /// 等级名称
        /// </summary>
        [Display(Name = "等级")]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = true, SortOrder = 6)]
        public string GradeName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [Display(Name = "性别")]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = true, SortOrder = 7)]
        public Sex Sex { get; set; } = Sex.Man;

        /// <summary>
        ///     用户状态
        /// </summary>
        [Display(Name = "用户状态")]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = true, SortOrder = 8)]
        public Status Status { get; set; } = Status.Normal;

        /// <summary>
        /// 注册时间
        /// </summary>
        [Display(Name = "注册时间")]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = true, SortOrder = 9)]
        public DateTime RegTime { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [Display(Name = "最后登录时间")]
        [Field(ControlsType = ControlsType.TextBox, Width = "150", ListShow = true, SortOrder = 9)]
        public DateTime LastLoginTime { get; set; }

        public List<TableAction> Actions() {
            return new List<TableAction>();
        }

        public PageResult<ViewUserWakeUp> PageTable(object query, AutoBaseModel autoModel) {
            return ToPageResult(new PagedList<ViewUserWakeUp>());
        }
    }
}