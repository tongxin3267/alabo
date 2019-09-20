using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace Alabo.App.Core.Reports {

    public class ReportConfigRuleResult : IReportRuleResult {
        private static readonly string AssemblyName = "Alabo.App.Core.Reports";

        private static readonly AssemblyBuilder _reportConfigRuleAssemblyBuilder =
            AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(AssemblyName), AssemblyBuilderAccess.Run);

        private static readonly ModuleBuilder _reportConfigRuleModuleBuilder =
            _reportConfigRuleAssemblyBuilder.DefineDynamicModule("main");

        private Func<object> _createJsonReusltFunction;

        public ReportConfigRuleResult(IEnumerable<ReportTableRow> rows, ReportScheme scheme) {
            Rows = rows.ToArray();
            Scheme = scheme;
        }

        public IReportRow[] Rows { get; }

        public ReportScheme Scheme { get; }

        public object ToJsonResult() {
            if (_createJsonReusltFunction == null) {
                var tableRows = (ReportTableRow[])Rows;
                var resultType = CreateResultType();
                _createJsonReusltFunction = CreateResultJsonFunction(resultType);
            }

            return _createJsonReusltFunction();
        }

        public Func<object> CreateResultJsonFunction(Type resultType) {
            var resultListType = typeof(List<>).MakeGenericType(resultType);
            var label = Expression.Label(typeof(object));
            var resultListNewExpression = Expression.New(resultListType);
            var resultListVariableExpression = Expression.Variable(resultListType);
            var resultAssignExpression = Expression.Assign(resultListVariableExpression, resultListNewExpression);
            var rowExpression = Expression.Constant(Rows);
            IList<Expression> bodyExpressionList = new List<Expression>
            {
                resultAssignExpression
            };
            IList<ParameterExpression> variableExpressionList = new List<ParameterExpression>
            {
                resultListVariableExpression
            };
            var resultFields = resultType.GetFields();
            var getDataMethod = typeof(IReportRow).GetMethod("GetData");
            var addListMethod = resultListType.GetMethod("Add");
            for (var i = 0; i < Rows.Length; i++) {
                var rowIndexExpression = Expression.Constant(i);
                var currentRowExpression = Expression.ArrayIndex(rowExpression, rowIndexExpression);
                var itemNewExpression = Expression.New(resultType);
                var itemVariableExpression = Expression.Variable(resultType);
                variableExpressionList.Add(itemVariableExpression);
                var itemAssignExpression = Expression.Assign(itemVariableExpression, itemNewExpression);
                bodyExpressionList.Add(itemAssignExpression);
                foreach (var field in resultFields) {
                    if (!Rows[i].HasColumn(field.Name)) {
                        continue;
                    }

                    var fieldExpression = Expression.Field(itemVariableExpression, field);
                    var getMethodExpression = Expression.Call(currentRowExpression,
                        getDataMethod.MakeGenericMethod(field.FieldType), Expression.Constant(field.Name));
                    var propertyAsginExpression = Expression.Assign(fieldExpression, getMethodExpression);
                    bodyExpressionList.Add(propertyAsginExpression);
                }

                var addListExpression =
                    Expression.Call(resultListVariableExpression, addListMethod, itemVariableExpression);
                bodyExpressionList.Add(addListExpression);
            }

            var resultConvertExpression = Expression.Convert(resultListVariableExpression, typeof(object));
            var returnLabel = Expression.Label(label, resultConvertExpression);
            var returnExpression = Expression.Return(label, resultConvertExpression);
            bodyExpressionList.Add(returnExpression);
            bodyExpressionList.Add(returnLabel);
            var blockExpression = Expression.Block(variableExpressionList, bodyExpressionList);
            var lambaExpression = Expression.Lambda<Func<object>>(blockExpression);
            return lambaExpression.Compile();
        }

        private Type CreateResultType() {
            var typeBuilder = _reportConfigRuleModuleBuilder.DefineType($"{Scheme.Name}_{Guid.NewGuid().ToString("N")}",
                TypeAttributes.Public | TypeAttributes.Class);
            foreach (var item in Scheme.Columns) {
                typeBuilder.DefineField(item.Name, item.DataType, FieldAttributes.Public);
            }

            var result = typeBuilder.CreateTypeInfo().AsType();
            return result;
        }
    }
}