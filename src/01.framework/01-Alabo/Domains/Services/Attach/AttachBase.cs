using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;
using Alabo.Domains.Services.Add;
using Alabo.Runtime.Config;
using System;

namespace Alabo.Domains.Services.Attach
{
    public abstract class AttachBase<TEntity, TKey> : AddBase<TEntity, TKey>, IAttach<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        public AttachBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public string GetTableName()
        {
            throw new NotImplementedException();
            // return Store.GetTableName();
        }

        public DatabaseType GetDatabaseType()
        {
            throw new NotImplementedException();
            //return Store.GetDatabaseType();
        }
    }
}