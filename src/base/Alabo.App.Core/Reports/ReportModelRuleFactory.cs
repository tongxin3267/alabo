using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Alabo.Reflections;
using Alabo.Runtime;

namespace Alabo.App.Core.Reports {

    public sealed class ReportModelRuleFactory : IReportRuleFactory {
        private readonly IDictionary<Guid, ReportRuleAttribute> _reportRuleAttributeCache;

        private readonly ConcurrentDictionary<Type, Func<ReportContext, IReportRule>> _reportRuleFuncDictionary
            = new ConcurrentDictionary<Type, Func<ReportContext, IReportRule>>();

        private readonly ConcurrentDictionary<Guid, ReportScheme> _reportRuleSchemeCache
            = new ConcurrentDictionary<Guid, ReportScheme>();

        private readonly IDictionary<Guid, Type> _reportRuleTypeCache;

        public ReportModelRuleFactory() {
            var types = RuntimeContext.Current.GetPlatformRuntimeAssemblies()
                .SelectMany(e => e.GetTypes())
                .Where(e => e.GetTypeInfo().IsClass && !e.GetTypeInfo().IsAbstract && !e.GetTypeInfo().IsGenericType &&
                            typeof(IReportRule).IsAssignableFrom(e) &&
                            e.GetTypeInfo().GetAttribute<ReportRuleAttribute>() != null)
                .ToArray();
            _reportRuleTypeCache =
                types.ToDictionary(e => e.GetTypeInfo().GetAttribute<ReportRuleAttribute>().Id, e => e);
            _reportRuleAttributeCache = types.ToDictionary(e => e.GetTypeInfo().GetAttribute<ReportRuleAttribute>().Id,
                e => e.GetTypeInfo().GetAttribute<ReportRuleAttribute>());
        }

        public bool HasRule(Guid id) {
            return _reportRuleTypeCache.ContainsKey(id);
        }

        public IReportRule CreateRule(Guid id, ReportContext context) {
            if (!_reportRuleTypeCache.TryGetValue(id, out var ruleType)) {
                throw new ArgumentException($"rule type with id {id} not found.");
            }

            var func = _reportRuleFuncDictionary.GetOrAdd(ruleType, e => CreateReportRuleFunc(e));
            return func(context);
        }

        public ReportScheme GetRuleScheme(Guid id) {
            if (!_reportRuleAttributeCache.TryGetValue(id, out var ruleAttribute)) {
                throw new ArgumentException($"rule type with id {id} not found.");
            }

            return _reportRuleSchemeCache.GetOrAdd(id,
                _ => CreateRuleScheme(id, ruleAttribute.Name, ruleAttribute.Summary, ruleAttribute.ModelType));
        }

        private Func<ReportContext, IReportRule> CreateReportRuleFunc(Type type) {
            var parameterExpression = Expression.Parameter(typeof(ReportContext));
            var newExpression = Expression.New(type.GetConstructor(new[] { typeof(ReportContext) }), parameterExpression);
            var convertExpression = Expression.Convert(newExpression, typeof(IReportRule));
            var lambdaExpression =
                Expression.Lambda<Func<ReportContext, IReportRule>>(newExpression, parameterExpression);
            return lambdaExpression.Compile();
        }

        private ReportScheme CreateRuleScheme(Guid id, string name, string summary, Type modelType) {
            var properties = modelType.GetProperties();
            IList<ReportColumn> columnList = new List<ReportColumn>();
            foreach (var item in properties) {
                var attribute = item.GetAttribute<ReportColumnAttribute>();
                if (attribute != null) {
                    columnList.Add(new ReportColumn {
                        Name = attribute.Name,
                        Text = attribute.Text,
                        Summary = attribute.Summary,
                        Format = attribute.Format,
                        Order = attribute.Order,
                        DataType = item.PropertyType
                    });
                } else {
                    columnList.Add(new ReportColumn {
                        Name = item.Name,
                        Text = item.Name,
                        Summary = item.Name,
                        Format = string.Empty,
                        Order = 1000,
                        DataType = item.PropertyType
                    });
                }
            }

            return new ReportScheme(id, name, summary, columnList.ToArray());
        }
    }
}