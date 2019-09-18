using System;
using Xunit;
using ZKCloud.App.Core.Admin.Domain.Services;
using ZKCloud.App.Core.User.Domain.Repositories;
using ZKCloud.App.Core.User.Domain.Services;
using ZKCloud.App.Shop.Order.Domain.Services;
using ZKCloud.Datas.UnitOfWorks;
using ZKCloud.Domains.Base.Services;
using ZKCloud.Domains.Repositories;
using ZKCloud.Domains.Repositories.Model;
using ZKCloud.Helpers;
using ZKCloud.Linq.Dynamic;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Linq.Dynamic
{
    public class DynamicServiceTest : CoreTest
    {
        [Theory]
        [InlineData(typeof(ITypeService))]
        public void ResolveTest(Type type)
        {
            var result = Ioc.ResolveType(type);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("Order")]
        [InlineData("User")]
        public void DynamicService_Resolve(string type)
        {
            var result = DynamicService.Resolve(type);

            Assert.NotNull(result);
        }

        /// <summary>
        ///     所有的实体服务测试，看是否配置正确，非常重要
        /// </summary>
        [Fact]
        public void AllEntityServcie_Test()
        {
            var entitysList = Resolve<ITableService>()
                .GetList(r => r.TableType == TableType.Mongodb || r.TableType == TableType.SqlServer);
            foreach (var item in entitysList)
            {
                // 动态服务测试
                var dynamicService = DynamicService.Resolve(item.Key);
                Assert.NotNull(dynamicService);
            }
        }

        [Fact]
        public void DynamicService_Test()
        {
            var result = DynamicService.ResolveMethod("AutoConfigService", "GetAllTypes");
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAllAutoConfig()
        {
            var result = EntityDynamicService.GetAllAutoConfig();
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAllEnum()
        {
            var result = EntityDynamicService.GetAllEnum();
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAllRelationTypes()
        {
            var result = EntityDynamicService.GetAllRelationTypes();
            Assert.NotNull(result);
        }

        [Fact]
        public void GetSingleOrder()
        {
            var order = Resolve<IOrderService>().FirstOrDefault();
            var result = EntityDynamicService.GetSingleOrder(order.Id);
            Assert.NotNull(result);

            Assert.Equal(result, order);
        }

        [Fact]
        public void GetSingleUser()
        {
            var user = Resolve<IUserService>().FirstOrDefault();
            var result = EntityDynamicService.GetSingleUser(user.Id);
            Assert.NotNull(result);

            Assert.Equal(result, user);
        }

        [Fact]
        public void GetSingleUser_UserName()
        {
            var user = Resolve<IUserService>().FirstOrDefault();
            var result = EntityDynamicService.GetSingleUser(user.UserName);
            Assert.NotNull(result);

            Assert.Equal(result, user);
        }

        [Fact]
        public void GetSqlTable()
        {
            var result = EntityDynamicService.GetSqlTable();
            Assert.NotNull(result);
        }

        [Fact]
        public void IsAdmin()
        {
            var user = Resolve<IUserService>().FirstOrDefault();
            var result = EntityDynamicService.IsAdmin(user.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public void UserService_Test()
        {
            var unitOfWork = Ioc.Resolve<IUnitOfWork>();
            var userRepository = new UserRepository(unitOfWork);
            var repository = Ioc.Resolve<IRepository>();
            var userService = new UserService(unitOfWork, userRepository);
            var user = userService.FirstOrDefault();
            Assert.NotNull(user);
        }
    }
}