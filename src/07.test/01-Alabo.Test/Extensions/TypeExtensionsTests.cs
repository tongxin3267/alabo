using System;
using System.Collections.Generic;
using Xunit;
using ZKCloud.App.Core.Common.Domain.Entities;
using ZKCloud.App.Core.Themes.DiyModels.Links;
using ZKCloud.Extensions;
using ZKCloud.Test.Base.Core;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Extensions
{
    public class TypeExtensionsTests : CoreTest
    {
        [Theory]
        [InlineData(typeof(Link))]
        [InlineData(typeof(AutoConfig))]
        //[InlineData(typeof(List<Link>))]
        public void GetTypeByFullName_StateUnderTest_ExpectedBehavior(Type type)
        {
            var fullName = type.FullName;
            var findType = fullName.GetTypeByFullName();
            Assert.NotNull(findType);
            Assert.Equal(findType.FullName, type.FullName);
        }

        [Theory]
        [InlineData(
            "System.Collections.Generic.List`1[[ZKCloud.App.Core.Themes.DiyModels.Links.Link, ZKCloud.App.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]",
            typeof(List<Link>))]
        public void GetTypeByName_StateUnderTest_ExpectedBehavior(string fullName, Type type)
        {
            var findType = fullName.GetTypeByFullName();
            Assert.NotNull(findType);
            Assert.Equal(findType.FullName, type.FullName);
        }

        //  [Fact]
        [TestMethod("GetAllPropertys_String")]
        public void GetAllPropertys_String_test()
        {
            var fullName = "";
            var result = TypeExtensions.GetAllPropertys(fullName);
            Assert.NotNull(result);
        }

        //  [Fact]
        [TestMethod("GetClassDescription_String_Boolean")]
        public void GetClassDescription_String_Boolean_test()
        {
            var fullName = "";
            var result = fullName.GetClassDescription();
            Assert.NotNull(result);
        }

        // [Fact]
        [TestMethod("GetEditPropertys_String")]
        public void GetEditPropertys_String_test()
        {
            var fullName = "";
            var result = TypeExtensions.GetEditPropertys(fullName);
            Assert.NotNull(result);
        }

        //  [Fact]
        [TestMethod("GetListPropertys_String")]
        public void GetListPropertys_String_test()
        {
            var fullName = "";
            var result = TypeExtensions.GetListPropertys(fullName);
            Assert.NotNull(result);
        }

        //[Fact]
        [TestMethod("GetPropertiesFromCache_Type")]
        public void GetPropertiesFromCache_Type_test()
        {
            Type type = null;
            var result = type.GetPropertiesFromCache();
            Assert.NotNull(result);
        }

        // [Fact]
        [TestMethod("GetPropertyResultFromCache_Type")]
        public void GetPropertyResultFromCache_Type_test()
        {
            Type type = null;
            var result = type.GetPropertyResultFromCache();
            Assert.NotNull(result);
        }

        //[Fact]
        [TestMethod("GetTypeByFullName_String")]
        public void GetTypeByFullName_String_test()
        {
            var input = "";
            var result = input.GetTypeByFullName();
            Assert.NotNull(result);
        }

        // [Fact]
        [TestMethod("GetTypeByName_String")]
        public void GetTypeByName_String_test()
        {
            var input = "";
            var result = input.GetTypeByName();
            Assert.NotNull(result);
        }

        [Fact]
        public void GetAllPropertys_StateUnderTest_ExpectedBehavior()
        {
            var type = typeof(List<Link>);
        }

        [Fact]
        public void GetClassDescription_StateUnderTest_ExpectedBehavior()
        {
        }

        [Fact]
        public void GetEditPropertys_StateUnderTest_ExpectedBehavior()
        {
        }

        [Fact]
        public void GetListPropertys_StateUnderTest_ExpectedBehavior()
        {
        }

        [Fact]
        public void GetPropertiesFromCache_StateUnderTest_ExpectedBehavior()
        {
        }

        [Fact]
        public void GetPropertyResultFromCache_StateUnderTest_ExpectedBehavior()
        {
            //var fullName = typeof(List<Link>).FullName;
            //var findType = fullName.GetTypeByFullName();
            //Assert.NotNull(findType);
            //Assert.Equal(findType.FullName, typeof(List<Link>).FullName);
        }

        [Fact]
        public void GetType_StateUnderTest_ExpectedBehavior()
        {
        }

        [Fact]
        [TestMethod("SafeValue_Nullable_T")]
        public void SafeValue_Nullable_T_test()
        {
            //T value = null;
            //var result = TypeExtensions.SafeValue(value);
            //Assert.NotNull(result);
        }

        [Fact]
        public void SafeValue_StateUnderTest_ExpectedBehavior()
        {
        }

        /*end*/
    }
}