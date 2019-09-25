using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.AutoConfigs;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.ApiStore.CallBacks {

    [NotMapped]
    /// <summary>
    /// 物流跟踪配置
    /// </summary>
    [ClassProperty(Name = "物流跟踪配置", Icon = "fa fa-puzzle-piece",
        SideBarType = SideBarType.ApiStoreSideBar,
        SortOrder = 2, Description = "物流跟踪配置")]
    public class LogisticsTrackConfig : BaseViewModel, IAutoConfig {

        /// <summary>
        ///     是否启用
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1)]
        [Display(Name = "是否启用")]
        [HelpBlock("启用以后，同时保证物流对接账户、物流对接Key设置正确的情况下才可以物流跟踪")]
        public bool IsEnable { get; set; } = false;

        /// <summary>
        ///     物流对接账户
        ///     从快递鸟等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, ListShow = true)]
        [HelpBlock("物流对接账户，可快递鸟等第三方平台获取")]
        [Display(Name = "物流对接账户")]
        [Required]
        public string AppId { get; set; } = "";

        /// <summary>
        ///     物流对接Key
        ///     从快递鸟等第三方平台获取
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox)]
        [HelpBlock("物流对接Key，可快递鸟等第三方平台获取")]
        [Display(Name = "物流对接Key")]
        [Required]
        public string Key { get; set; } = "";

        public void SetDefault() {
        }
    }
}