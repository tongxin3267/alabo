﻿using Xunit;
using ZKCloud.Datas.Queries.Criterias;
using ZKCloud.Datas.Queries.Enums;
using ZKCloud.Test.Samples;

namespace ZKCloud.Test.Datas.Queries.Criterias
{
    /// <summary>
    ///     测试double范围过滤条件
    /// </summary>
    public class DoubleSegmentCriteriaTest
    {
        /// <summary>
        ///     测试获取查询条件
        /// </summary>
        [Fact]
        public void TestGetPredicate()
        {
            var criteria = new DoubleSegmentCriteria<AggregateRootSample, double>(t => t.DoubleValue, 1.1, 10.1);
            Assert.Equal("t => ((t.DoubleValue >= 1.1) AndAlso (t.DoubleValue <= 10.1))",
                criteria.GetPredicate().ToString());

            var criteria2 =
                new DoubleSegmentCriteria<AggregateRootSample, double?>(t => t.NullableDoubleValue, 1.1, 10.1);
            Assert.Equal("t => ((t.NullableDoubleValue >= 1.1) AndAlso (t.NullableDoubleValue <= 10.1))",
                criteria2.GetPredicate().ToString());
        }

        /// <summary>
        ///     测试获取查询条件 - 最大值和最小值均为null,忽略所有条件
        /// </summary>
        [Fact]
        public void TestGetPredicate_BothNull()
        {
            var criteria = new DoubleSegmentCriteria<AggregateRootSample, double>(t => t.DoubleValue, null, null);
            Assert.Null(criteria.GetPredicate());

            var criteria2 =
                new DoubleSegmentCriteria<AggregateRootSample, double?>(t => t.NullableDoubleValue, null, null);
            Assert.Null(criteria2.GetPredicate());
        }

        /// <summary>
        ///     测试获取查询条件 - 设置边界
        /// </summary>
        [Fact]
        public void TestGetPredicate_Boundary()
        {
            var criteria =
                new DoubleSegmentCriteria<AggregateRootSample, double>(t => t.DoubleValue, 1.1, 10.1, Boundary.Neither);
            Assert.Equal("t => ((t.DoubleValue > 1.1) AndAlso (t.DoubleValue < 10.1))",
                criteria.GetPredicate().ToString());

            criteria = new DoubleSegmentCriteria<AggregateRootSample, double>(t => t.DoubleValue, 1.1, 10.1,
                Boundary.Left);
            Assert.Equal("t => ((t.DoubleValue >= 1.1) AndAlso (t.DoubleValue < 10.1))",
                criteria.GetPredicate().ToString());

            var criteria2 =
                new DoubleSegmentCriteria<AggregateRootSample, double?>(t => t.NullableDoubleValue, 1.1, 10.1,
                    Boundary.Right);
            Assert.Equal("t => ((t.NullableDoubleValue > 1.1) AndAlso (t.NullableDoubleValue <= 10.1))",
                criteria2.GetPredicate().ToString());

            criteria2 = new DoubleSegmentCriteria<AggregateRootSample, double?>(t => t.NullableDoubleValue, 1.1, 10.1,
                Boundary.Both);
            Assert.Equal("t => ((t.NullableDoubleValue >= 1.1) AndAlso (t.NullableDoubleValue <= 10.1))",
                criteria2.GetPredicate().ToString());
        }

        /// <summary>
        ///     测试获取查询条件 - 最大值为空，忽略最大值条件
        /// </summary>
        [Fact]
        public void TestGetPredicate_MaxIsNull()
        {
            var criteria = new DoubleSegmentCriteria<AggregateRootSample, double>(t => t.DoubleValue, 1.1, null);
            Assert.Equal("t => (t.DoubleValue >= 1.1)", criteria.GetPredicate().ToString());

            var criteria2 =
                new DoubleSegmentCriteria<AggregateRootSample, double?>(t => t.NullableDoubleValue, 1.1, null);
            Assert.Equal("t => (t.NullableDoubleValue >= 1.1)", criteria2.GetPredicate().ToString());
        }

        ///// <summary>
        ///// 测试获取查询条件 - 属性为int类型，则发生类型不匹配异常
        ///// </summary>
        //[Fact]
        //public void TestGetPredicate_ValidateType() {
        //    Assert.Throws<ArgumentException>(() => {
        //        var criteria = new DoubleSegmentCriteria<AggregateRootSample, int?>(t => t.Age, 1.1, 10.1);
        //        var result = criteria.GetPredicate();
        //    });
        //}

        /// <summary>
        ///     测试获取查询条件 - 最小值大于最大值，则交换大小值的位置
        /// </summary>
        [Fact]
        public void TestGetPredicate_MinGreaterMax()
        {
            var criteria = new DoubleSegmentCriteria<AggregateRootSample, double>(t => t.DoubleValue, 10.1, 1.1);
            Assert.Equal("t => ((t.DoubleValue >= 1.1) AndAlso (t.DoubleValue <= 10.1))",
                criteria.GetPredicate().ToString());

            var criteria2 =
                new DoubleSegmentCriteria<AggregateRootSample, double?>(t => t.NullableDoubleValue, 10.1, 1.1);
            Assert.Equal("t => ((t.NullableDoubleValue >= 1.1) AndAlso (t.NullableDoubleValue <= 10.1))",
                criteria2.GetPredicate().ToString());
        }

        /// <summary>
        ///     测试获取查询条件 - 最小值为空，忽略最小值条件
        /// </summary>
        [Fact]
        public void TestGetPredicate_MinIsNull()
        {
            var criteria = new DoubleSegmentCriteria<AggregateRootSample, double>(t => t.DoubleValue, null, 10.1);
            Assert.Equal("t => (t.DoubleValue <= 10.1)", criteria.GetPredicate().ToString());

            var criteria2 =
                new DoubleSegmentCriteria<AggregateRootSample, double?>(t => t.NullableDoubleValue, null, 10.1);
            Assert.Equal("t => (t.NullableDoubleValue <= 10.1)", criteria2.GetPredicate().ToString());
        }
    }
}