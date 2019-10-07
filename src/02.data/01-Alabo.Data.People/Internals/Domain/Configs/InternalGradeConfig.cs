using Alabo.AutoConfigs;
using Alabo.Data.People.Users;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Data.People.Internals.Domain.Configs
{
    [NotMapped]
    [ClassProperty(Name = "内部合伙人等级", Icon = "fa fa-user-times",
        Description = "内部合伙人等级", PageType = ViewPageType.List, SortOrder = 12)]
    public class InternalGradeConfig : BaseGradeConfig, IAutoConfig
    {
        /// <summary>
        ///     会员类型Id，不同的会员类型有不同的等级
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden, ListShow = false, EditShow = true, Width = "10%",
            DisplayMode = DisplayMode.Text, SortOrder = 1)]
        [Display(Name = "内部合伙人等级")]
        public new Guid UserTypeId { get; set; } = Guid.Parse("71BE65E6-3A64-415D-982E-1AEDEA365102");

        public void SetDefault()
        {
            throw new NotImplementedException();
        }
    }
}