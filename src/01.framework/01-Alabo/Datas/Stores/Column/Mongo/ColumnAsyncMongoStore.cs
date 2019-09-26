using Alabo.Datas.Stores.Add.Mongo;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using System;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Column.Mongo
{
    public abstract class ColumnAsyncMongoStore<TEntity, TKey> : AddAsyncMongoStore<TEntity, TKey>,
        IColumnAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected ColumnAsyncMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task<object> GetFieldValueAsync(object id, string field)
        {
            throw new NotImplementedException();
        }
    }
}