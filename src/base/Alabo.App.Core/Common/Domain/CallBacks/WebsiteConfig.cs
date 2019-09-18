using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Helpers;
using Alabo.Runtime;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.Common.Domain.CallBacks {

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

        [Field(ControlsType = ControlsType.DropdownList, GroupTabId = 3,
            DataSource = "Alabo.Core.Enums.Enum.Country")]
        [Display(Name = "默认国家")]
        [HelpBlock("请输入默认国家，国家名称的长度1-18个字符，默认值为中国")]
        public Country DefaultCountry { get; set; } = Country.China;

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
            var webSite = new WebSiteConfig();
            Ioc.Resolve<IAutoConfigService>().AddOrUpdate<WebSiteConfig>(webSite);
        }
    }
}

//return agreeeMent.Replace("{{websiteName}}", WebsiteName);
//var agreeeMent = @"

//{{websiteName}}通过互联网服务为您和您的朋友提供一种全新的在线社交方式，您只有完全同意下列所有服务条款并完成注册程序，才能成为{{websiteName}}的用户并使用相应服务。您在使用{{websiteName}}提供的各项服务之前，应仔细阅读本用户协议。 您在注册程序过程中点击“同意条款，立即注册”按钮即表示您与{{websiteName}}达成协议，完全接受本服务条款项下的全部条款。您一旦使用{{websiteName}}的服务，即视为您已了解并完全同意本服务条款各项内容，包括{{websiteName}}对服务条款随时做的任何修改。<br/><br/>

//一．服务内容<br/>

//{{websiteName}}的具体服务内容由{{websiteName}}根据实际情况提供，例如微博客服务、照片上传功能、在线交流、逛逛软件等。{{websiteName}}保留变更、中断或终止部分网络服务的权利。 {{websiteName}}保留根据实际情况随时调整{{websiteName}}平台提供的服务种类、形式。{{websiteName}}不承担因业务调整给用户造成的损失。<br/><br/>

//二．内容使用<br/>

//我们鼓励用户充分利用{{websiteName}}平台自由地张贴和共享他们自己的信息。您可以自由张贴从{{websiteName}}个人主页或其他网站复制的图片等内容，但这些内容必须位于公共领域内，或者您拥有这些内容的使用权。用户对经由{{websiteName}}平台上载、张贴或传送的内容承担全部责任。未经版权人许可，用户不得在{{websiteName}}平台上张贴任何受版权保护的内容。用户对于其创作并在{{websiteName}}上发布的合法内容依法享有著作权及其相关权利。 对于用户通过{{websiteName}}上传到{{websiteName}}网站上的任何内容，用户授予{{websiteName}}在全世界范围内具有免费的、永久的、不可撤销的、非独家的许可以及再许可的权利，以使用、复制、修改、改编、出版、翻译、据以创作衍生作品、传播、表演和展示此等内容（整体或部分），和/或将此等内容编入当前已知的或以后开发的其他任何形式的作品、媒体或技术中。任何其他用户或网站需要转载该作品的，必须征得原文作者授权。{{websiteName}}制作、发布、传播的信息内容的版权归{{websiteName}}所有，非经{{websiteName}}许可，任何用户或者第三方不得复制、修改或者转载该内容，或者将其用于任何商业目的。<br/><br/>

//三． 知识产权<br/>

//本网站的整体内容版权属于北京美丽时空科技有限公司所有。本网站所有的产品、技术与所有程序均属于{{websiteName}}知识产权，在此并未授权。{{websiteName}}为我们的商标。 本网站所有设计图样以及其他图样、产品及服务名称，均为北京美丽时空科技有限公司及/或其关联公司所享有的商标、标识。任何人不得使用、复制或用作其他用途。 我们对{{websiteName}}网站专有内容、原创内容和其他通过授权取得的独占或者独家内容享有完全知识产权。未经我们许可，任何单位和个人不得私自复制、传播、展示、镜像、上载、下载、使用，或者从事任何其他侵犯我们知识产权的行为。否则，我们将追究相关法律责任。<br/><br/>

//四．注册信息<br/>

