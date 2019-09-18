using System;
using System.Data;
using System.Linq;
using Alabo.Datas.UnitOfWorks;

namespace Alabo.Domains.Repositories.EFCore
{
    /// <summary>
    ///     Interface IRepositoryContext
    /// </summary>
    public interface IRepositoryContext
    {
        /// <summary>
        ///     工作单元
        /// </summary>
        UnitOfWorkBase UnitOfWork { get; }

        /// <summary>
        ///     Gets the connection string.
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        ///     Gets a value indicating whether this instance is in transaction.
        /// </summary>
        bool IsInTransaction { get; }

        /// <summary>
        ///     Saves the changes.
        /// </summary>
        int SaveChanges();

        /// <summary>
        ///     Queries this instance.
        /// </summary>
        IQueryable<T> Query<T>() where T : class;

        /// <summary>
        ///     Begins the transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        ///     Begins the transaction.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        void BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        ///     Commits the transaction.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        ///     Rollbacks the transaction.
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        ///     Disposes the transaction.
        /// </summary>
        void DisposeTransaction();

        /// <summary>
        ///     Opens the transaction.
        /// </summary>
        IRepositoryTransaction OpenTransaction();

        /// <summary>
        ///     Transations the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        bool Transation(Action action);

        void Close();
    }
}