using Alabo.AutoConfigs;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.Cloud.Shop.ProductQrCode.Configs
{
    /// <summary>
    ///     二维码设置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "二维码设置",
        Icon = "fa fa-qrcode", SortOrder = 1, Description = "修改二维码设置，会员所有的二维码会重新生成")]
    public class ProductQrCodeConfig : BaseViewModel, IAutoConfig
    {
        /// <summary>
        ///     背景图片
        /// </summary>
        [Field(ControlsType = ControlsType.AlbumUploder)]
        [Display(Name = "背景图片")]
        [HelpBlock("请输入二维码的背景图片，注意尺寸与大小，以及二维码的位置,建议大小：宽度500px,高度800px")]
        public string BgPicture { get; set; } = "/wwwroot/assets/mobile/images/qrcode/01.png";

        /// <summary>
        ///     二维码距离左部高度
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic)]
        [Display(Name = "二维码距离左部高度")]
        [HelpBlock("二维码距离左部高度，请输入数字")]
        public int PositionLeft { get; set; } = 40;

        /// <summary>
        ///     二维码距离顶部高度
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic)]
        [Display(Name = "二维码距离顶部高度")]
        [HelpBlock("二维码距离顶部高度，请输入数字")]
        public int PositionTop { get; set; } = 50;

        /// <summary>
        ///     二维码大小
        /// </summary>
        [Field(ControlsType = ControlsType.Numberic)]
        [Display(Name = "二维码宽度/高度px")]
        [HelpBlock("二维码大小，请输入数字，正方形，单位像素")]
        public int QrCodeBig { get; set; } = 100;

        /// <summary>
        ///     是否显示用户信息
        /// </summary>
        [Field(ControlsType = ControlsType.Switch)]
        [Display(Name = "是否显示用户信息")]
        [HelpBlock("是否显示用户信息,例如用户头像 用户等级")]
        public bool IsDisplayUserInformation { get; set; } = false;

        /// <summary>
        ///     是否跳转
        /// </summary>
        [Field(ControlsType = ControlsType.Switch)]
        [Required]
        [Display(Name = "是否跳转")]
        [HelpBlock("用户扫描二维码后显示的内容，如果跳转，则根据跳转到下面设置的网址。如果不跳转则显示下面的内容")]
        public bool IsRedirect { get; set; } = true;

        /// <summary>
        ///     跳转网址
        /// </summary>
        [Required]
        [Field(ControlsType = ControlsType.TextBox)]
        [Display(Name = "跳转网址")]
        [HelpBlock("是否跳转开启后，跳转的网址，常用的网址包括  注册页面：/user/reg 首页/User/Index")]
        public string RedirectUrl { get; set; } = "/User/Reg";

        /// <summary>
        ///     跳转网址
        /// </summary>
        [Required]
        [Field(ControlsType = ControlsType.Editor)]
        [Display(Name = "个性内容")]
        [HelpBlock("是否跳转关闭后，显示的内容，可上传图片")]
        public string ShowContent { get; set; }

        /// <summary>
        /// </summary>
        public void SetDefault()
        {
        }
    }
}