//{{websiteName}}为用户提供对个人注册信息的绝对的控制权，用户可以通过“修改个人信息”查看或修改个人信息。用户自愿注册个人信息，用户在注册时提供的所有信息，都是基于自愿，用户有权在任何时候拒绝提供这些信息。 用户在申请使用{{websiteName}}服务时，必须提供真实的个人资料，并不断更新注册资料。如果因注册信息不真实而引起的问题及其后果，{{websiteName}}不承担任何责任。 用户不得将其帐号、密码转让或出借予他人使用。如用户发现其帐号遭他人非法使用，应立即通知{{websiteName}}。因黑客行为或用户的保管疏忽导致帐号、密码遭他人非法使用，{{websiteName}}不承担任何责任。<br/><br/>

//五．隐私保护<br/>

//保护用户隐私是{{websiteName}}的重点原则，{{websiteName}}通过技术手段、提供隐私保护服务功能、强化内部管理等办法充分保护用户的个人资料安全。 {{websiteName}}保证不对外公开或向第三方提供用户注册的个人资料，及用户在使用服务时存储的非公开内容， 但下列情况除外： ◇ 事先获得用户的明确授权； ◇ 根据有关的法律法规要求； ◇ 按照相关司法机构或政府主管部门的要求； ◇ 只有透露你的个人资料，才能提供你所要求的产品和服务 当用户访问{{websiteName}}的时候，我们可能会保存会员的用户登录状态并且为用户分配一个或多个“Cookies”（一个很小的文本文件），例如：当会员访问一个需要会员登录才可以提供的信息或服务，当会员登录时，我们会把该会员的登录名和密码加密存储在用会员计算机的Cookie文件中，由于是不可逆转的加密存储，其他人即使可以使用该会员的计算机，也无法识别出会员的用户名和密码。会员并不需要额外做任何工作，所有的收集和保存都由系统自动完成。Cookie文件将永久的保存在您的计算机硬盘上，除非您使用浏览器或操作系统软件手工将其删除。您也可以选择“不使用Cookie”或“在使用Cookie是事先通知我”的选项禁止Cookie的产生，但是您将为此无法使用一些{{websiteName}}的查询和服务。 我们选择有信誉的第三方公司或网站作为我们的合作伙伴为用户提供信息和服务。尽管我们只选择有信誉的公司或网站作为我们的合作伙伴，但是每个合作伙伴都会有与{{websiteName}}不同的隐私保护政策，一旦您通过点击进入了合作伙伴的网站，{{websiteName}}隐私保护原则将不再生效，我们建议您查看该合作伙伴网站的隐私条款，并了解该合作伙伴对于收集、使用、泄露您的个人信息的规定。<br/><br/>

//您授权{{websiteName}}向中国人民银行个人信用信息基础数据库及其他有关政府机构、信息机构、公安机关、检察院、法院、金融机构了解您的信用信息和信用评估等信息资料，同时您授权{{websiteName}}及其他征信机构采集您的信用信息，并用于相关法律法规规章和规范性文件许可的用途。<br/><br/>

//您同意并确认，{{websiteName}}有权搜集、整理、加工并保存您的个人信息，{{websiteName}}将根据相关法规严格保护和使用该等信息和资料。<br/><br/>

//您授权{{websiteName}}将您所提供的信息和资料用于市场调研、客户服务支持、产品及服务的开发、向您推荐相关产品及服务、产品及服务的申请或开通、贷后管理、内部报告分析等用途，包括将您的信息和资料披露给经贷款人选择的有权限的人员、关联机构和服务商。<br/><br/>

//六．社区准则<br/>

//用户在使用{{websiteName}}服务过程中，必须遵循国家的相关法律法规，不得利用{{websiteName}}平台，发布危害国家安全、色情、暴力等非法内容；不得利用{{websiteName}}平台发布含有虚假、有害、胁迫、侵害他人隐私、骚扰、侵害、中伤、粗俗、或其它道德上令人反感的内容。 用户使用本服务的行为若有任何违反国家法律法规或侵犯任何第三方的合法权益的情形时，{{websiteName}}有权直接删除该等违反规定之内容。 除非与{{websiteName}}单独签订合同，否则不得将社区用于商业目的；{{websiteName}}仅供个人使用。 不可以通过自动方式创建账户，也不可以对账户使用自动系统执行操作。 用户影响系统总体稳定性或完整性的操作可能会被暂停或终止，直到问题得到解决。<br/><br/>

