using Alabo.Aspects;
using System;
using System.Threading.Tasks;

namespace Alabo.Datas.UnitOfWorks {

    /// <summary>
    ///     工作单元
    /// </summary>
    [Ignore]
    public interface IUnitOfWork : IDisposable {

        /// <summary>
        ///     跟踪Id号
        /// </summary>
        Guid TraceId { get; set; }

        /// <summary>
        ///     提交,返回影响的行数
        /// </summary>
        int Commit();

        /// <summary>
        ///     提交,返回影响的行数
        /// </summary>
        Task<int> CommitAsync();
    }
}