using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.Cloud.People.UserQrCode.Dtos
{
    /// <summary>
    ///     图片
    /// </summary>
    [ClassProperty(Name = "用户二维码", Icon = "fa fa-puzzle-piece", Description = "用户二维码",
        ListApi = "Api/QrCode/QrcodeList")]
    public class ViewImagePage : BaseViewModel
    {
        /// <summary>
        ///     主键Id
        /// </summary>
        public object Id { get; set; }

        /// <summary>
        ///     标题
        /// </summary>
        [Display(Name = "标题")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", IsShowBaseSerach = true,
            IsShowAdvancedSerach = true, ListShow = true, SortOrder = 5)]
        public string Title { get; set; }

        /// <summary>
        ///     图片地址
        /// </summary>
        [Display(Name = "图片路径")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "150", ListShow = true, SortOrder = 5)]
        public string ImageUrl { get; set; }
    }
}