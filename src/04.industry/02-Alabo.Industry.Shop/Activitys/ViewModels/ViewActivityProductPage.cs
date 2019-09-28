using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.Mongo.Extension;
using Alabo.Extensions;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.UI.Enum;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Alabo.Industry.Shop.Activitys.ViewModels
{
    /// <summary>
    ///     Class ViewActivityProductPage.
    /// </summary>
    [ClassProperty(Name = "活动商品")]
    public class ViewActivityProductPage : BaseViewModel
    {
        /// <summary>
        ///     Key
        /// </summary>
        [Display(Name = "标识")]
        public long Id { get; set; }

        /// <summary>
        ///     活动对应的店铺Id
        /// </summary>
        [Display(Name = "活动对应的店铺Id")]
        [JsonConverter(typeof(ObjectIdConverter))] public ObjectId StoreId { get; set; }

        /// <summary>
        ///     活动名称:比如活动券，满就送，一元夺宝等等
        /// </summary>
        [Display(Name = "商品名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(IsShowBaseSerach = true, SortOrder = 1, ControlsType = ControlsType.TextBox, IsMain = true,
            EditShow = true, ListShow = true, Width = "300", Link = "/Admin/Activitys/Edit?Key=[[Key]]&Id=[[Id]]")]
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the bn.
        /// </summary>
        [Field]
        [Display(Name = "货号")]
        public string Bn { get; set; }

        /// <summary>
        ///     活动的具体配置信息
        ///     显示活动配置中List=true的字段
        /// </summary>
        [Display(Name = "配置信息")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "80", ListShow = true,
            DataSource = "ConfigDictionaryType", Mark = "Type", SortOrder = 2)]
        public Dictionary<string, object> ConfigDictionary { get; set; } = new Dictionary<string, object>();

        /// <summary>
        ///     Gets or sets the full name of the configuration dictionary.
        ///     自动配置的全部命名空间
        /// </summary>
        [Display(Name = "自动配置的全部命名空间")]
        public Type ConfigDictionaryType { get; set; }

        /// <summary>
        ///     所属营销活动类型，如：Alabo.App.Shop.Activitys.Domain.Modules.PinTuanActivity
        ///     比如满就送，或者限量购
        /// </summary>
        [Display(Name = "所属营销活动类型")]
        public string Key { get; set; }

        /// <summary>
        ///     具体活动内容，活动类型的Json数据
        /// </summary>
        [Display(Name = "具体活动内容")]
        public string Value { get; set; }

        /// <summary>
        ///     根据开始时间自动开始
        ///     根据结束时间自动结束
        /// </summary>
        [Display(Name = "活动状态")]
        [Field(DataSource = "ActivityStatus", ControlsType = ControlsType.DropdownList, Width = "90",
            IsTabSearch = true, SortOrder = 20, EditShow = false, ListShow = true)]
        public ActivityStatus Status { get; set; }

        /// <summary>
        ///     活动是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        [Field(ControlsType = ControlsType.Switch, IsShowAdvancedSerach = true, SortOrder = 20, ListShow = true)]
        public bool IsEnable { get; set; } = true;

        /// <summary>
        ///     活动已经使用的库存量，随用户购买逐步增长，当与AllowStock相等时，活动失效
        /// </summary>
        [Display(Name = "已用库存")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowAdvancedSerach = true, SortOrder = 15, Width = "90",
            ListShow = true)]
        public long UsedStock { get; set; }

        /// <summary>
        ///     获取链接
        /// </summary>
        public IEnumerable<ViewLink> ViewLinks()
        {
            var quickLinks = new List<ViewLink>
            {
                new ViewLink("活动管理", "/Admin/Activitys/Index?Key=[[Key]]", Icons.List, LinkType.FormQuickLink),
                new ViewLink("活动订单统计", "/Admin/Activitys/Index?Method=GetOrderPageList&Key=[[Key]]",
                    FontAwesomeIcon.ShoppingCart.GetIcon(), LinkType.FormQuickLink),
                new ViewLink("活动商品统计", "/Admin/Activitys/Index?Method=GetProductPageList&Key=[[Key]]", Icons.List,
                    LinkType.TableQuickLink),
                new ViewLink("活动管理", "/Admin/Activitys/Index?Method=GetRecordPageList&Key=[[Key]]", Icons.List,
                    LinkType.TableQuickLink),
                new ViewLink("编辑", "/Admin/Activitys/Edit?Key=[[Key]]&Id=[[Id]]", Icons.Edit, LinkType.ColumnLink),
                new ViewLink("活动删除", "/Admin/Basic/Delete?id=[[Id]]&service=IActivityAdminService&method=Delete",
                    Icons.Delete, LinkType.ColumnLinkDelete)
            };
            return quickLinks;
        }
    }
}