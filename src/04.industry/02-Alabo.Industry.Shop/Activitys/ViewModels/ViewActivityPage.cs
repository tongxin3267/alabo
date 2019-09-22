using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Alabo.Core.Enums.Enum;
using Alabo.Core.UI.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Shop.Activitys.ViewModels {

    /// <summary>
    ///     Class ViewActivityPage.
    /// </summary>
    [ClassProperty(Name = "活动")]
    public class ViewActivityPage : BaseViewModel {

        /// <summary>
        ///     Key
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     活动对应的店铺Id
        /// </summary>
        [Display(Name = "活动对应的店铺Id")]
        public long StoreId { get; set; }

        /// <summary>
        ///     活动名称:比如活动券，满就送，一元夺宝等等
        /// </summary>
        [Display(Name = "活动名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(IsShowBaseSerach = true, SortOrder = 1, ControlsType = ControlsType.TextBox, IsMain = true,
            EditShow = true, ListShow = true, Width = "300", Link = "/Admin/Activitys/Edit?Key=[[Key]]&Id=[[Id]]")]
        public string Name { get; set; }

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
        ///     活动条件：如果用户范围为根据用户等级的时候，请设置该会员的等级Id
        /// </summary>
        [Display(Name = "用户等级")]
        public Guid LimitGradeId { get; set; }

        /// <summary>
        ///     活动允许的最大库存使用量，如果为0，则不限制
        /// </summary>
        [Display(Name = "最大库存")]
        [Field(ControlsType = ControlsType.NumberRang, LabelColor = LabelColor.Danger, SortOrder = 15, Width = "90",
            ListShow = true)]
        public string MaxStock { get; set; }

        /// <summary>
        ///     活动已经使用的库存量，随用户购买逐步增长，当与AllowStock相等时，活动失效
        /// </summary>
        [Display(Name = "已用库存")]
        [Field(ControlsType = ControlsType.NumberRang, IsShowAdvancedSerach = true, SortOrder = 15, Width = "90",
            ListShow = true)]
        public long UsedStock { get; set; }

        /// <summary>
        ///     扩展属性
        /// </summary>
        [Field(ExtensionJson = "ActivityExtension")]
        [Display(Name = "扩展属性")]
        public string Extension { get; set; }

        /// <summary>
        ///     活动条件：活动开始时间
        /// </summary>
        [Display(Name = "开始时间")]
        [Field(ControlsType = ControlsType.DateTimeRang, IsShowAdvancedSerach = true, SortOrder = 21, ListShow = true)]
        public DateTime StartTime { get; set; }

        /// <summary>
        ///     活动条件：活动结束时间
        /// </summary>
        [Field(ControlsType = ControlsType.DateTimeRang, IsShowAdvancedSerach = true, SortOrder = 22, ListShow = true)]
        [Display(Name = "结束时间")]
        public DateTime EndTime { get; set; }

        /// <summary>
        ///     获取链接
        /// </summary>
        public IEnumerable<ViewLink> ViewLinks() {
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