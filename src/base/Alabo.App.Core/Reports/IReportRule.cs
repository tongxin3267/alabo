namespace Alabo.App.Core.Reports {

    public interface IReportRule {
        ReportContext Context { get; }

        IReportRuleResult Execute(ReportRuleParameter parameter);
    }
}