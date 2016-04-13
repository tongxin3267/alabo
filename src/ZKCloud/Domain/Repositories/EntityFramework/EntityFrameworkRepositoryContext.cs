using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Sqlite;
using ZKCloud.Container;
using ZKCloud.Runtime;
using ZKCloud.Runtime.Config;
using ZKCloud.Extensions;

namespace ZKCloud.Domain.Repositories.EntityFramework {
    public class EntityFrameworkRepositoryContext : IRepositoryContext {
        private class InnerDbContext : DbContext {
            public string DbType { get; private set; }

            public string ConnectionString { get; private set; }

            public InnerDbContext(string dbType, string connectionString) {
                DbType = dbType;
                ConnectionString = connectionString;
                Database.EnsureCreated();
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
                if (DbType == DatabaseTypes.MSSQL) {
                    optionsBuilder.UseSqlServer(ConnectionString);
                } else if (DbType == DatabaseTypes.SQLite) {
                    optionsBuilder.UseSqlite(ConnectionString.Replace("{{App_Data}}", RuntimeContext.Current.Path.AppDataDirectory));
                } else if (DbType == DatabaseTypes.PostgreSQL) {
                    optionsBuilder.UseNpgsql(ConnectionString);
                } else {
                    throw new NotSupportedException($"not support {DbType}.");
                }
                base.OnConfiguring(optionsBuilder);
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder) {
                var creators = ContainerManager.Default.ResolveAll<IModelCreator>();
                if (creators != null && creators.Count() > 0) {
                    foreach (var item in creators) {
                        item.CreateModel(modelBuilder);
                    }
                }
                base.OnModelCreating(modelBuilder);
            }
        }

        private InnerDbContext _context = null;

        public EntityFrameworkRepositoryContext(string dbType, string connectionString) {
            DbType = dbType;
            ConnectionString = connectionString;
            Open();
        }

        public string DbType { get; private set; }

        public string ConnectionString { get; private set; }

        private IRelationalTransaction _transaction;

        private int _transactionCount = 0;

        public object DbContext {
            get { return _context; }
        }

        public void Add<T>(T data) where T : class {
            Set<T>().Add(data);
        }

        public void Close() {
            Dispose();
        }

        public void Delete<T>(T data) where T : class {
            _context.Remove(data);
            SaveChanges();
        }

        public void Dispose() {
            if (_context != null) {
                _context.Dispose();
                _context = null;
            }
        }

        public void Open() {
            if (_context == null)
                _context = new InnerDbContext(DbType, ConnectionString);
        }

        public IQueryable<T> Query<T>() where T : class {
            return Set<T>();
        }

        public void SaveChanges() {
            _context.SaveChanges();
        }

        public void Update<T>(T data) where T : class {
            _context.Update(data);
            SaveChanges();
        }

        public DbSet<T> Set<T>() where T : class {
            return _context.Set<T>();
        }

        public void CheckContextIsInitialized() {
            if (_context == null)
                throw new InvalidOperationException("please open context first.");
        }

        public void BeginTransaction() {
            RaseExceptionIfConnectionIsNotInitialization();
            if (_transactionCount <= 0) {
                _transaction = _context.Database.BeginTransaction();
            } else {
                _transactionCount++;
            }
        }

        public void BeginTransaction(IsolationLevel isolationLevel) {
            RaseExceptionIfConnectionIsNotInitialization();
            if (_transactionCount <= 0) {
                _transaction = _context.Database.BeginTransaction(isolationLevel);
            } else {
                _transactionCount++;
            }
        }

        public void CommitTransaction() {
            RaseExceptionIfConnectionIsNotInitialization();
            if (_transactionCount > 0) {
                _transactionCount--;
            } else {
                _transaction.Commit();
            }
        }

        public void RollbackTransaction() {
            RaseExceptionIfConnectionIsNotInitialization();
            if (_transactionCount > 0) {
                _transactionCount--;
            } else {
                _transaction.Rollback();
            }
        }

        public void DisposeTransaction() {
            RaseExceptionIfConnectionIsNotInitialization();
            if (_transactionCount > 0) {
                _transactionCount--;
            } else {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        private void RaseExceptionIfConnectionIsNotInitialization() {
            if (_context == null)
                throw new RepositoryContextException("sqlite connection is not initialization.");
        }
    }
}
