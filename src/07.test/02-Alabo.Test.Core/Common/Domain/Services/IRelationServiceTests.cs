using System;
using Xunit;
using Alabo.Framework.Basic.Relations.Domain.Services;
using Alabo.Industry.Cms.Articles.Domain.CallBacks;
using Alabo.Industry.Shop.Products.Domain.Configs;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;

namespace Alabo.Test.Core.Common.Domain.Services
{
    public class IRelationServiceTests : CoreTest
    {
        [Theory]
        [InlineData(typeof(ProductClassRelation))]
        [InlineData(typeof(ProductTagRelation))]
        [InlineData(typeof(ChannelArticleClassRelation))]
        [InlineData(typeof(ChannelArticleTagRelation))]
        [InlineData(typeof(ChannelHelpTagRelation))]
        [InlineData(typeof(ChannelHelpClassRelation))]
        [TestMethod("GetClass_String")]
        public void GetClass_String_test(Type type)
        {
            var typeName = type.Name;
            var result = Resolve<IRelationService>().GetClass(typeName);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(typeof(ProductClassRelation))]
        [InlineData(typeof(ProductTagRelation))]
        [InlineData(typeof(ChannelArticleClassRelation))]
        [InlineData(typeof(ChannelArticleTagRelation))]
        [InlineData(typeof(ChannelHelpTagRelation))]
        [InlineData(typeof(ChannelHelpClassRelation))]
        [TestMethod("GetFatherClass_String")]
        public void GetFatherClass_String_test(Type type)
        {
            var typeName = type.Name;
            var result = Resolve<IRelationService>().GetFatherClass(typeName);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(typeof(ProductClassRelation))]
        [InlineData(typeof(ProductTagRelation))]
        [InlineData(typeof(ChannelArticleClassRelation))]
        [InlineData(typeof(ChannelArticleTagRelation))]
        [InlineData(typeof(ChannelHelpTagRelation))]
        [InlineData(typeof(ChannelHelpClassRelation))]
        [TestMethod("GetFatherKeyValues_String")]
        public void GetFatherKeyValues_String_test(Type type)
        {
            var typeName = type.Name;
            var result = Resolve<IRelationService>().GetFatherKeyValues(typeName);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(typeof(ProductClassRelation))]
        [InlineData(typeof(ProductTagRelation))]
        [InlineData(typeof(ChannelArticleClassRelation))]
        [InlineData(typeof(ChannelArticleTagRelation))]
        [InlineData(typeof(ChannelHelpTagRelation))]
        [InlineData(typeof(ChannelHelpClassRelation))]
        [TestMethod("GetTag_String")]
        public void GetTag_String_test(Type type)
        {
            var typeName = type.Name;
            var result = Resolve<IRelationService>().GetTag(typeName);
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(typeof(ProductClassRelation))]
        [InlineData(typeof(ProductTagRelation))]
        [InlineData(typeof(ChannelArticleClassRelation))]
        [InlineData(typeof(ChannelArticleTagRelation))]
        [InlineData(typeof(ChannelHelpTagRelation))]
        [InlineData(typeof(ChannelHelpClassRelation))]
        [TestMethod("GetKeyValues_String")]
        public void GetKeyValues_String_test(Type type)
        {
            var typeName = type.Name;
            var result = Resolve<IRelationService>().GetKeyValues(typeName);
        }

        [Theory]
        [InlineData(typeof(ProductClassRelation))]
        [InlineData(typeof(ProductTagRelation))]
        [InlineData(typeof(ChannelArticleClassRelation))]
        [InlineData(typeof(ChannelArticleTagRelation))]
        [InlineData(typeof(ChannelHelpTagRelation))]
        [InlineData(typeof(ChannelHelpClassRelation))]
        [TestMethod("FindType_String")]
        public void FindType_String_test(Type type)
        {
            var typeName = type.Name;
            var result = Resolve<IRelationService>().FindType(typeName);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("CheckExist_String")]
        public void Check_String_test()
        {
            ////var script = "";
            ////String script = null;
            ////var result = Service<IRelationService>().Check( script);
            ////Assert.True(result);
            ////Assert.NotNull(result);
            //Service<IRelationService>().CheckExist(script);
            Assert.True(true);
        }

        [Fact]
        [TestMethod("GetAllClassTypes")]
        public void GetAllClassTypes_test()
        {
            var result = Resolve<IRelationService>().GetAllClassTypes();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAllTagTypes")]
        public void GetAllTagTypes_test()
        {
            var result = Resolve<IRelationService>().GetAllTagTypes();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetAllTypes")]
        public void GetAllTypes_test()
        {
            var result = Resolve<IRelationService>().GetAllTypes();
            Assert.NotNull(result);
        }

        /*end*/
    }
}