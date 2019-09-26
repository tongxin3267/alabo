using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Alabo.AutoConfigs;
using Alabo.AutoConfigs.Entities;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Helpers;
using Alabo.Reflections;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using Newtonsoft.Json;

namespace Alabo.Data.People.Users.Domain.Configs {

    /// <summary>
    ///     任务配置
    /// </summary>
    [NotMapped]
    [ClassProperty(Name = "任务配置 ", Icon = "fa fa-life-ring", PageType = ViewPageType.List, Description = "任务配置",
        SortOrder = 30)]
    public class TaskConfig : AutoConfigBase, IAutoConfig {

        [Field(ControlsType = ControlsType.TextBox, SortOrder = 2, ListShow = true)]
        [Display(Name = "任务名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("输入该任务的名称")]
        public string Name { get; set; }

        [Field(ControlsType = ControlsType.DropdownList, SortOrder = 2,
            DataSource = "Alabo.Framework.Core.Enums.Enum.TaskType")]
        [Display(Name = "任务类型")]
        [Required(ErrorMessage = "请选择任务类型")]
        [HelpBlock("任务的类型，每个任务对应不同的任务类型,任务的条件有系统后台触发，任务类型只能用系统人员添加")]
        public TaskType TaskType { get; set; } = 0;

        [Field(ControlsType = ControlsType.DropdownList, ListShow = false, SortOrder = 2,
            DataSource = "Alabo.App.Core.Finance.Domain.CallBacks.MoneyTypeConfig")]
        [Display(Name = "奖励货币类型")]
        [Required(ErrorMessage = "请选择奖励货币类型")]
        [HelpBlock("完成任务奖励的货币类型，比如推荐一个会员赠送20个升级点，奖励货币类型为升级点。注册一个会员赠送20元人民币，奖励货币类型为人民币")]
        public Guid MoneyTypeConfigId { get; set; }

        [Field(ControlsType = ControlsType.TextBox, ListShow = true, SortOrder = 2)]
        [Display(Name = "奖励货币金额")]
        [HelpBlock("奖励货币金额")]
        [Range(0, 10000, ErrorMessage = "请输入正确的金额大小")]
        [HelpBlock("完成任务奖励的货币金额，比如推荐一个会员赠送20个升级点，奖励货币金额为20。注册一个会员赠送20元人民币，奖励货币金额为20")]
        public decimal Amount { get; set; } = 0;

        public void SetDefault() {
            var list = Ioc.Resolve<IAutoConfigService>().GetList<TaskConfig>();
            var moneyTypes = Ioc.Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>();
            var sharedValue = moneyTypes.FirstOrDefault(r => r.Currency == Currency.UpgradePoints); //存在升级点

            if (list.Count == 0) {
                var configs = new List<TaskConfig>();
                var config = new TaskConfig();

                foreach (TaskType item in Enum.GetValues(typeof(TaskType))) {
                    if (item.IsDefault()) {
                        config = new TaskConfig();

                        ///一级推荐会员
                        if (item == TaskType.RecommendedMember) {
                            if (sharedValue != null) {
                            }
                        }

                        config.Name = "会员注册时候赠送积分";
                        config.Id = Guid.NewGuid();
                        // configs.Add(config);
                    }
                }

                var typeclassProperty = config.GetType().GetTypeInfo().GetAttribute<ClassPropertyAttribute>();
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