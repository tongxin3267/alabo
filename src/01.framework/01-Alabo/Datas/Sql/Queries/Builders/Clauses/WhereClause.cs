﻿using Alabo.Datas.Queries.Enums;
using Alabo.Datas.Sql.Queries.Builders.Abstractions;
using Alabo.Datas.Sql.Queries.Builders.Conditions;
using Alabo.Datas.Sql.Queries.Builders.Core;
using Alabo.Datas.Sql.Queries.Builders.Internal;
using Alabo.Linq;
using Alabo.Properties;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Alabo.Datas.Sql.Queries.Builders.Clauses {

    /// <summary>
    ///     Where子句
    /// </summary>
    public class WhereClause : IWhereClause {

        /// <summary>
        ///     谓词表达式解析器
        /// </summary>
        private readonly PredicateExpressionResolver _expressionResolver;

        /// <summary>
        ///     辅助操作
        /// </summary>
        private readonly Helper _helper;

        /// <summary>
        ///     实体解析器
        /// </summary>
        private readonly IEntityResolver _resolver;

        /// <summary>
        ///     查询条件
        /// </summary>
        private ICondition _condition;

        /// <summary>
        ///     初始化Where子句
        /// </summary>
        /// <param name="dialect">方言</param>
        /// <param name="resolver">实体解析器</param>
        /// <param name="register">实体别名注册器</param>
        /// <param name="parameterManager">参数管理器</param>
        public WhereClause(IDialect dialect, IEntityResolver resolver, IEntityAliasRegister register,
            IParameterManager parameterManager) {
            _resolver = resolver;
            _helper = new Helper(dialect, resolver, register, parameterManager);
            _expressionResolver = new PredicateExpressionResolver(dialect, resolver, register, parameterManager);
        }

        /// <summary>
        ///     And连接条件
        /// </summary>
        /// <param name="condition">查询条件</param>
        public void And(ICondition condition) {
            _condition = new AndCondition(_condition, condition);
        }

        /// <summary>
        ///     Or连接条件
        /// </summary>
        /// <param name="condition">查询条件</param>
        public void Or(ICondition condition) {
            _condition = new OrCondition(_condition, condition);
        }

        /// <summary>
        ///     设置查询条件
        /// </summary>
        /// <param name="condition">查询条件</param>
        public void Where(ICondition condition) {
            And(condition);
        }

        /// <summary>
        ///     设置查询条件
        /// </summary>
        /// <param name="column">列名</param>
        /// <param name="value">值</param>
        /// <param name="operator">运算符</param>
        public void Where(string column, object value, Operator @operator = Operator.Equal) {
            And(_helper.CreateCondition(column, value, @operator));
        }

        /// <summary>
        ///     设置查询条件
        /// </summary>
        /// <param name="expression">列名表达式</param>
        /// <param name="value">值</param>
        /// <param name="operator">运算符</param>
        public void Where<TEntity>(Expression<Func<TEntity, object>> expression, object value,
            Operator @operator = Operator.Equal) where TEntity : class {
            Where(_helper.GetColumn(expression), value, @operator);
        }

        /// <summary>
        ///     设置查询条件
        /// </summary>
        /// <param name="expression">查询条件表达式</param>
        public void Where<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class {
            if (expression == null) {
                throw new ArgumentNullException(nameof(expression));
            }

            var condition = _expressionResolver.Resolve(expression);
            And(condition);
        }

        /// <summary>
        ///     设置查询条件
        /// </summary>
        /// <param name="column">列名</param>
        /// <param name="value">值</param>
        /// <param name="condition">该值为true时添加查询条件，否则忽略</param>
        /// <param name="operator">运算符</param>
        public void WhereIf(string column, object value, bool condition, Operator @operator = Operator.Equal) {
            if (condition) {
                Where(column, value, @operator);
            }
        }

        /// <summary>
        ///     设置查询条件
        /// </summary>
        /// <param name="expression">列名表达式</param>
        /// <param name="value">值</param>
        /// <param name="condition">该值为true时添加查询条件，否则忽略</param>
        /// <param name="operator">运算符</param>
        public void WhereIf<TEntity>(Expression<Func<TEntity, object>> expression, object value, bool condition,
            Operator @operator = Operator.Equal) where TEntity : class {
            if (condition) {
                Where(expression, value, @operator);
            }
        }

        /// <summary>
        ///     设置查询条件
        /// </summary>
        /// <param name="expression">查询条件表达式</param>
        /// <param name="condition">该值为true时添加查询条件，否则忽略</param>
        public void WhereIf<TEntity>(Expression<Func<TEntity, bool>> expression, bool condition) where TEntity : class {
            if (condition) {
                Where(expression);
            }
        }

        /// <summary>
        ///     设置查询条件
        /// </summary>
        /// <param name="column">列名</param>
        /// <param name="value">值,如果值为空，则忽略该查询条件</param>
        /// <param name="operator">运算符</param>
        public void WhereIfNotEmpty(string column, object value, Operator @operator = Operator.Equal) {
            if (string.IsNullOrWhiteSpace(Alabo.Extensions.Extensions.SafeString(value))) {
                return;
            }

            Where(column, value, @operator);
        }

        /// <summary>
        ///     设置查询条件
        /// </summary>
        /// <param name="expression">列名表达式</param>
        /// <param name="value">值,如果值为空，则忽略该查询条件</param>
        /// <param name="operator">运算符</param>
        public void WhereIfNotEmpty<TEntity>(Expression<Func<TEntity, object>> expression, object value,
            Operator @operator = Operator.Equal) where TEntity : class {
            if (expression == null) {
                throw new ArgumentNullException(nameof(expression));
            }

            if (string.IsNullOrWhiteSpace(Alabo.Extensions.Extensions.SafeString(value))) {
                return;
            }

            Where(expression, value, @operator);
        }

        /// <summary>
        ///     设置查询条件
        /// </summary>
        /// <param name="expression">查询条件表达式,如果参数值为空，则忽略该查询条件</param>
        public void WhereIfNotEmpty<TEntity>(Expression<Func<TEntity, bool>> expression) where TEntity : class {
            if (expression == null) {
                throw new ArgumentNullException(nameof(expression));
            }

            if (Lambda.GetConditionCount(expression) > 1) {
                throw new InvalidOperationException(string.Format(LibraryResource.OnlyOnePredicate, expression));
            }

            if (string.IsNullOrWhiteSpace(Alabo.Extensions.Extensions.SafeString(Lambda.GetValue(expression)))
            ) {
                return;
            }

            Where(expression);
        }

        /// <summary>
        ///     添加到Where子句
        /// </summary>
        /// <param name="sql">Sql语句</param>
        public void AppendSql(string sql) {
            And(new SqlCondition(sql));
        }

        /// <summary>
        ///     设置Is Null条件
        /// </summary>
        /// <param name="column">列名</param>
        public void IsNull(string column) {
            Where(column, null);
        }

        /// <summary>
        ///     设置Is Null条件
        /// </summary>
        /// <param name="expression">列名表达式</param>
        public void IsNull<TEntity>(Expression<Func<TEntity, object>> expression) where TEntity : class {
            Where(expression, null);
        }

        /// <summary>
        ///     设置Is Not Null条件
        /// </summary>
        /// <param name="column">列名</param>
        public void IsNotNull(string column) {
            column = _helper.GetColumn(column);
            And(new IsNotNullCondition(column));
        }

        /// <summary>
        ///     设置Is Not Null条件
        /// </summary>
        /// <param name="expression">列名表达式</param>
        public void IsNotNull<TEntity>(Expression<Func<TEntity, object>> expression) where TEntity : class {
            var column = _helper.GetColumn(_resolver.GetColumn(expression), typeof(TEntity));
            IsNotNull(column);
        }

        /// <summary>
        ///     设置空条件
        /// </summary>
        /// <param name="column">列名</param>
        public void IsEmpty(string column) {
            column = _helper.GetColumn(column);
            And(new OrCondition(new IsNullCondition(column), new EqualCondition(column, "''")));
        }

        /// <summary>
        ///     设置空条件
        /// </summary>
        /// <param name="expression">列名表达式</param>
        public void IsEmpty<TEntity>(Expression<Func<TEntity, object>> expression) where TEntity : class {
            var column = _helper.GetColumn(_resolver.GetColumn(expression), typeof(TEntity));
            IsEmpty(column);
        }

        /// <summary>
        ///     设置非空条件
        /// </summary>
        /// <param name="column">列名</param>
        public void IsNotEmpty(string column) {
            column = _helper.GetColumn(column);
            And(new AndCondition(new IsNotNullCondition(column), new NotEqualCondition(column, "''")));
        }

        /// <summary>
        ///     设置非空条件
        /// </summary>
        /// <param name="expression">列名表达式</param>
        public void IsNotEmpty<TEntity>(Expression<Func<TEntity, object>> expression) where TEntity : class {
            var column = _helper.GetColumn(expression);
            IsNotEmpty(column);
        }

        /// <summary>
        ///     设置In条件
        /// </summary>
        /// <param name="column">列名</param>
        /// <param name="values">值集合</param>
        public void In(string column, IEnumerable<object> values) {
            Where(column, values, Operator.Contains);
        }

        /// <summary>
        ///     设置In条件
        /// </summary>
        /// <param name="expression">列名表达式</param>
        /// <param name="values">值集合</param>
        public void In<TEntity>(Expression<Func<TEntity, object>> expression, IEnumerable<object> values)
            where TEntity : class {
            Where(expression, values, Operator.Contains);
        }

        /// <summary>
        ///     添加范围查询条件
        /// </summary>
        /// <param name="expression">列名表达式</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="boundary">包含边界</param>
        public void Between<TEntity>(Expression<Func<TEntity, object>> expression, int? min, int? max,
            Boundary boundary) where TEntity : class {
            var column = _helper.GetColumn(expression);
            Between(column, min, max, boundary);
        }

        /// <summary>
        ///     添加范围查询条件
        /// </summary>
        /// <param name="expression">列名表达式</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="boundary">包含边界</param>
        public void Between<TEntity>(Expression<Func<TEntity, object>> expression, double? min, double? max,
            Boundary boundary) where TEntity : class {
            var column = _helper.GetColumn(expression);
            Between(column, min, max, boundary);
        }

        /// <summary>
        ///     添加范围查询条件
        /// </summary>
        /// <param name="expression">列名表达式</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="boundary">包含边界</param>
        public void Between<TEntity>(Expression<Func<TEntity, object>> expression, decimal? min, decimal? max,
            Boundary boundary) where TEntity : class {
            var column = _helper.GetColumn(expression);
            Between(column, min, max, boundary);
        }

        /// <summary>
        ///     添加范围查询条件
        /// </summary>
        /// <param name="expression">列名表达式</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="includeTime">是否包含时间</param>
        /// <param name="boundary">包含边界</param>
        public void Between<TEntity>(Expression<Func<TEntity, object>> expression, DateTime? min, DateTime? max,
            bool includeTime, Boundary? boundary) where TEntity : class {
            var column = _helper.GetColumn(expression);
            Between(column, min, max, includeTime, boundary);
        }

        /// <summary>
        ///     添加范围查询条件
        /// </summary>
        /// <param name="column">列名</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="boundary">包含边界</param>
        public void Between(string column, int? min, int? max, Boundary boundary) {
            if (min > max) {
                Where(_helper.Between(column, max, min, boundary));
                return;
            }

            Where(_helper.Between(column, min, max, boundary));
        }

        /// <summary>
        ///     添加范围查询条件
        /// </summary>
        /// <param name="column">列名</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="boundary">包含边界</param>
        public void Between(string column, double? min, double? max, Boundary boundary) {
            if (min > max) {
                Where(_helper.Between(column, max, min, boundary));
                return;
            }

            Where(_helper.Between(column, min, max, boundary));
        }

        /// <summary>
        ///     添加范围查询条件
        /// </summary>
        /// <param name="column">列名</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="boundary">包含边界</param>
        public void Between(string column, decimal? min, decimal? max, Boundary boundary) {
            if (min > max) {
                Where(_helper.Between(column, max, min, boundary));
                return;
            }

            Where(_helper.Between(column, min, max, boundary));
        }

        /// <summary>
        ///     添加范围查询条件
        /// </summary>
        /// <param name="column">列名</param>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="includeTime">是否包含时间</param>
        /// <param name="boundary">包含边界</param>
        public void Between(string column, DateTime? min, DateTime? max, bool includeTime, Boundary? boundary) {
            Where(_helper.Between(column, GetMin(min, max, includeTime), GetMax(min, max, includeTime),
                GetBoundary(boundary, includeTime)));
        }

        /// <summary>
        ///     输出Sql
        /// </summary>
        public string ToSql() {
            var condition = GetCondition();
            if (string.IsNullOrWhiteSpace(condition)) {
                return null;
            }

            return $"Where {condition}";
        }

        /// <summary>
        ///     获取查询条件
        /// </summary>
        public string GetCondition() {
            return _condition?.GetCondition();
        }

        /// <summary>
        ///     获取最小日期
        /// </summary>
        private DateTime? GetMin(DateTime? min, DateTime? max, bool includeTime) {
            if (min == null && max == null) {
                return null;
            }

            var result = min;
            if (min > max) {
                result = max;
            }

            if (includeTime) {
                return result;
            }

            return Alabo.Extensions.Extensions.SafeValue(result).Date;
        }

        /// <summary>
        ///     获取最大日期
        /// </summary>
        private DateTime? GetMax(DateTime? min, DateTime? max, bool includeTime) {
            if (min == null && max == null) {
                return null;
            }

            var result = max;
            if (min > max) {
                result = min;
            }

            if (includeTime) {
                return result;
            }

            return Alabo.Extensions.Extensions.SafeValue(result).Date.AddDays(1);
        }

        /// <summary>
        ///     获取日期范围查询条件边界
        /// </summary>
        private Boundary GetBoundary(Boundary? boundary, bool includeTime) {
            if (boundary != null) {
                return Alabo.Extensions.Extensions.SafeValue(boundary);
            }

            if (includeTime) {
                return Boundary.Both;
            }

            return Boundary.Left;
        }
    }
}