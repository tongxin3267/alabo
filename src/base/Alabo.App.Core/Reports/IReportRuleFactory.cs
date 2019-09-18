using System;

namespace Alabo.App.Core.Reports {

    public interface IReportRuleFactory {

        bool HasRule(Guid id);

        ReportScheme GetRuleScheme(Guid id);

        IReportRule CreateRule(Guid id, ReportContext context);
    }
}