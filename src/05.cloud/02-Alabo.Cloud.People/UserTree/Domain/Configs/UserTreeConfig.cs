using Alabo.AutoConfigs;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace Alabo.Cloud.People.UserTree.Domain.Configs
{
    /// <summary>
    ///     组织架构图设置
    /// </summary>
    [ClassProperty(Name = "组织架构图设置 ", Icon = "fa fa-user", Description = "组织架构图设置")]
    public class UserTreeConfig : BaseViewModel, IAutoConfig
    {
        [Field(ControlsType = ControlsType.Switch, ListShow = true, GroupTabId = 1)]
        [Display(Name = "显示门店")]
        [HelpBlock("如果该会员是门店，则在家谱上面区分显示出来")]
        public bool IsShowServiceCenter { get; set; } = true;

        [Field(ControlsType = ControlsType.Switch, ListShow = true, GroupTabId = 1)]
        [Display(Name = "显示会员等级")]
        [HelpBlock("是否在组织架构图上面显示用户的会员等级")]
        public bool IsShowUserGrade { get; set; } = true;

        [Field(ControlsType = ControlsType.Switch, ListShow = true, GroupTabId = 1)]
        [Display(Name = "显示直推会员数")]
        [HelpBlock("是否在组织架构图上面显示用户的直推会员数")]
        public bool IsShowDirectMemberNum { get; set; } = true;

        /// <summary>
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic, ListShow = true, GroupTabId = 1)]
        [Display(Name = "会员中心显示层数")]
        [Range(1, 3, ErrorMessage = "组织架构图在会员中心显示的层数，层数限制在1-3层之间")]
        [HelpBlock("组织架构图在会员中心显示的层数")]
        public long ShowLevel { get; set; } = 3;

        /// <summary>
        ///     会员物理删除后修改推荐人
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, ListShow = true, GroupTabId = 1)]
        [Display(Name = "会员物理删除后改推荐人")]
        [HelpBlock("物理删除会员后, 将该删除会员下所有的会员推荐人改成，删除会员的推荐人，并重新生成组织架构图")]
        public bool AfterDeleteUserUpdateParentMap { get; set; } = false;

        public void SetDefault()
        {
        }
    }
}