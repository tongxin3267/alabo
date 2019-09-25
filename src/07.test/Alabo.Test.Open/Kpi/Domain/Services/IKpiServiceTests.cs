using Alabo.App.Core.User.Domain.Services;
using Alabo.App.Share.Kpi.Domain.Services;
using Alabo.Domains.Enums;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using System;
using System.Collections.Generic;
using Alabo.Randoms;
using Xunit;

namespace Alabo.Test.Open.Kpi.Domain.Services {

    public class IKpiServiceTests : CoreTest {
        private readonly Guid TestModuleId = Guid.Parse("DF000650-C400-46aa-9EC0-9A4401018888");
        private readonly long TestEntityId = 198471;

        public void DeleteModlue() {
            var result = Resolve<IKpiService>().Delete(r => r.ModuleId == TestModuleId);
        }

        private App.Share.Kpi.Domain.Entities.Kpi GetSingleKpi(DateTime dateTime, TimeType timeType) {
            var user = Resolve<IUserService>().GetSingle("admin");

            var kpi = new App.Share.Kpi.Domain.Entities.Kpi {
                EntityId = TestEntityId,
                ModuleId = TestModuleId,
                UserId = user.Id,
                Value = 100,
                Type = timeType,
                CreateTime = dateTime
            };
            return kpi;
        }

        [Fact]
        [TestMethod("AddSingle_Kpi")]
        public void AddSingle_Kpi_test() {
            DeleteModlue();

            var kpi = GetSingleKpi(DateTime.Now, TimeType.Month);
            Resolve<IKpiService>().AddSingle(kpi);
            var lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);
            Assert.NotNull(lastSingle);
        }

        [Fact]
        public void AddTestModule() {
            var list = new List<App.Share.Kpi.Domain.Entities.Kpi>();
            for (var i = 0; i < 10; i++) {
                var index = RandomHelper.Number(1, 400);
                var kpi = GetSingleKpi(DateTime.Now.AddDays(-index), TimeType.HalfYear);
                list.Add(kpi);
            }

            Resolve<IKpiService>().AddMany(list);
        }

        /// <summary>
        ///     测试当天统计
        /// </summary>
        [Fact]
        public void DayMonth() {
            DeleteModlue();
            var kpi = GetSingleKpi(DateTime.Now, TimeType.Day);
            Resolve<IKpiService>().AddSingle(kpi);
            var lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);
            Assert.NotNull(lastSingle);
            Assert.Equal(100, lastSingle.TotalValue);

