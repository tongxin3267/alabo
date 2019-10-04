using System;

namespace Alabo.Domains.Repositories.EFCore {

    public interface IRepositoryTransaction : IDisposable {

        /// <summary>
        ///     Commits this instance.
        /// </summary>
        void Commit();

        /// <summary>
        ///     Rollbacks this instance.
        /// </summary>
        void Rollback();
    }
}