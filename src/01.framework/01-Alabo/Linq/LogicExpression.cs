using System;
using System.Collections.Generic;

namespace Alabo.Linq
{
    /// <summary>
    ///     逻辑运算符
    /// </summary>
    public static class LogicExpression
    {
        /// <summary>
        ///     逻辑计算 支持：括号、&& 、||
        ///     假设所有bool值都用一个字母表示，也可以使用多个字母，但是需要统一字母的数量
        /// </summary>
        /// <param name="logicExpression">示范值：((AA && !BB)|| CC) && DD ||(EE && FF)</param>
        /// <param name="logicValueBool">示范值：{true, false, false, true, false, true}</param>
        public static bool Operate(string logicExpression, List<bool> logicValueBool)
        {
            //去除空格,括号
            var logicExpressionTemp = logicExpression.Replace(" ", "");
            logicExpressionTemp = logicExpressionTemp.Replace("(", "");
            logicExpressionTemp = logicExpressionTemp.Replace(")", "");

            //将逻辑运算符用逗号替换
            var logicExpressionVariable = logicExpressionTemp.Replace("&&", ",");
            logicExpressionVariable = logicExpressionVariable.Replace("||", ",");

            //将逻辑表达式中的所有条件名称取出
            var arrVariable = logicExpressionVariable.Split(',');

            //条件名称替换成其对应的bool值
            for (var i = 0; i < arrVariable.Length; i++)
                if (arrVariable[i].Contains("!"))
                    logicExpression = logicExpression.Replace(arrVariable[i], (!logicValueBool[i]).ToString());
                else
                    logicExpression = logicExpression.Replace(arrVariable[i], logicValueBool[i].ToString());

            return Operate(logicExpression);
        }

        /// <summary>
        ///     逻辑计算 支持：括号、&& 、||
        /// </summary>
        /// <param name="logicExpression">示范：((true && !false) || false) && true || false </param>
        public static bool Operate(string logicExpression)
        {
            var finnalResult = DealBrackets(logicExpression);
            return finnalResult;
        }

        /// <summary>
        ///     处理括号
        /// </summary>
        /// <param name="logicExpression"></param>
        private static bool DealBrackets(string logicExpression)
        {
            while (logicExpression.Contains("("))
            {
                //最后一个左括号
                var lasttLeftBracketIndex = -1;
                //与最后第一个左括号对应的右括号
                var firstRightBracketIndex = -1;

                //找到最后一个左括号
                for (var i = 0; i < logicExpression.Length; i++)
                {
                    //获取字符串中的第i个字符
                    var tempChar = logicExpression.Substring(i, 1);
                    //如果是左括号，则将该字符的索引号给lasttLeftBracketIndex，直到最后一个
                    if (tempChar == "(") lasttLeftBracketIndex = i;
                }

                //找到与最后第一个左括号对应的右括号
                for (var i = lasttLeftBracketIndex; i < logicExpression.Length; i++)
                {
                    //获取字符串中的第i个字符
                    var tempChar = logicExpression.Substring(i, 1);
                    if (tempChar == ")" && firstRightBracketIndex == -1) firstRightBracketIndex = i;
                }

                var calculateExpression = logicExpression.Substring(lasttLeftBracketIndex + 1,
                    firstRightBracketIndex - lasttLeftBracketIndex - 1);
                var logicResult = LogicOperate(calculateExpression);
                logicExpression = logicExpression.Replace("(" + calculateExpression + ")", logicResult.ToString());
            }

            var finnalResult = LogicOperate(logicExpression);
            return finnalResult;
        }

        /// <summary>
        ///     运算逻辑表达式
        /// </summary>
        /// <param name="logicExpression"></param>
        private static bool LogicOperate(string logicExpression)
        {
            //去除空格
            logicExpression = logicExpression.Replace(" ", "");

            //获取所有的条件的Bool值
            var logicExpressionValue = logicExpression.Replace("&&", ",");
            logicExpressionValue = logicExpressionValue.Replace("||", ",");
            var arrLogicValue = logicExpressionValue.Split(',');

            //获取表达式的逻辑运算符
            var logicExpressionOperator = logicExpression;
            logicExpressionOperator = logicExpressionOperator.Replace("True", ",");
            logicExpressionOperator = logicExpressionOperator.Replace("False", ",");
            logicExpressionOperator = logicExpressionOperator.Remove(0, 1);
            if (logicExpressionOperator.Length > 0)
                logicExpressionOperator = logicExpressionOperator.Remove(logicExpressionOperator.Length - 1, 1);

            var arrOperator = logicExpressionOperator.Split(',');

            //最终运算结果
            var logicResult = Convert.ToBoolean(arrLogicValue[0]);

            //更具逻辑运算符的数量通过循环进行运算（不包含"!"）
            for (var i = 0; i < arrOperator.Length; i++)
                if (arrOperator[i] == "&&")
                    logicResult = logicResult && Convert.ToBoolean(arrLogicValue[i + 1]);
                else if (arrOperator[i] == "||") logicResult = logicResult || Convert.ToBoolean(arrLogicValue[i + 1]);

            return logicResult;
        }
    }
}