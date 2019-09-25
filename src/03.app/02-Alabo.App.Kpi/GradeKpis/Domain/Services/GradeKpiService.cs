using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.App.Core.User.Domain.Entities;
using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Share.Kpi.Domain.CallBack;
using Alabo.App.Share.Kpi.Domain.Entities;
using Alabo.App.Share.Kpi.Domain.Enum;
using Alabo.App.Share.Kpi.ViewModels;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Linq;
using Alabo.Schedules;
using Alabo.Users.Entities;

namespace Alabo.App.Share.Kpi.Domain.Services {

    /// <summary>
    /// 等级考核
    /// </summary>
    public class GradeKpiService : ServiceBase<GradeKpi, ObjectId>, IGradeKpiService {

        public GradeKpiService(IUnitOfWork unitOfWork, IRepository<GradeKpi, ObjectId> repository) : base(unitOfWork,
            repository) {
        }

        public void TaskQueue() {
            var backJobParameter = new BackJobParameter {
                ModuleId = TaskQueueModuleId.UserGradeKpiReport,
                CheckLastOne = true,
                ServiceName = typeof(IGradeKpiService).Name,
                Method = "KpiOperatorAllUser"
            };
            Resolve<ITaskQueueService>().AddBackJob(backJobParameter);
        }

        public void KpiOperatorAllUser() {
            var pageSize = 15; // 每页处理15个
            var pageCount = Resolve<IUserService>().PageCount(pageSize);

            for (var i = 1; i <= pageCount; i++) {
                var pageList = Resolve<IUserService>().GetListByPage(pageSize, i);
                IList<GradeKpi> gradeKpiList = new List<GradeKpi>();
                foreach (var user in pageList) {
                    //计算降职
                    gradeKpiList.Add(DemotionKpiOperator(user));
                    // 计算晋升
                    gradeKpiList.Add(PromotedKpiOperator(user));
                }
                // 批量添加到数据库中
                AddMany(gradeKpiList);
            }
        }

        public GradeKpi PromotedKpiOperator(User user) {
            var promotedKpiConfigs = Resolve<IAutoConfigService>().GetList<PromotedKpiConfig>();
            promotedKpiConfigs = promotedKpiConfigs.Where(r => r.GradeId == user.GradeId).ToList();
            foreach (var gradeKpiConfig in promotedKpiConfigs) {
                if (gradeKpiConfig != null && !gradeKpiConfig.LogicalOperator.IsNullOrEmpty()) {
                    var kpiItemOperatorResult = KpiItemOperator(user, gradeKpiConfig.KpiItems,
                        gradeKpiConfig.LogicalOperator, gradeKpiConfig.TimeType);
                    GradeKpi gradeKpi = new GradeKpi {
                        GradeId = user.GradeId,
                        UserId = user.Id,
                        TimeType = gradeKpiConfig.TimeType,
                        ConfigName = gradeKpiConfig.Name,
                        KpiItems = kpiItemOperatorResult.Item2
                    };
                    // 考核通过，晋升处理
                    if (kpiItemOperatorResult.Item1) {
                        gradeKpi.ChangeGradeId = gradeKpiConfig.ChangeGradeId;
                        gradeKpi.KpiResult = KpiResult.Promote;
                    } else {
                        // 考核不通过，维持不变
                        gradeKpi.KpiResult = KpiResult.NoChange;
                        gradeKpi.ChangeGradeId = gradeKpiConfig.GradeId;
                    }

                    return gradeKpi;
                }
            }

            return null;
        }

        public GradeKpi DemotionKpiOperator(User user) {
            var demotionKpiConfigs = Resolve<IAutoConfigService>().GetList<DemotionKpiConfig>();

            demotionKpiConfigs = demotionKpiConfigs.Where(r => r.GradeId == user.GradeId).ToList();
            foreach (var gradeKpiConfig in demotionKpiConfigs) {
                if (gradeKpiConfig != null && !gradeKpiConfig.LogicalOperator.IsNullOrEmpty()) {
                    var kpiItemOperatorResult = KpiItemOperator(user, gradeKpiConfig.KpiItems, gradeKpiConfig.LogicalOperator, gradeKpiConfig.TimeType);
                    GradeKpi gradeKpi = new GradeKpi {
                        GradeId = user.GradeId,
                        UserId = user.Id,
                        TimeType = gradeKpiConfig.TimeType,
                        ConfigName = gradeKpiConfig.Name,
                        KpiItems = kpiItemOperatorResult.Item2
                    };

                    if (kpiItemOperatorResult.Item1) {
                        // 考核通过，维持不变
                        gradeKpi.KpiResult = KpiResult.NoChange;
                        gradeKpi.ChangeGradeId = gradeKpiConfig.GradeId;
                    } else {
                        // 考核不通过，降级处理
                        gradeKpi.ChangeGradeId = gradeKpiConfig.ChangeGradeId;
                        gradeKpi.KpiResult = KpiResult.Demotion;
                    }
                    return gradeKpi;
                }
            }

            return null;
        }

