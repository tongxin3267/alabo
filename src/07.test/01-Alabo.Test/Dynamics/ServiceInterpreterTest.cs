using System;
using System.Collections.Generic;
using System.Text;
using Alabo.Data.People.Users.Domain.Services;
using Alabo.Dynamics;
using Alabo.Framework.Basic.PostRoles.Dtos;
using Alabo.Framework.Themes.Domain.Entities;
using Alabo.Linq.Dynamic;
using Alabo.Test.Base.Core.Model;
using Alabo.Users.Entities;
using Xunit;
using ZKCloud.Open.DynamicExpression;
using Parameter = AspectCore.DynamicProxy.Parameters.Parameter;

namespace Alabo.Test.Dynamics
{
    public class ServiceInterpreterTest : CoreTest
    {
        [Fact]
        public void EvalTest() {
            var defaultUser = Resolve<IUserService>().PlatformUser();
            var result = ServiceInterpreter.Eval<User>("User", "PlatformUser");
            Assert.NotNull(result);
            Assert.Equal(defaultUser.Id, result.Id);
        }

        [Fact]
        public void EvalTest_1() {
            var userService = DynamicService.Resolve("User");

            var target = new Interpreter().SetVariable("userService", userService);

            var user = (User)target.Eval("userService.PlatformUser()");

            Assert.NotNull(user);
        }

        [Fact]
        public void EvalTest_2() {
            var defaultUser = Resolve<IUserService>().GetRandom(1);
            var result = ServiceInterpreter.Eval<User>("User", "GetSingle", defaultUser.Id);
            Assert.NotNull(result);
            Assert.Equal(defaultUser.Id, result.Id);
        }
    }
}