using Alabo.Security;

namespace Alabo.Framework.Core.WebApis
{
    public class AutoBaseModel
    {
        /// <summary>
        ///     数据过滤方式
        /// </summary>
        public FilterType Filter { get; set; } = FilterType.All;

        /// <summary>
        ///     前台登录用户
        /// </summary>
        public BasicUser BasicUser { get; set; }

        /// <summary>
        ///     Url查询参数
        /// </summary>
        public object Query { get; set; }

        /// <summary>
        ///     UserManager对象
        /// </summary>
        public dynamic UserManager { get; set; }
    }

    /// <summary>
    ///     权限类型
    /// </summary>
    public enum FilterType
    {
        /// <summary>
        ///     无权限，匿名，谁都可以访问
        /// </summary>
        All = 1,

        /// <summary>
        ///     会员级别
        /// </summary>
        User = 2,

        /// <summary>
        ///     管理员级别
        /// </summary>
        Admin = 3,

        /// <summary>
        ///     供应商级别
        /// </summary>
        Store = 4,

        /// <summary>
        ///     线下店铺
        /// </summary>
        Offline = 5,

        /// <summary>
        ///     城市合伙人
        /// </summary>
        City = 6,

        /// <summary>
        ///     营销中心
        /// </summary>
        Market = 101
    }
}