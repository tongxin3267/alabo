using System;

namespace Alabo.App.Core.Reports {

    public class ReportColumnAttribute : Attribute {

        public ReportColumnAttribute(string name) {
            Name = name;
        }

        public string Name { get; }

        public string Text { get; set; }

        public string Summary { get; set; }

        public string Format { get; set; }

        public int Order { get; set; } = 1000;
    }
}