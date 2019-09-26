using System.Threading.Tasks;

namespace Alabo.Framework.Core.Reflections.Interfaces {

    /// <summary>
    /// 数据一致性检查
    /// </summary>
    public interface ICheckData {

        /// <summary>
        /// 同步方法执行数据检查
        /// </summary>
        void Execute();

        /// <summary>
        /// 异步方法执行检查
        /// </summary>
        Task ExcuteAsync();
    }
}