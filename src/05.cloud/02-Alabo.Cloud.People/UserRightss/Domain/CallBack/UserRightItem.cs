using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.Cloud.People.UserRightss.Domain.CallBack {

    /// <summary>
    ///     用户等级权益赠送数量设置
    /// </summary>
    [ClassProperty(Name = "用户等级权益赠送数量设置")]
    public class UserRightItem : BaseViewModel {

        /// <summary>
        ///     会员等级
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, ListShow = true, DisplayMode = DisplayMode.Grade,
            EditShow = true, SortOrder = 1,
            DataSourceType = typeof(UserGradeConfig))]
        [Display(Name = "赠送等级")]
        [HelpBlock("考核等级，一个等级降职配置只能有一条，有多条时默认选择第一条")]
        public Guid GradeId { get; set; }

        /// <summary>
        ///     赠送名额
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, SortOrder = 5)]
        [Display(Name = "赠送名额")]
        public long Count { get; set; }
    }
}