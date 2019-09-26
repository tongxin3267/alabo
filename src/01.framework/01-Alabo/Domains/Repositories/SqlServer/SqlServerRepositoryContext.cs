using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Alabo.Domains.Repositories.EFCore;

namespace Alabo.Domains.Repositories.SqlServer
{
    public sealed class SqlServerRepositoryContext
    {
        private int _transactionCount;

        public SqlServerRepositoryContext(string connectionString)
        {
            DbContext = new SqlConnection(connectionString);
            Open();
        }

        public string ConnectionString { get; private set; }

        public object DbContext { get; private set; }

        public bool IsInTransaction => _transactionCount > 0;

        public DbTransaction DbTransaction { get; private set; }

        public DbConnection DbConnection => DbContext as DbConnection;

        public void Open()
        {
            if (DbConnection != null) DbConnection.Open();
        }

        public IRepositoryTransaction OpenTransaction()
        {
            return new SqlServerRepositoryTransaction(this);
        }

        public void Close()
        {
            if (DbContext != null && DbContext is IDisposable)
            {
                (DbContext as IDisposable).Dispose();
                DbContext = null;
            }
        }

        public void Dispose()
        {
            Close();
        }

        public void BeginTransaction()
        {
            RaseExceptionIfConnectionIsNotInitialization();
            if (_transactionCount <= 0) DbTransaction = DbConnection.BeginTransaction();

            _transactionCount++;
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            RaseExceptionIfConnectionIsNotInitialization();
            if (_transactionCount <= 0) DbTransaction = DbConnection.BeginTransaction(isolationLevel);

            _transactionCount++;
        }

        public void CommitTransaction()
        {
            RaseExceptionIfConnectionIsNotInitialization();
            if (_transactionCount > 0) _transactionCount--;

            if (_transactionCount == 0) DbTransaction.Commit();
        }

        public void RollbackTransaction()
        {
            RaseExceptionIfConnectionIsNotInitialization();
            if (_transactionCount > 0) _transactionCount--;

            if (_transactionCount == 0) DbTransaction.Rollback();
        }

        public void DisposeTransaction()
        {
            RaseExceptionIfConnectionIsNotInitialization();
            if (_transactionCount > 0)
            {
                _transactionCount--;
            }
            else
            {
                DbTransaction.Dispose();
                DbTransaction = null;
            }
        }

        private void RaseExceptionIfConnectionIsNotInitialization()
        {
            if (DbContext == null) throw new System.Exception("sql connection is not initialization.");
        }
    }
}