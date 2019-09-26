using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Alabo.Data.Things.Orders.ResultModel {

    public class TaskQueueParameter {

        public TaskQueueParameter() {
        }

        internal TaskQueueParameter(IDictionary<string, TaskQueueParameterItem> parameters) {
            Parameters = parameters;
        }

        internal IDictionary<string, TaskQueueParameterItem> Parameters { get; } =
            new Dictionary<string, TaskQueueParameterItem>();

        public TaskQueueParameterItem this[string name] {
            get {
                if (!Parameters.ContainsKey(name)) {
                    throw new ArgumentOutOfRangeException(nameof(name));
                }

                return Parameters[name];
            }
        }

        public void Add(string name, object value) {
            if (Parameters.ContainsKey(name)) {
                throw new ArgumentException($"data with key {name} is in parameter.");
            }

            Parameters.Add(name, new TaskQueueParameterItem {
                Value = value,
                Type = value.GetType()
            });
        }

        public object GetValue(string name) {
            if (!Parameters.TryGetValue(name, out var item)) {
                throw new KeyNotFoundException($"value with key {name} not found.");
            }

            var value = item.Value;
            if (item.Type.IsClass && item.Type != typeof(string)) {
                value = JsonConvert.DeserializeObject(value.ToString(), item.Type);
            }

            return Convert.ChangeType(value, item.Type);
        }

        public T GetValue<T>(string name) {
            var value = GetValue(name);
            if (value.GetType() != typeof(T)) {
                throw new ArgumentException(
                    $"value with key {name} of type {typeof(T).Name} not equals data type {value.GetType().Name}");
            }

            if (value.GetType() != typeof(T)) {
                value = Convert.ChangeType(value, typeof(T));
            }

            return (T)value;
        }

        public bool TryGetValue<T>(string name, out T value) {
            if (!Parameters.TryGetValue(name, out var item)) {
                value = default(T);
                return false;
            }

            if (item.Type != typeof(T)) {
                value = default(T);
                return false;
            }

            var res = Convert.ChangeType(item.Value, item.Type);
            value = (T)res;
            return true;
        }

        public string[] GetKeys() {
            return Parameters.Keys.ToArray();
        }
    }

    public class TaskQueueParameterItem {
        public Type Type { get; set; }
        public object Value { get; set; }
    }
}