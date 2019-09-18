using System;
using System.Collections.Generic;
using Alabo.App.Core.Reports;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Helpers;

namespace Alabo.App.Core.User.Reports {

    [ReportRule(Id, typeof(RegMonthReportModel), Name = "月会员注册统计", Summary = "统计月注册会员数据")]
    public class RegMonthReport : ReportModelRuleBase<RegMonthReportModel> {
        public const string Id = "1519C5AC-B8C7-492A-8D88-C79AC223E5A9";

        public RegMonthReport(ReportContext context)
            : base(context) {
        }

        public override IReportRuleResult Execute(ReportRuleParameter parameter) {
            var repositoryContext = Ioc.Resolve<IUserRepository>().RepositoryContext;
            var sql = @"select * from GetUserByDate('2016-11-1')";
            var resultList = new List<RegMonthReportModel>();
            using (var dr = repositoryContext.ExecuteDataReader(sql)) {
                while (dr.Read()) {
                    var item = new RegMonthReportModel {
                        CreateDate = dr["CreateDate"].ToString().Substring(0, 10),
                        RegCount = dr["RegCount"].ToDecimal()
                    };
                    //item.RealName = dr["RealName"].ToString();
                    //item.LastLoginTime = Convert.ToDateTime(dr["LastLoginTime"].ToString()).ToTimeString();
                    resultList.Add(item);
                }
            }

            return ModelResult(new Guid(Id), resultList);
        }
    }

    public class RegMonthReportModel : ReportModelRowBase<RegMonthReportModel>, IReportModel {

        [ReportColumn("CreateDate", Text = "时间")]
        public string CreateDate { get; set; }

        [ReportColumn("RegCount", Text = "注册人数")]
        public decimal RegCount { get; set; }

        public void SetDefault() {
        }
    }
}