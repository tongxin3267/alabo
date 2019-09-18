using Xunit;
using ZKCloud.Datas.Queries.Criterias;
using ZKCloud.Test.Samples;

namespace ZKCloud.Test.Datas.Queries.Criterias
{
    /// <summary>
    ///     测试或查询条件
    /// </summary>
    public class OrCriteriaTest
    {
        /// <summary>
        ///     测试获取查询条件
        /// </summary>
        [Fact]
        public void TestGetPredicate()
        {
            var criteria = new OrCriteria<AggregateRootSample>(t => t.Name == "a", t => t.Name != "b");
            Assert.Equal("t => ((t.Name == \"a\") OrElse (t.Name != \"b\"))", criteria.GetPredicate().ToString());
        }
    }
}