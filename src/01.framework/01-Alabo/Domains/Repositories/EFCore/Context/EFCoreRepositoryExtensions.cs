using System;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;
using EF = Microsoft.EntityFrameworkCore;

namespace Alabo.Domains.Repositories.EFCore.Context {

    /// <summary>
    /// </summary>
    public static class EfCoreRepositoryExtensions {

        /// <summary>
        ///     Databases the set.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context">上下文</param>
        public static EF.DbSet<T> DbSet<T>(this IRepositoryContext context)
            where T : class {
            if (!(context is EfCoreRepositoryContext)) {
                throw new InvalidCastException("can not convert context to EntityFrameworkRepositoryContext");
            }

            return (context as EfCoreRepositoryContext).Set<T>();
        }

        /// <summary>
        ///     Gets the database transaction.
        /// </summary>
        /// <param name="context">上下文</param>
        /// <exception cref="InvalidCastException">can not convert context to EntityFrameworkRepositoryContext</exception>
        public static DbTransaction GetDbTransaction(this IRepositoryContext context) {
            if (!(context is EfCoreRepositoryContext)) {
                throw new InvalidCastException("can not convert context to EntityFrameworkRepositoryContext");
            }

            var efContext = context as EfCoreRepositoryContext;
            var transaction = context.UnitOfWork.Database.CurrentTransaction;
            var filedInfo = transaction.GetType()
                .GetField("_dbTransaction", BindingFlags.NonPublic | BindingFlags.Instance);
            var instanceExpression = Expression.Constant(transaction);
            var fieldExperssion = Expression.Field(instanceExpression, filedInfo);
            var dbTransactionConvertExpression = Expression.Convert(fieldExperssion, typeof(DbTransaction));
            var lambdaExpression = Expression.Lambda<Func<DbTransaction>>(dbTransactionConvertExpression);
            return lambdaExpression.Compile()();
        }
    }
}