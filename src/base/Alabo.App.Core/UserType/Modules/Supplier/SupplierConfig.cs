using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Core.UserType.Modules.Supplier {

    /// <summary>
    ///     供应商配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "供应商配置", Icon = "fa fa-user-times", Description = "供应商配置", PageType = ViewPageType.Edit,
        SortOrder = 12, SideBarType = SideBarType.SupplierSideBar)]
    public class SupplierConfig : BaseViewModel, IAutoConfig {

        ///// <summary>
        ///// 店铺的模板主题
        ///// </summary>
        //public string ThemeName { get; set; }
        ///// <summary>
        ///// 尚衣阁服饰
        ///// </summary>
        //public string Dress { get; set; }
        ///// <summary>
        ///// 市场
        ///// </summary>
        //public string Market { get; set; }
        ///// <summary>
        ///// 档口
        ///// </summary>
        //public string Stall { get; set; }
        ///// <summary>
        ///// 货物
        ///// </summary>
        //public string Goods { get; set; }
        ///// <summary>
        ///// 楼层
        ///// </summary>
        //public string Floor { get; set; }
        ///// <summary>
        ///// 主营
        ///// </summary>
        //public string Laser { get; set; }
        ///// <summary>
        ///// 人气
        ///// </summary>
        //public string Popularity { get; set; }
        ///// <summary>
        ///// 综合评分
        ///// </summary>
        //public decimal TotalScore { get; set; }
        ///// <summary>
        ///// 商品得分
        ///// </summary>
        //public decimal ProductScore { get; set; }
        ///// <summary>
        ///// 服务得分
        ///// </summary>
        //public decimal ServiceScore { get; set; }
        ///// <summary>
        ///// 物流得分
        ///// </summary>
        //public decimal LogisticsScore { get; set; }
        /// <summary>
        ///     主题市场Guid
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, ListShow = true, SortOrder = 2, EditShow = true)]
        [Display(Name = "主题市场")]
        [HelpBlock("主题市场")]
        public Guid StoreMarketConfigId { set; get; }

        ///// <summary>
        ///// 主题市场
        ///// </summary>
        //[Field("主题市场", ControlsType.TextBox, GroupTabId = 1, ListShow = true, SortOrder = 2, EditShow = true)]
        //[Display(Name = "主题市场")]
        //[HelpBlock("主题市场")]
        //public StoreMarketConfig StoreMarketConfig { set; get; }
        /// <summary>
        ///     公司名称
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, ListShow = true, SortOrder = 2, EditShow = true)]
        [Display(Name = "公司名称")]
        [HelpBlock("公司名称")]
        public string CompanyName { get; set; }

        [Field(ControlsType = ControlsType.Switch, GroupTabId = 1, ListShow = true, SortOrder = 2, EditShow = true)]
        [Display(Name = "商家返利")]
        [HelpBlock("开启商家返利")]
        public bool OpenRebate { get; set; }

        /// <summary>
        ///     详细介绍
        /// </summary>
        [Field(ControlsType = ControlsType.TextArea, GroupTabId = 1, SortOrder = 15)]
        [Display(Name = "详细介绍")]
        [HelpBlock("详细介绍")]
        public string Intro { get; set; }

        /// <summary>
        ///     区域ID
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, SortOrder = 14)]
        [Display(Name = "所在区域")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("所在区域")]
        public long AreaID { get; set; }

        /// <summary>
        ///     商圈Id,比如黄河时装、南城步行街等
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, SortOrder = 14)]
        [Display(Name = "所在商圈")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("所在商圈")]
        public long CircleId { get; set; }

        /// <summary>
        ///     详细地址
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, SortOrder = 5)]
        [Display(Name = "详细地址")]
        [HelpBlock("详细地址")]
        public string Address { get; set; }

        /// <summary>
        ///     联系方式
        ///     可以有多个联系人，List<LinkMan>的json数据
        /// </summary>
        public string Contacts { get; set; }

        /// <summary>
        ///     已交费用
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 2, SortOrder = 10)]
        [Display(Name = "已交费用")]
        [HelpBlock("已交费用")]
        public string PayCost { get; set; }

        /// <summary>
        ///     服务承诺
        /// </summary>
        [Field(ControlsType = ControlsType.TextArea, GroupTabId = 2, SortOrder = 16)]
        [Display(Name = "服务承诺")]
        [HelpBlock("服务承诺")]
        public string Promise { get; set; }

        /// <summary>
        ///     商家资质
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 2, SortOrder = 11)]
        [Display(Name = "商家资质")]
        [HelpBlock("商家资质")]
        public string Qualification { get; set; }

        /// <summary>
        ///     经营范围
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 2, SortOrder = 12)]
        [Display(Name = "经营范围")]
        [HelpBlock("经营范围")]
        public string Service { get; set; }

        /// <summary>
        ///     官网
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 2, SortOrder = 13)]
        [Display(Name = "官网")]
        [HelpBlock("官网网站地址")]
        public string Url { get; set; }

        /// <summary>
        ///     服务条款
        ///     留空时使用默认条款
        /// </summary>
        [Field(ControlsType = ControlsType.Editor, Row = 15)]
        [Display(Name = "供应商服务条款")]
        [HelpBlock("请输入供应商服务条款，默认值为供应商服务条款")]
        public string SupplierServiceAgreement { get; set; } = "供应商服务条款";

        public void SetDefault() {
            //  throw new NotImplementedException();
        }
    }
}