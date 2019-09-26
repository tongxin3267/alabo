using System;
using System.Collections.Generic;
using System.Linq;

namespace Alabo.Framework.Tasks.Queues.Models {

    public class TaskParameter {
        private readonly IDictionary<string, object> _dataCache = new Dictionary<string, object>();

        private readonly IDictionary<string, Type> _dataTypeCache = new Dictionary<string, Type>();

        public void AddValue<T>(string name, T value) {
            if (_dataCache.ContainsKey(name)) {
                throw new ArgumentException($"data with key {name} is in parameter.");
            }

            _dataCache.Add(name, value);
            _dataTypeCache.Add(name, typeof(T));
        }

        public void AddValue(string name, object value) {
            if (_dataCache.ContainsKey(name)) {
                throw new ArgumentException($"data with key {name} is in parameter.");
            }

            _dataCache.Add(name, value);
            _dataTypeCache.Add(name, value.GetType());
        }

        public void SetValue<T>(string name, T value) {
            if (_dataTypeCache.ContainsKey(name)) {
                if (_dataTypeCache[name] != typeof(T)) {
                    throw new ArgumentException($"update data with key {name} error, the type not equals");
                }

                _dataCache[name] = value;
            } else {
                AddValue(name, value);
            }
        }

        public T GetValue<T>(string name) {
            var find = GetValue(name);
            if (find.GetType() != typeof(T)) {
                throw new ArgumentException(
                    $"value with key {name} of type {typeof(T).Name} not equals data type {find.GetType().Name}");
            }

            if (find.GetType() != typeof(T)) {
                find = Convert.ChangeType(find, typeof(T));
            }

            return (T)find;
        }

        public object GetValue(string name) {
            if (!_dataCache.TryGetValue(name, out var find)) {
                throw new KeyNotFoundException($"value with key {name} not found.");
            }

            return find;
        }

        public bool TryGetValue<T>(string name, out T value) {
            if (!_dataCache.TryGetValue(name, out var find)) {
                value = default(T);
                return false;
            }

            var type = _dataTypeCache[name];
            if (find.GetType() != type) {
                value = default(T);
                return false;
            }

            if (find.GetType() != typeof(T)) {
                find = Convert.ChangeType(find, typeof(T));
            }

            value = (T)find;
            return true;
        }

        public string[] GetDataKeys() {
            return _dataCache.Keys.ToArray();
        }

        public KeyValuePair<string, Type>[] GetDataKeyAndTypes() {
            return _dataTypeCache.ToArray();
        }

        public bool Exists(string name) {
            return _dataCache.ContainsKey(name);
        }
    }
}