using System;

namespace Alabo.Framework.Basic.Notifications {

    public class MessageQueueHandleException : Exception {

        public MessageQueueHandleException(long queueId, string message)
            : base(message) {
            QueueId = queueId;
        }

        public MessageQueueHandleException(long queueId, string message, Exception innerException)
            : base(message, innerException) {
            QueueId = queueId;
        }

        public long QueueId { get; }
    }
}