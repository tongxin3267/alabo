using System;
using Xunit;
using ZKCloud.App.Core.Reports.Domain.Dtos;
using ZKCloud.App.Core.Reports.Domain.Services;
using ZKCloud.Domains.Dtos;
using ZKCloud.Test.Base.Core;
using ZKCloud.Test.Base.Core.Model;
using ZKCloud.UI.AutoReports.Dtos;

namespace ZKCloud.Test.Core.Reports.Domain.Services
{
    public class IAutoReportServiceTests : CoreTest
    {
        [Fact]
        [TestMethod("GetDayCountReport_CountReportInput")]
        public void GetDayCountReport_CountReportInput_test()
        {
            CountReportInput dateCountReportInput = null;
            var result = Resolve<IAutoReportService>().GetDayCountReport(dateCountReportInput);
            Assert.NotNull(result);
            Resolve<IAutoReportService>().GetDayCountReport(dateCountReportInput);
        }

        #region 图形报表测试-用例 
        /// <summary>
        /// 场景：会员数量统计
        /// </summary>
        [Fact]
        [TestMethod("GetDayCountReport_CountReportInput")]
        public void GetDayCountReport_CountReportInput_test_User_Count()
        {
            CountReportInput dateCountReportInput = new CountReportInput
            {
                EntityType = "User",
  
            };
            var result = Resolve<IAutoReportService>().GetDayCountReport(dateCountReportInput);
            Assert.NotNull(result);
            Resolve<IAutoReportService>().GetDayCountReport(dateCountReportInput);
        }
       
        /// <summary>
        /// 场景：订单数量统计
        /// </summary>
        /// GetDayCountReport_CountReportInput_test_Order_count
        [Fact]
        [TestMethod("GetDayCountReport_CountReportInput_test_Order_count")]
        public void GetDayCountReport_CountReportInput_test_Order_count()
        {
            CountReportInput dateCountReportInput = new CountReportInput
            {
                EntityType = "order",

            };
            var result = Resolve<IAutoReportService>().GetDayCountReport(dateCountReportInput);
            Assert.NotNull(result);
            Resolve<IAutoReportService>().GetDayCountReport(dateCountReportInput);
        }

        /// <summary>
        /// 场景：会员根据状态统计
        /// </summary>
        [Fact]
        [TestMethod("GetDayCountReport_CountReportInput")]
        public void GetDayCountReport_CountReportInput_test_UserByStatus() {
            CountReportInput dateCountReportInput = new CountReportInput {
                EntityType = "User",
                Field = "Status"
            };
            var result = Resolve<IAutoReportService>().GetDayCountReport(dateCountReportInput);
            Assert.NotNull(result);
            //Resolve<IAutoReportService>().GetDayCountReport(dateCountReportInput);
        }

        /// <summary>
        /// 场景：订单根据状态统计
        /// </summary>
        [Fact]
        [TestMethod("GetDayCountReport_CountReportInput")]
        public void GetDayCountReport_CountReportInput_test_Order_Status() {
            CountReportInput dateCountReportInput = new CountReportInput {
                EntityType = "Order",
                Field = "Status"
            };
            var result = Resolve<IAutoReportService>().GetDayCountReport(dateCountReportInput);
            Assert.NotNull(result);
            Resolve<IAutoReportService>().GetDayCountReport(dateCountReportInput);
        }

        #endregion

        #region 报表表格测试

        /// <summary>
        /// 场景：简单 会员数量统计报表表格
        /// 条件：用户与开始、结束时间
        /// </summary>
        /// 测试ok
        [Fact]
        [TestMethod("GetDayCountTable_User_Count_Field")]
        public void GetDayCountTable_User_Count_Field()
        {
            var x = Convert.ToDateTime("2019-04-05".ToString());
            var y = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));

            CountReportInput dateCountReportInput = new CountReportInput
            {
                EntityType = "User",
                StartTime = x,
                EndTime=y,
            };
            var result = Resolve<IAutoReportService>().GetDayCountTableByField(dateCountReportInput);
            Assert.NotNull(result);
            Resolve<IAutoReportService>().GetDayCountReport(dateCountReportInput);
        }

        /// <summary>
        /// 场景：多维度-会员数量统计报表表格
        /// 条件：用户、开始、结束时间、统计字段
        /// </summary>
        [Fact]
        [TestMethod("GetDayCountTable_User_Count")]
        public void GetDayCountTable_User_Count()
        {
            var x = Convert.ToDateTime("2019-04-05".ToString());
            var y = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));


            CountReportInput dateCountReportInput = new CountReportInput
            {
                EntityType = "User",
                StartTime = x,
                EndTime = y,
                Field = "Status",

            };
            var result = Resolve<IAutoReportService>().GetDayCountTableByField(dateCountReportInput);
            Assert.NotNull(result);
            Resolve<IAutoReportService>().GetDayCountReport(dateCountReportInput);
        }

        /// <summary>
        /// 场景：多维度-会员数量统计报表表格
        /// 条件：支付、开始，结束时间、统计字段
        /// </summary>
        [Fact]
        [TestMethod("GetDayCountTable_Pay_Count")]
        public void GetDayCountTable_Pay_Count()
        {
            var x = Convert.ToDateTime("2019-04-05".ToString());
            var y = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));


            CountReportInput dateCountReportInput = new CountReportInput
            {
                EntityType = "Pay",
                StartTime = x,
                EndTime = y,
                Field = "Status",

            };
            var result = Resolve<IAutoReportService>().GetDayCountTableByField(dateCountReportInput);
            Assert.NotNull(result);
            Resolve<IAutoReportService>().GetDayCountReport(dateCountReportInput);
        }

        /// <summary>
        /// 场景：多维度-会员数量统计报表表格
        /// 条件：订单、开始，结束时间、统计字段
        /// </summary>
        [Fact]
        [TestMethod("GetDayCountTable_Order_Count")]
        public void GetDayCountTable_Order_Count()
        {
            var x = Convert.ToDateTime("2019-04-05".ToString());
            var y = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));


            CountReportInput dateCountReportInput = new CountReportInput
            {
                EntityType = "order",
                StartTime = x,
                EndTime = y,
                Field = "Status",

            };
            var result = Resolve<IAutoReportService>().GetDayCountTableByField(dateCountReportInput);
            Assert.NotNull(result);
            Resolve<IAutoReportService>().GetDayCountReport(dateCountReportInput);
        }

        //GetDayCountTableByField
        #endregion


        [Fact]
        [TestMethod("GetDayCountReport_CountReportInput")]
        public void GetDayCountReport_CountReportInput_test_Order() {
            CountReportInput dateCountReportInput = new CountReportInput {
                EntityType = "Order",
            };
            var result = Resolve<IAutoReportService>().GetDayCountReport(dateCountReportInput);
            Assert.NotNull(result);
            Resolve<IAutoReportService>().GetDayCountReport(dateCountReportInput);
        }

        [Fact]
        [TestMethod("GetSumReport_SumTableInput")]
        public void GetSumReport_SumTableInput_test()
        {
            SumTableInput reportInput = null;
            var result = Resolve<IAutoReportService>().GetSumReport(reportInput);
            Assert.NotNull(result);
            Resolve<IAutoReportService>().GetSumReport(reportInput);
        }

/*end*/
    }
}
