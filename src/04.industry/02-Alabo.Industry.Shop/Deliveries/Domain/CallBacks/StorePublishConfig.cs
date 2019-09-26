using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.AutoConfigs;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.Industry.Shop.Deliveries.Domain.CallBacks
{
    [NotMapped]
    /// <summary>
    /// 店铺商品发布规则
    /// </summary>
    [ClassProperty(Name = "入驻规则", SortOrder = 449, Icon = "fa fa-bullseye", GroupName = "Shop",
        PageType = ViewPageType.Edit,
        SideBarType = SideBarType.SupplierSideBar)]
    //SideBar = "Shop/StoreSideBar")]
    public class StorePublishConfig : BaseViewModel, IAutoConfig
    {
        [Field(ControlsType = ControlsType.Switch)]
        [Display(Name = "是否启用商家入驻申请")]
        [HelpBlock("关闭以后供应商在前台不可以申请")]
        public bool IsEnableApply { get; set; } = true;

        [Field(ControlsType = ControlsType.Switch)]
        [Display(Name = "申请时开启等级")]
        [HelpBlock("开启后，供应商在前台申请时候，需要填写等级，修改<a>供应商等级</a>")]
        public bool IsOpenSupplierGrade { get; set; } = true;

        [Field(ControlsType = ControlsType.Switch)]
        [Display(Name = "供应商商品是否要审核")]
        [HelpBlock("关闭后，商品将不审核，将直接上架平台，可以进行购买")]
        public bool IsProductCheck { get; set; } = true;

        [Field(ControlsType = ControlsType.Switch)]
        [Display(Name = "平台是否是否收取分润")]
        [HelpBlock("入驻商家商品销售，分配给平台的的分润。关闭时候不收取分润，开启时候根据供应商等级设置的分润比例收取分润")]
        public bool IsExtractingFenRun { get; set; } = true;

        [Field(ControlsType = ControlsType.Editor)]
        [Display(Name = "商家入驻协议")]
        [HelpBlock("商家入驻协议")]
        public string Agreement { get; set; }

        public void SetDefault()
        {
        }
    }
}