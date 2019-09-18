using System.Collections.Generic;

namespace Alabo.Domains.Entities
{
    /// <summary>
    ///     键值 对
    /// </summary>
    public class KeyValue
    {
        public KeyValue()
        {
        }

        public KeyValue(object key, object value)
        {
            Key = key;
            Value = value;
        }

        public KeyValue(object key, object value, string name)
        {
            Key = key;
            Value = value;
            Name = name;
        }

        /// <summary>
        ///     键
        /// </summary>
        public object Key { get; set; }

        /// <summary>
        ///     值
        /// </summary>
        public object Value { get; set; }

        public long SortOrder { get; set; }

        /// <summary>
        ///     图标
        /// </summary>
        public string Icon { set; get; }

        public string Name { get; set; }
    }

    public class EnumList
    {
        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     键值对
        /// </summary>
        public List<EnumKeyValue> KeyValue { get; set; } = new List<EnumKeyValue>();
    }

    public class EnumKeyValue
    {
        /// <summary>
        ///     关键字
        /// </summary>
        public object Key { get; set; }

        /// <summary>
        ///     值
        /// </summary>
        public string Value { get; set; }
    }
}