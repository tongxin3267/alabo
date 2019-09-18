namespace Alabo.Apps
{
    /// <summary>
    ///     Class CompilerResult.
    /// </summary>
    public class CompilerResult
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="CompilerResult" /> class.
        /// </summary>
        /// <param name="success">if set to <c>true</c> [success].</param>
        /// <param name="message">The message.</param>
        internal CompilerResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        /// <summary>
        ///     Gets a value indicating whether this <see cref="CompilerResult" /> is success.
        /// </summary>
        public bool Success { get; }

        /// <summary>
        ///     Gets the message.
        /// </summary>
        public string Message { get; }
    }
}