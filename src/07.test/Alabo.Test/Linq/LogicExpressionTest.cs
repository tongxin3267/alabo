using System.Linq;
using Xunit;
using ZKCloud.Linq;

namespace ZKCloud.Test.Linq
{
    public class LogicExpressionTest
    {
        [Theory]
        [InlineData("false && true", false)]
        [InlineData("true && true", true)]
        [InlineData("true && true||false", true)]
        [InlineData("true", true)]
        [InlineData("(true)", true)]
        [InlineData("false", false)]
        [InlineData("(false)", false)]
        [InlineData("((true && !false) || false) && true || false ", true)]
        [InlineData("(false && true)", false)]
        public void OperateTest(string logicExpression, bool result)
        {
            var logicResult = LogicExpression.Operate(logicExpression);
            Assert.Equal(logicResult, result);
        }

        [Fact]
        public void OperateTest_2()
        {
            var arrLogicValue = new bool[6] {true, false, false, true, false, true};


            //假设所有bool值都用一个字母表示，也可以使用多个字母，但是需要统一字母的数量
            var logicExpression = " ((AA && !BB)|| CC) && DD ||(EE && FF)";
            var logicResult = LogicExpression.Operate(logicExpression, arrLogicValue.ToList());
            Assert.Equal(logicResult, true);
        }
    }
}