using System.Collections.Generic;
using System.Linq;

namespace Alabo.App.Core.Reports {

    public class ReportModelRuleResult : IReportRuleResult {

        public ReportModelRuleResult(IEnumerable<IReportRow> rows, ReportScheme scheme) {
            Rows = rows.ToArray();
            Scheme = scheme;
        }

        public IReportRow[] Rows { get; }

        public ReportScheme Scheme { get; }

        public object ToJsonResult() {
            return Rows;
        }
    }
}