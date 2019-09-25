using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;using MongoDB.Bson.Serialization.Attributes;
using Alabo.App.Core.Common;
using Alabo.AutoConfigs;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Shop.Store.Domain.CallBacks {

    [NotMapped]
    [ClassProperty(Name = "商品配送配置", GroupName = "商品配送配置", Icon = "fa fa-cloud-upload", Description = "商品图片上传",
        SortOrder = 400,
        SideBarType = SideBarType.SupplierSideBar
    )]
    //SideBar = "Shop/StoreSideBar"
    public class ProductDeliveryConfig : BaseViewModel, IAutoConfig {

        /// <summary>
        ///     系统类型
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, SortOrder = 1, ListShow = true,
            DataSource = "Alabo.App.Shop.Store.Domain.CallBacks.DeliveryType")]
        [Display(Name = "配送方式")]
        [HelpBlock("配送方式，平台或者店铺订单配送时的选择")]
        public DeliveryType DeliveryType { get; set; } = DeliveryType.ManualAllot;

        public void SetDefault() {
        }
    }

    [ClassProperty(Name = "分配类型")]
    public enum DeliveryType {

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "手动分配")]
        ManualAllot,

        [LabelCssClass(BadgeColorCalss.Success)]
        [Display(Name = "智能分配")]
        AutoAllot
    }
}