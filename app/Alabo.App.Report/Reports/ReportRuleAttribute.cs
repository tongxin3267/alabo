using System;

namespace Alabo.App.Core.Reports {

    [AttributeUsage(AttributeTargets.Class)]
    public class ReportRuleAttribute : Attribute {

        public ReportRuleAttribute(string id, Type modelType) {
            if (!Guid.TryParse(id, out var guid)) {
                throw new ArgumentException($"{nameof(id)} is not guid string.");
            }

            Id = guid;
            ModelType = modelType;
        }

        public Guid Id { get; }

        public Type ModelType { get; }

        /// <summary>
        ///     图标名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     副标题
        /// </summary>
        public string Summary { get; set; }
    }
}