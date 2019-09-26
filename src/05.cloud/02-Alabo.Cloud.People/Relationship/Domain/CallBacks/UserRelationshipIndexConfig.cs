using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;using MongoDB.Bson.Serialization.Attributes;
using System.Reflection;
using Alabo.App.Core.Common;
using Alabo.Framework.Basic.Relations.Domain.Entities;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Market.Relationship.Domain.Enums;
using Alabo.AutoConfigs;
using Alabo.AutoConfigs.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Basic.Grades.Domain.Configs;
using Alabo.Helpers;
using Alabo.Reflections;
using Alabo.Web.Mvc.Attributes;
using Alabo.Web.Mvc.ViewModel;

namespace Alabo.App.Market.Relationship.Domain.CallBacks {

    /// <summary>
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "会员关系网配置", Icon = "fa fa-cny",
        Description = "会员关系网配置较为复杂，需要在专业人员的指导下完成配置，请慎重修改，配置不当将会影响整个系统的分润数据",
        PageType = ViewPageType.List, SortOrder = 20,
        SideBarType = SideBarType.RelationshipIndexSideBar)]
    public class UserRelationshipIndexConfig : BaseViewModel, IAutoConfig {

        /// <summary>
        ///     标识
        /// </summary>
        [Required(ErrorMessage = "Id不能为空")]
        [Key]
        [Display(Name = "ID", Order = 1)]
        [Field(ControlsType = ControlsType.Hidden, ListShow = true, SortOrder = 1, Width = "50")]
        public Guid Id { get; set; }

        /// <summary>
        ///     关系配置名称
        /// </summary>
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 0, ListShow = true, Width = "200")]
        [Required]
        [Display(Name = "关系配置名称，一条规则一次只能关注一个会员")]
        [Main]
        public string Name { get; set; }

        /// <summary>
        ///     货币类型
        /// </summary>
        [Field(ControlsType = ControlsType.DropdownList, SortOrder = 1, EnumUniqu = true, ListShow = true,
            Width = "150",
            DataSourceType = typeof(RelationshipIndexType))]
        [Display(Name = "内置关系网类型")]
        [HelpBlock("内置关系网类型，为了确保Id的唯一性，一种内置关系网类型只能添加一次。关系网类型名称")]
        public RelationshipIndexType Type { get; set; }

        /// <summary>
        ///     等级名称
        /// </summary>
        [Field(ControlsType = ControlsType.CheckBoxMultipl, DataSourceType = typeof(UserGradeConfig), SortOrder = 14)]
        [Display(Name = "推荐人会员等级范围(高级别)")]
        [HelpBlock("推荐人用户等级必须得在该等级区间，。区间等级不存在的时候，规则不执行。升级点越高等级越高,可以选择一个或多个高级别的等级。")]
        public string HighGrades { get; set; }

        /// <summary>
        ///     等级名称
        /// </summary>
        [Field(ControlsType = ControlsType.CheckBoxMultipl, DisplayMode = DisplayMode.Grade,
            DataSourceType = typeof(UserGradeConfig), SortOrder = 15)]
        [Display(Name = "触发用户等级范围(按升级点)")]
        [HelpBlock("触发用户必须得在该等级区间，比如刚刚注册的用户、升级的用户都需要在该设置区间。区间等级不存在的时候，规则不执行。等级范围升级点越高等级越高")]
        public string LowGrades { get; set; }

        /// <summary>
        ///     是否默认 0：不是，1：是默认
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, SortOrder = 23, Width = "80", ListShow = true)]
        [Display(Name = "是否启用")]
        public bool IsEnable { get; set; } = false;

        /// <summary>
        ///     升级是否触发
        /// </summary>
        [Field(ControlsType = ControlsType.Switch, SortOrder = 24, Width = "80", ListShow = true)]
        [Display(Name = "升级是否触发")]
        [HelpBlock("开启时，会员升级时候会触发关系图的改变")]
        public bool UpgradeTrigger { get; set; }

        /// <summary>
        ///     简要说明
        /// </summary>
        [Field(ControlsType = ControlsType.TextArea, SortOrder = 30, ListShow = true, Width = "300")]
        [Required]
        [Display(Name = "简要说明")]
        public string Intro { get; set; }

        public void SetDefault() {
            //  Ioc.Resolve<IAutoConfigService>().Delete(r => r.Type == typeof(UserRelationshipIndexConfig).Name);
            var list = Ioc.Resolve<IAutoConfigService>().GetList<UserRelationshipIndexConfig>();
            if (list == null || list.Count == 0) {
                var configs = new List<UserRelationshipIndexConfig>();
                var config = new UserRelationshipIndexConfig();
                foreach (RelationshipIndexType item in Enum.GetValues(typeof(RelationshipIndexType))) {
                    config = new UserRelationshipIndexConfig();
                    config.Id = item.GetFieldAttribute().GuidId.ToGuid();
                    config.Name = item.GetDisplayName();
                    config.Type = item;
                    configs.Add(config);
                }

                var typeclassProperty = config.GetType().GetTypeInfo().GetAttribute<ClassPropertyAttribute>();
                var autoConfig = new AutoConfig {
                    Type = config.GetType().FullName,
                    //// AppName = typeclassProperty.AppName,
                    LastUpdated = DateTime.Now,
                    Value = JsonConvert.SerializeObject(configs)
                };
                Ioc.Resolve<IAutoConfigService>().AddOrUpdate(autoConfig);
            }
        }
    }
}