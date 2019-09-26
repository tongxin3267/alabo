using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.Data.People.Users.Dtos;
using Alabo.Domains.Attributes;
using Alabo.Domains.Services;

namespace Alabo.App.Core.User.Domain.Services {

    public interface IGradeInfoService : IService<GradeInfo, ObjectId> {

        /// <summary>
        /// 获取当前会员等级是否满足条件
        /// </summary>
        /// <param name="gradeInfo"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        Tuple<bool, Guid> GetKpiResult(GradeInfo gradeInfo, Users.Entities.User user);

        /// <summary>
        /// 最小项目的Kpi计算
        /// </summary>
        /// <param name="gradeInfo"></param>
        /// <param name="autoUpgradeItem"></param>
        /// <returns></returns>
        bool GetKpiItemResult(GradeInfo gradeInfo, AutoUpgradeItem autoUpgradeItem);

        /// <summary>
        /// 添加处理所有还有的后台任务
        /// </summary>
        void UpdataAllUserBackJob();

        /// <summary>
        ///     更新单个用户的等级信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        [Method(RunInUrl = true)]
        void UpdateSingle(long userId);

        /// <summary>
        ///     更新所有用的团队等级
        /// </summary>
        void UpdateAllUser();

        /// <summary>
        /// 根据用户更新该用户的团队等级
        /// 计算会员自动升级Kpi，
        /// 如果满足条件，则会员自动升级，同时更新上级团队信息、添加会员等级记录
        /// </summary>
        /// <param name="userId"></param>
        void TeamUserGradeAutoUpdate(long userId);

        /// <summary>
        ///     根据用户获取用户等数据统计
        /// </summary>
        /// <param name="users"></param>
        IEnumerable<GradeInfoItem> GetUsersGradeInfo(IEnumerable<Users.Entities.User> users);
    }
}