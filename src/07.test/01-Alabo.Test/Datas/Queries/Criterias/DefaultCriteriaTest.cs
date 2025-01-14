﻿using Xunit;
using ZKCloud.Datas.Queries.Criterias;
using ZKCloud.Test.Samples;

namespace ZKCloud.Test.Datas.Queries.Criterias
{
    /// <summary>
    ///     测试默认查询条件
    /// </summary>
    public class DefaultCriteriaTest
    {
        /// <summary>
        ///     测试获取查询条件
        /// </summary>
        [Fact]
        public void TestGetPredicate()
        {
            var criteria = new DefaultCriteria<AggregateRootSample>(t => t.Name == "a");
            Assert.Equal("t => (t.Name == \"a\")", criteria.GetPredicate().ToString());
        }
    }
}