//七．免责声明<br/>

//互联网是一个开放平台，用户将照片等个人资料上传到互联网上，有可能会被其他组织或个人复制、转载、擅改或做其它非法用途，用户必须充分意识此类风险的存在。用户明确同意其使用{{websiteName}}服务所存在的风险将完全由其自己承担；因其使用{{websiteName}}服务而产生的一切后果也由其自己承担，我们对用户不承担任何责任。我们不保证服务一定能满足用户的要求，也不保证服务不会中断，对服务的及时性、安全性、准确性也都不作保证。对于因不可抗力或{{websiteName}}无法控制的原因造成的网络服务中断或其他缺陷，{{websiteName}}不承担任何 责任。我们不对用户所发布信息的删除或储存失败承担责任。我们有权判断用户的行为是否符合本网站使用协议条款之规定，如果我们认为用户违背了协议条款的规定，我们有终止向其提供网站服务的权利 。<br/><br/>

//八．服务变更、中断或终止<br/>

//如因系统维护或升级的需要而需暂停网络服务、服务功能的调整，{{websiteName}}将尽可能事先在网站上进行通告。 如发生下列任何一种情形，{{websiteName}}有权单方面中断或终止向用户提供服务而无需通知用户： ◇ 用户提供的个人资料不真实； ◇ 用户违反本服务条款中规定的使用规则； ◇ 未经{{websiteName}}同意，将{{websiteName}}平台用于商业目的。<br/><br/>

//九．服务条款的完善和修改<br/>

//{{websiteName}}会有权根据互联网的发展和中华人民共和国有关法律、法规的变化，不时地完善和修改{{websiteName}}服务条款。{{websiteName}}保留随时修改服务条款的权利，用户在使用{{websiteName}}平台服务时，有必要对最新的{{websiteName}}服务条款进行仔细阅读和重新确认，当发生有关争议时，请以最新的服务条款为准。<br/><br/>

//十．青少年用户特别提示<br/>

//青少年用户必须遵守全国青少年网络文明公约：要善于网上学习，不浏览不良信息；要诚实友好交流，不侮辱欺诈他人；要增强自护意识，不随意约会网友；要维护网络安全，不破坏网络秩序；要有益身心健康，不沉溺虚拟时空。<br/><br/>

//十一．特别约定<br/>

//用户使用本服务的行为若有任何违反国家法律法规或侵犯任何第三方的合法权益的情形时，{{websiteName}}有权直接删除该等违反规定之信息，并可以暂停或终止向该用户提供服务。 若用户利用本服务从事任何违法或侵权行为，由用户自行承担全部责任，因此给{{websiteName}}或任何第三方造成任何损失，用户应负责全额赔偿。<br/><br/>

//十二．其他<br/>

//本用户条款的订立、执行和解释及争议的解决均应适用中华人民共和国法律。 如用户就本协议内容或其执行发生任何争议，用户应尽量与我们友好协商解决；协商不成时，任何一方均可向{{websiteName}}所在地的人民法院提起诉讼。我们未行使或执行本服务协议任何权利或规定，不构成对前述权利或权利之放弃。如本用户条款中的任何条款无论因何种原因完全或部分无效或不具有执行力，本用户条款的其余条款仍应有效并且有约束力。<br/><br/>

//{{websiteName}}交易平台服务规则<br/><br/>

//我们，{{websiteName}}网站经营者通过{{websiteName}}网站无线端为您提供在线交易平台服务，您可与入驻{{websiteName}}网站的各类商家进行交易，包括但不限于查询商品和服务信息、达成交易意向并进行交易、对商品进行评价、参加{{websiteName}}组织的活动以及使用其它信息服务及技术服务（以下简称“交易平台服务”），就使用该等服务，您理解、同意并遵守交易平台服务规则（以下简称“本规则”）。<br/><br/>

//一、规则适用<br/>

