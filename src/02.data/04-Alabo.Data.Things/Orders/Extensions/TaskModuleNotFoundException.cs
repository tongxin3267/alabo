using System;

namespace Alabo.Data.Things.Orders.Extensions
{
    public class TaskModuleNotFoundException : Exception
    {
        public TaskModuleNotFoundException(string message)
            : base(message)
        {
        }

        public TaskModuleNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}