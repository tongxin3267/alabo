using System.Collections.Generic;
using Xunit;
using ZKCloud.App.Core.User.Domain.Services;
using ZKCloud.Domains.Base.Entities;
using ZKCloud.Domains.Base.Services;
using ZKCloud.Linq;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Domain.Services
{
    [TestCaseOrderer("ZKCloud.Test.Core.PriorityOrderer", "ZKCloud.Test")]
    public class ReadSingleServiceTest : CoreTest
    {
        [Theory]
        [InlineData(-1)]
        public void GetSingleFromCache_ExpectedBehavior(long userId)
        {
            var user = Resolve<IUserService>().GetRandom(userId);
            var newUser = Resolve<IUserService>().GetSingleFromCache(user.Id);
            Assert.NotNull(newUser);
            Assert.Equal(newUser?.Id, user.Id);
        }

        [Theory]
        [InlineData(-1)]
        public void GetSingleFromCache_Logs_ExpectedBehavior(long entityId)
        {
            var model = Resolve<ILogsService>().GetRandom(entityId);
            if (model != null)
            {
                var newModel = Resolve<ILogsService>().GetSingleFromCache(model.Id);
                Assert.NotNull(newModel);
                Assert.Equal(newModel.Id, model.Id);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void GetExpressionGuid_Logs_ExpectedBehavior(long entityId)
        {
            //var model = Service<ICategoryPropertyService>().GetRandom(entityId);
            //if (model != null)
            //{
            //    var expression =
            //        LinqHelper.GetExpression<CategoryProperty, bool>($"entity.Id == Guid.Parse(\"{model.Id}\")",
            //            "entity");
            //    Assert.NotNull(expression);
            //}
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void GetExpressionGuid_Logsd_ExpectedBehavior(long entityId)
        {
            //var model = Service<ICategoryPropertyService>().GetRandom(entityId);
            //if (model != null)
            //{
            //    var newModel = Service<ICategoryPropertyService>().GetSingleFromCache(model.Id);
            //    Assert.NotNull(newModel);
            //    Assert.Equal(newModel.Id, model.Id);
            //}
        }

        [Theory]
        [InlineData(-1)]
        public void GetExpression_Logs_ExpectedBehavior(long entityId)
        {
            var model = Resolve<ILogsService>().GetRandom(entityId);
            if (model != null)
            {
                var expression = Lambda.GreaterEqual<Logs>("Id", model.Id);

                Assert.NotNull(expression);
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void Find_ExpectedBehavior(long userId)
        {
            var user = Resolve<IUserService>().GetRandom(userId);
            var dic = new Dictionary<string, string>
            {
                {"Id", $"=={user.Id}"},
                {"userName", $"=={user.UserName}"}
            };

            var newUser = Resolve<IUserService>().Find(dic);
            Assert.NotNull(newUser);
            Assert.Equal(newUser?.Id, user.Id);
        } /*end*/

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(0)]
        public void Find_ExpectedBehavior_WorkOrder(long entityId)
        {
            //var workOrder = Service<IWorkOrderService>().GetRandom(entityId);
            //if (workOrder == null)
            //{
            //    workOrder = new WorkOrder();
            //    Service<IWorkOrderService>().Add(workOrder);
            //    workOrder = Service<IWorkOrderService>().LastOrDefault();
            //}

            //var dic = new Dictionary<string, string>
            //{
            //    {"Id", $"=={workOrder.Id}"}
            //};
            //var newUser = Service<IWorkOrderService>().Find(dic);
            //Assert.NotNull(newUser);
            //Assert.Equal(newUser?.Id, workOrder.Id);
        }

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
            //var newUser = Service<IWorkOrderService>().Find(dic);
            //Assert.NotNull(newUser);
            //Assert.Equal(newUser.Id, workOrder.Id);
        }

        [Fact]
        public void FirstOrDefault()
        {
            var user = Resolve<IUserService>().FirstOrDefault();
            Assert.NotNull(user);
            Assert.True(user.Id > 0);
        }

        [Fact]
        public void FirstOrDefault_Mongodb()
        {
            //var workOrder = Service<IWorkOrderService>().FirstOrDefault();
            //if (workOrder == null)
            //{
            //    workOrder = new WorkOrder();
            //    Service<IWorkOrderService>().Add(workOrder);
            //    workOrder = Service<IWorkOrderService>().FirstOrDefault();
            //}

            //Assert.NotNull(workOrder);
            //Assert.False(workOrder.IsObjectIdEmpty());
        }

        [Fact]
        public void GetRandom()
        {
            var user = Resolve<IUserService>().LastOrDefault();
            var randomUser = Resolve<IUserService>().GetRandom(-1);
            Assert.NotNull(user);
            Assert.NotNull(randomUser);
            Assert.True(user.Id > 0);
            Assert.Equal(randomUser.Id, user.Id);

            user = Resolve<IUserService>().FirstOrDefault();
            randomUser = Resolve<IUserService>().GetRandom(1);
            Assert.NotNull(user);
            Assert.NotNull(randomUser);
            Assert.True(user.Id > 0);
            Assert.Equal(randomUser.Id, user.Id);

            randomUser = Resolve<IUserService>().GetRandom(0);
            Assert.NotNull(randomUser);
        }

        [Fact]
        public void GetRandom_ObjectId()
        {
            var user = Resolve<ILogsService>().LastOrDefault();
            var randomUser = Resolve<ILogsService>().GetRandom(-1);
            Assert.NotNull(user);
            Assert.NotNull(randomUser);
            Assert.Equal(randomUser.Id, user.Id);

            user = Resolve<ILogsService>().FirstOrDefault();
            randomUser = Resolve<ILogsService>().GetRandom(1);
            Assert.NotNull(user);
            Assert.NotNull(randomUser);
            Assert.Equal(randomUser.Id, user.Id);

            randomUser = Resolve<ILogsService>().GetRandom(0);
            Assert.NotNull(randomUser);

            randomUser = Resolve<ILogsService>().GetRandom(2);
            Assert.NotNull(randomUser);

            randomUser = Resolve<ILogsService>().GetRandom(6);
            Assert.NotNull(randomUser);
        }

        [Fact]
        public void LastOrDefault()
        {
            var user = Resolve<IUserService>().LastOrDefault();
            Assert.NotNull(user);
            Assert.True(user.Id > 0);
        }

        [Fact]
        public void LastOrDefault_Mongodb()
        {
            //var workOrder = Service<IWorkOrderService>().LastOrDefault();
            //if (workOrder == null)
            //{
            //    workOrder = new WorkOrder();
            //    Service<IWorkOrderService>().Add(workOrder);
            //    workOrder = Service<IWorkOrderService>().LastOrDefault();
            //}

            //Assert.NotNull(workOrder);
            //Assert.False(workOrder.IsObjectIdEmpty());
        }
    }
}