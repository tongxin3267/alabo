using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Alabo.Reflections;

namespace Alabo.App.Core.Reports {

    public abstract class ReportModelRowBase<TModel> : IReportRow
        where TModel : class, IReportModel {
        private static readonly IDictionary<string, PropertyInfo> _modelRowDictionary;

        private static readonly IDictionary<string, Func<TModel, object>> _modelRowGetFunctionDictionary;

        static ReportModelRowBase() {
            _modelRowDictionary = typeof(TModel).GetProperties().ToDictionary(e => e.Name, e => e);
            _modelRowGetFunctionDictionary = typeof(TModel).GetProperties()
                .ToDictionary(e => e.Name, e => CreatePropertyGetFunction(e));
        }

        public bool HasColumn(string columnName) {
            return _modelRowDictionary.ContainsKey(columnName);
        }

        public T GetData<T>(string columnName) {
            if (!TryGetData(columnName, out T result)) {
                return default(T);
            }

            return result;
        }

        public bool TryGetData<T>(string columnName, out T value) {
            if (_modelRowGetFunctionDictionary.TryGetValue(columnName, out var func)) {
                var result = func(this as TModel);
                try {
                    value = (T)result;
                    return true;
                } catch {
                    value = default(T);
                    return false;
                }
            }

            value = default(T);
            return false;
        }

        private static Func<TModel, object> CreatePropertyGetFunction(PropertyInfo property) {
            var parameterExpression = Expression.Parameter(typeof(TModel));
            var propertyExpression = Expression.Property(parameterExpression, property);
            var convertExpression = Expression.Convert(propertyExpression, typeof(object));
            var lambdaExpression = Expression.Lambda<Func<TModel, object>>(convertExpression, parameterExpression);
            return lambdaExpression.Compile();
        }

        private static ReportColumnAttribute GetPropertyColumnAttribute(PropertyInfo property) {
            var result = property.GetAttribute<ReportColumnAttribute>();
            if (result == null) {
                result = new ReportColumnAttribute(property.Name);
            }

            return result;
        }
    }
}