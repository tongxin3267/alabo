using Alabo.Web.Mvc.Attributes;

namespace Alabo.Domains.Trees
{
    /// <summary>
    ///     加载操作
    /// </summary>
    [ClassProperty(Name = "加载操作")]
    public enum LoadOperation
    {
        /// <summary>
        ///     初次加载
        /// </summary>
        FirstLoad = 1,

        /// <summary>
        ///     加载子节点
        /// </summary>
        LoadChild = 2,

        /// <summary>
        ///     搜索
        /// </summary>
        Search = 3
    }
}