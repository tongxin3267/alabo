﻿using Alabo.Datas.Queries.Enums;
using Alabo.Linq;
using Alabo.Linq.Expressions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Alabo.Extensions {

    /// <summary>
    ///     系统扩展 - Lambda表达式
    /// </summary>
    public static partial class Extensions {

        #region Value(获取lambda表达式的值)

        /// <summary>
        ///     获取lambda表达式的值
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        public static object Value<T>(this Expression<Func<T, bool>> expression) {
            return Lambda.GetValue(expression);
        }

        #endregion Value(获取lambda表达式的值)

        #region StartsWith(头匹配)

        /// <summary>
        ///     头匹配
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static Expression StartsWith(this Expression left, object value) {
            return left.Call("StartsWith", new[] { typeof(string) }, value);
        }

        #endregion StartsWith(头匹配)

        #region EndsWith(尾匹配)

        /// <summary>
        ///     尾匹配
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static Expression EndsWith(this Expression left, object value) {
            return left.Call("EndsWith", new[] { typeof(string) }, value);
        }

        #endregion EndsWith(尾匹配)

        #region Contains(模糊匹配)

        /// <summary>
        ///     模糊匹配
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static Expression Contains(this Expression left, object value) {
            return left.Call("Contains", new[] { typeof(string) }, value);
        }

        #endregion Contains(模糊匹配)

        #region Operation(操作)

        /// <summary>
        ///     操作
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="operator">运算符</param>
        /// <param name="value">值</param>
        public static Expression Operation(this Expression left, Operator @operator, object value) {
            switch (@operator) {
                case Operator.Equal:
                    return left.Equal(value);

                case Operator.NotEqual:
                    return left.NotEqual(value);

                case Operator.Greater:
                    return left.Greater(value);

                case Operator.GreaterEqual:
                    return left.GreaterEqual(value);

                case Operator.Less:
                    return left.Less(value);

                case Operator.LessEqual:
                    return left.LessEqual(value);

                case Operator.Starts:
                    return left.StartsWith(value);

                case Operator.Ends:
                    return left.EndsWith(value);

                case Operator.Contains:
                    return left.Contains(value);
            }

            throw new NotImplementedException();
        }

        #endregion Operation(操作)

        #region Compose(组合表达式)

        /// <summary>
        ///     组合表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="first">左操作数</param>
        /// <param name="second">右操作数</param>
        /// <param name="merge">合并操作</param>
        internal static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second,
            Func<Expression, Expression, Expression> merge) {
            var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        #endregion Compose(组合表达式)

        #region ToLambda(创建Lambda表达式)

        /// <summary>
        ///     创建Lambda表达式
        /// </summary>
        /// <typeparam name="TDelegate">委托类型</typeparam>
        /// <param name="body">表达式</param>
        /// <param name="parameters">参数列表</param>
        public static Expression<TDelegate> ToLambda<TDelegate>(this Expression body,
            params ParameterExpression[] parameters) {
            if (body == null) {
                return null;
            }

            return Expression.Lambda<TDelegate>(body, parameters);
        }

        #endregion ToLambda(创建Lambda表达式)

        #region Property(属性表达式)

        /// <summary>
        ///     创建属性表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="propertyName">属性名,支持多级属性名，与句点分隔，范例：Customer.Name</param>
        public static Expression Property(this Expression expression, string propertyName) {
            if (propertyName.All(t => t != '.')) {
                return Expression.Property(expression, propertyName);
            }

            var propertyNameList = propertyName.Split('.');
            Expression result = null;
            for (var i = 0; i < propertyNameList.Length; i++) {
                if (i == 0) {
                    result = Expression.Property(expression, propertyNameList[0]);
                    continue;
                }

                result = result.Property(propertyNameList[i]);
            }

            return result;
        }

        /// <summary>
        ///     创建属性表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="member">属性</param>
        public static Expression Property(this Expression expression, MemberInfo member) {
            return Expression.MakeMemberAccess(expression, member);
        }

        #endregion Property(属性表达式)

        #region And(与表达式)

        /// <summary>
        ///     与操作表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static Expression And(this Expression left, Expression right) {
            if (left == null) {
                return right;
            }

            if (right == null) {
                return left;
            }

            return Expression.AndAlso(left, right);
        }

        /// <summary>
        ///     与操作表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right) {
            if (left == null) {
                return right;
            }

            if (right == null) {
                return left;
            }

            return left.Compose(right, Expression.AndAlso);
        }

        #endregion And(与表达式)

        #region Or(或表达式)

        /// <summary>
        ///     或操作表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static Expression Or(this Expression left, Expression right) {
            if (left == null) {
                return right;
            }

            if (right == null) {
                return left;
            }

            return Expression.OrElse(left, right);
        }

        /// <summary>
        ///     或操作表达式
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right) {
            if (left == null) {
                return right;
            }

            if (right == null) {
                return left;
            }

            return left.Compose(right, Expression.OrElse);
        }

        #endregion Or(或表达式)

        #region Equal(等于表达式)

        /// <summary>
        ///     创建等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static Expression Equal(this Expression left, Expression right) {
            return Expression.Equal(left, right);
        }

        /// <summary>
        ///     创建等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static Expression Equal(this Expression left, object value) {
            return left.Equal(Lambda.Constant(left, value));
        }

        #endregion Equal(等于表达式)

        #region NotEqual(不等于表达式)

        /// <summary>
        ///     创建不等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static Expression NotEqual(this Expression left, Expression right) {
            return Expression.NotEqual(left, right);
        }

        /// <summary>
        ///     创建不等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static Expression NotEqual(this Expression left, object value) {
            return left.NotEqual(Lambda.Constant(left, value));
        }

        #endregion NotEqual(不等于表达式)

        #region Greater(大于表达式)

        /// <summary>
        ///     创建大于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static Expression Greater(this Expression left, Expression right) {
            return Expression.GreaterThan(left, right);
        }

        /// <summary>
        ///     创建大于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static Expression Greater(this Expression left, object value) {
            return left.Greater(Lambda.Constant(left, value));
        }

        #endregion Greater(大于表达式)

        #region GreaterEqual(大于等于表达式)

        /// <summary>
        ///     创建大于等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static Expression GreaterEqual(this Expression left, Expression right) {
            return Expression.GreaterThanOrEqual(left, right);
        }

        /// <summary>
        ///     创建大于等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static Expression GreaterEqual(this Expression left, object value) {
            return left.GreaterEqual(Lambda.Constant(left, value));
        }

        #endregion GreaterEqual(大于等于表达式)

        #region Less(小于表达式)

        /// <summary>
        ///     创建小于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static Expression Less(this Expression left, Expression right) {
            return Expression.LessThan(left, right);
        }

        /// <summary>
        ///     创建小于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static Expression Less(this Expression left, object value) {
            return left.Less(Lambda.Constant(left, value));
        }

        #endregion Less(小于表达式)

        #region LessEqual(小于等于表达式)

        /// <summary>
        ///     创建小于等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="right">右操作数</param>
        public static Expression LessEqual(this Expression left, Expression right) {
            return Expression.LessThanOrEqual(left, right);
        }

        /// <summary>
        ///     创建小于等于运算表达式
        /// </summary>
        /// <param name="left">左操作数</param>
        /// <param name="value">值</param>
        public static Expression LessEqual(this Expression left, object value) {
            return left.LessEqual(Lambda.Constant(left, value));
        }

        #endregion LessEqual(小于等于表达式)

        #region Call(调用方法表达式)

        /// <summary>
        ///     创建调用方法表达式
        /// </summary>
        /// <param name="instance">调用的实例</param>
        /// <param name="methodName">方法名</param>
        /// <param name="values">参数值列表</param>
        public static Expression Call(this Expression instance, string methodName, params Expression[] values) {
            var type = instance.Type.GetTypeInfo();
            var methods = type.GetMethods();
            foreach (var method in methods) {
                if (method.Name == methodName) {
                    return Expression.Call(instance, method, values);
                }
            }

            return null;
        }

        /// <summary>
        ///     创建调用方法表达式
        /// </summary>
        /// <param name="instance">调用的实例</param>
        /// <param name="methodName">方法名</param>
        /// <param name="values">参数值列表</param>
        public static Expression Call(this Expression instance, string methodName, params object[] values) {
            if (values == null || values.Length == 0) {
                return Expression.Call(instance, instance.Type.GetTypeInfo().GetMethod(methodName));
            }

            return Expression.Call(instance, instance.Type.GetTypeInfo().GetMethod(methodName),
                values.Select(Expression.Constant));
        }

        /// <summary>
        ///     创建调用方法表达式
        /// </summary>
        /// <param name="instance">调用的实例</param>
        /// <param name="methodName">方法名</param>
        /// <param name="paramTypes">参数类型列表</param>
        /// <param name="values">参数值列表</param>
        public static Expression Call(this Expression instance, string methodName, Type[] paramTypes,
            params object[] values) {
            if (values == null || values.Length == 0) {
                return Expression.Call(instance, instance.Type.GetTypeInfo().GetMethod(methodName, paramTypes));
            }

            return Expression.Call(instance, instance.Type.GetTypeInfo().GetMethod(methodName, paramTypes),
                values.Select(Expression.Constant));
        }

        #endregion Call(调用方法表达式)
    }
}