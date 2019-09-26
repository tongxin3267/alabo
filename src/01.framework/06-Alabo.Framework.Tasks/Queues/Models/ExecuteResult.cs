using System;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Tasks.Queues.Models {

    public class ExecuteResult {

        protected ExecuteResult() {
        }

        public ResultStatus Status { get; protected set; }

        public string Message { get; protected set; }

        public Exception Exception { get; protected set; }

        public static ExecuteResult Success(string message = null) {
            return new ExecuteResult {
                Status = ResultStatus.Success,
                Message = message,
                Exception = null
            };
        }

        public static ExecuteResult Cancel(string message = null) {
            return new ExecuteResult {
                Status = ResultStatus.Cancel,
                Message = message,
                Exception = null
            };
        }

        public static ExecuteResult Fail(string message) {
            return new ExecuteResult {
                Status = ResultStatus.Failure,
                Message = message,
                Exception = null
            };
        }

        public static ExecuteResult Error(Exception exception) {
            if (exception == null) {
                throw new ArgumentNullException(nameof(exception));
            }

            return new ExecuteResult {
                Status = ResultStatus.Error,
                Message = exception.Message,
                Exception = exception
            };
        }
    }
}