using System;
using Xunit;
using Alabo.App.Core.Reports.Domain.Services;
using Alabo.App.Open.Reports.Admin;
using Alabo.App.Open.Reports.Finance;
using Alabo.App.Open.Reports.Order;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Reports.Domain.Services
{
    public class IReportServiceTests : CoreTest
    {
        [Theory]
        [InlineData(typeof(RewardReport))]
        [InlineData(typeof(OrderReport))]
        [InlineData(typeof(AdminSideBarReport))]
        [TestMethod("GetValue_Type_Guid")]
        public void GetValue_Type_Guid_test(Type type)
        {
            var id = Guid.Empty;
            var result = Resolve<IReportService>().GetValue(type, id);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(typeof(RewardReport))]
        [InlineData(typeof(OrderReport))]
        [InlineData(typeof(AdminSideBarReport))]
        [TestMethod("GetObjectList_Type")]
        public void GetObjectList_Type_test(Type type)
        {
            var result = Resolve<IReportService>().GetObjectList(type);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("AddOrUpdate_Object")]
        public void AddOrUpdate_Object_test()
        {
            //Object value = null;
            //var result = Service<IReportService>().AddOrUpdate( value);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("AddOrUpdate_Report")]
        [TestIgnore]
        public void AddOrUpdate_Report_test()
        {
            //Report report = null;
            //Service<IReportService>().AddOrUpdate(report);
        }

        [Fact]
        [TestMethod("AddOrUpdate_T")]
        public void AddOrUpdate_T_test()
        {
            //T userReport = null;
            //Service<IReportService>().AddOrUpdate( userReport);
        }

        [Fact]
        [TestMethod("FromIEnumerable_IEnumerable1_Func2_Func2")]
        [TestIgnore]
        public void FromIEnumerable_IEnumerable1_Func2_Func2_test()
        {
            //var result = Service<IReportService>().FromIEnumerable( null, null, null);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAllTypes")]
        public void GetAllTypes_test()
        {
            var result = Resolve<IReportService>().GetAllTypes();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetList_Func2_Func2_Func2")]
        [TestIgnore]
        public void GetList_Func2_Func2_Func2_test()
        {
            //var result = Service<IReportService>().GetList( null, null, null);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetList_Func2")]
        [TestIgnore]
        public void GetList_Func2_test()
        {
            //var result = Service<IReportService>().GetList(null);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetList_String")]
        [TestIgnore]
        public void GetList_String_test()
        {
            //var key = "";
            //var result = Service<IReportService>().GetList(key);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetReport_String")]
        [TestIgnore]
        public void GetReport_String_test()
        {
            //var key = "";
            //var result = Service<IReportService>().GetReport(key);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetValue_String")]
        [TestIgnore]
        public void GetValue_String_test()
        {
            //var key = "";
            //var result = Service<IReportService>().GetValue(key);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetValue")]
        [TestIgnore]
        public void GetValue_test()
        {
            //var result = Service<IReportService>().GetValue();
            //Assert.NotNull(result);
        }

        /*end*/
    }
}