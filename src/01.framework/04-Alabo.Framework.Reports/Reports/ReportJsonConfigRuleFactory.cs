using System;
using System.Linq;

namespace Alabo.App.Core.Reports {

    public class ReportJsonConfigRuleFactory : IReportRuleFactory {
        private readonly ReportJsonConfigManager _reportJsonConfigManager;

        public ReportJsonConfigRuleFactory(ReportJsonConfigManager reportJsonConfigManager) {
            _reportJsonConfigManager = reportJsonConfigManager;
        }

        public IReportRule CreateRule(Guid id, ReportContext context) {
            var result = new ReportJsonConfigRule(id, context, _reportJsonConfigManager);
            return result;
        }

        public ReportScheme GetRuleScheme(Guid id) {
            var config = _reportJsonConfigManager.GetRuleConfig(id);
            var columns = config.Columns.Select(e => new ReportColumn {
                Name = e.Name,
                Text = e.Text,
                Format = e.Format,
                Order = e.Order,
                Summary = e.Summary,
                DataType = e.Type
            })
                .ToList();
            return new ReportScheme(id, config.Name, "", columns);
        }

        public bool HasRule(Guid id) {
            return _reportJsonConfigManager.HasRule(id);
        }
    }
}