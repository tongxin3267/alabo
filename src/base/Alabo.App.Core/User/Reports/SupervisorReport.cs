using System;
using System.Collections.Generic;
using Alabo.App.Core.Reports;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Helpers;

namespace Alabo.App.Core.User.Reports {

    [ReportRule(Id, typeof(SupervisorReportModel), Name = "显示会员上下级", Summary = "历史累计显示会员上下级")]
    public class SupervisorReport : ReportModelRuleBase<SupervisorReportModel> {
        public const string Id = "1519C5AC-B8C7-492A-8D88-C79AC223E5F6";

        public SupervisorReport(ReportContext context)
            : base(context) {
        }

        public override IReportRuleResult Execute(ReportRuleParameter parameter) {
            var num = parameter.GetValue<string>("num").ToInt();
            var repositoryContext = Ioc.Resolve<IUserRepository>().RepositoryContext;
            var sql = $@"select * from GetDownUser('{num}')";
            var resultList = new List<SupervisorReportModel>();
            using (var dr = repositoryContext.ExecuteDataReader(sql)) {
                while (dr.Read()) {
                    var item = new SupervisorReportModel {
                        UserName = dr["UserName"].ToString(),
                        ParentId = dr["ParentId"].ToString(),
                        UserId = dr["UserId"].ToString()
                    };
                    //item.LastLoginTime = Convert.ToDateTime(dr["LastLoginTime"].ToString()).ToTimeString();
                    resultList.Add(item);
                }
            }

            return ModelResult(new Guid(Id), resultList);
        }
    }

    public class SupervisorReportModel : ReportModelRowBase<SupervisorReportModel>, IReportModel {

        [ReportColumn("UserName", Text = "用户名")]
        public string UserName { get; set; }

        [ReportColumn("ParentId", Text = "上级会员")]
        public string ParentId { get; set; }

        [ReportColumn("UserId", Text = "下级会员")]
        public string UserId { get; set; }

        public void SetDefault() {
        }
    }
}