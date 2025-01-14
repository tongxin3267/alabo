using Alabo.Framework.Core.Enums.Enum;
using Alabo.Industry.Cms.Articles.Domain.Dto;
using Alabo.Industry.Cms.Articles.Domain.Services;
using Alabo.Test.Base.Core;
using Alabo.Test.Base.Core.Model;
using Xunit;

namespace Alabo.Test.Cms.Articles.Domain.Services
{
    public class IArticleServiceTests : CoreTest
    {
        [Theory]
        [InlineData(2)]
        [InlineData(21)]
        [InlineData(1)]
        [InlineData(-1)]
        [TestMethod("GetArticle_String")]
        public void GetArticle_String_test(long entityId)
        {
            var find = Resolve<IArticleAdminService>().GetRandom(entityId);
            if (find != null)
            {
                var result = Resolve<IArticleAdminService>().GetViewArticle(find.Id);
                Assert.NotNull(result);
                Assert.Equal(result.Id, find.Id);
            }
        }

        /*end*/

        [Fact]
        [TestMethod("AddOrUpdate_Article_HttpRequest")]
        public void AddOrUpdate_Article_HttpRequest_test()
        {
            //Article model = null;
            //HttpRequest request = null;
            //var result = Service<IArticleService>().AddOrUpdate( model, request);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("Delete_String")]
        public void Delete_String_test()
        {
            var id = "";
            var result = Resolve<IArticleAdminService>().Delete(id);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetArticleList_ArticleInput")]
        public void GetArticleList_ArticleInput_test()
        {
            var articleInput = new ArticleInput();
            var result = Resolve<IArticleService>().GetArticleList(articleInput);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetArticleList")]
        public void GetArticleList_test()
        {
            var result = Resolve<IArticleAdminService>().GetArticleList();
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetArticleListByChannelType_ChannelType_Int32_Int32")]
        public void GetArticleListByChannelType_ChannelType_Int32_Int32_test()
        {
            var type = (ChannelType) 0;
            var pageSize = 0;
            var pageIndex = 0;
            var result = Resolve<IArticleAdminService>().GetArticleListByChannelType(type, pageSize, pageIndex);
            Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetMaxRelationId")]
        public void GetMaxRelationId_test()
        {
            var result = Resolve<IArticleAdminService>().GetMaxRelationId();
            Assert.True(result > 0);
        }

        [Fact]
        [TestMethod("GetPageList_Object")]
        public void GetPageList_Object_test()
        {
            //object query = null;
            //var result = Service<IArticleAdminService>().GetPageList(query);
            //Assert.NotNull(result);
        }

        [Fact]
        [TestMethod("GetSingle_ObjectId")]
        public void GetSingle_ObjectId_test()
        {
            //ObjectId id = ObjectId.Empty;
            //var result = Service<IArticleService>().GetSingle( id);
            //Assert.NotNull(result);
        }

        /*end*/
    }
}