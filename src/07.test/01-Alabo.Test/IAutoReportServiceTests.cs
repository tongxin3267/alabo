using System.Linq;
using Xunit;
using Alabo.App.Core.Reports.Domain.Dtos;
using Alabo.App.Core.Reports.Domain.Services;
using Alabo.App.Shop.Order.Domain.Enums;
using Alabo.Domains.Base.Services;
using Alabo.Domains.Dtos;
using Alabo.Extensions;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Alabo.UI.AutoReports.Dtos;

namespace Alabo.Test.Core.Reports.Domain.Services
{
    public class IAutoReportServiceTests : CoreTest {
		[Fact]
		[TestMethod("GetDayCountReport_CountReportInput")]
		public void GetDayCountReport_CountReportInput_test () {
			CountReportInput dateCountReportInput = null;
			var result = Resolve<IAutoReportService>().GetDayCountReport( dateCountReportInput);
			Assert.NotNull(result);
            Resolve<IAutoReportService>().GetDayCountReport( dateCountReportInput);
		}

		[Fact]
		[TestMethod("GetSumReport_SumTableInput")]
		public void GetSumReport_SumTableInput_test () {
			SumTableInput reportInput = null;
			var result = Resolve<IAutoReportService>().GetSumReport( reportInput);
			Assert.NotNull(result);
            Resolve<IAutoReportService>().GetSumReport( reportInput);
		}

        [Fact]

        public void Init()
        {
            Resolve<ITableService>().DeleteAll();
            Resolve<ITableService>().Init();
        }

        [Fact]
        public void GetFiledOrType()
        {
            var displayName = "Order".GetFiledDisplayName("orderStatus");
            Assert.Equal( "¶©µ¥×´Ì¬", displayName);
          
            var fileType= "Order".GetFiledType("orderStatus");
            Assert.Equal(typeof(OrderStatus), fileType);
        }

/*end*/
	}
}
