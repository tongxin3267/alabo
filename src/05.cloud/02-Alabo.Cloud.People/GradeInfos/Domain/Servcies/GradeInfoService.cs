using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.User.Domain.Callbacks;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Core.Enums.Enum;
using Alabo.Datas.Queries.Enums;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Attributes;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Services;
using Alabo.Extensions;
using Alabo.Linq;
using Alabo.Schedules;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.App.Core.Tasks.Domain.Entities;
using Alabo.App.Core.Tasks.Domain.Enums;
using Alabo.App.Core.Tasks.Domain.Services;

namespace Alabo.App.Core.User.Domain.Services {

    /// <summary>
    /// �ȼ�����
    /// </summary>
    public class GradeInfoService : ServiceBase<GradeInfo, ObjectId>, IGradeInfoService {

        public GradeInfoService(IUnitOfWork unitOfWork, IRepository<GradeInfo, ObjectId> repository) : base(unitOfWork, repository) {
        }

        /// <summary>
        ///   �����Ա�Զ�����Kpi��
        /// ����������������Ա�Զ�������ͬʱ�����ϼ��Ŷ���Ϣ����ӻ�Ա�ȼ���¼
        /// </summary>
        /// <param name="userId"></param>
        public void TeamUserGradeAutoUpdate(long userId) {
            var parentMaps = Resolve<ITeamService>().GetTeamMap(userId).ToList();
            var userIds = parentMaps.Select(r => r.UserId);
            var users = Resolve<IUserService>().GetList(userIds);
            // �����Ŷӵȼ�����
            foreach (var item in parentMaps) {
                // �����Ŷ��ϼ��ĵȼ�����
                // �����µĻ�Ա��ʼ���£�UserId�����ǰ��
                UpdateSingle(item.UserId);

                var itemGradeInfo = GetSingle(r => r.UserId == item.UserId); // �������»�ȡ����Ϊ����¼���Ա���³ɹ������Զ�����
                var itemUser = users.FirstOrDefault(r => r.Id == item.UserId);
                if (itemGradeInfo == null || itemUser == null) {
                    continue;
                }
                var kpiResult = GetKpiResult(itemGradeInfo, itemUser);
                if (kpiResult.Item1) {
                    var user = Resolve<IUserService>().GetSingle(r => r.Id == item.UserId);
                    user.GradeId = kpiResult.Item2;
                    if (Resolve<IUserService>().Update(user)) {
                        var upgradeRecord = new UpgradeRecord {
                            UserId = item.UserId,
                            Type = UpgradeType.TeamUserGradeChange,
                            QueueId = 0,
                            BeforeGradeId = itemUser.GradeId,
                            AfterGradeId = kpiResult.Item2,
                        };
                        Resolve<IUpgradeRecordService>().Add(upgradeRecord);
                        Log($"{user.GetUserName()}�Զ������ɹ�");
                    }
                }
            }
        }

        /// <summary>
        /// ��ȡ��ǰ��Ա�ȼ��Ƿ���������
        /// </summary>
        /// <param name="gradeInfo"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public Tuple<bool, Guid> GetKpiResult(GradeInfo gradeInfo, Entities.User user) {
            var autoUpgradeConfigs = Resolve<IAutoConfigService>().GetList<AutoUpgradeConfig>();
            var userAutoUpgradeConfig = autoUpgradeConfigs.FirstOrDefault(r => r.GradeId == user.GradeId);
            // �ж�����Id���ڵ�ǰId

