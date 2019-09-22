using System;
using System.Threading.Tasks;

namespace Alabo.Initialize {

    /// <summary>
    ///     默认数据初始
    /// </summary>
    public interface IDefaultInit {

        /// <summary>
        /// 是否租户初始化时初始
        /// </summary>
        bool IsTenant { get; }

        /// <summary>
        ///     初始化默认数据
        /// </summary>
        void Init();
    }

    /// <summary>
    /// DefaultInitSortAttribute SortIndex ASC
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DefaultInitSortAttribute : Attribute {

        /// <summary>
        /// Sort index
        /// </summary>
        public long SortIndex { get; set; } = 100;
    }
}