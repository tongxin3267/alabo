using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Tenants.Callbacks {
    /// <summary>
    /// 后台Api服务器
    /// </summary>

    [NotMapped]
    [ClassProperty(Name = "前台访问地址", Icon = "fa fa-cny", Description = "支付方式",
        PageType = ViewPageType.List)]
    public class TenantClientHostConfig : BaseViewModel, IAutoConfig {

        /// <summary>
        /// Id
        /// </summary>
        [Field(ControlsType = ControlsType.Hidden)]
        public Guid Id { get; set; }

        /// <summary>
        ///     货币名称
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 0, IsMain = true, ListShow = true, Width = "110", IsShowBaseSerach = true, IsShowAdvancedSerach = true)]
        [Required]
        [Display(Name = "前台访问地址")]
        [HelpBlock("租户前台访问地址，对应后台zkweb程序")]
        [Main]
        public string Url { get; set; }

        public void SetDefault() {
        }
    }
}