            if (userAutoUpgradeConfig != null) {
                var grades = Resolve<IGradeService>().GetUserGradeList();
                var grade = grades.FirstOrDefault(r => r.Id == userAutoUpgradeConfig.GradeId);
                var changeGrade = grades.FirstOrDefault(r => r.Id == userAutoUpgradeConfig.ChangeGradeId);
                if (changeGrade?.Contribute <= grade?.Contribute || grade == null || changeGrade == null) {
                    // ���ó���
                    return Tuple.Create(false, Guid.Empty);
                }
                var logicalOperator = userAutoUpgradeConfig.LogicalOperator;
                foreach (var upgradeItem in userAutoUpgradeConfig.UpgradeItems) {
                    var result = GetKpiItemResult(gradeInfo, upgradeItem);
                    // ��Ψһ��ʶ������������滻
                    logicalOperator = logicalOperator.Replace(upgradeItem.Key, result.ToString().ToLower());
                }
                var kpiResult = LogicExpression.Operate(logicalOperator);
                if (kpiResult) {
                    // ����ͨ��
                    return Tuple.Create(true, userAutoUpgradeConfig.ChangeGradeId);
                }
            }

            return Tuple.Create(false, Guid.Empty);
        }

        /// <summary>
        /// ��С��Ŀ��Kpi����
        /// </summary>
        /// <param name="gradeInfo"></param>
        /// <param name="autoUpgradeItem"></param>
        /// <returns></returns>
        public bool GetKpiItemResult(GradeInfo gradeInfo, AutoUpgradeItem autoUpgradeItem) {
            var result = false;
            var kpiCount = autoUpgradeItem.Count;
            switch (autoUpgradeItem.KpiUpgradeType) {
                case KpiUpgradeType.RecommendUser:
                    var count = gradeInfo.RecomendCount;
                    var kpiItemResult = LinqHelper.CompareByOperator(OperatorCompare.GreaterEqual, count, autoUpgradeItem.Count);
                    return kpiItemResult;

                case KpiUpgradeType.SecondUser:
                    count = gradeInfo.SecondCount;
                    kpiItemResult = LinqHelper.CompareByOperator(OperatorCompare.GreaterEqual, count, autoUpgradeItem.Count);
                    return kpiItemResult;

                case KpiUpgradeType.TeamUser:
                    count = gradeInfo.TeamCount;
                    kpiItemResult = LinqHelper.CompareByOperator(OperatorCompare.GreaterEqual, count, autoUpgradeItem.Count);
                    return kpiItemResult;

                case KpiUpgradeType.RecommendAOrSecondUser:
                    return (gradeInfo.RecomendCount >= kpiCount || gradeInfo.SecondCount >= kpiCount);

                case KpiUpgradeType.RecommendUserOrTeamUser:
                    return (gradeInfo.RecomendCount >= kpiCount || gradeInfo.TeamCount >= kpiCount);

                case KpiUpgradeType.SecondUserOrTeamUser:
                    return (gradeInfo.SecondCount >= kpiCount || gradeInfo.TeamCount >= kpiCount);

                case KpiUpgradeType.RecommendAndSecondUser:
                    return (gradeInfo.RecomendCount + gradeInfo.SecondCount) >= kpiCount;

                case KpiUpgradeType.RecommendUserAndTeamUser:
                    return (gradeInfo.RecomendCount + gradeInfo.TeamCount) >= kpiCount;

                case KpiUpgradeType.SecondUserAndTeamUser:
                    return (gradeInfo.SecondCount + gradeInfo.TeamCount) >= kpiCount;

                // ������
                case KpiUpgradeType.UpgradePoint:
                    var pointAmount = Resolve<IAccountService>()
                        .GetAccountAmount(gradeInfo.UserId, Currency.UpgradePoints.GetFieldId());
                    return pointAmount >= kpiCount;
            }

            return result;
        }

        public void UpdateTeamInfo(long childuUserId = 0) {
            Repository<IUserMapRepository>().UpdateTeamInfo(childuUserId);
        }

