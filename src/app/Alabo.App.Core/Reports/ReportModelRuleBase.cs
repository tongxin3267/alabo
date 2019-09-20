using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.App.Core.Reports {

    public abstract class ReportModelRuleBase<TModel> : IReportRule
        where TModel : class, IReportModel, IReportRow, new() {

        public ReportModelRuleBase(ReportContext context) {
            Context = context;
        }

        public ReportContext Context { get; }

        public abstract IReportRuleResult Execute(ReportRuleParameter parameter);

        protected IReportRuleResult ModelResult(Guid id, IEnumerable<TModel> source) {
            if (source == null) {
                throw new ArgumentNullException(nameof(source));
            }

            var ruleFactory = Context.HttpContextAccessor.HttpContext.RequestServices.GetService<IReportRuleFactory>();
            if (ruleFactory == null) {
                throw new ReportRuleFactoryNotFoundException(
                    "get IReportRuleFactory from ServiceProvider return null.");
            }

            return new ReportModelRuleResult(source.ToArray(), ruleFactory.GetRuleScheme(id));
        }
    }
}