//1. 如您选择使用交易平台服务，则视为您承诺接受并遵守本规则的约定。如果您不同意本规则的约定，您应立即停止注册/激活程序或停止使用交易平台服务。 2. 我们有权根据需要不时地制订、修改本规则，并以网站公示的方式进行公告，不再单独通知您。变更后的规则一经在网站公布后，立即自动生效。如您不同意相关变更，应当立即停止使用交易平台服务。您继续使用{{websiteName}}服务的，即表示您接受经修订的规则。<br/><br/>

//二、账户信息<br/>

//1. 您应当准确填写并及时更新您提供的电子邮件地址、联系电话、联系地址、邮政编码等联系方式，以便我们或商家与您进行有效联系，因通过这些联系方式无法与您取得联系，导致您在使用交易平台服务过程中产生任何损失或增加费用的，应由您完全独自承担。 2. 如有合理理由怀疑您提供的资料错误、不实、过时或不完整的，我们有权向您发出询问及/或要求改正的通知，并有权直接做出删除相应资料的处理，直至中止、终止对您提供部分或全部交易平台服务。我们对此不承担任何责任，您将承担因此产生的任何直接或间接损失及不利后果。 3. 您须自行负责对您的{{websiteName}}账号和密码保密，且须对您在该账号和密码下发生的所有活动（包括但不限于信息披露、发布信息、网上点击同意或提交各类规则协议、网上续签协议或购买服务等）承担责任。我们不能也不会对因您未能遵守本款规定而发生的任何损失负责。您理解我们对您的请求采取行动需要合理时间，我们对在采取行动前已经产生的后果（包括但不限于您的任何损失）不承担任何责任。<br/><br/>

//三、交易平台服务<br/>

//1. 通过我们提供的交易平台服务，您可在{{websiteName}}网站上发布交易信息、查询商品和服务信息、达成交易意向并进行交易、对商家进行评价、参加我们组织的活动以及使用其它信息服务及技术服务。 2. 您在{{websiteName}}网站上交易过程中与商家发生交易纠纷时，一旦您或商家任一方或双方共同提交我们要求调处，则我们有权根据单方判断做出调处决定，您了解并同意接受我们的判断和调处决定。如果商家与您不能通过“退款申请”（用户）与“退款申请的确认”（商家）功能完成对对交易纠纷的处理，则用户、商家都可以在工作时间通过电话和客服QQ的方式，联系我们。由我们向单方或双方了解事实情况后，判定交易货款（含运费）的归属办法。 3. 您了解并同意，我们有权应政府部门（包括司法及行政部门）的要求，向其提供您向我们提供的用户信息和交易记录等必要信息。如您涉嫌侵犯他人知识产权等合法权益，则我们亦有权在初步判断涉嫌侵权行为存在的情况下，向权利人提供您必要的身份信息。 4. 您在使用交易平台服务过程中，所产生的应纳税赋，以及一切硬件、软件、服务及其它方面的费用，均由您独自承担。<br/><br/>

//四、使用规范<br/>

