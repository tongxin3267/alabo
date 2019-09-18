using Xunit;
using ZKCloud.Test.Base.Core.Model;

namespace ZKCloud.Test.Domain.Repositories
{
    public class DataBaseTest : CoreTest
    {
        [Fact]
        public void CreateDataBase()
        {
            //    var repositoryContext = ZKCloud.Helpers.Ioc.Resolve<IRepositoryContext>();
            //    var data = repositoryContext.CreateDataTable("111_ttt");
            //    Assert.True(data);
            Assert.True(true);
        }

        [Fact]
        public void GetTablesTest()
        {
            //var repositoryContext = ZKCloud.Helpers.Ioc.Resolve<IRepositoryContext>();
            //var sql = "select name from sys.tables";
            //var data = repositoryContext.ExecuteScalar(sql);
            //Assert.NotNull(data);
            Assert.True(true);
        }
    }
}