using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Alabo.AutoConfigs;
using Alabo.AutoConfigs.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Helpers;
using Alabo.Industry.Shop.Deliveries.Domain.Enums;
using Alabo.Reflections;
using Alabo.Web.Mvc.Attributes;
using Newtonsoft.Json;

namespace Alabo.Industry.Shop.Deliveries.Domain.CallBacks {

    [NotMapped]
    [ClassProperty(Name = "常用快递 ", Icon = "fa fa-life-ring", PageType = ViewPageType.List,
        Description = "配置常用快递", SortOrder = 30)]
    public class ExpressConfig : AutoConfigBase, IAutoConfig {

        /// <summary>
        ///     快递名称
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 0, ListShow = true)]
        [Required]
        [Display(Name = "快递名称")]
        [Main]
        public string Name { get; set; }

        /// <summary>
        ///     快递类型
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, SortOrder = 2, EnumUniqu = true, ListShow = true,
            DataSource = "Alabo.App.Shop.Store.Domain.Enums.ExpressType")]
        [Display(Name = "快递类型")]
        [HelpBlock("快递类型")]
        public ExpressType ExpressType { get; set; }

        /// <summary>
        ///     快递标志
        /// </summary>
        [Field(ControlsType = ControlsType.AlbumUploder, SortOrder = 8)]
        [Display(Name = "快递标志")]
        public string Logo { get; set; }

        /// <summary>
        ///     官方网站
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 8)]
        [Display(Name = "官方网站")]
        public string Url { get; set; }

        public void SetDefault() {
            var list = Ioc.Resolve<IAutoConfigService>().GetList<ExpressConfig>();
            if (list == null || list.Count == 0) {
                var configs = new List<ExpressConfig>();
                var config = new ExpressConfig();
                foreach (ExpressType item in Enum.GetValues(typeof(ExpressType))) {
                    config = new ExpressConfig {
                        ExpressType = item,
                        Id = item.GetFieldAttribute().GuidId.ToGuid(),
                        Name = item.GetDisplayName(),
                        Status = Status.Normal
                    };
                    if (!item.IsDefault()) {
                        config.Status = Status.Freeze;
                    }

                    configs.Add(config);
                }

                var autoConfig = new AutoConfig {
                    Type = config.GetType().FullName,

                    LastUpdated = DateTime.Now,
                    Value = JsonConvert.SerializeObject(configs)
                };
                Ioc.Resolve<IAutoConfigService>().AddOrUpdate(autoConfig);
            }
        }
    }
}