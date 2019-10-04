using System;

namespace Alabo.Apps {

    /// <summary>
    ///     Class AppConfigErrorException.
    /// </summary>
    public class AppConfigErrorException : Exception {

        /// <summary>
        ///     Initializes a new instance of the <see cref="AppConfigErrorException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public AppConfigErrorException(string message)
            : base(message) {
        }
    }
}