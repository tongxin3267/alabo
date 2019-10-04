using System;
using System.Collections.Generic;
using System.Text;
using Alabo.Dynamics;
using Alabo.Framework.Basic.PostRoles.Dtos;
using Alabo.Framework.Themes.Domain.Entities;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Dynamics
{
    public class ServiceInterpreterTest : CoreTest
    {
        [Fact]
        public void EvalTest() {
            var result = ServiceInterpreter.Eval<List<Theme>>("ThemeService", "GetList");
            Assert.NotNull(result);
        }
    }
}