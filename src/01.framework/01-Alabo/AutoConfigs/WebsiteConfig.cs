using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Enums;
using Alabo.Runtime;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.AutoConfigs {

    /// <summary>
    /// 网站设置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "网站设置", GroupName = "基本设置,联系信息,本地化信息,法律信息",
        Icon = "fa fa-puzzle-piece", SortOrder = 1, Description = "设置以及查看系统的详细信息",
        SideBarType = SideBarType.ControlSideBar)]
    public class WebSiteConfig : BaseViewModel, IAutoConfig {

        #region 第一个标签 基本设置

        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1)]
        [Display(Name = "网站名称")]
        [Required]
        [HelpBlock("请输入网站名称，名称的长度1-30个字符")]
        public string WebSiteName { get; set; }

        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1)]
        [Display(Name = "网站关键字")]
        [HelpBlock("请输入网站关键字，网站关键字的长度1-30个字符")]
        public string Keyword { get; set; } = "关键字";

        [Field(ControlsType = ControlsType.TextArea, GroupTabId = 1)]
        [Display(Name = "网站描述")]
        [HelpBlock("请输入网站描述，网站描述的长度1-30个字符")]
        public string Description { get; set; } = "描述";

        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1)]
        [Display(Name = "域名")]
        [HelpBlock("请输入域名，域名的长度1-30个字符，请不要填写www.http,等直接填写域名即可，否则会导致移动端Api接口不能访问")]
        public string DomainName { get; set; } = "m.qiniuniu99.com";

        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1)]
        [Display(Name = "Api图片地址")]
        [HelpBlock("Api图片地址,一般情况下为网址，请补全http://或https://格式如http://www.5ug.com")]
        public string ApiImagesUrl { get; set; } = RuntimeContext.Current.WebsiteConfig.ClientHost;//"https://s-open.qiniuniu99.com";

        [Field(ControlsType = ControlsType.FileUploder, GroupTabId = 1)]
        [Display(Name = "图标")]
        [HelpBlock("请输入图标，图标的大小必须小于1M，支持:jpg、jpeg、png、gif图片格式")]
        public string Icon { get; set; } = "/wwwroot/favorite.ico";

        [Field(ControlsType = ControlsType.AlbumUploder, GroupTabId = 1)]
        [Display(Name = "Logo")]
        [HelpBlock("请输入Logo图片，图片的大小必须小于2M，支持:jpg、jpeg、png、gif图片格式")]
        public string Logo { get; set; } = "/wwwroot/static/images/logo.png";

        [Field(ControlsType = ControlsType.AlbumUploder, GroupTabId = 1)]
        [Display(Name = "会员中心Logo")]
        [HelpBlock("请输入会员中心Logo图片，图片的大小必须小于2M，支持:jpg、jpeg、png、gif图片格式 ")]
        public string UserLogo { get; set; } = "/wwwroot/static/images/logo.png";

        [Field(ControlsType = ControlsType.AlbumUploder, GroupTabId = 1)]
        [Display(Name = "手机Logo")]
        [HelpBlock("请输入手机Logo图片，图片的大小必须小于2M，支持:jpg、jpeg、png、gif图片格式")]
        public string MobileLogo { get; set; } = "/wwwroot/static/images/logo.png";

        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1)]
        [Display(Name = "首页标题")]
        [HelpBlock("请输入首页标题，标题的长度1-18个字符，默认值为首页标题")]
        public string HomePageTitle { get; set; } = "首页";

        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1)]
        [Display(Name = "底部内容")]
        [HelpBlock("请输入底部内容，长度1-18个字符")]
        public string FootContent { get; set; }

        /// <summary>
        ///     Gets or sets the no pic.
        /// </summary>
        [Field(ControlsType = ControlsType.AlbumUploder, GroupTabId = 1)]
        [Display(Name = "缺省图片")]
        [HelpBlock("默认缺省图片，如果图片不存在时，使用缺省图片。支持:jpg、jpeg、png、gif图片格式")]
        public string NoPic { get; set; } = "/wwwroot/static/images/nopic.jpg";

        #endregion 第一个标签 基本设置

        #region 第一个标签 联系信息

        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 2)]
        [Display(Name = "公司名称")]
        [HelpBlock("请输入公司名称，名称的长度1-40个字符")]
        public string CompanyName { get; set; }

        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 2)]
        [Display(Name = "联系人")]
        [HelpBlock("请输入联系人，联系人的长度1-30个字符，默认值为您的称呼")]
        public string LinkMan { get; set; } = "您的称呼";

        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 2)]
        [Display(Name = "联系电话")]
        [HelpBlock("请输入11位数的联系电话")]
        public string Tel { get; set; }

        [Field(ControlsType = ControlsType.CityDropList, GroupTabId = 2)]
        [Display(Name = "所在城市")]
        public long RegionId { get; set; }

        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 2)]
        [Display(Name = "联系地址")]
        [HelpBlock("请输入联系地址，联系地址的长度少于150个字符")]
        public string Address { get; set; }

        #endregion 第一个标签 联系信息

        #region 第三个标签 本地化信息

        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 3)]
        [Display(Name = "默认语言")]
        [HelpBlock("请输入默认语言，默认语言的长度1-18个字符，默认值为zh-CN")]
        public string DefaultLanguage { get; set; } = "zh-CN";

        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 3)]
        [Display(Name = "默认时区")]
        [HelpBlock("请输入默认时区，默认时区的长度1-40个字符，默认值为China Standard Time")]
        public string DefaultTimeZone { get; set; } = "China Standard Time";

        [Field(ControlsType = ControlsType.Switch, GroupTabId = 3)]
        [Display(Name = "自动检测语言")]
        [HelpBlock("开启后，系统的语言将会自动检测浏览器语言")]
        public bool AllowDetectLanguageFromBrowser { get; set; } = false;

        #endregion 第三个标签 本地化信息

        #region 第四个标签，法律信息

        /// <summary>
        ///     备案号
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 4)]
        [Display(Name = "备案号")]
        [HelpBlock("请输入备案号，备案号的长度1-40个字符")]
        public string WebSiteRecordNo { get; set; }

        /// <summary>
        ///     公安备案号
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 4)]
        [Display(Name = "公安备案号")]
        [HelpBlock("请输入公安备案号，公安备案号的长度1-40个字符")]
        public string WebSiteRecordSecurityNo { get; set; }

        /// <summary>
        ///     备案号查询链接
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 4)]
        [Display(Name = "备案号查询链接")]
        [HelpBlock("请输入备案号查询链接，备案号查询链接默认值为http://www.miitbeian.gov.cn")]
        public string WebSiteRecordQueryUrl { get; set; } = "http://www.miitbeian.gov.cn";

        /// <summary>
        ///     法人名称
        ///     留空时使用网站名称
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 4)]
        [Display(Name = "法人名称")]
        [HelpBlock("请输入法人名称，法人名称的长度1-20个字符")]
        public string CorporationName { get; set; }

        /// <summary>
        ///     服务条款
        ///     留空时使用默认条款
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, EditShow = true, Row = 15, GroupTabId = 4)]
        [Display(Name = "服务条款")]
        [HelpBlock("请输入服务条款，服务条款的长度少于1000个字符，默认值为服务条款")]
        public string ServiceAgreement { get; set; }

        #endregion 第四个标签，法律信息

        /// <summary>
        /// SetDefault
        /// </summary>
        public void SetDefault() {
        }
    }
}