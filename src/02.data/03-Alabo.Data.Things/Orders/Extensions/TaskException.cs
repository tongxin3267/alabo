using System;

namespace Alabo.App.Core.Tasks.Extensions {

    public class TaskException : Exception {

        public TaskException(Type taskType, string message)
            : base(message) {
            TaskType = taskType;
        }

        public Type TaskType { get; }
    }
}