using System;

namespace Alabo.Mapping.Dynamic {

    /// <summary>
    ///     使用动态更新时，忽略该属性，一般不需要更改的字段请设置改属性，比如主键Id,UserId，添加时间等
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DynamicIgnoreAttribute : Attribute {
    }
}