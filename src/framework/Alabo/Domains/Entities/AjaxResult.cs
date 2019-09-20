namespace Alabo.Domains.Entities
{
    public class AjaxResult
    {
        private AjaxResult()
        {
        }

        private AjaxResult(object data, string message, bool state)
        {
            Data = data;
            Message = message;
            State = state;
        }

        public bool State { get; }
        public object Data { get; }
        public string Message { get; }

        public static AjaxResult Success(string message)
        {
            return new AjaxResult(null, message, true);
        }

        public static AjaxResult Success(object data = null, string message = "")
        {
            return new AjaxResult(data, message, true);
        }

        public static AjaxResult Error(string message)
        {
            return new AjaxResult(null, message, false);
        }

        public static AjaxResult Error(object data = null, string message = "")
        {
            return new AjaxResult(data, message, false);
        }
    }
}