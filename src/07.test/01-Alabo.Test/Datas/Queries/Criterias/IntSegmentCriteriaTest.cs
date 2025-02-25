﻿using Xunit;
using ZKCloud.Datas.Queries.Criterias;
using ZKCloud.Datas.Queries.Enums;
using ZKCloud.Test.Samples;

namespace ZKCloud.Test.Datas.Queries.Criterias
{
    /// <summary>
    ///     测试整数范围过滤条件
    /// </summary>
    public class IntSegmentCriteriaTest
    {
        /// <summary>
        ///     测试获取查询条件 - 最大值和最小值均为null,忽略所有条件
        /// </summary>
        [Fact]
        public void Test_BothNull()
        {
            var criteria = new IntSegmentCriteria<AggregateRootSample, int>(t => t.Tel, null, null);
            Assert.Null(criteria.GetPredicate());

            var criteria2 = new IntSegmentCriteria<AggregateRootSample, int?>(t => t.Age, null, null);
            Assert.Null(criteria2.GetPredicate());
        }

        /// <summary>
        ///     测试获取查询条件
        /// </summary>
        [Fact]
        public void TestGetPredicate()
        {
            var criteria = new IntSegmentCriteria<AggregateRootSample, int>(t => t.Tel, 1, 10);
            Assert.Equal("t => ((t.Tel >= 1) AndAlso (t.Tel <= 10))", criteria.GetPredicate().ToString());

            var criteria2 = new IntSegmentCriteria<AggregateRootSample, int?>(t => t.Age, 1, 10);
            Assert.Equal("t => ((t.Age >= 1) AndAlso (t.Age <= 10))", criteria2.GetPredicate().ToString());
        }

        /// <summary>
        ///     测试获取查询条件 - 设置边界
        /// </summary>
        [Fact]
        public void TestGetPredicate_Boundary()
        {
            var criteria = new IntSegmentCriteria<AggregateRootSample, int>(t => t.Tel, 1, 10, Boundary.Neither);
            Assert.Equal("t => ((t.Tel > 1) AndAlso (t.Tel < 10))", criteria.GetPredicate().ToString());

            criteria = new IntSegmentCriteria<AggregateRootSample, int>(t => t.Tel, 1, 10, Boundary.Left);
            Assert.Equal("t => ((t.Tel >= 1) AndAlso (t.Tel < 10))", criteria.GetPredicate().ToString());

            var criteria2 = new IntSegmentCriteria<AggregateRootSample, int?>(t => t.Age, 1, 10, Boundary.Right);
            Assert.Equal("t => ((t.Age > 1) AndAlso (t.Age <= 10))", criteria2.GetPredicate().ToString());

            criteria2 = new IntSegmentCriteria<AggregateRootSample, int?>(t => t.Age, 1, 10, Boundary.Both);
            Assert.Equal("t => ((t.Age >= 1) AndAlso (t.Age <= 10))", criteria2.GetPredicate().ToString());
        }

        /// <summary>
        ///     测试获取查询条件 - 最大值为空，忽略最大值条件
        /// </summary>
        [Fact]
        public void TestGetPredicate_MaxIsNull()
        {
            var criteria = new IntSegmentCriteria<AggregateRootSample, int>(t => t.Tel, 1, null);
            Assert.Equal("t => (t.Tel >= 1)", criteria.GetPredicate().ToString());

            var criteria2 = new IntSegmentCriteria<AggregateRootSample, int?>(t => t.Age, 1, null);
            Assert.Equal("t => (t.Age >= 1)", criteria2.GetPredicate().ToString());
        }

        /// <summary>
        ///     测试获取查询条件 - 最小值大于最大值，则交换大小值的位置
        /// </summary>
        [Fact]
        public void TestGetPredicate_MinGreaterMax()
        {
            var criteria = new IntSegmentCriteria<AggregateRootSample, int>(t => t.Tel, 10, 1);
            Assert.Equal("t => ((t.Tel >= 1) AndAlso (t.Tel <= 10))", criteria.GetPredicate().ToString());

            var criteria2 = new IntSegmentCriteria<AggregateRootSample, int?>(t => t.Age, 10, 1);
            Assert.Equal("t => ((t.Age >= 1) AndAlso (t.Age <= 10))", criteria2.GetPredicate().ToString());
        }

        /// <summary>
        ///     测试获取查询条件 - 最小值为空，忽略最小值条件
        /// </summary>
        [Fact]
        public void TestGetPredicate_MinIsNull()
        {
            var criteria = new IntSegmentCriteria<AggregateRootSample, int>(t => t.Tel, null, 10);
            Assert.Equal("t => (t.Tel <= 10)", criteria.GetPredicate().ToString());

            var criteria2 = new IntSegmentCriteria<AggregateRootSample, int?>(t => t.Age, null, 10);
            Assert.Equal("t => (t.Age <= 10)", criteria2.GetPredicate().ToString());
        }
    }
}