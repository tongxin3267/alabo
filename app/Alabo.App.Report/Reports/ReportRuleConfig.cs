using System;
using System.Collections.Generic;

namespace Alabo.App.Core.Reports {

    public class ReportRuleConfig {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Summary { get; set; }

        public string QueryString { get; set; }

        public IList<ReportRuleConfigParameter> QueryParameters { get; set; }
            = new List<ReportRuleConfigParameter>();

        public IList<ReportRuleConfigColumn> Columns { get; set; }
            = new List<ReportRuleConfigColumn>();
    }

    public class ReportRuleConfigParameter {
        public string Name { get; set; }

        public string DefaultValue { get; set; }

        public Type Type { get; set; }
    }

    public class ReportRuleConfigColumn {
        public string Name { get; set; }

        public string Text { get; set; }

        public string Summary { get; set; }

        public string Format { get; set; }

        public Type Type { get; set; }

        public int Order { get; set; }
    }
}