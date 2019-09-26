using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;using MongoDB.Bson.Serialization.Attributes;
using System.Reflection;
using Alabo.App.Core.Common;
using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.AutoConfigs;
using Alabo.AutoConfigs.Entities;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Reflections;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Shop.Store.Domain.CallBacks {

    [NotMapped]
    /// <summary>
    /// 主题市场
    /// </summary>
    [ClassProperty(Name = "主题市场", PageType = ViewPageType.List, SortOrder = 450, Icon = "fa fa-sun-o",
        GroupName = "Core",
        SideBarType = SideBarType.SupplierSideBar)]
    // SideBar = "Shop/StoreSideBar")]
    public class StoreMarketConfig : AutoConfigBase, IAutoConfig {

        [Field(ControlsType = ControlsType.DropdownList, ListShow = true)]
        [Display(Name = "市场名称")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        ///     市场类型
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, SortOrder = 1, EnumUniqu = true, ListShow = true,
            DataSource = "Alabo.Framework.Core.MarketEnum")]
        [Display(Name = "系统类型")]
        [HelpBlock("系统类型")]
        public MarketEnum MarketGuid { get; set; }

        /// <summary>
        ///     是否默认 0：不是，1：是默认
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, SortOrder = 3, ListShow = true)]
        [Display(Name = "是否默认")]
        public bool IsDefault { get; set; }

        public void SetDefault() {
            var list = Alabo.Helpers.Ioc.Resolve<IAutoConfigService>().GetList<StoreMarketConfig>();
            if (list == null || list.Count == 0) {
                var configs = new List<StoreMarketConfig>();
                var config = new StoreMarketConfig();
                foreach (MarketEnum item in Enum.GetValues(typeof(MarketEnum))) {
                    config = new StoreMarketConfig {
                        MarketGuid = item
                    };
                    if (item == MarketEnum.Custom) {
                        config.Id = Guid.NewGuid();
                    } else {
                        config.Id = item.GetFieldAttribute().GuidId.ToGuid();
                    }

                    config.Name = item.GetDisplayName();
                    if (Convert.ToInt32(item) > 5) {
                        config.Status = Status.Freeze;
                    }

                    config.IsDefault = true;
                    configs.Add(config);
                }

                var typeclassProperty = config.GetType().GetTypeInfo().GetAttribute<ClassPropertyAttribute>();
                var autoConfig = new AutoConfig {
                    Type = config.GetType().FullName,

                    LastUpdated = DateTime.Now,
                    Value = JsonConvert.SerializeObject(configs)
                };
                Alabo.Helpers.Ioc.Resolve<IAutoConfigService>().AddOrUpdate(autoConfig);
            }
        }
    }
}