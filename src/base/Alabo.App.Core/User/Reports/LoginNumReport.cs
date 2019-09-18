using System;
using System.Collections.Generic;
using Alabo.App.Core.Reports;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Extensions;
using Alabo.Helpers;

namespace Alabo.App.Core.User.Reports {

    [ReportRule(Id, typeof(LoginNumReportModel), Name = "会员登录排行版", Summary = "历史累计会员登录次数")]
    public class LoginNumTop10Report : ReportModelRuleBase<LoginNumReportModel> {
        public const string Id = "1519C5AC-B8C7-492A-8D88-C79AC223E5F3";

        public LoginNumTop10Report(ReportContext context)
            : base(context) {
        }

        public override IReportRuleResult Execute(ReportRuleParameter parameter) {
            var num = parameter.GetValue<string>("num").ToInt();
            if (num == 0) {
                num = 10;
            }

            var repositoryContext = Ioc.Resolve<IUserRepository>().RepositoryContext;
            var sql = $@"SELECT   TOP {num} LoginNum,UserId from User_UserDetail order by LoginNum desc ";
            var resultList = new List<LoginNumReportModel>();
            using (var dr = repositoryContext.ExecuteDataReader(sql)) {
                while (dr.Read()) {
                    var item = new LoginNumReportModel {
                        LoginNum = dr["LoginNum"].ToString().ToDecimal(),
                        UserName = dr["UserId"].ToString()
                    };
                    //item.RealName = dr["RealName"].ToString();
                    //item.LastLoginTime = Convert.ToDateTime(dr["LastLoginTime"].ToString()).ToTimeString();
                    resultList.Add(item);
                }
            }

            return ModelResult(new Guid(Id), resultList);
        }
    }

    public class LoginNumReportModel : ReportModelRowBase<LoginNumReportModel>, IReportModel {

        [ReportColumn("UserName", Text = "用户名")]
        public string UserName { get; set; }

        [ReportColumn("LoginNum", Text = "登录次数")]
        public decimal LoginNum { get; set; }

        //[ReportColumn("RealName", Text = "真实姓名")]
        //public string RealName { get; set; }

        //[ReportColumn("LastLoginTime", Text = "最后登录时间")]
        //public string LastLoginTime { get; set; }

        public void SetDefault() {
        }
    }
}