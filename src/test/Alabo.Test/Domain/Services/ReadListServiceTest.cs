using Xunit;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Domain.Services
{
    [TestCaseOrderer("ZKCloud.Test.Core.PriorityOrderer", "ZKCloud.Test")]
    public class ReadListServiceTest : CoreTest
    {
        [Fact]
        public void Find_ExpectedBehavior_Mongodb()
        {
            //var workOrder = Service<IWorkOrderService>().FirstOrDefault();
            //if (workOrder == null)
            //{
            //    workOrder = new WorkOrder();
            //    Service<IWorkOrderService>().Add(workOrder);
            //    workOrder = Service<IWorkOrderService>().FirstOrDefault();
            //}

            //var dic = new Dictionary<string, string>
            //{
            //    {"Id", $"=={workOrder.Id}"}
            //};
            //var newUser = Service<IWorkOrderService>().GetList(dic);

            //Assert.NotNull(newUser);
            //var workOrders = newUser.ToList();
            //Assert.True(workOrders.Any());
            //Assert.Equal(workOrders.FirstOrDefault()?.Id, workOrder.Id);
        } /*end*/
    }
}