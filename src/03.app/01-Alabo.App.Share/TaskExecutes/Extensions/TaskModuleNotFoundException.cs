using System;

namespace Alabo.App.Core.Tasks.Extensions {

    public class TaskModuleNotFoundException : Exception {

        public TaskModuleNotFoundException(string message)
            : base(message) {
        }

        public TaskModuleNotFoundException(string message, Exception innerException)
            : base(message, innerException) {
        }
    }
}