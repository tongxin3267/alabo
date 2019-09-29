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
        ///     ����
        /// </summary>
        /// <returns></returns>
        Tuple<ServiceResult, RewardEditSimpleView> GetEditSimpleView(Guid moduleId);

        /// <summary>
        ///     ��ȡ����������ģ��
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        RewardModulesOutput GetModules(string key, TaskManager _taskManager);

        /// <summary>
        ///     ��ȡ����༭��ͼ
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Tuple<ServiceResult, RewardRuleOutput> GetEditView(Guid moduleId, ObjectId id);

        /// <summary>
        ///     ����ģ��༭
        /// </summary>
        /// <param name="rewardRuleOutput"></param>
        /// <returns></returns>
        ServiceResult Save(RewardRuleOutput rewardRuleOutput);

        /// <summary>
        ///     ��ȡ����ģ������
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        Type ModuleType(Guid moduleId);

        /// <summary>
        ///     ��ȡ�Զ�������
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        AutoForm GetAutoForm(Guid moduleId);
    }
}