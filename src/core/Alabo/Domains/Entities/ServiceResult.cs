using System.Collections.Generic;

namespace Alabo.Domains.Entities
{
    public class ServiceResult
    {
        public ServiceResult()
        {
        }

        /// <summary>
        ///     构建返回值
        /// </summary>
        /// <param name="succeeded">失败或成功，成功则为true,失败则为false</param>
        /// <param name="errorMessages">失败信息</param>
        /// <param name="returnMessage">需要返回的信息，比如返回登录成功后，返回UserId</param>
        public ServiceResult(bool succeeded, IEnumerable<string> errorMessages = null, string returnMessage = null)
        {
            Succeeded = succeeded;
            ErrorMessages = errorMessages;
            ReturnMessage = returnMessage;
        }

        public bool Succeeded { get; }

        public IEnumerable<string> ErrorMessages { get; }

        public string ReturnMessage { get; }

        public static ServiceResult Success { get; } = new ServiceResult(true);

        public static ServiceResult Failed { get; } = new ServiceResult(false);

        /// <summary>
        ///     Gets or sets the return object.
        ///     返回对象
        /// </summary>
        public object ReturnObject { get; set; }

        /// <summary>
        ///     返回当前数据的自增Id
        /// </summary>
        public object Id { get; set; } = 0;

        public override string ToString()
        {
            if (ErrorMessages == null) {
                return string.Empty;
            }

            return string.Join("<br />", ErrorMessages);
        }

        public static ServiceResult FailedWithMessage(string message)
        {
            return FailedWithMessage(new[] {message});
        }

        public static ServiceResult SuccessWithObject(object resultObject)
        {
            var result = Success;
            result.ReturnObject = resultObject;
            return result;
        }

        public static ServiceResult FailedWithMessage(IEnumerable<string> messages)
        {
            return new ServiceResult(false, messages);
        }

        public static ServiceResult FailedMessage(IEnumerable<string> messages)
        {
            return new ServiceResult(false, messages);
        }

        public static ServiceResult FailedMessage(string v)
        {
            return new ServiceResult(false, returnMessage: v, errorMessages: new List<string> {v});
        }
    }
}