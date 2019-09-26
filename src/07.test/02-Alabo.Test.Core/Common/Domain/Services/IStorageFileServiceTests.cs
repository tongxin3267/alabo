using MongoDB.Bson;
using Xunit;
using Alabo.Test.Base.Attribute;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Common.Domain.Services
{
    public class IStorageFileServiceTests : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        [TestMethod("GetSingleFromCache_Test")]
        public void GetSingleFromCache_Test_ExpectedBehavior(long entityId)
        {
            //var model = Service<IAdminService>().GetRandom(entityId);
            //if (model != null)
            //{
            //    var newModel = Service<IAdminService>().GetSingleFromCache(model.Id);
            //    Assert.NotNull(newModel);
            //    Assert.Equal(newModel.Id, model.Id);
            //}
        }

        [Fact]
        [TestMethod("Add_StorageFile")]
        [TestIgnore]
        public void Add_StorageFile_test()
        {
            //StorageFile file = null;
            //Service<IStorageFileService>().Add( file);
        }

        [Fact]
        [TestMethod("AddVisit_Guid")]
        public void AddVisit_Guid_test()
        {
            var id = ObjectId.Empty;
            //Resolve<IStorageFileService>().AddVisit(id);
        }

        [Fact]
        [TestMethod("Delete_Guid")]
        public void Delete_Guid_test()
        {
            var id = ObjectId.Empty;
            Resolve<IStorageFileService>().Delete(r => r.Id == id);
        }

        [Fact]
        [TestMethod("ReadSingle_Guid")]
        [TestIgnore]
        public void ReadSingle_Guid_test()
        {
            //var id =Guid.Empty ;
            //var result = Service<IStorageFileService>().ReadSingle( id);
            //Assert.NotNull(result);
        }

        /*end*/
    }
}