using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Regexs;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Markets.EnterpriseCertification.Domain.Entities {

    /// <summary>
    /// 商家认证
    /// 企业资质认证
    /// </summary>
    [BsonIgnoreExtraElements]//PostApi = "Api/Enterprise/EnterpriseList", ListApi = "Api/Enterprise/EnterpriseList"
    [ClassProperty(Name = "企业认证", Icon = "fa fa-puzzle-piece", Description = "企业认证",
        PageType = ViewPageType.List)]
    [Table("Cloud_People_Enterprise")]
    public class Enterprise : AggregateMongodbRoot<Enterprise> {

        /// <summary>`
        /// </summary>
        [Display(Name = "联系电话")]
        [RegularExpression(RegularExpressionHelper.ChinaMobile, ErrorMessage = ErrorMessage.NotMatchFormat)]
        [HelpBlock("请您务必输入联系电话")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, Width = "80", SortOrder = 6)]
        public string Mobile { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Display(Name = "联系人")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请您务必输入联系人")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, Width = "80", SortOrder = 5, IsShowBaseSerach = true, IsShowAdvancedSerach = true)]
        public string LinkMan { get; set; }

        /// <summary>
        /// 地址Id
        /// </summary>
        [Display(Name = "地址")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请您务必选择企业所在区域")]
        [Field(ControlsType = ControlsType.CityDropList, ListShow = true, Width = "80", SortOrder = 7)]
        public long RegionId { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [Display(Name = "详细地址")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请您务必填写企业所在详细地址")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "80", EditShow = true, SortOrder = 8)]
        public string Address { get; set; }

        /// <summary>
        /// 企业主页
        /// </summary>
        [Display(Name = "企业主页")]
        [HelpBlock("请您输入企业主页地址")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "80", EditShow = true, SortOrder = 2)]
        public string EnterpriseUrl { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        [Display(Name = "企业名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请您务必填写企业名称【必须与营业执照图片保持一致】")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "80", EditShow = true, SortOrder = 1, IsShowBaseSerach = true, IsShowAdvancedSerach = true)]
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 企业主要产品
        /// </summary>
        [Display(Name = "主要产品")]
        [HelpBlock("请您填写您企业主要产品")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "80", EditShow = true, SortOrder = 3)]
        public string EnterpriseProductIntro { get; set; }

        /// <summary>
        /// 应用领域
        /// </summary>
        [Display(Name = "应用领域")]
        [HelpBlock("请您填写企业应用领域")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = false, Width = "80", EditShow = true, SortOrder = 4, IsShowBaseSerach = true, IsShowAdvancedSerach = true)]
        public string ApplicationArea { get; set; }

        /// <summary>
        /// 营业执照号码
        /// </summary>
        [Display(Name = "营业执照号码")]
        [HelpBlock("请您务必填写营业执照号码【必须与营业执照图片保持一致】")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "80", EditShow = true, SortOrder = 900, IsShowBaseSerach = true, IsShowAdvancedSerach = true)]
        public string LicenseNumber { get; set; }

        /// <summary>
        /// 营业执照图
        /// </summary>
        [Display(Name = "营业执照图")]
        [HelpBlock("请您务必上传企业营业执照图【图片清晰】")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.AlbumUploder, ListShow = false, EditShow = true, Width = "80", SortOrder = 1000)]
        public string LicenseImage { get; set; }

        /// <summary>
        /// 会员Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 门店坐标（经度）
        /// </summary>
        [Display(Name = "门店坐标（经度）")]
        public decimal Longitude { get; set; }

        /// <summary>
        /// 门店坐标（纬度）
        /// </summary>
        [Display(Name = "门店坐标（纬度）")]
        public decimal Latitude { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        [HelpBlock("请输入状态")]
        public IdentityStatus Status { get; set; }
    }
}