//1. 在使用交易平台服务过程中，您承诺遵守以下约定： a) 在使用交易平台服务过程中实施的所有行为均遵守国家法律、法规等规范性文件及{{websiteName}}网站各项规则的规定和要求，不违背社会公共利益或公共道德，不损害他人的合法权益，不违反本规则及相关规则。您如果违反前述承诺，产生任何法律后果的，您应以自己的名义独立承担所有的法律责任，并确保我们免于因此产生任何损失 b) 在与商家交易过程中，遵守诚实信用原则，不扰乱网上交易的正常秩序，不从事与网上交易无关的行为。 c) 不以虚构或歪曲事实的方式不当评价商家，不采取不正当方式制造或提高自身的信用度，不采取不正当方式制造或提高（降低）商家的信用度。 d) 不对{{websiteName}}网站上的任何数据作商业性利用，包括但不限于在未经我们事先书面同意的情况下，以复制、传播等任何方式使用{{websiteName}}网站站上展示的资料。 e) 不使用任何装置、软件或例行程序干预或试图干预{{websiteName}}网站的正常运作或正在{{websiteName}}网站上进行的任何交易、活动。 2. 您了解并同意： a) 我们有权对您是否违反上述承诺做出单方认定，并根据单方认定结果适用规则予以处理或终止向您提供服务，且无须征得您的同意或提前通知予您。 b) 基于维护{{websiteName}}网站交易秩序及交易安全的需要， 我们有权在发生恶意购买等扰乱市场正常交易秩序的情形下，执行关闭相应交易订单等操作。 c) 经国家行政或司法机关的生效法律文书确认您存在违法或侵权行为，或者我们根据自身的判断，认为您的行为涉嫌违反本规则和/或其他规则的条款或涉嫌违反法律法规的规定的，则我们有权在{{websiteName}}网站上公示您的该等涉嫌违法或违约行为及我们已对您采取的措施。 d) 对于您在{{websiteName}}网站上发布的涉嫌违法或涉嫌侵犯他人合法权利或违反本规则和/或规则的信息，我们有权不经通知您即予以删除，且按照本规则的规定进行处罚。 e) 对于您在{{websiteName}}网站上实施的行为，包括您未在{{websiteName}}网站上实施但已经对{{websiteName}}网站及其用户产生影响的行为，我们有权单方认定您行为的性质及是否构成对本规则和/或规则的违反，并据此作出相应处罚。您应自行保存与您行为有关的全部证据，并应对无法提供充要证据而承担的不利后果。 f) 对于您涉嫌违反承诺的行为对任意第三方造成损害的，您均应当以自己的名义独立承担所有的法律责任，并应确保我们免于因此产生损失或增加费用。 g) 如您涉嫌违反有关法律或者本规则之规定，使我们遭受任何损失，或受到任何第三方的索赔，或受到任何行政管理部门的处罚，您应当赔偿我们因此造成的损失及（或）发生的费用，包括合理的律师费用。<br/><br/>

//五、特别授权<br/>

//您完全理解并不可撤销地授予我们下列权利： 1. 您完全理解并不可撤销地授权我们根据本规则及{{websiteName}}规则的规定，处理您在{{websiteName}}网站上发生的所有交易及可能产生的交易纠纷。您同意接受我们的判断和调处决定。如果商家与您不能通过“退款申请”（用户）与“退款申请的确认”（商家）功能完成对交易纠纷的处理，则用户、商家都可以在工作时间联系我们。由我们向单方或双方了解事实情况后，判定交易货款（含运费）的归属办法。 2. 一旦您违反本规则，或与我们签订的其他协议的约定，我们有权以任何方式中止、终止对您提供部分或全部服务，且在其经营或实际控制的任何网站公示您的违约情况。 3. 对于您提供的资料及数据信息，您授予我们及我们的关联公司独家的、全球通用的、永久的、免费的许可使用权利 (并有权对该权利进行再授权)。此外，我们及我们的关联公司有权(全部或部份地) 使用、复制、修订、改写、发布、翻译、分发、执行和展示您的全部资料数据（包括但不限于注册资料、交易行为数据及全部展示于{{websiteName}}网站的各类信息）或制作其衍生作品，并以现在已知或日后开发的任何形式、媒体或技术，将上述信息纳入其它作品内。<br/><br/>

//六、责任范围和责任限制<br/>

