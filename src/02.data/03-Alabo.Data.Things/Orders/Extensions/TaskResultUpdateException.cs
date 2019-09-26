using System;
using Alabo.Data.Things.Orders.ResultModel;

namespace Alabo.Data.Things.Orders.Extensions {

    /// <summary>
    ///     Class TaskResultUpdateException.
    /// </summary>
    public class TaskResultUpdateException : Exception {

        /// <summary>
        ///     Initializes a new instance of the <see cref="TaskResultUpdateException" /> class.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="message">The message.</param>
        public TaskResultUpdateException(ITaskResult result, string message)
            : base(message) {
            Result = result;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TaskResultUpdateException" /> class.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public TaskResultUpdateException(ITaskResult result, string message, Exception innerException)
            : base(message, innerException) {
            Result = result;
        }

        /// <summary>
        ///     Gets the result.
        /// </summary>
        public ITaskResult Result { get; }
    }
}