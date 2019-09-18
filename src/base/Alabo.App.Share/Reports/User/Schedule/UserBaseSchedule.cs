using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Reports.Domain.Services;
using Alabo.App.Core.Reports.ViewModels;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.App.Open.Reports.User.Model;
using Alabo.Dependency;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Schedules.Job;

namespace Alabo.App.Open.Reports.User.Schedule {

    /// <summary>
    /// 会员基础数据统计
    /// </summary>
    public class UserBaseSchedule : JobBase {

        /// <summary>
        /// 数据统计
        /// </summary>
        protected override async Task Execute(IJobExecutionContext context, IScope scope) {
            var reportService = scope.Resolve<IReportService>();
            var gradeService = scope.Resolve<IGradeService>();
            var repositoryContext = scope.Resolve<IUserRepository>().RepositoryContext;
            if (repositoryContext == null || gradeService == null || reportService == null) {
                return;
            }

            var userReport = new UserBaseReport();
            var sql = "select count(id) from User_User";
            userReport.UserTotalNumber = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0);

            sql = "select count(id) from User_User where Status=1";
            userReport.UserNormalNumber = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0);

            sql = "select count(id) from User_User where Status=2";
            userReport.UserFreezeNumber = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0);

            sql = "select count(id) from User_User where Status=3";
            userReport.UserDeleteNumber = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0);

            userReport.GradeNumber = new List<ViewReport<long>>();
            foreach (var item in gradeService.GetUserGradeList()) {
                var gradeSql = $" select count(id) from user_user where GradeId='{item.Id}'";
                var number = repositoryContext.ExecuteScalar(gradeSql).ToStr().ToLong(0);
                var viewReport = new ViewReport<long> {
                    Key = item.Id.ToString(),
                    Value = number,
                    Name = item.Name,
                    Raido = number / (decimal)userReport.UserTotalNumber
                };
                viewReport.Raido = Math.Round(viewReport.Raido, 4);

                userReport.GradeNumber.Add(viewReport);
            }
            reportService.AddOrUpdate(userReport);
        }
    }
}