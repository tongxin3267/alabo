using System;
using System.Linq;
using Xunit;
using ZKCloud.App.Core.Common.Domain.Services;
using ZKCloud.App.Core.Finance.Domain.CallBacks;
using ZKCloud.App.Core.Finance.Domain.Services;
using ZKCloud.App.Core.Finance.ViewModels.Account;
using ZKCloud.App.Core.User.Domain.Entities;
using ZKCloud.App.Core.User.Domain.Repositories;
using ZKCloud.App.Core.User.Domain.Services;
using ZKCloud.Core.Randoms;
using ZKCloud.Domains.Base.Entities;
using ZKCloud.Domains.Base.Services;
using ZKCloud.Domains.Enums;
using ZKCloud.Domains.Repositories.EFCore;
using ZKCloud.Extensions;
using ZKCloud.Helpers;
using ZKCloud.Runtime;
using ZKCloud.Test.Base.Core.Model;
using DateTimeExtensions = ZKCloud.Extensions.DateTimeExtensions;
using Random = System.Random;

namespace ZKCloud.Test.Domain.Repositories.EFCore.Context
{
    public class EfCoreRepositoryContextTests : CoreTest
    {
        /// <summary>
        ///     Sql 多租户测试
        /// </summary>
        [Fact]
        public void CurrentTenantTest()
        {
            // 租户v12_test测试
            RuntimeContext.CurrentTenant = "test";
            var testUser = Resolve<IUserService>().LastOrDefault();
            Assert.NotNull(testUser);
            var testNativeUser = Resolve<IUserService>().GetSingle(testUser.Id);
            Assert.NotNull(testNativeUser);
            Assert.Equal(testUser.Id, testNativeUser.Id);

            // 租户测试
            RuntimeContext.CurrentTenant = "dev";
            var devNativeUser = Resolve<IUserService>().LastOrDefault();
            Assert.NotNull(devNativeUser);
            var devUser = Resolve<IUserService>().GetSingle(devNativeUser.Id);
            Assert.NotNull(devUser);
            Assert.Equal(devUser.Id, devNativeUser.Id);

            Assert.NotEqual(testUser.Id, devUser.Id);
        }

        /// <summary>
        ///     Dapper 多租户测试
        /// </summary>
        [Fact]
        public void CurrentTenantTest_Dapper()
        {
            // 租户v12_test测试
            RuntimeContext.CurrentTenant = "test";

            var sql = "select top 1 * from User_User";
            var user = Ioc.Resolve<IUserRepository>().RepositoryContext.DapperGet<User>(sql);
            Assert.NotNull(user);

            RuntimeContext.CurrentTenant = "test";

            RuntimeContext.CurrentTenant = "dev";
            var devUser = Ioc.Resolve<IUserRepository>().RepositoryContext.DapperGet<User>(sql);
            Assert.NotNull(devUser);

            Assert.NotEqual(devUser.Id, user.Id);
        }

        /// <summary>
        ///     Sql 多租户测试
        /// </summary>
        [Fact]
        public void CurrentTenantTest_Mongodb()
        {
            // 租户v12_test测试
            RuntimeContext.CurrentTenant = "test";
            var logs = new Logs
            {
                Content = "test mongodb",
                Type = "CurrentTenantTest_Mongodb_test"
            };
            Resolve<ILogsService>().Add(logs);
            var lastLog = Resolve<ILogsService>().LastOrDefault();
            Assert.Equal(logs.Id, lastLog.Id);
            var testCount = Resolve<ILogsService>().Count();

            // 租户v12_dev测试
            RuntimeContext.CurrentTenant = "dev";
            logs = new Logs
            {
                Content = "dev mongodb",
                Type = "CurrentTenantTest_Mongodb_dev"
            };
            Resolve<ILogsService>().Add(logs);
            lastLog = Resolve<ILogsService>().LastOrDefault();
            Assert.Equal(logs.Id, lastLog.Id);

            var find = Resolve<ILogsService>().GetSingle(r => r.Type == "CurrentTenantTest_Mongodb_test");
            Assert.Null(find);
        }

        [Fact]
        public void EfCoreRepositoryContext_Tests()
        {
            var user = Resolve<IUserService>().PlanformUser();
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>(r => r.Status == Status.Normal)
                .ToList();

            foreach (var moneyType in moneyTypes)
            {
                var index = new Random().Next(0, moneyTypes.Count);
                var view = new ViewAccount
                {
                    Amount = new Random().Next(100, 1000).ToDecimal(),
                    ActionType = 1
                };
                var result = Resolve<IBillService>().Increase(user, moneyType, view.Amount, view.Remark);
                Assert.True(result.Succeeded);
            }
        }

        /// <summary>
        ///     Efs the core repository context tests 1.
        ///     测试报告：ef 1000条 39秒
        ///     ef扩展 1000条 26s
        ///     /// 测试报告：ef 1000条 39秒
        ///     ef扩展 10000条 4分钟
        /// </summary>
        [Fact]
        public void EfCoreRepositoryContext_Tests_1()
        {
            var user = Resolve<IUserService>().FirstOrDefault();
            var moneyTypes = Resolve<IAutoConfigService>().GetList<MoneyTypeConfig>(r => r.Status == Status.Normal)
                .ToList();
            var count = 1;
            var result = false;
            if (user != null)
            {
                var loop = 1;
                var timespan = DateTimeExtensions.Performance(() =>
                    {
                        while (loop < count)
                        {
                            var logs = new Logs
                            {
                                UserId = 1,
                                UserName = user.GetUserName(),
                                Content = RandomHelper.GenerateSerial()
                            };
                            result = Resolve<ILogsService>().Add(logs);
                            Assert.True(result);
                            loop++;
                        }
                    }
                );
                Console.WriteLine(timespan.Seconds);
            }
        } /*end*/
    }
}