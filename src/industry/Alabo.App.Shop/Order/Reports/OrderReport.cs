using System;
using System.Collections.Generic;
using Alabo.App.Core.Reports;
using Alabo.App.Core.User.Domain.Repositories;
using Alabo.Domains.Repositories.EFCore;

namespace Alabo.App.Shop.Order.Reports {

    [ReportRule(Id, typeof(OrderReportModel), Name = "订单购物排行版", Summary = "历史累计会员登录次数")]
    public class LoginNumTop10Report : ReportModelRuleBase<OrderReportModel> {
        public const string Id = "A1130393-D71E-4C0B-A9C5-7ED026924D3E";

        public LoginNumTop10Report(ReportContext context)
            : base(context) {
        }

        public override IReportRuleResult Execute(ReportRuleParameter parameter) {
            var begin = parameter.GetValue<string>("begin");
            var end = parameter.GetValue<string>("end");
            if (end == "1900-1-1") {
                end = DateTime.Now.ToString("yyyy-MM-dd");
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
                        PersonCount = Convert.ToDecimal(dr["PersonCount"])
                    };
                    list.Add(item);
                }
            }

            return ModelResult(new Guid(Id), list);
        }
    }

    public class OrderReportModel : ReportModelRowBase<OrderReportModel>, IReportModel {

        [ReportColumn("DateName", Text = "日期")]
        public string DateName { get; set; }

        /// <summary>
        ///     包含已支付的和未支付
        /// </summary>
        [ReportColumn("TotalAmount", Text = "交易总额")]
        public decimal TotalAmount { get; set; }

        /// <summary>
        ///     已支付金额
        /// </summary>
        [ReportColumn("PaymentAmount", Text = "已支付金额")]
        public decimal PaymentAmount { get; set; }

        /// <summary>
        ///     未支付金额
        /// </summary>
        [ReportColumn("UnPayAmount", Text = "未支付金额")]
        public decimal UnPayAmount { get; set; }

        /// <summary>
        ///     购买人次
        /// </summary>
        [ReportColumn("PersonCount", Text = "购买人次")]
        public decimal PersonCount { get; set; }

        public void SetDefault() {
        }
    }
}