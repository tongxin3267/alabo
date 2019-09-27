using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Cloud.People.Enterprise.Domain.Services;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Core.WebApis;
using Alabo.Framework.Core.WebUis;
using Alabo.Maps;
using Alabo.UI;
using Alabo.UI.Design.AutoForms;
using Alabo.Users.Enum;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Cloud.People.Enterprise.UI
{
    /// <summary>
    ///     实名认证
    /// </summary>
    [Display(Name = "企业认证")]
    [ClassProperty(Name = "企业认证", Description = "企业认证")]
    public class EnterpriseAutoForm : UIBase, IAutoForm
    {
        /// <summary>
        ///     `
        /// </summary>
        [Display(Name = "联系电话")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请您务必输入联系电话")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, Width = "80", SortOrder = 6)]
        public string Mobile { get; set; }

        /// <summary>
        ///     联系人
        /// </summary>
        [Display(Name = "联系人")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请您务必输入联系人")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, EditShow = true, Width = "80", SortOrder = 5,
            IsShowBaseSerach = true, IsShowAdvancedSerach = true)]
        public string LinkMan { get; set; }

        /// <summary>
        ///     地址Id
        /// </summary>
        [Display(Name = "区域")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请您务必选择企业所在区域")]
        [Field(ControlsType = ControlsType.CityDropList, ListShow = true, Width = "80", SortOrder = 7)]
        public long RegionId { get; set; }

        /// <summary>
        ///     详细地址
        /// </summary>
        [Display(Name = "详细地址")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请您务必填写企业所在详细地址")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "80", EditShow = true, SortOrder = 8)]
        public string Address { get; set; }

        /// <summary>
        ///     企业主页
        /// </summary>
        [Display(Name = "企业主页")]
        [HelpBlock("请您输入企业主页地址")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "80", EditShow = true, SortOrder = 2)]
        public string EnterpriseUrl { get; set; }

        /// <summary>
        ///     企业名称
        /// </summary>
        [Display(Name = "企业名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请您务必填写企业名称")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "80", EditShow = true, SortOrder = 1,
            IsShowBaseSerach = true, IsShowAdvancedSerach = true)]
        public string EnterpriseName { get; set; }

        /// <summary>
        ///     企业主要产品
        /// </summary>
        [Display(Name = "主要产品")]
        [HelpBlock("请您填写您企业主要产品")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "80", EditShow = true, SortOrder = 3)]
        public string EnterpriseProductIntro { get; set; }

        /// <summary>
        ///     应用领域
        /// </summary>
        [Display(Name = "应用领域")]
        [HelpBlock("请您填写企业应用领域")]
        [Field(ControlsType = ControlsType.TextBox, ListShow = false, Width = "80", EditShow = true, SortOrder = 4,
            IsShowBaseSerach = true, IsShowAdvancedSerach = true)]
        public string ApplicationArea { get; set; }

        /// <summary>
        ///     营业执照号码
        /// </summary>
        [Display(Name = "营业执照号码")]
        [HelpBlock("请您务必填写营业执照号码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, Width = "80", EditShow = true, SortOrder = 900,
            IsShowBaseSerach = true, IsShowAdvancedSerach = true)]
        public string LicenseNumber { get; set; }

        /// <summary>
        ///     营业执照图
        /// </summary>
        [Display(Name = "营业执照图")]
        [HelpBlock("请您务必上传企业营业执照图【图片清晰】")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.AlbumUploder, ListShow = false, EditShow = true, Width = "80",
            SortOrder = 1000)]
        public string LicenseImage { get; set; }

        /// <summary>
        ///     会员Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     门店坐标（经度）
        /// </summary>
        [Display(Name = "门店坐标（经度）")]
        public decimal Longitude { get; set; }

        /// <summary>
        ///     门店坐标（纬度）
        /// </summary>
        [Display(Name = "门店坐标（纬度）")]
        public decimal Latitude { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        [HelpBlock("请输入状态")]
        public IdentityStatus Status { get; set; }

        public AutoForm GetView(object id, AutoBaseModel autoModel)
        {
            var model = Resolve<IEnterpriseService>().GetSingle(u => u.UserId == id.ConvertToLong(0));
            var result = new AutoForm();
            if (model != null)
            {
                if (model.Status == IdentityStatus.Failed)
                    result = ToAutoForm(new Domain.Entities.Enterprise());
                else //if (model.Status == IdentityStatus.Succeed)
                    //{
                    result.FromMessage = new FromMessage(FromMessageType.Success, "恭喜您,企业认证已成功");
                //}
            }
            else
            {
                result = ToAutoForm(new Domain.Entities.Enterprise());
            }

            result.AlertText = "【企业认证】主要对企业信息真实性进行的一种验证审核。有助于建立完善可靠的互联网信用平台。";
            result.ButtomHelpText = new List<string>
            {
                "请您务必填写联系电话、联系人及其详细地址。",
                "请您务必上传【营业执照】扫描件（含副页），图片清晰。否则不予通过！",
                "请您认真检查所填【企业名称】与【营业执照证号】，必须与图片保持一致。否则不予通过！",
                "提交后将在3个工作日内进行审核"
            };

            return result;
        }

        public ServiceResult Save(object model, AutoBaseModel autoModel)
        {
            var enterprise = (EnterpriseAutoForm) model;
            enterprise.Status = IdentityStatus.IsPost;
            var result = Resolve<IEnterpriseService>().AddOrUpdate(enterprise.MapTo<Domain.Entities.Enterprise>());

            return result ? ServiceResult.Success : ServiceResult.Failed;
        }
    }
}