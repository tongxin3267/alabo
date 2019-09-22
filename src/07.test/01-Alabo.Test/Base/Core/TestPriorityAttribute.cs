using System;

namespace Alabo.Test.Base.Core
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TestPriorityAttribute : System.Attribute
    {
        public TestPriorityAttribute(int priority)
        {
            Priority = priority;
        }

        public int Priority { get; }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class TestMethodAttribute : System.Attribute
    {
        public TestMethodAttribute(string method)
        {
            Mehtod = method;
        }

        public string Mehtod { get; }
    }
}