        /// <summary>
        ///     ���������õ��Ŷӵȼ�
        ///     �ӵ�һ����ʼ���£�������
        /// </summary>
        public void UpdateAllUser() {
            var pageCount = 30; // ÿ�δ���30��
            var totalCount = Repository<IUserMapRepository>().RepositoryContext
                .ExecuteScalar("select count(id) from User_User").ConvertToLong();
            var totalPage = totalCount / 30 + 1;
            for (var i = 1; i <= totalPage; i++) {
                var userIds = new List<long>();
                var sql =
                    $"SELECT TOP 30 Id FROM (SELECT  ROW_NUMBER() OVER (ORDER BY id desc ) AS RowNumber,Id FROM User_User  ) as A WHERE RowNumber > {pageCount}*({i}-1)  ";
                using (var reader = Repository<IUserMapRepository>().RepositoryContext.ExecuteDataReader(sql)) {
                    while (reader.Read()) {
                        userIds.Add(reader["Id"].ConvertToLong());
                    }
                }

                foreach (var userId in userIds) {
                    UpdateSingle(userId);
                }
            }
        }

        /// <summary>
        ///     �����û��Ŷӵĵȼ���Ϣ
        /// </summary>
        /// <param name="userId">�û�Id</param>
        [Method(RunInUrl = true)]
        public void UpdateSingle(long userId) {
            var gradeInfo = GetSingle(r => r.UserId == userId);
            if (gradeInfo == null) {
                gradeInfo = new GradeInfo();
            }

            gradeInfo.UserId = userId;
            // ��ȡ�Ƽ����û��ȼ���ϵ
            var users = Resolve<IUserService>().GetList(r => r.ParentId == gradeInfo.UserId);
            gradeInfo.RecomendGradeInfo = GetUsersGradeInfo(users).ToList();
            gradeInfo.RecomendCount = gradeInfo.RecomendGradeInfo.Sum(r => r.Count);

            //��ȡ����û��Ĺ�ϵ
            var secondUsers = Resolve<IUserService>().GetList(r => users.Select(e => e.Id).Contains(r.ParentId));
            gradeInfo.SecondGradeInfo = GetUsersGradeInfo(secondUsers).ToList();
            gradeInfo.SecondCount = gradeInfo.SecondGradeInfo.Sum(r => r.Count);

            // ��ȡ�Ŷ��û��ȼ���ϵ
            var teamUsers = Resolve<ITeamService>().GetChildUsers(userId);
            gradeInfo.TeamGradeInfo = GetUsersGradeInfo(teamUsers).ToList();
            gradeInfo.TeamCount = gradeInfo.TeamGradeInfo.Sum(r => r.Count);

            gradeInfo.ModifiedTime = DateTime.Now;

            if (gradeInfo.Id.IsObjectIdNullOrEmpty()) {
                Add(gradeInfo);
            } else {
                Update(gradeInfo);
            }
        }

        /// <summary>
        ///     �����û���ȡ�û�������ͳ��
        /// </summary>
        /// <param name="users"></param>
        public IEnumerable<GradeInfoItem> GetUsersGradeInfo(IEnumerable<User.Domain.Entities.User> users) {
            var gradeList = new List<GradeInfoItem>();
            if (users != null) {
                var grades = Resolve<IGradeService>().GetUserGradeList();
                foreach (var grade in grades) {
                    gradeList.Add(new GradeInfoItem {
                        GradeId = grade.Id
                    });
                }

                gradeList.ForEach(r => {
                    r.Count = users.Count(e => e.GradeId == r.GradeId); // ͳ�Ƶȼ�����
                    r.UserIds = users.Where(e => e.GradeId == r.GradeId).Select(e => e.Id); // �����û�Id
                });
            }
            return gradeList;
        }

        public void UpdataAllUserBackJob() {
            var backJobParameter = new BackJobParameter {
                ModuleId = TaskQueueModuleId.TeamUserGradeAutoUpdate,
                CheckLastOne = true,
                ServiceName = typeof(IGradeInfoService).Name,
                Method = "UpdateAllUser"
            };
            Resolve<ITaskQueueService>().AddBackJob(backJobParameter);
        }
    }
}