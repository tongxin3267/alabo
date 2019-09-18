namespace Alabo.Domains.Query.Dto
{
    /// <summary>
    ///     搜索范围
    /// </summary>
    public class RangDto
    {
        /// <summary>
        ///     字段名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     开始值
        ///     对应高级搜索中_Start中的值
        /// </summary>
        public string StartValue { get; set; }

        /// <summary>
        ///     结束值
        ///     对应高级搜索中_End中的值
        /// </summary>
        public string EndValue { get; set; }
    }

    /// <summary>
    ///     高级搜索排序方式
    /// </summary>
    public class SortTypeDto
    {
        /// <summary>
        ///     字段名称
        /// </summary>
        public string Name { get; set; } = "Id";

        /// <summary>
        ///     默认排序方式
        ///     desc 或asc
        /// </summary>
        public OrderType OrderType { get; set; } = OrderType.Descending;
    }
}