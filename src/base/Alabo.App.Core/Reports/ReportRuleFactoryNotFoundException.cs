using System;

namespace Alabo.App.Core.Reports {

    public class ReportRuleFactoryNotFoundException : Exception {

        public ReportRuleFactoryNotFoundException(string message, Exception innerException)
            : base(message, innerException) {
        }

        public ReportRuleFactoryNotFoundException(string message)
            : base(message) {
        }
    }
}