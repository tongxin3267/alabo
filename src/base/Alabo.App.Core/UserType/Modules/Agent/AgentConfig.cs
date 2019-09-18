﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.UserType.Modules.Agent {

    /// <summary>
    ///     微代理配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "微代理配置", Icon = "fa fa-user-times",
        Description = "微代理配置", PageType = ViewPageType.Edit, SortOrder = 12,
        SideBarType = SideBarType.AgentSideBar)]
    public class AgentConfig : BaseUserTypeConfig, IAutoConfig {

        /// <summary>
        ///     服务条款
        ///     留空时使用默认条款
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, Row = 15)]
        [Display(Name = "微代理服务条款")]
        [HelpBlock("请输入微代理服务条款，默认值为微代理服务条款")]
        public string AgentServiceAgreement { get; set; } = "微代理服务条款";

        public void SetDefault() {
        }
    }
}