//1. 我们负责按“现状”和“可得到”的状态向您提供交易平台服务。但我们对交易平台服务不作任何明示或暗示的保证，包括但不限于交易平台服务的适用性、没有错误或疏漏、持续性、准确性、可靠性、适用于某一特定用途。同时，我们也不对交易平台服务所涉及的技术及信息的有效性、准确性、正确性、可靠性、质量、稳定、完整和及时性作出任何承诺和保证。 2. 您了解{{websiteName}}网站上的信息系用户及商家自行发布，且可能存在风险和瑕疵。{{websiteName}}网站仅作为交易地点。{{websiteName}}网站仅作为您获取物品或服务信息、物色交易对象、就物品和/或服务的交易进行协商及开展交易的场所，但我们无法控制交易所涉及的物品的质量、安全或合法性，商贸信息的真实性或准确性，以及交易各方履行其在贸易协议中各项义务的能力。您应自行谨慎判断确定相关物品及/或信息的真实性、合法性和有效性，并自行承担因此产生的责任与损失。 3. 除非法律法规明确要求，否则，我们没有义务对所有用户的信息数据、商品（服务）信息、交易行为以及与交易有关的其它事项进行事先审查。 4. 您在{{websiteName}}网站上交易过程中与商家发生交易纠纷时，一旦您或商家任一方或双方共同提交我们要求调处，则我们有权根据单方判断做出调处决定，您了解并同意接受我们的判断和调处决定。如果商家与您不能通过“退款申请”（用户）与“退款申请的确认”（商家）功能完成对对交易纠纷的处理，则用户、商家都可以在工作时间联系我们。由我们向单方或双方了解事实情况后，判定交易货款（含运费）的归属办法。 5. 您了解并同意，我们不对因下述任一情况而导致您的任何损害赔偿承担责任，包括但不限于利润、商誉、使用、数据等方面的损失或其它无形损失的损害赔偿 (无论我们是否已被告知该等损害赔偿的可能性) ： a) 使用或未能使用交易平台服务。 b) 第三方未经批准的使用您的账户或更改您的数据。 c) 通过交易平台服务购买或获取任何商品、样品、数据、信息或进行交易等行为或替代行为产生的费用及损失。 d) 您对交易平台服务的误解。 e) 任何非因我们的原因而引起的与交易平台服务有关的其它损失。 6. 不论在何种情况下，我们均不对由于信息网络正常的设备维护，信息网络连接故障，电脑、通讯或其他系统的故障，电力故障，罢工，劳动争议，暴乱，起义，骚乱，生产力或生产资料不足，火灾，洪水，风暴，爆炸，战争，政府行为，司法行政机关的命令或第三方的不作为而造成的不能服务或延迟服务承担责任。<br/><br/>

//七、规则终止<br/>

//1. 您同意，我们有权自行全权决定以任何理由不经事先通知的中止、终止向您提供部分或全部交易平台服务，暂时冻结或永久冻结（注销）您的账户在{{websiteName}}网站及交易平台的权限，且无须为此向您或任何第三方承担任何责任。 2. 出现以下情况时，我们有权直接以注销账户的方式终止本规则，并有权永久冻结（注销）您的账户在{{websiteName}}网站及交易平台的权限和收回账户对应的账号： a) 我们终止向您提供服务后，您涉嫌再一次直接或间接或以他人名义注册为{{websiteName}}用户的。 b) 您提供的电子邮箱不存在或无法接收电子邮件，且没有其他方式可以与您进行联系，或我们以其它联系方式通知您更改电子邮件信息，而您在我们通知后三个工作日内仍未更改为有效的电子邮箱的。 c) 您提供的用户信息中的主要内容不真实或不准确或不及时或不完整。 d) 本规则变更时，您明示并通知我们不愿接受新的服务规则的。 e) 其它我们认为应当终止服务的情况。 3. 您的账户服务被终止或者账户在{{websiteName}}网站的权限被永久冻结（注销）后，我们没有义务为您保留或向您披露您账户中的任何信息，也没有义务向您或第三方转发任何您未曾阅读或发送过的信息。 4. 您同意，您与我们的合同关系终止后，我们仍享有下列权利： a) 继续保存您的用户信息及您使用交易平台服务期间的所有交易信息。 b) 您在使用交易平台服务期间存在违法行为或违反本规则和/或规则的行为的，我们仍可依据本规则向您主张权利。 5. 我们中止或终止向您提供交易平台服务后，对于您在服务中止或终止之前的交易行为依下列原则处理，您应独力处理并完全承担进行以下处理所产生的任何争议、损失或增加的任何费用，并应确保我们免于因此产生任何损失或承担任何费用： a) 您在服务中止或终止之前已经与商家达成买卖合同，但合同尚未实际履行的，我们有权删除该买卖合同及其交易物品的相关信息。 b) 您在服务中止或终止之前已经与商家达成买卖合同且已部分履行的，我们可以不删除该项交易，但我们有权在中止或终止服务的同时将相关情形通知您的交易对方。<br/><br/>

//八、其他<br/>

//本规则为《{{websiteName}}服务条款》不可分割的组成部分，与《{{websiteName}}服务条款》具有同等法律效力。除另行明确声明外，本规则未约定的内容以《{{websiteName}}服务条款》的约定为准。<br/><br/>