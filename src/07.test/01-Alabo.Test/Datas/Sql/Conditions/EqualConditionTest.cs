using Xunit;
using ZKCloud.Datas.Sql.Queries.Builders.Conditions;

namespace ZKCloud.Test.Datas.Sql.Conditions
{
    /// <summary>
    ///     Sql相等查询条件测试
    /// </summary>
    public class EqualConditionTest
    {
        /// <summary>
        ///     获取条件
        /// </summary>
        [Fact]
        public void Test()
        {
            var condition = new EqualCondition("Name", "@Name");
            Assert.Equal("Name=@Name", condition.GetCondition());
        }
    }
}