        public Tuple<bool, List<GradeKpiItem>> KpiItemOperator(User user, IList<KpiItem> kpiItems, string logicalOperator, TimeType timeType) {
            // 条件结果

            var oldOperator = logicalOperator;
            var kpiItemResult = false;
            var gradeKpiItemList = new List<GradeKpiItem>();
            foreach (var kpiItem in kpiItems) {
                var kpi = new Entities.Kpi {
                    ModuleId = kpiItem.KpiConfigId,
                    UserId = user.Id,
                    Type = timeType,
                };
                // 最后一条Kpi考核记录
                var lastKpi = Resolve<IKpiService>().GetLastSingle(kpi);
                if (lastKpi != null) {
                    kpiItemResult = LinqHelper.CompareByOperator(kpiItem.OperatorCompare, kpiItem.Amount, lastKpi.TotalValue);

                    // 记录记录
                    var kpiConfig = Resolve<IAutoConfigService>().GetList<KpiAutoConfig>().ToList()
                        .FirstOrDefault(r => r.Id == kpiItem.KpiConfigId);
                    var gradeKpiItem = new GradeKpiItem {
                        Amount = lastKpi.TotalValue,
                        KpiId = kpi.Id,
                        KpiName = kpiConfig?.Name,
                        KpiConfigId = kpiConfig?.Id
                    };
                    gradeKpiItemList.Add(gradeKpiItem);
                } else { kpiItemResult = false; }
                // 将唯一标识，用条件结果替换
                logicalOperator = logicalOperator.Replace(kpiItem.Key, kpiItemResult.ToString().ToLower());
            }
            // 根据逻辑计算结算结果
            try {
                kpiItemResult = LogicExpression.Operate(logicalOperator);
            } catch {
                throw new ValidException("计算报错，请确认逻辑运算符格式是否正确，逻辑预算符:" + oldOperator);
            }

            return Tuple.Create(kpiItemResult, gradeKpiItemList);
        }

        public PagedList<GradeKpi> GetGradeKpiList(object query) {
            var model = Resolve<IGradeKpiService>().GetPagedList(query);
            var users = Resolve<IUserService>().GetList();

            foreach (var item in model) {
                item.UserName = users.FirstOrDefault(u => u.Id == item.UserId)?.UserName;
            }

            return model;
        }

        /// <summary>
        /// 等级修改
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GradeChangeView GetGradeChangeView(object id) {
            var find = GetSingle(id);
            if (find == null) {
                throw new ValidException("等级考核结果不存在");
            }

            var user = Resolve<IUserService>().GetSingle(find.UserId);
            if (user == null) {
                throw new ValidException("用户不存在");
            }
            GradeChangeView view = new GradeChangeView {
                Id = find.Id.ToString(),
                UserName = user.GetUserName(),
                GradeName = Resolve<IGradeService>().GetGrade(user.GradeId)?.Name,
                KpiResult = find.KpiResult,
                ChangeGradeName = Resolve<IGradeService>().GetGrade(find.ChangeGradeId)?.Name
            };
            return view;
        }

        public ServiceResult SaveChangeGrade(GradeChangeView view) {
            var find = GetSingle(view.Id);
            if (find == null) {
                throw new ValidException("等级考核结果不存在");
            }
            var user = Resolve<IUserService>().GetSingle(find.UserId);
            if (user == null) {
                throw new ValidException("用户不存在");
            }

            if (find.GradeId != user.GradeId) {
                return ServiceResult.FailedWithMessage("用户等级已发生改变");
            }

            if (find.GradeId == find.ChangeGradeId) {
                return ServiceResult.FailedWithMessage("用户当前等级和更改后等级一样，无需修改");
            }

            if (find.KpiResult == KpiResult.NoChange) {
                return ServiceResult.FailedWithMessage("考核结果为保持，无需修改");
            }
            user.GradeId = find.ChangeGradeId;
            var result = Resolve<IUserService>().UpdateUser(user);
            if (result) {
                Log($"会员{user.GetUserName()}考核通过,手动修改等级成功");
                Resolve<IUserService>().DeleteUserCache(user.Id, user.UserName);
                return ServiceResult.Success;
            } else {
                return ServiceResult.FailedWithMessage("等级修改失败");
            }
        }
    }
}