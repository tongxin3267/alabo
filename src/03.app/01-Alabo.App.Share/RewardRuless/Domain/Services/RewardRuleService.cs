using Alabo.App.Share.OpenTasks.Base;
using Alabo.App.Share.RewardRuless.Domain.Entities;
using Alabo.App.Share.RewardRuless.Dtos;
using Alabo.App.Share.TaskExecutes;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Framework.Basic.AutoConfigs.Domain.Configs;
using Alabo.Framework.Basic.AutoConfigs.Domain.Services;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Framework.Tasks.Queues.Domain.Servcies;
using Alabo.Framework.Tasks.Queues.Models;
using Alabo.UI.Design.AutoForms;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.App.Share.RewardRuless.Domain.Services
{
    public class RewardRuleService : ServiceBase<RewardRule, ObjectId>, IRewardRuleService
    {
        public RewardRuleService(IUnitOfWork unitOfWork, IRepository<RewardRule, ObjectId> repository) : base(
            unitOfWork, repository)
        {
        }

        public Tuple<ServiceResult, RewardEditSimpleView> GetEditSimpleView(Guid moduleId)
        {
            var taskModuleAttribute = Resolve<ITaskQueueService>().GetTaskModuleAttribute(moduleId);
            if (taskModuleAttribute == null) {
                return new Tuple<ServiceResult, RewardEditSimpleView>(
                    ServiceResult.FailedWithMessage("类型不存在，请确定Id是否输入正确"), null);
            }

            var view = new RewardEditSimpleView
            {
                Name = taskModuleAttribute.Name,
                Intro = taskModuleAttribute.Intro,
                AutoForm = AutoFormMapping.Convert(taskModuleAttribute.ConfigurationType.FullName)
            };
            return new Tuple<ServiceResult, RewardEditSimpleView>(ServiceResult.Success, view);
        }

        /// <summary>
        ///     获取或搜索所有模块
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public RewardModulesOutput GetModules(string key, TaskManager _taskManager)
        {
            var result = new RewardModulesOutput();

            var moduleAttributeArray = _taskManager.GetModuleAttributeArray().ToList();
            moduleAttributeArray = moduleAttributeArray.OrderByDescending(r => r.SortOrder).ToList();
            result.Count = moduleAttributeArray.Count;
            result.ShareModules = moduleAttributeArray;

            var taskModuleAttributeList = new List<TaskModuleAttribute>();
            if (!key.IsNullOrEmpty())
            {
                foreach (var item in moduleAttributeArray) {
                    if (item.Name.Contains(key, StringComparison.OrdinalIgnoreCase)
                        || item.Id.ToString().Contains(key, StringComparison.OrdinalIgnoreCase)
                        || item.Intro.Contains(key, StringComparison.OrdinalIgnoreCase)) {
                        taskModuleAttributeList.Add(item);
                    }
                }

                result.ShareModules = taskModuleAttributeList;
            }

            return result;
        }

        public Tuple<ServiceResult, RewardRuleOutput> GetEditView(Guid moduleId, ObjectId id)
        {
            #region 安全验证

            var taskModuleAttribute = Resolve<ITaskQueueService>().GetTaskModuleAttribute(moduleId);
            if (taskModuleAttribute == null) {
                return new Tuple<ServiceResult, RewardRuleOutput>(ServiceResult.FailedWithMessage("类型不存在，请确定Id是否输入正确"),
                    null);
            }

            #endregion 安全验证

            var view = new RewardRuleOutput
            {
                AutoForm = AutoFormMapping.Convert(taskModuleAttribute.ConfigurationType.FullName),
                Base = new RewardEditOutputBase
                {
                    Name = taskModuleAttribute.Name,
                    Title = taskModuleAttribute.Name,
                    Intro = taskModuleAttribute.Intro,
                    Summary = $"{taskModuleAttribute.Name}的简要说明"
                },
                ModuleId = moduleId
            };

            IList<AssetsRule> ruleItems = new List<AssetsRule>();
            var rule = new AssetsRule
            {
                MoneyTypeId = Currency.Cny.GetCustomAttr<FieldAttribute>().GuidId.ToGuid(),
                Ratio = 0.8m
            };
            ruleItems.Add(rule);
            var moneyType = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>().FirstOrDefault(r =>
                r.Id != Currency.Cny.GetCustomAttr<FieldAttribute>().GuidId.ToGuid() && r.Status == Status.Normal);
            rule = new AssetsRule
            {
                MoneyTypeId = moneyType.Id,
                Ratio = 0.2m
            };
            ruleItems.Add(rule);
            view.RuleItems = ruleItems;

            return new Tuple<ServiceResult, RewardRuleOutput>(ServiceResult.Success, view);
        }

        public ServiceResult Save(RewardRuleOutput rewardRuleOutput)
        {
            throw new NotImplementedException();
        }

        public Type ModuleType(Guid moduleId)
        {
            var taskModuleAttribute = Resolve<ITaskQueueService>().GetTaskModuleAttribute(moduleId);
            if (taskModuleAttribute == null) {
                return null;
            }

            return taskModuleAttribute.ConfigurationType;
        }

        public AutoForm GetAutoForm(Guid moduleId)
        {
            var taskModuleAttribute = Resolve<ITaskQueueService>().GetTaskModuleAttribute(moduleId);
            if (taskModuleAttribute == null) {
                return null;
            }

            var autoForm = AutoFormMapping.Convert(taskModuleAttribute.ConfigurationType.FullName);
            return autoForm;
        }
    }
}