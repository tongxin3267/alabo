using System;
using System.Linq;
using System.Linq.Expressions;

namespace Alabo.Extensions
{
    public static class QueryExtensions
    {
        /// <summary>
        ///     动态排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="propertyName">属性名</param>
        /// <param name="ascending">是否升序</param>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool ascending)
            where T : class
        {
            if (string.IsNullOrEmpty(propertyName)) return source;

            var type = typeof(T);
            var property = type.GetProperty(propertyName);
            if (property == null) throw new ArgumentException("propertyName", "不存在");

            var param = Expression.Parameter(type, "p");
            Expression propertyAccessExpression = Expression.MakeMemberAccess(param, property);
            var orderByExpression = Expression.Lambda(propertyAccessExpression, param);

            var methodName = ascending ? "OrderBy" : "OrderByDescending";
            var resultExp = Expression.Call(typeof(Queryable), methodName, new[] {type, property.PropertyType},
                source.Expression, Expression.Quote(orderByExpression)); //第三个类型为泛型的类型
            return source.Provider.CreateQuery<T>(resultExp);
        }

        /// <summary>
        ///     获取分页，从数据库上下文调用该方法前需现有OrderBy或OrderByDescending 进行排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">源对象</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">分页大小</param>
        public static IQueryable<T> Page<T>(this IQueryable<T> source, int pageIndex = 1, int pageSize = 10)
        {
            return source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }
    }
}