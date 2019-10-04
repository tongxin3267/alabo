using System.Linq.Expressions;

namespace Alabo.Linq.Expressions {

    public class ChangeParameterTypeExpressionVisitor : ExpressionVisitor {
        private readonly Expression _parameterExpression;

        private readonly Expression _predicateExpression;

        public ChangeParameterTypeExpressionVisitor(Expression predicate, Expression parameter) {
            _parameterExpression = parameter;
            _predicateExpression = predicate;
        }

        public Expression Convert() {
            return Visit(_predicateExpression);
        }

        protected override Expression VisitParameter(ParameterExpression node) {
            return _parameterExpression;
        }
    }
}