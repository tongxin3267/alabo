using System;

namespace Alabo.App.Core.Reports {

    public class ReportRuleNotFoundException : Exception {

        public ReportRuleNotFoundException(string message)
            : base(message) {
        }

        public ReportRuleNotFoundException(string message, Exception innerException)
            : base(message, innerException) {
        }
    }
}