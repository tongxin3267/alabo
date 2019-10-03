using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories.Exception;
using Alabo.Domains.Repositories.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace Alabo.Domains.Repositories.EFCore.Context
{
    /// <summary>
    ///     Class EntityFrameworkRepositoryContext.
    /// </summary>
    public class EfCoreRepositoryContext : IRepositoryContext
    {
        /// <summary>
        ///     The context
        /// </summary>
        private readonly DbContext _context;

        /// <summary>
        ///     The transaction
        /// </summary>
        private IDbContextTransaction _transaction;

        /// <summary>
        ///     The transaction count
        /// </summary>
        private int _transactionCount;

        public UnitOfWorkBase _unitOfWork;

        public EfCoreRepositoryContext(UnitOfWorkBase unitOfWork)
        {
            ConnectionString = unitOfWork.ConnectionString;
            _unitOfWork = unitOfWork;
            _context = _unitOfWork;
        }

        /// <summary>
        ///     Gets the database context.
        /// </summary>
        public object DbContext => _context;

        /// <summary>
        ///     Gets the connection string.
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        ///     Gets a value indicating whether this instance is in transaction.
        /// </summary>
        public bool IsInTransaction => _transactionCount > 0;

        public UnitOfWorkBase UnitOfWork => _unitOfWork;

        public IQueryable<T> Query<T>() where T : class
        {
            return Set<T>();
        }

        /// <summary>
        ///     Saves the changes.
        /// </summary>
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        ///     Begins the transaction.
        /// </summary>
        public void BeginTransaction()
        {
            RaseExceptionIfConnectionIsNotInitialization();
            if (_transactionCount <= 0) {
                _transaction = _context.Database.BeginTransaction();
            } else {
                _transactionCount++;
            }
        }

        /// <summary>
        ///     Begins the transaction.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            RaseExceptionIfConnectionIsNotInitialization();
            if (_transactionCount <= 0) {
                _transaction = _context.Database.BeginTransaction(isolationLevel);
            } else {
                _transactionCount++;
            }
        }

        /// <summary>
        ///     Commits the transaction.
        /// </summary>
        public void CommitTransaction()
        {
            RaseExceptionIfConnectionIsNotInitialization();
            if (_transactionCount > 0) {
                _transactionCount--;
            } else {
                _transaction.Commit();
            }
        }

        /// <summary>
        ///     Rollbacks the transaction.
        /// </summary>
        public void RollbackTransaction()
        {
            RaseExceptionIfConnectionIsNotInitialization();
            if (_transactionCount > 0) {
                _transactionCount--;
            } else {
                _transaction.Rollback();
            }
        }

        /// <summary>
        ///     Disposes the transaction.
        /// </summary>
        public void DisposeTransaction()
        {
            RaseExceptionIfConnectionIsNotInitialization();
            if (_transactionCount > 0)
            {
                _transactionCount--;
            }
            else
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        /// <summary>
        ///     通过SQl方式打开事物
        /// </summary>
        public IRepositoryTransaction OpenTransaction()
        {
            RaseExceptionIfConnectionIsNotInitialization();
            var sqlServerRepositoryContext = new SqlServerRepositoryContext(ConnectionString);
            return sqlServerRepositoryContext.OpenTransaction();
        }

        /// <summary>
        ///     Transations the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        public bool Transation(Action action)
        {
            var sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            var sqlTransaction = sqlConnection.BeginTransaction();
            var sqlCommand = new SqlCommand
            {
                Connection = sqlConnection,
                Transaction = sqlTransaction
            };
            try
            {
                action();
                sqlTransaction.Commit();
                return true;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                sqlTransaction.Rollback();
                return false;
            }
            finally
            {
                sqlTransaction.Dispose();
                sqlCommand.Dispose();
            }
        }

        public void Close()
        {
        }

        /// <summary>
        ///     更新s the specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <param name="action">The action.</param>
        public void Update<T>(Expression<Func<T, bool>> predicate, Action<T> action)
            where T : class
        {
            var query = Query<T>();
            if (predicate != null) {
                query = query.Where(predicate);
            }

            var source = query.ToList();
            foreach (var item in source) {
                action(item);
            }

            SaveChanges();
        }

        public DbSet<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }

        private void RaseExceptionIfConnectionIsNotInitialization()
        {
            if (_context == null) {
                throw new RepositoryContextException("sqlite connection is not initialization.");
            }
        }
    }
}