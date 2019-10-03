using System;
using ZKCloud.Open.ApiBase.Models;

namespace Alabo.Framework.Tasks.Queues.Models
{
    public class ExecuteResult<T> : ExecuteResult
    {
        protected ExecuteResult()
        {
        }

        public T Result { get; protected set; }

        public static ExecuteResult<T> Success(T result, string message = null)
        {
            return new ExecuteResult<T>
            {
                Result = result,
                Message = message,
                Status = ResultStatus.Success,
                Exception = null
            };
        }

        public new static ExecuteResult<T> Success(string message = null)
        {
            return new ExecuteResult<T>
            {
                Result = default,
                Message = message,
                Status = ResultStatus.Success,
                Exception = null
            };
        }

        public new static ExecuteResult<T> Cancel(string message = null)
        {
            return new ExecuteResult<T>
            {
                Result = default,
                Message = message,
                Status = ResultStatus.Cancel,
                Exception = null
            };
        }

        public static ExecuteResult<T> Cancel(T result, string message = null)
        {
            return new ExecuteResult<T>
            {
                Result = result,
                Message = message,
                Status = ResultStatus.Cancel,
                Exception = null
            };
        }

        public new static ExecuteResult<T> Fail(string message)
        {
            return new ExecuteResult<T>
            {
                Status = ResultStatus.Failure,
                Message = message,
                Exception = null
            };
        }

        public new static ExecuteResult<T> Error(Exception exception)
        {
            if (exception == null) {
                throw new ArgumentNullException(nameof(exception));
            }

            return new ExecuteResult<T>
            {
                Status = ResultStatus.Error,
                Message = exception.Message,
                Exception = exception
            };
        }
    }
}