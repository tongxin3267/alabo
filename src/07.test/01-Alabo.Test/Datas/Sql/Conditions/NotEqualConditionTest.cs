using Xunit;
using ZKCloud.Datas.Sql.Queries.Builders.Conditions;

namespace ZKCloud.Test.Datas.Sql.Conditions
{
    /// <summary>
    ///     Sql不相等查询条件测试
    /// </summary>
    public class NotEqualConditionTest
    {
        /// <summary>
        ///     获取条件
        /// </summary>
        [Fact]
        public void Test()
        {
            var condition = new NotEqualCondition("Name", "@Name");
            Assert.Equal("Name<>@Name", condition.GetCondition());
        }
    }
}