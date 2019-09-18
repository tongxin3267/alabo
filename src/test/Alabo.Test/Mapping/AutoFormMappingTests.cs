using System;
using Xunit;
using ZKCloud.App.Core.Finance.Domain.Dtos.Recharge;
using ZKCloud.App.Core.User.Domain.Dtos;
using ZKCloud.Extensions;
using ZKCloud.Mapping;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Mapping
{
    public class AutoFormMappingTests : CoreTest
    {
        [Fact]
        public void ConvertTest()
        {
            var regAutoForm = AutoFormMapping.Convert<RegInput>();
            var classDescription = typeof(RegInput).FullName.GetClassDescription();
            var content = regAutoForm.ToJson();

            Assert.True(regAutoForm.Name == classDescription.ClassPropertyAttribute.Name);
            Assert.NotNull(regAutoForm);
        }

        [Fact]
        public void ConvertTest_RechargeAddInput()
        {
            var regAutoForm = AutoFormMapping.Convert<RechargeAddInput>();
            var classDescription = typeof(RechargeAddInput).FullName.GetClassDescription();
            var content = regAutoForm.ToJson();
            Assert.True(content.Contains("Api/Common/GetKeyValuesByEnum?type",
                StringComparison.CurrentCultureIgnoreCase));

            Assert.True(regAutoForm.Name == classDescription.ClassPropertyAttribute.Name);
            Assert.NotNull(regAutoForm);
        }

        [Fact]
        public void ConvertTest_Reg()
        {
            var regAutoForm = AutoFormMapping.Convert<RegInput>();
            var classDescription = typeof(RegInput).FullName.GetClassDescription();
            var content = regAutoForm.ToJson();

            Assert.True(regAutoForm.Name == classDescription.ClassPropertyAttribute.Name);
            Assert.NotNull(regAutoForm);
        } /*end*/
    }
}