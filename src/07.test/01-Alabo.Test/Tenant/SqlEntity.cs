using System.Collections.Generic;
using Xunit;
using Alabo.App.Core.Admin.Domain.Services;
using Alabo.App.Core.Common.Domain.Entities;
using Alabo.App.Core.Common.Domain.Services;
using Alabo.App.Core.Finance.Domain.Entities;
using Alabo.App.Core.Finance.Domain.Services;
using Alabo.AutoConfigs.Entities;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Tenant {

    /// <summary>
    ///     测试bill表所有的同步方法
    /// </summary>
    public class BillEntityTest : CoreTest {

        [Fact]
        public void Create_Database()
        {
            var tenant = "tenant1";
           // Resolve<IAdminService>().InitTenantDatabase(tenant);
        }

        [Fact]
        public void Add_AutoConfig() {
            var autoConfig = new AutoConfig {
                AppName = "test"
            };
            var result = Resolve<IAutoConfigService>().Add(autoConfig);
        }

        [Fact]
        public void Add_Test() {
            var bill = new Bill {
                UserId = 0,
                EntityId = 0
            };
            var result = Resolve<IBillService>().Add(bill);
        }

        [Fact]
        public void GetSingle() {
            var bill = Resolve<IBillService>().GetList();
        }

        [Fact]
        public void AddMany_Test() {
            var bill = new Bill {
                UserId = 0,
                EntityId = 0
            };
            var list = new List<Bill>();
            list.Add(bill);
            Resolve<IBillService>().AddMany(list);
        }

        [Fact]
        public void AddOrUpdate_Test() {
            var bill = new Bill {
                UserId = 0,
                EntityId = 0
            };
            var result = Resolve<IBillService>().AddOrUpdate(bill);
        }

        [Fact]
        public void Count_Test() {
            var result = Resolve<IBillService>().Count();
            Assert.NotNull(result);
        }

        [Fact]
        public void CountId_Test() {
            var result = Resolve<IBillService>().Count(r => r.Id >= 0);
            Assert.NotNull(result);
        }

        [Fact]
        public void Delete_Test() {
            var result = Resolve<IBillService>().Delete(r => r.Id >= 0);
            Assert.NotNull(result);
        }

        [Fact]
        public void DeleteMany_Test() {
            var bill = new Bill {
                UserId = 0,
                EntityId = 0
            };
            Resolve<IBillService>().Add(bill);
            var list = new List<Bill>();
            list.Add(bill);
            Resolve<IBillService>().DeleteMany(list);
        }

        [Fact]
        public void Exists_Test() {
            var result = Resolve<IBillService>().Exists(r => r.Id >= 0);
            Assert.NotNull(result);
        }

        [Fact]
        public void ExistsId_Text() {
            var bill = Resolve<IBillService>().FirstOrDefault();
            if (bill != null) {
                var result = Resolve<IBillService>().Exists(bill.Id);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void FirstOrDefault_Test() {
            var result = Resolve<IBillService>().FirstOrDefault();
            Assert.NotNull(result);
        }

        [Fact]
        public void GetByIdNoTracking_Test() {
            var bill = Resolve<IBillService>().FirstOrDefault();
            if (bill != null) {
                var result = Resolve<IBillService>().GetByIdNoTracking(bill.Id);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void GetList_Test() {
            var result = Resolve<IBillService>().GetList();
            Assert.NotNull(result);
        }

        [Fact]
        public void GetRandom_Test() {
            var bill = Resolve<IBillService>().FirstOrDefault();
            if (bill != null) {
                var result = Resolve<IBillService>().GetRandom(bill.Id);
                Assert.NotNull(result);
                var newExists = Resolve<IBillService>().Exists(u => u.UserId == bill.UserId);
                Assert.True(newExists);
            }
        }

        [Fact]
        public void GetSingle_test() {
            var result = Resolve<IBillService>().GetSingle(r => r.Id >= 0);
            Assert.NotNull(result);
        }

        [Fact]
        public void GetSingleId_Test() {
            var bill = Resolve<IBillService>().FirstOrDefault();
            if (bill != null) {
                var result = Resolve<IBillService>().GetSingle(bill.Id);
                Assert.NotNull(result);
            }
        }

        [Fact]
        public void GetSingleOrderBy_Test() {
            var result = Resolve<IBillService>().GetSingleOrderBy(r => r.Id);
            Assert.NotNull(result);
            var last = Resolve<IBillService>().FirstOrDefault();
            Assert.Equal(result.Id, last.Id);
            Assert.Equal(result, last);
        }

        [Fact]
        public void GetSingleOrderByDescending_Test() {
            var result = Resolve<IBillService>().GetSingleOrderByDescending(r => r.Id);
            Assert.NotNull(result);
            var last = Resolve<IBillService>().LastOrDefault();
            Assert.Equal(result.Id, last.Id);
            Assert.Equal(result, last);
        }

        [Fact]
        public void LastOrDefault_Test() {
            var result = Resolve<IBillService>().LastOrDefault();
            Assert.NotNull(result);
        }

        [Fact]
        public void Max_Test() {
            var result = Resolve<IBillService>().Max();
            Assert.NotNull(result);
            var last = Resolve<IBillService>().LastOrDefault();
            Assert.Equal(result.Id, last.Id);
            Assert.Equal(result, last);
        }

        [Fact]
        public void MaxAmount_Test() {
            var result = Resolve<IBillService>().Max(r => r.Id >= 0);
            Assert.NotNull(result);
        }

        [Fact]
        public void PageCount_Count() {
            var count = Resolve<IBillService>().PageCount(30);
            var pageCount = Resolve<IBillService>().PageCount(30, r => r.Id > 0);
            Assert.Equal(count, pageCount);
        }

        [Fact]
        public void Update_Test() {
            var bill = Resolve<IBillService>().FirstOrDefault();
            var result = Resolve<IBillService>().Update(bill);
            Assert.True(result);
        }
    }
}