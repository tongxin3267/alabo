using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Reports;
using Alabo.App.Core.Reports.Domain.Services;
using Alabo.App.Core.Reports.ViewModels;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Dependency;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Schedules.Job;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Open.Reports.Admin {

    /// <summary>
    /// 后台左侧菜单统计数据
    /// </summary>
    [ClassProperty(Name = "后台左侧菜单统计数据", Icon = "fa fa-building", Description = "后台左侧菜单统计数据,动态构建左侧菜单数据")]
    public class AdminSideBarReport : IReportModel {

        /// <summary>
        /// 左侧菜单类型
        /// </summary>
        public SideBarType SideBarType { get; set; }

        /// <summary>
        /// 统计数据
        /// </summary>
        public IList<ViewReport<long>> DataList { get; set; } = new List<ViewReport<long>>();
    }

    /// <summary>
    /// 后台左侧菜单统计数据
    /// </summary>
    public class AdminSideBarReportSchedule : JobBase {

        protected override async Task Execute(IJobExecutionContext context, IScope scope) {
            var reportService = scope.Resolve<IReportService>();
            if (reportService == null) {
                return;
            }
            var list = new List<AdminSideBarReport> {
                //设置会员中心左侧数据
                GetUserSideBar(scope)
            };
            reportService.AddOrUpdate<AdminSideBarReport>(list);
        }

        /// <summary>
        /// 设置会员中心左侧数据
        /// </summary>
        private AdminSideBarReport GetUserSideBar(IScope scope) {
            var gradeService = scope.Resolve<IGradeService>();
            var repositoryContext = scope.Resolve<IUserRepository>().RepositoryContext;
            if (gradeService == null || repositoryContext == null) {
                return null;
            }

            var sideBarReport = new AdminSideBarReport {
                SideBarType = SideBarType.UserSideBar
            };

            var sql = "select count(id) from User_User";
            var report = new ViewReport<long> {
                Key = "Total number of members",
                Name = "会员总数量",
                Value = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0)
            };
            sideBarReport.DataList.Add(report);
            var totalNumber = report.Value; //会员总数

            sql = "select count(id) from User_User where Status=1";
            report = new ViewReport<long> {
                Key = "The number of normal members.",
                Name = "正常会员数",
                Value = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0)
            };
            sideBarReport.DataList.Add(report);

            sql = "select count(id) from User_User where Status!=1";
            report = new ViewReport<long> {
                Key = "Freeze or delete members.",
                Name = "冻结或删除会员数",
                Value = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0)
            };
            sideBarReport.DataList.Add(report);

            foreach (var item in gradeService.GetUserGradeList()) {
                var gradeSql = $" select count(id) from user_user where GradeId='{item.Id}'";
                var number = repositoryContext.ExecuteScalar(gradeSql).ToStr().ToLong(0);
                var viewReport = new ViewReport<long> {
                    Value = number,
                    Name = item.Name,
                    Raido = number / (decimal)totalNumber
                };
                viewReport.Raido = Math.Round(viewReport.Raido, 4);
                sideBarReport.DataList.Add(viewReport);
            }
            return sideBarReport;
        }
    }
}