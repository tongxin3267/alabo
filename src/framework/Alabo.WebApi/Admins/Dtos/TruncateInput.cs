using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.UI;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Admin.Domain.Dtos {

    /// <summary>
    /// 清空数据
    /// </summary>
    [ClassProperty(Name = "清空数据", Icon = IconFlaticon.exclamation, SideBarType = SideBarType.ControlSideBar)]
    public class TruncateInput : BaseViewModel {
    }
}