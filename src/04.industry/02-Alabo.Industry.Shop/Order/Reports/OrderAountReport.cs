using System;
using System.Collections.Generic;
using System.Linq;
using Alabo.App.Core.Reports;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Domains.Repositories.EFCore;

namespace Alabo.App.Shop.Order.Reports {

    [ReportRule(Id, typeof(OrderReportModel), Name = "订单支付统计", Summary = "时间范围订单支付统计")]
    public class OrderAountReport : ReportModelRuleBase<OrderReportModel> {
        private const string Id = "ad90d20a-b1fc-450f-970f-e69cb1e6df13";

        public OrderAountReport(ReportContext context)
            : base(context) {
        }

        public override IReportRuleResult Execute(ReportRuleParameter parameter) {
            var date = parameter.GetValue<string>("date");
            var begin = parameter.GetValue<string>("begin");
            var end = parameter.GetValue<string>("end");
            switch (date) {
                case "today":
                    begin = DateTime.Now.ToString("yyyy-MM-dd");
                    end = DateTime.Now.ToString("yyyy-MM-dd");
                    break;

                case "yesterday":
                    begin = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    end = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                    break;

                case "month":
                    begin = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                    end = DateTime.Now.ToString("yyyy-MM-dd");
                    break;

                case "year":
                    begin = DateTime.Now.AddDays(-365).ToString("yyyy-MM-dd");
                    end = DateTime.Now.ToString("yyyy-MM-dd");
                    break;
            }

            var repositoryContext = Alabo.Helpers.Ioc.Resolve<IUserRepository>().RepositoryContext;
            var sql = string.Format("select * from GetOrderMS('{0}','{1}')", begin, end);
            var list = new List<OrderReportModel>();
            using (var dr = repositoryContext.ExecuteDataReader(sql)) {
                while (dr.Read()) {
                    var item = new OrderReportModel {
                        DateName = dr["DateName"].ToString(),
                        TotalAmount = Convert.ToDecimal(dr["TotalAmount"]),
                        PaymentAmount = Convert.ToDecimal(dr["PaymentAmount"]),
                        UnPayAmount = Convert.ToDecimal(dr["UnPayAmount"]),
                        PersonCount = Convert.ToInt32(dr["PersonCount"])
                    };
                    list.Add(item);
                }
            }

            var model = new List<OrderReportModel>
            {
                new OrderReportModel
                {
                    DateName = "default",
                    TotalAmount = list.Sum(e => e.TotalAmount),
                    PaymentAmount = list.Sum(e => e.PaymentAmount),
                    UnPayAmount = list.Sum(e => e.UnPayAmount),
                    PersonCount = list.Sum(e => e.PersonCount)
                }
            };
            return ModelResult(new Guid(Id), model);
        }
    }
}