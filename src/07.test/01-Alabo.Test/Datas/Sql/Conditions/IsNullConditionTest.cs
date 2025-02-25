﻿using Xunit;
using ZKCloud.Datas.Sql.Queries.Builders.Conditions;

namespace ZKCloud.Test.Datas.Sql.Conditions
{
    /// <summary>
    ///     Is Null查询条件测试
    /// </summary>
    public class IsNullConditionTest
    {
        /// <summary>
        ///     获取条件
        /// </summary>
        [Fact]
        public void Test_1()
        {
            var condition = new IsNullCondition("Email");
            Assert.Equal("Email Is Null", condition.GetCondition());
        }

        /// <summary>
        ///     获取条件 - 验证列为空
        /// </summary>
        [Fact]
        public void Test_2()
        {
            var condition = new IsNullCondition("");
            Assert.Null(condition.GetCondition());
        }
    }
}