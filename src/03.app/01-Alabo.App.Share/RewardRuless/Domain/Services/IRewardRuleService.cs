using System;
using Alabo.App.Share.OpenTasks.Base;
using Alabo.App.Share.RewardRuless.Domain.Entities;
using Alabo.App.Share.RewardRuless.Dtos;
using Alabo.App.Share.TaskExecutes;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using Alabo.UI.Design.AutoForms;
using MongoDB.Bson;

namespace Alabo.App.Share.RewardRuless.Domain.Services
{
    public interface IRewardRuleService : IService<RewardRule, ObjectId>
    {
        /// <summary>
        ///     分润
        /// </summary>
        /// <returns></returns>
        Tuple<ServiceResult, RewardEditSimpleView> GetEditSimpleView(Guid moduleId);

        /// <summary>
        ///     获取或搜索所有模块
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        RewardModulesOutput GetModules(string key, TaskManager _taskManager);

        /// <summary>
        ///     获取分润编辑视图
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Tuple<ServiceResult, RewardRuleOutput> GetEditView(Guid moduleId, ObjectId id);

        /// <summary>
        ///     分润模块编辑
        /// </summary>
        /// <param name="rewardRuleOutput"></param>
        /// <returns></returns>
        ServiceResult Save(RewardRuleOutput rewardRuleOutput);

        /// <summary>
        ///     获取分润模块类型
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        Type ModuleType(Guid moduleId);

        /// <summary>
        ///     获取自动表单类型
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        AutoForm GetAutoForm(Guid moduleId);
    }
}