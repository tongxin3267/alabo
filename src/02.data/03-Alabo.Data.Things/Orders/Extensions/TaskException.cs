using System;

namespace Alabo.Data.Things.Orders.Extensions {

    public class TaskException : Exception {

        public TaskException(Type taskType, string message)
            : base(message) {
            TaskType = taskType;
        }

        public Type TaskType { get; }
    }
}