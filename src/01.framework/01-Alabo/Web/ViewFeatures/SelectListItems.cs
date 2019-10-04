using Alabo.Linq.Dynamic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Alabo.Web.ViewFeatures {

    public static class SelectListItems {

        /// <summary>
        ///     从查询对象返回选择项列表
        ///     注意会逐项延迟返回，需要一次获取所有数据请使用ToList或ToArray
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TSelected"></typeparam>
        /// <param name="query">查询对象</param>
        /// <param name="selector">选择文本和值数据的表达式，用于减少从数据库中获取的字段数量</param>
        /// <param name="textSelector">选择文本的函数，会转换到字符串</param>
        /// <param name="valueSelector">选择值的函数，会转换到字符串</param>
        public static IEnumerable<SelectListItem> FromQuery<TEntity, TSelected>(
            IQueryable<TEntity> query,
            Expression<Func<TEntity, TSelected>> selector,
            Func<TSelected, object> textSelector,
            Func<TSelected, object> valueSelector) {
            return FromIEnumerable(query.Select(selector).ToList(), textSelector, valueSelector);
        }

        public static IEnumerable<SelectListItem> FromQuery<T>(Expression<Func<T, bool>> predicate,
            Func<T, object> textSelector,
            Func<T, object> valueSelector) where T : class {
            var list = DynamicService.ResolveList(predicate);
            return FromIEnumerable(list, textSelector, valueSelector);
        }

        /// <summary>
        ///     从对象列表返回选择项列表
        ///     注意会逐项延迟返回，需要一次获取所有数据请使用ToList或ToArray
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements">对象列表</param>
        /// <param name="textSelector">选择文本的函数，会转换到字符串</param>
        /// <param name="valueSelector">选择值的函数，会转换到字符串</param>
        public static IEnumerable<SelectListItem> FromIEnumerable<T>(
            IEnumerable<T> elements,
            Func<T, object> textSelector, Func<T, object> valueSelector) {
            foreach (var element in elements) {
                yield return new SelectListItem {
                    Text = textSelector(element)?.ToString(),
                    Value = valueSelector(element)?.ToString()
                };
            }
        }
    }
}