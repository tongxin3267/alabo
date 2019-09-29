using Alabo.Domains.Repositories.EFCore;
using System;

namespace Alabo.Domains.Repositories.SqlServer
{
    public sealed class SqlServerRepositoryTransaction : IRepositoryTransaction
    {
        private readonly SqlServerRepositoryContext _context;

        public SqlServerRepositoryTransaction(SqlServerRepositoryContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            _context = context;
            _context.BeginTransaction();
        }

        public void Commit()
        {
            _context.CommitTransaction();
        }

        public void Rollback()
        {
            _context.RollbackTransaction();
        }

        public void Dispose()
        {
            _context.DisposeTransaction();
        }
    }
}