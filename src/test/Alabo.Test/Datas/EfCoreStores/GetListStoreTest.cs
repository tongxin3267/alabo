using Xunit;
using ZKCloud.Datas.UnitOfWorks;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Datas.EfCoreStores
{
    public class GetListStoreTest : CoreTest
    {
        /// <summary>
        ///     工作单元
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        [Fact]
        public void GetListBaseTest()
        {
            var unitOfWork = new UnitOfWorkManager();
        }
    }
}