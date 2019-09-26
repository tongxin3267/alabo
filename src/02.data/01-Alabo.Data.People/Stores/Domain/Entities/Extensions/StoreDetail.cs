using System;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Entities.Extensions;
using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.Data.People.Stores.Domain.Entities.Extensions
{
    /// <summary>
    ///     店铺详细属性
    /// </summary>
    public class StoreDetail : EntityExtension
    {
        /// <summary>
        ///     主题市场Guid
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, ListShow = true, SortOrder = 2, EditShow = true)]
        [Display(Name = "主题市场")]
        [HelpBlock("主题市场")]
        public Guid StoreMarketConfigId { set; get; }

        /// 公司名称
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, ListShow = true, SortOrder = 2, EditShow = true)]
        [Display(Name = "公司名称")]
        [HelpBlock("公司名称")]
        public string CompanyName { get; set; }

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
        ///     可以有多个联系人，List<LinkMan> 的json数据
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
        ///     图片
        /// </summary>
        [Display(Name = "图片")]
        [HelpBlock("图片")]
        public string ImageUrl { get; set; }

        /// <summary>
        ///     市场
        /// </summary>
        [Display(Name = "市场")]
        public string Market { get; set; }

        /// <summary>
        ///     档口
        /// </summary>
        [Display(Name = "档口")]
        public string Stall { get; set; }

        /// <summary>
        ///     货物
        /// </summary>
        [Display(Name = "货物")]
        public string Goods { get; set; }

        /// <summary>
        ///     楼层
        /// </summary>
        [Display(Name = "楼层")]
        public string Floor { get; set; }

        /// <summary>
        ///     主营
        /// </summary>
        [Display(Name = "主营")]
        public string Laser { get; set; }

        /// <summary>
        ///     企业ID
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, ListShow = true, EditShow = false, SortOrder = 10)]
        [Display(Name = "企业")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long CompanyID { get; set; }

        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, ListShow = true, EditShow = false, SortOrder = 10)]
        [Display(Name = "店铺QQ")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string StoreQQ { get; set; }

        /// <summary>
        ///     店铺的模板主题
        /// </summary>
        [Display(Name = "店铺的模板主题")]
        public string ThemeName { get; set; }
    }
}