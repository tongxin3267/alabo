using Xunit;
using ZKCloud.Datas.Queries.Criterias;
using ZKCloud.Test.Samples;

namespace ZKCloud.Test.Datas.Queries.Criterias
{
    /// <summary>
    ///     测试与查询条件
    /// </summary>
    public class AndCriteriaTest
    {
        /// <summary>
        ///     测试获取查询条件
        /// </summary>
        [Fact]
        public void TestGetPredicate()
        {
            var criteria = new AndCriteria<AggregateRootSample>(t => t.Name == "a", t => t.Name != "b");

            Assert.Equal("t => ((t.Name == \"a\") AndAlso (t.Name != \"b\"))", criteria.GetPredicate().ToString());
        }
    }
}