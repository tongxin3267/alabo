using System;

namespace Alabo.App.Core.Admin.Extensions {

    /// <summary>
    /// DefaultInitSortAttribute SortIndex ASC
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DefaultInitSortAttribute : Attribute {

        /// <summary>
        /// Sort index
        /// </summary>
        public long SortIndex { get; set; } = 100;
    }
}