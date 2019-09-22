using System;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.App.Core.Reports {

    public class ReportTableRow : IReportRow, IReportModel {
        private readonly IDictionary<string, object> _tableValueDictionary = new Dictionary<string, object>();

        public bool HasColumn(string columnName) {
            return _tableValueDictionary.ContainsKey(columnName);
        }

        public T GetData<T>(string columnName) {
            TryGetData(columnName, out T result);
            return result;
        }

        public bool TryGetData<T>(string columnName, out T value) {
            var result = _tableValueDictionary.TryGetValue(columnName, out var valueObject);
            if (result) {
                value = (T)valueObject;
            } else {
                value = default(T);
            }

            return result;
        }

        public string[] GetColumns() {
            return _tableValueDictionary.Keys.ToArray();
        }

        public void AddValue(string columnName, object value) {
            if (_tableValueDictionary.ContainsKey(columnName)) {
                throw new ArgumentException($"value with key {columnName} is in table.");
            }

            _tableValueDictionary.Add(columnName, value);
        }

        public void AddOrUpdateValue(string columnName, object value) {
            _tableValueDictionary[columnName] = value;
        }

        public void SetDefault() {
        }
    }
}