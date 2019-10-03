using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Alabo.Test.Base.Core
{
    /// <summary>
    ///     Class PriorityOrderer.
    /// </summary>
    public class PriorityOrderer : ITestCaseOrderer
    {
        /// <summary>
        ///     Orders test cases for execution.
        /// </summary>
        /// <param name="testCases">The test cases to be ordered.</param>
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
            where TTestCase : ITestCase
        {
            var sortedMethods = new SortedDictionary<int, List<TTestCase>>();
            foreach (var testCase in testCases)
            {
                var priority = 0;

                foreach (var attr in testCase.TestMethod.Method.GetCustomAttributes(typeof(TestPriorityAttribute)
                    .AssemblyQualifiedName)) {
                    priority = attr.GetNamedArgument<int>("Priority");
                }

                GetOrCreate(sortedMethods, priority).Add(testCase);
            }

            foreach (var list in sortedMethods.Keys.Select(priority => sortedMethods[priority]))
            {
                list.Sort((x, y) =>
                    StringComparer.OrdinalIgnoreCase.Compare(x.TestMethod.Method.Name, y.TestMethod.Method.Name));
                foreach (var testCase in list) {
                    yield return testCase;
                }
            }
        }

        /// <summary>
        ///     获取s the 或 create.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="key">The key.</param>
        private static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key)
            where TValue : new()
        {
            TValue result;
            if (dictionary.TryGetValue(key, out result)) {
                return result;
            }

            result = new TValue();
            dictionary[key] = result;
            return result;
        }
    }
}