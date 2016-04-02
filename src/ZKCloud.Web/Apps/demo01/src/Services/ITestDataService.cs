using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZKCloud.Domain.Models;
using ZKCloud.Domain.Services;
using ZKCloud.Web.Apps.Demo01.Domain.Models;

namespace ZKCloud.Web.Apps.Demo01.Domain.Services {
    public interface ITestDataService : IAutoApiService {
        IList<TestData> GetList();

        PagedList<TestData> GetList(int pageIndex, int pageSize);
    }
}
