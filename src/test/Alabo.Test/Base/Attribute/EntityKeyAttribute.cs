using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace Alabo.Test.Base.Attribute
{
    [DataDiscoverer("Xunit.Sdk.InlineDataDiscoverer", "xunit.core")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class EntityKeyAttribute : DataAttribute
    {
        private readonly object[] data;

        /// <summary>
        ///     Initializes a new instance of the <see cref="InlineDataAttribute" /> class.
        /// </summary>
        /// <param name="data">The data values to pass to the theory.</param>
        public EntityKeyAttribute(params object[] data)
        {
            this.data = new object[] {10};
        }

        /// <inheritdoc />
        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            // This is called by the WPA81 version as it does not have access to attribute ctor params
            return new[] {data};
        }
    }
}