            kpi = GetSingleKpi(DateTime.Now.AddMinutes(1), TimeType.Day);
            Resolve<IKpiService>().AddSingle(kpi);
            lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);
            Assert.NotNull(lastSingle);
            Assert.Equal(200, lastSingle.TotalValue);

            kpi = GetSingleKpi(DateTime.Now.AddMinutes(-1), TimeType.Day);
            Resolve<IKpiService>().AddSingle(kpi);
            lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);

            kpi = GetSingleKpi(DateTime.Now.AddDays(1), TimeType.Day);
            Resolve<IKpiService>().AddSingle(kpi);

            kpi = GetSingleKpi(DateTime.Now.AddDays(-1), TimeType.Day);
            Resolve<IKpiService>().AddSingle(kpi);

            Assert.NotNull(lastSingle);
            Assert.Equal(300, lastSingle.TotalValue);
        }

        [Fact]
        [TestMethod("GetLastSingle_StageType_Int64_Guid")]
        public void GetLastSingle_StageType_Int64_Guid_test() {
            var user = Resolve<IUserService>().GetSingle("admin");
            var kpi = new App.Share.Kpi.Domain.Entities.Kpi {
                EntityId = TestEntityId,
                ModuleId = TestModuleId,
                UserId = user.Id,
                Value = 100,
                Type = TimeType.Month
            };
            Resolve<IKpiService>().AddSingle(kpi);
            var result = Resolve<IKpiService>().GetLastSingle(kpi);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetLastSingle_StageType_Int64_Int64")]
        public void GetLastSingle_StageType_Int64_Int64_test() {
            //var stageType = (Alabo.Core.Enums.Enum.StageType)0;
            //StageType stageType = null;
            //var userId = 0;
            //Int64 userId = 0;
            //Int64 userId = null;
            //var enityId = 0;
            //Int64 enityId = 0;
            //Int64 enityId = null;
            //var result = Service<IKpiService>().GetLastSingle(stageType, userId, enityId);
            //Assert.NotNull(result);
            //Service<IKpiService>().GetLastSingle(stageType, userId, enityId);
        }

        /// <summary>
        ///     测试当小时统计
        /// </summary>
        [Fact]
        public void HoursMonth() {
            DeleteModlue();
            var kpi = GetSingleKpi(DateTime.Now, TimeType.Hours);
            Resolve<IKpiService>().AddSingle(kpi);
            var lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);
            Assert.NotNull(lastSingle);
            Assert.Equal(100, lastSingle.TotalValue);

            kpi = GetSingleKpi(DateTime.Now, TimeType.Hours);
            Resolve<IKpiService>().AddSingle(kpi);
            lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);
            Assert.NotNull(lastSingle);
            Assert.Equal(200, lastSingle.TotalValue);

            kpi = GetSingleKpi(DateTime.Now, TimeType.Hours);
            Resolve<IKpiService>().AddSingle(kpi);
            lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);

            kpi = GetSingleKpi(DateTime.Now.AddHours(1), TimeType.Hours);
            Resolve<IKpiService>().AddSingle(kpi);

            kpi = GetSingleKpi(DateTime.Now.AddDays(1), TimeType.Hours);
            Resolve<IKpiService>().AddSingle(kpi);

            kpi = GetSingleKpi(DateTime.Now.AddMonths(1), TimeType.Hours);
            Resolve<IKpiService>().AddSingle(kpi);

            kpi = GetSingleKpi(DateTime.Now.AddYears(1), TimeType.Hours);
            Resolve<IKpiService>().AddSingle(kpi);

            kpi = GetSingleKpi(DateTime.Now.AddDays(7), TimeType.Hours);
            Resolve<IKpiService>().AddSingle(kpi);

            kpi = GetSingleKpi(DateTime.Now.AddHours(-1), TimeType.Hours);
            Resolve<IKpiService>().AddSingle(kpi);

            Assert.NotNull(lastSingle);
            Assert.Equal(300, lastSingle.TotalValue);
        }

        /// <summary>
        ///     测试分钟统计
        /// </summary>
        [Fact]
        public void MinuteMonth() {
            DeleteModlue();
            var time = DateTime.Now;
            var kpi = GetSingleKpi(time, TimeType.Minute);
            Resolve<IKpiService>().AddSingle(kpi);
            var lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);
            Assert.NotNull(lastSingle);
            Assert.Equal(100, lastSingle.TotalValue);

            kpi = GetSingleKpi(time, TimeType.Minute);
            Resolve<IKpiService>().AddSingle(kpi);
            lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);
            Assert.NotNull(lastSingle);
            Assert.Equal(200, lastSingle.TotalValue);

            kpi = GetSingleKpi(time, TimeType.Minute);
            Resolve<IKpiService>().AddSingle(kpi);
            lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);

            kpi = GetSingleKpi(time.AddMinutes(1), TimeType.Minute);
            Resolve<IKpiService>().AddSingle(kpi);

            kpi = GetSingleKpi(time.AddMinutes(2), TimeType.Minute);
            Resolve<IKpiService>().AddSingle(kpi);

            Assert.NotNull(lastSingle);
            Assert.Equal(300, lastSingle.TotalValue);
        }

        /// <summary>
        ///     测试季度统计
        /// </summary>
        [Fact]
        public void TestHalfYear() {
            DeleteModlue();
            var kpi = GetSingleKpi(DateTime.Now, TimeType.HalfYear);
            Resolve<IKpiService>().AddSingle(kpi);
            var lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);
            Assert.NotNull(lastSingle);
            Assert.Equal(100, lastSingle.TotalValue);

            kpi = GetSingleKpi(DateTime.Now.AddMinutes(15), TimeType.HalfYear);
            Resolve<IKpiService>().AddSingle(kpi);
            lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);
            Assert.NotNull(lastSingle);
            Assert.Equal(200, lastSingle.TotalValue);

            kpi = GetSingleKpi(DateTime.Now.AddMinutes(10), TimeType.HalfYear);
            Resolve<IKpiService>().AddSingle(kpi);
            lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);

            kpi = GetSingleKpi(DateTime.Now.AddYears(5), TimeType.HalfYear);
            Resolve<IKpiService>().AddSingle(kpi);

            kpi = GetSingleKpi(DateTime.Now.AddMonths(8), TimeType.HalfYear);
            Resolve<IKpiService>().AddSingle(kpi);

            kpi = GetSingleKpi(DateTime.Now.AddMonths(18), TimeType.HalfYear);
            Resolve<IKpiService>().AddSingle(kpi);
            Assert.NotNull(lastSingle);
            Assert.Equal(300, lastSingle.TotalValue);
        }

        /// <summary>
        ///     测试月份统计
        /// </summary>
        [Fact]
        public void TestMonth() {
            DeleteModlue();
            var kpi = GetSingleKpi(DateTime.Now, TimeType.Month);
            Resolve<IKpiService>().AddSingle(kpi);
            var lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);
            Assert.NotNull(lastSingle);
            Assert.Equal(100, lastSingle.TotalValue);

            kpi = GetSingleKpi(DateTime.Now.AddMinutes(15), TimeType.Month);
            Resolve<IKpiService>().AddSingle(kpi);
            lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);
            Assert.NotNull(lastSingle);
            Assert.Equal(200, lastSingle.TotalValue);

            kpi = GetSingleKpi(DateTime.Now.AddMinutes(10), TimeType.Month);
            Resolve<IKpiService>().AddSingle(kpi);
            lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);

            kpi = GetSingleKpi(DateTime.Now.AddMonths(1), TimeType.Month);
            Resolve<IKpiService>().AddSingle(kpi);
            Assert.NotNull(lastSingle);
            Assert.Equal(300, lastSingle.TotalValue);
        }

        /// <summary>
        ///     测试季度统计
        /// </summary>
        [Fact]
        public void TestQuarter() {
            DeleteModlue();
            var kpi = GetSingleKpi(DateTime.Now, TimeType.Quarter);
            Resolve<IKpiService>().AddSingle(kpi);
            var lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);
            Assert.NotNull(lastSingle);
            Assert.Equal(100, lastSingle.TotalValue);

            kpi = GetSingleKpi(DateTime.Now.AddMinutes(15), TimeType.Quarter);
            Resolve<IKpiService>().AddSingle(kpi);
            lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);
            Assert.NotNull(lastSingle);
            Assert.Equal(200, lastSingle.TotalValue);

            kpi = GetSingleKpi(DateTime.Now.AddMinutes(10), TimeType.Quarter);
            Resolve<IKpiService>().AddSingle(kpi);
            lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);

            kpi = GetSingleKpi(DateTime.Now.AddMonths(5), TimeType.Quarter);
            Resolve<IKpiService>().AddSingle(kpi);
            Assert.NotNull(lastSingle);
            Assert.Equal(300, lastSingle.TotalValue);
        }

        /// <summary>
        ///     测试季度统计
        /// </summary>
        [Fact]
        public void TestYear() {
            DeleteModlue();
            var kpi = GetSingleKpi(DateTime.Now, TimeType.Year);
            Resolve<IKpiService>().AddSingle(kpi);
            var lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);
            Assert.NotNull(lastSingle);
            Assert.Equal(100, lastSingle.TotalValue);

            kpi = GetSingleKpi(DateTime.Now.AddMinutes(15), TimeType.Year);
            Resolve<IKpiService>().AddSingle(kpi);
            lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);
            Assert.NotNull(lastSingle);
            Assert.Equal(200, lastSingle.TotalValue);

            kpi = GetSingleKpi(DateTime.Now.AddMinutes(10), TimeType.Year);
            Resolve<IKpiService>().AddSingle(kpi);
            lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);

            kpi = GetSingleKpi(DateTime.Now.AddYears(5), TimeType.Year);
            Resolve<IKpiService>().AddSingle(kpi);
            Assert.NotNull(lastSingle);
            Assert.Equal(300, lastSingle.TotalValue);
        }

        /// <summary>
        ///     测试当周统计
        /// </summary>
        [Fact]
        public void WeekMonth() {
            DeleteModlue();
            var kpi = GetSingleKpi(DateTime.Now, TimeType.Week);
            Resolve<IKpiService>().AddSingle(kpi);
            var lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);
            Assert.NotNull(lastSingle);
            Assert.Equal(100, lastSingle.TotalValue);

            kpi = GetSingleKpi(DateTime.Now, TimeType.Week);
            Resolve<IKpiService>().AddSingle(kpi);
            lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);
            Assert.NotNull(lastSingle);
            Assert.Equal(200, lastSingle.TotalValue);

            kpi = GetSingleKpi(DateTime.Now, TimeType.Week);
            Resolve<IKpiService>().AddSingle(kpi);
            lastSingle = Resolve<IKpiService>().GetLastSingle(kpi);

            kpi = GetSingleKpi(DateTime.Now.AddDays(7), TimeType.Week);
            Resolve<IKpiService>().AddSingle(kpi);

            kpi = GetSingleKpi(DateTime.Now.AddHours(8), TimeType.Week);
            Resolve<IKpiService>().AddSingle(kpi);

            Assert.NotNull(lastSingle);
            Assert.Equal(300, lastSingle.TotalValue);
        }

        /*end*/
    }
}