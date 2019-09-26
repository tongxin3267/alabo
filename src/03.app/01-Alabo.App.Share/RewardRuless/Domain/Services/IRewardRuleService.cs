using Alabo.App.Core.Tasks;
using Alabo.App.Open.Tasks.Base;
using Alabo.App.Share.Share.Domain.Dtos;
using Alabo.App.Share.Share.Domain.Entities;
using Alabo.Domains.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;
using System;
using Alabo.Framework.Core.WebUis.Design.AutoForms;

namespace Alabo.App.Share.Share.Domain.Services {

    public interface IRewardRuleService : IService<RewardRule, ObjectId> {

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        Tuple<ServiceResult, RewardEditSimpleView> GetEditSimpleView(Guid moduleId);

        /// <summary>
        /// ��ȡ����������ģ��
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        RewardModulesOutput GetModules(string key, TaskManager _taskManager);

        /// <summary>
        /// ��ȡ����༭��ͼ
        /// </summary>
        /// <param name="moduleId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Tuple<ServiceResult, RewardRuleOutput> GetEditView(Guid moduleId, ObjectId id);

        /// <summary>
        /// ����ģ��༭
        /// </summary>
        /// <param name="rewardRuleOutput"></param>
        /// <returns></returns>
        ServiceResult Save(RewardRuleOutput rewardRuleOutput);

        /// <summary>
        /// ��ȡ����ģ������
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        Type ModuleType(Guid moduleId);

        /// <summary>
        /// ��ȡ�Զ�������
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        AutoForm GetAutoForm(Guid moduleId);
    }
}