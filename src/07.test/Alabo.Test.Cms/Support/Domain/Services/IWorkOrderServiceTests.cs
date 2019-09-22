using MongoDB.Bson;
using Xunit;
using Alabo.App.Cms.Support.Domain.Entities;
using Alabo.App.Cms.Support.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Cms.Support.Domain.Services
{
    public class IWorkOrderServiceTests : CoreTest
    {
        [Fact]
        [TestMethod("AddWorkOrder_WorkOrder")]
        public void AddWorkOrder_WorkOrder_test()
        {
            var view = new WorkOrder();
            var result = Resolve<IWorkOrderService>().AddWorkOrder(view);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Delete_ObjectId")]
        public void Delete_ObjectId_test()
        {
            var id = ObjectId.Empty;
            var result = Resolve<IWorkOrderService>().Delete(id);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetPageList_Object")]
        public void GetPageList_Object_test()
        {
            object query = null;
            var result = Resolve<IWorkOrderService>().GetPageList(query);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetWorkOrdersList")]
        public void GetWorkOrdersList_test()
        {
            var result = Resolve<IWorkOrderService>().GetWorkOrdersList();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("UpdateWorkOrder_WorkOrder")]
        public void UpdateWorkOrder_WorkOrder_test()
        {
            WorkOrder view = null;
            var result = Resolve<IWorkOrderService>().UpdateWorkOrder(view);
            Assert.NotNull(result);
        }

        /*end*/
    }
}