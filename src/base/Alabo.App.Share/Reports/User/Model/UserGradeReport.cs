using Quartz;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Reports;
using Alabo.App.Core.Reports.Domain.Services;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Dependency;
using Alabo.Domains.Enums;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Schedules.Job;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Share.Reports.User.Model {

    /// <summary>
    /// 会员等级统计
    /// </summary>
    [ClassProperty(Name = "会员等级统计", PageType = ViewPageType.List, Description = "会员等级统计数据", SideBarType = SideBarType.ReportSideBar)]
    public class UserGradeReport : IReportModel {

        /// <summary>
        /// 唯一标识
        /// </summary>
        public object Id { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 统计数据
        /// </summary>

        public long Value { get; set; }

        /// <summary>
        /// 比例
        /// </summary>
        public decimal Raido { get; set; } = 0.00000m;
    }

    /// <summary>
    /// 数据统计后台服务
    /// </summary>
    public class UserGradeSchedule : JobBase {

        protected override async Task Execute(IJobExecutionContext context, IScope scope) {
            //获取数据库操作对象
            var repositoryContext = scope.Resolve<IUserRepository>().RepositoryContext;
            if (repositoryContext == null) {
                return;
            }

            // 统计会员总数
            var sql = "select count(id) from User_User";
            var totalNumber = repositoryContext.ExecuteScalar(sql).ToStr().ToLong(0);

            var list = new List<UserGradeReport>();
            //获取等级服务
            var gradeService = scope.Resolve<IGradeService>();
            foreach (var item in gradeService.GetUserGradeList()) {
                var gradeSql = $" select count(id) from user_user where GradeId='{item.Id}'";
                var number = repositoryContext.ExecuteScalar(gradeSql).ToStr().ToLong(0);
                var report = new UserGradeReport {
                    Id = item.Id.ToString(),
                    Value = number,
                    Name = item.Name,
                    Raido = number / (decimal)totalNumber,
                };
                list.Add(report);
            }

            //添加统计数据到数据库
            scope.Resolve<IReportService>().AddOrUpdate<UserGradeReport>(list);
        }
    }
}