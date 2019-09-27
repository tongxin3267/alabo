using Alabo.Datas.Enums;
using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.Add;
using System;
using System.Threading.Tasks;

namespace Alabo.Domains.Services.Attach
{
    public abstract class AttachAsyncBase<TEntity, TKey> : AddBase<TEntity, TKey>, IAttachAsync<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        protected AttachAsyncBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public Task<DatabaseType> GetDatabaseType()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetTableName()
        {
            throw new NotImplementedException();
        }
    }
}