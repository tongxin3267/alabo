using System;

namespace Alabo.App.Core.Reports {

    public class DefaultReportRuleFactory : IReportRuleFactory {
        private readonly IReportRuleFactory _jsonConfigRuleFactory;
        private readonly IReportRuleFactory _modelRuleFactory;

        public DefaultReportRuleFactory(ReportJsonConfigManager reportJsonConfigManager) {
            _modelRuleFactory = new ReportModelRuleFactory();
            _jsonConfigRuleFactory = new ReportJsonConfigRuleFactory(reportJsonConfigManager);
        }

        public IReportRule CreateRule(Guid id, ReportContext context) {
            if (_modelRuleFactory.HasRule(id)) {
                return _modelRuleFactory.CreateRule(id, context);
            }

            if (_jsonConfigRuleFactory.HasRule(id)) {
                return _jsonConfigRuleFactory.CreateRule(id, context);
            }

            throw new ReportRuleNotFoundException($"report with rule id {id} not found in system.");
        }

        public ReportScheme GetRuleScheme(Guid id) {
            if (_modelRuleFactory.HasRule(id)) {
                return _modelRuleFactory.GetRuleScheme(id);
            }

            if (_jsonConfigRuleFactory.HasRule(id)) {
                return _jsonConfigRuleFactory.GetRuleScheme(id);
            }

            throw new ReportRuleNotFoundException($"report with rule id {id} not found in system.");
        }

        public bool HasRule(Guid id) {
            return _modelRuleFactory.HasRule(id) || _jsonConfigRuleFactory.HasRule(id);
        }
    }
}