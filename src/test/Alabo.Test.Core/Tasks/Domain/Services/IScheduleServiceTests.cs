using System.Linq;
using Xunit;
using Alabo.App.Core.Tasks.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Tasks.Domain.Services
{
    public class IScheduleServiceTests : CoreTest
    {
        [Fact]
        [TestMethod("GetAllTypes")]
        public void GetAllTypes_test()
        {
            var result = Resolve<IScheduleService>().GetAllTypes();
            Assert.NotNull(result);
            Assert.True(result.Count() > 10);
        }

        [Fact]
        [TestMethod("Init")]
        public void Init_test()
        {
            Resolve<IScheduleService>().Init();
            var list = Resolve<IScheduleService>().GetList();
            var result = Resolve<IScheduleService>().GetAllTypes();
            Assert.NotNull(result);
            Assert.NotNull(list);
            Assert.True(result.Count() == list.Count());
        }

        /*end*/
    }
}