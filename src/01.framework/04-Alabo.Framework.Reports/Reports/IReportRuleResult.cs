namespace Alabo.App.Core.Reports {

    public interface IReportRuleResult {
        ReportScheme Scheme { get; }

        IReportRow[] Rows { get; }

        object ToJsonResult();
    }
}