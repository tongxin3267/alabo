using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;
using ZKCloud.Domain.Services;
using ZKCloud.Web.Apps.Demo01.Domain.Models;
using ZKCloud.Web.Apps.Demo01.Domain.Repositories;

namespace ZKCloud.Web.Apps.Demo01.Domain.Services {
    public class TestDataService : ServiceBase, ITestDataService {
        private static IList<TestData> _list;

        static TestDataService() {
            _list = new List<TestData>();
            for (int i = 0; i < 1000; i++) {
                _list.Add(new TestData() { Id = i, Name = $"name of id:{i}" });
            }
        }

        public IList<TestData> GetTestList() {
            System.Diagnostics.Debug.WriteLine("testdataservice::gettestlist()");
            return Repository<TestDataRepository>().ReadMany(e => true).ToList();
        }

        public PagedList<TestData> GetList(int pageIndex, int pageSize) {
            System.Diagnostics.Debug.WriteLine($"pageindex:{pageIndex}, pagesize:{pageSize}");
            var list = _list.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return PagedList<TestData>.Create(list, _list.Count, pageSize, pageIndex);
        }
    }
}
