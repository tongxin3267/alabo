using System;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.App.Core.Reports {

    public class ReportScheme {

        internal ReportScheme(Guid id, string name, string summary, IEnumerable<ReportColumn> source) {
            Id = id;
            Name = name;
            Summary = summary;
            Columns = source.ToArray();
        }

        public Guid Id { get; }

        /// <summary>
        ///     图表名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        ///     副标题
        /// </summary>
        public string Summary { get; set; }

        public ReportColumn[] Columns { get; }
    }
}