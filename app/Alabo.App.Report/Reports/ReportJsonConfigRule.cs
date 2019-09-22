using System;

namespace Alabo.App.Core.Reports {

    public class ReportJsonConfigRule : ReportConfigRuleBase {
        private readonly ReportJsonConfigManager _reportJsonConfigManager;

        public ReportJsonConfigRule(Guid id, ReportContext context, ReportJsonConfigManager reportJsonConfigManager)
            : base(id, context) {
            _reportJsonConfigManager = reportJsonConfigManager;
        }

        public override ReportRuleConfig ReadRuleConfig(Guid id) {
            return _reportJsonConfigManager.GetRuleConfig(id);
        }
    }
}