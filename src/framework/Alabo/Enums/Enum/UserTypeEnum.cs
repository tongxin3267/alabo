using System.ComponentModel.DataAnnotations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Core.Enums.Enum
{
    [ClassProperty(Name = "用户身份类型")]
    public enum UserTypeEnum
    {
        /// <summary>
        ///     会员
        ///     系统会员，每个用户类型必须是会员
        /// </summary>
        [Display(Name = "会员")]
        [Field(IsDefault = true, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A365000", Icon = "icon-user-following")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Member = 0,

        /// <summary>
        ///     股东
        /// </summary>
        [Display(Name = "股东")]
        [Field(IsDefault = true, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A365222", Icon = "socicon-slideshare ")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        ShareHolders = 2,

        /// <summary>
        ///     员工
        /// </summary>
        [Display(Name = "员工")]
        [Field(IsDefault = true, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A365333", Icon = "socicon-persona ")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Employees = 3,

        /// <summary>
        ///     OEM
        /// </summary>
        [Display(Name = "OEM")]
        [Field(IsDefault = false, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A365444", Icon = "icon-book-open")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Oem = 4,

        /// <summary>
        ///     合伙伙伴
        /// </summary>
        [Display(Name = "合伙伙伴")]
        [Field(IsDefault = false, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A365555", Icon = "icon-users")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        StrategicPartners = 5,

        /// <summary>
        ///     门店
        /// </summary>
        [Display(Name = "门店")]
        [Field(IsDefault = true, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A365666", Icon = "la la-github-square ")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        ServiceCenter = 6,

        /// <summary>
        ///     省代理
        /// </summary>
        [Display(Name = "省代理")]
        [Field(IsDefault = true, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A365101", Icon = "la la-line-chart ")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Province = 101,

        /// <summary>
        ///     市代理
        /// </summary>
        [Display(Name = "市代理")]
        [Field(IsDefault = true, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A365102", Icon = "la la-jsfiddle ")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        City = 102,

        /// <summary>
        ///     区县代理
        /// </summary>
        [Display(Name = "区县代理")]
        [Field(IsDefault = true, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A365103", Icon = "flaticon-technology-1")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        County = 103,

        /// <summary>
        ///     大区
        /// </summary>
        [Display(Name = "大区")]
        [Field(IsDefault = false, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A365106", Icon = "icon-graph")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Regional = 106,

        /// <summary>
        ///     圈主
        /// </summary>
        [Display(Name = "圈主")]
        [Field(IsDefault = false, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A365104", Icon = "icon-globe-alt")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Circle = 104,

        /// <summary>
        ///     商家(门店),表示线下实体店
        ///     类型Merchants，一定要有线下实体店，在实现会员锁定分润等等有特殊的用途
        /// </summary>
        [Display(Name = "商家")]
        [Field(IsDefault = false, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A365999", Icon = "icon-badge")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Merchants = 9,

        /// <summary>
        ///     供应商，为商城提供产品的厂家
        /// </summary>
        [Display(Name = "供应商")]
        [Field(IsDefault = false, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A365010", Icon = "icon-action-redo")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Supplier = 10,

        /// <summary>
        ///     平台
        /// </summary>
        [Display(Name = "平台")]
        [Field(IsDefault = false, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A361001", Icon = "flaticon-apps ")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Platform = 1001,

        /// <summary>
        ///     自定义
        /// </summary>
        [Display(Name = "自定义")] [Field(IsDefault = false)] [LabelCssClass(BadgeColorCalss.Metal)]
        Customer = -1,

        /// <summary>
        ///     微代理
        /// </summary>
        [Display(Name = "微代理")]
        [Field(IsDefault = false, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A365105", Icon = "icon-direction")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Agent = 105,

        /// <summary>
        ///     商学院
        /// </summary>
        [Display(Name = "商学院")]
        [Field(IsDefault = false, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A365107", Icon = "icon-graduation")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        School = 107,

        /// <summary>
        ///     合资公司
        /// </summary>
        [Display(Name = "合资公司")]
        [Field(IsDefault = false, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A365108", Icon = " icon-refresh")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        VentureCompany = 108,

        /// <summary>
        ///     商学院
        /// </summary>
        [Display(Name = "分公司")]
        [Field(IsDefault = false, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A365109", Icon = "icon-reload")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        BranchCompnay = 109,

        /// <summary>
        ///     创客
        /// </summary>
        [Display(Name = "创客")]
        [Field(IsDefault = false, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A365110", Icon = "flaticon-security")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        ChuangKe = 110,

        /// <summary>
        ///     服务员
        /// </summary>
        [Display(Name = "服务员")]
        [Field(IsDefault = false, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A365111", Icon = "flaticon-security")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Waiter = 111,

        /// <summary>
        ///     合伙人
        /// </summary>
        [Display(Name = "内部合伙人")]
        [Field(IsDefault = false, GuidId = "71BE65E6-3A64-414D-972E-1A3D4A365112", Icon = "flaticon-profile-1 ")]
        [LabelCssClass(BadgeColorCalss.Metal)]
        Partner = 112
    }
}