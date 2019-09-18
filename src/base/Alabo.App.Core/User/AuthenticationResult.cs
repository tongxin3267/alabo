using System.Collections.Generic;

namespace Alabo.App.Core.User {

    public class AuthenticationResult {

        public AuthenticationResult(bool succeeded, IEnumerable<string> errorMessages = null) {
            Succeeded = succeeded;
            ErrorMessages = errorMessages;
        }

        public bool Succeeded { get; }

        public IEnumerable<string> ErrorMessages { get; }

        public static AuthenticationResult Success { get; } = new AuthenticationResult(true);

        public static AuthenticationResult Failed { get; } = new AuthenticationResult(false);

        public override string ToString() {
            if (ErrorMessages == null) {
                return string.Empty;
            }

            return string.Join("<br />", ErrorMessages);
        }

        public static AuthenticationResult FailedWithMessage(string message) {
            return FailedWithMessage(new[] { message });
        }

        public static AuthenticationResult FailedWithMessage(IEnumerable<string> messages) {
            return new AuthenticationResult(false, messages);
        }
    }
}