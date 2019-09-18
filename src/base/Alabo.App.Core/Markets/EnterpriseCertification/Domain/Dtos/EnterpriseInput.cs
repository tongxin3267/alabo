using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Core.Markets.EnterpriseCertification.Domain.Dtos {

    public class EnterpriseInput {

        /// <summary>
        /// 联系电话
        /// </summary>
        [Display(Name = "联系电话")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, Width = "80", SortOrder = 3)]
        public string Mobile { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Display(Name = "联系人")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, Width = "80", SortOrder = 2)]
        public string LinkMan { get; set; }

        /// <summary>
        /// 地址Id
        /// </summary>
        [Display(Name = "地址")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.CityDropList, Width = "80", SortOrder = 4)]
        public long RegionId { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        [Display(Name = "详细地址")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, Width = "80", SortOrder = 5)]
        public string Address { get; set; }

        /// <summary>
        /// 企业主页
        /// </summary>
        [Display(Name = "企业主页")]
        [Field(ControlsType = ControlsType.TextBox, Width = "80", SortOrder = 6)]
        public string EnterpriseUrl { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        [Display(Name = "企业名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextArea, Width = "80", SortOrder = 1)]
        public string EnterpriseName { get; set; }

        /// <summary>
        /// 企业主要产品
        /// </summary>
        [Display(Name = "主要产品")]
        [Field(ControlsType = ControlsType.TextArea, Width = "80", SortOrder = 7)]
        public string EnterpriseProductIntro { get; set; }

        /// <summary>
        /// 应用领域
        /// </summary>
        [Display(Name = "应用领域")]
        [Field(ControlsType = ControlsType.TextArea, Width = "80", SortOrder = 4)]
        public string ApplicationArea { get; set; }

        /// <summary>
        /// 营业执照号码
        /// </summary>
        [Display(Name = "营业执照号码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, Width = "80", SortOrder = 900)]
        public string LicenseNumber { get; set; }

        /// <summary>
        /// 会员Id
        /// </summary>
        public long LoginUserId { get; set; }

        /// <summary>
        /// 营业执照图
        /// </summary>
        [Display(Name = "营业执照图")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.ImagePreview, Width = "80", SortOrder = 1000)]
        public string LicenseImg { get; set; }
    }
}