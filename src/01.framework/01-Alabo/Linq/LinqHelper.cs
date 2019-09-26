using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Alabo.Datas.Queries.Enums;
using Alabo.Domains.Entities.Core;
using Alabo.Domains.Query;
using Alabo.Extensions;
using ZKCloud.Open.DynamicExpression;

namespace Alabo.Linq
{
    /// <summary>
    ///     查询工具类
    /// </summary>
    public static class LinqHelper
    {
        /// <summary>
        ///     根据操作符比较值得大小
        /// </summary>
        /// <param name="operatorCompare">比较</param>
        /// <param name="baseValue">基准值</param>
        /// <param name="compareValue">比较值</param>
        public static bool CompareByOperator(OperatorCompare operatorCompare, decimal baseValue, decimal compareValue)
        {
            switch (operatorCompare)
            {
                case OperatorCompare.Equal:
                    return baseValue == compareValue;

                case OperatorCompare.Greater:
                    return compareValue > baseValue;

                case OperatorCompare.GreaterEqual:
                    return compareValue >= baseValue;

                case OperatorCompare.Less:
                    return compareValue < baseValue;

                case OperatorCompare.LessEqual:
                    return compareValue <= baseValue;

                case OperatorCompare.NotEqual:
                    return compareValue != baseValue;

                default:
                    return false;
            }
        }

        /// <summary>
        ///     URL参数转换为 Linq表单式
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="dictionary">字典数据</param>
        public static IExpressionQuery<TEntity> DictionaryToLinq<TEntity>(Dictionary<string, string> dictionary)
            where TEntity : class, IEntity
        {
            var query = new ExpressionQuery<TEntity>();
            foreach (var dic in dictionary)
            {
                var key = dic.Key;
                if (dic.Key == "loginUserId") key = "UserId";

                if (key == "dataId" || key == "widgetId" || key == "sign") continue;

                var dicValue = dic.Value;
                //  var _operator = SetOperator(ref dicValue);
                var _operator = Operator.Equal;
                try
                {
                    var expression = Lambda.ParsePredicate<TEntity>(key, dicValue, _operator);
                    if (expression != null) query.And(expression);
                }
                catch
                {
                }
            }

            return query;
        }

        /// <summary>
        ///     获取操作符
        /// </summary>
        /// <param name="value"></param>
        private static Operator SetOperator(ref string value)
        {
            var _operator = Operator.Equal;
            if (!value.IsNullOrEmpty() && value.Length > 2)
            {
                var key = value.Substring(0, 2).ToLower();
                if (GetOperators().TryGetValue(key, out _operator))
                    value = value.Replace(key, "", StringComparison.OrdinalIgnoreCase);
            }

            return _operator;
        }

        /// <summary>
        ///     获取操作符
        /// </summary>
        private static Dictionary<string, Operator> GetOperators()
        {
            var dictionary = new Dictionary<string, Operator>
            {
                {"==", Operator.Equal},
                {"<<", Operator.Less},
                {"<=", Operator.LessEqual},
                {">>", Operator.Greater},
                {">=", Operator.GreaterEqual},
                {"!=", Operator.NotEqual},
                {"s%", Operator.Starts},
                {"e%", Operator.Ends},
                {"c%", Operator.Contains}
            };
            return dictionary;
        }

        /// <summary>
        ///     获取查询条件表达式
        /// </summary>
        /// <param name="predicate">
        ///     查询条件,如果参数值为空，则忽略该查询条件，范例：t => t.Name == ""，该查询条件被忽略。
        ///     注意：一次仅能添加一个条件，范例：t => t.Name == "a" &amp;&amp; t.Mobile == "123"，不支持，将抛出异常
        /// </param>
        public static Expression<Func<TEntity, bool>> GetWhereIfNotEmptyExpression<TEntity>(
            Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            if (predicate == null) return null;

            if (Lambda.GetConditionCount(predicate) > 1)
                throw new InvalidOperationException(string.Format("仅允许添加一个条件,条件：{0}", predicate));

            var value = predicate.Value();
            if (string.IsNullOrWhiteSpace(value.ToString())) return null;

            return predicate;
        }

        /// <summary>
        ///     动态获取linq操作符
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey">范例：bool</typeparam>
        /// <param name="expressionText">范例：r.Id>1 </param>
        /// <param name="paramName">范例：r</param>
        public static Expression<Func<TEntity, TKey>> GetExpression<TEntity, TKey>(string expressionText,
            params string[] paramName) where TEntity : class
        {
            var interpreter = new Interpreter();
            var expression = interpreter.ParseAsExpression<Func<TEntity, TKey>>(expressionText, paramName);
            return expression;
        }

        /// <summary>
        ///     动态获取linq select  操作符
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey">范例：bool</typeparam>
        /// <param name="selectExpression"></param>
        /// <param name="paramName">范例：r</param>
        public static Func<TEntity, TKey> GetSelectExpression<TEntity, TKey>(string selectExpression,
            params string[] paramName)
        {
            var interpreter = new Interpreter();
            var dynamicSelect = interpreter.ParseAsDelegate<Func<TEntity, TKey>>(selectExpression, paramName);
            return dynamicSelect;
        }
    }
}