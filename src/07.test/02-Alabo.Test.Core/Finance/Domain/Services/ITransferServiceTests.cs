using Alabo.App.Asset.Transfers.Domain.Services;
using Xunit;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Finance.Domain.Services
{
    public class ITransferServiceTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            var model = Resolve<ITradeService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<ITradeService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        } /*end*/

        [Fact]
        [TestMethod("Add_TransferAddInput")]
        [TestIgnore]
        public void Add_TransferAddInput_test()
        {
            //TransferAddInput view = null;
            //var result = Service<ITransferService>().Add(view);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAdminTransfer_Int64")]
        [TestIgnore]
        public void GetAdminTransfer_Int64_test()
        {
            //var id = 0;
            //var result = Service<ITransferService>().GetAdminTransfer(id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPageList_Object")]
        public void GetPageList_Object_test()
        {
            object query = null;
            var result = Resolve<ITransferService>().GetPageList(query);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_Int64_Int64")]
        [TestIgnore]
        public void GetSingle_Int64_Int64_test()
        {
            //var id = 0;
            //var userId = 0;
            //var result = Service<ITransferService>().GetSingle( id, userId);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_Int64")]
        [TestIgnore]
        public void GetSingle_Int64_test()
        {
            //var id = 0;
            //var result = Service<ITransferService>().GetSingle(id);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetTransferConfig")]
        public void GetTransferConfig_test()
        {
            var result = Resolve<ITransferService>().GetTransferConfig();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUserList_TransferApiInput")]
        [TestIgnore]
        public void GetUserList_TransferApiInput_test()
        {
            //TransferApiInput transferApiInput = null;
            //var result = Service<ITransferService>().GetUserList(transferApiInput);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetUserPage_Object")]
        [TestIgnore]
        public void GetUserPage_Object_test()
        {
            //Object query = null;
            //var result = Service<ITransferService>().GetUserPage( query);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetView_Int64")]
        public void GetView_Int64_test()
        {
            var id = 0;
            var result = Resolve<ITransferService>().GetView(id);
            Assert.NotNull(result);
        }
    }
}