using System;

namespace Alabo.Mapping.Dynamic
{
    /// <summary>
    ///     数据为空的时候，继续更新
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DynamicNotIgnoreEmptyAttribute : Attribute
    {
    }
}