using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Alabo.Validations.Aspects;

namespace Alabo.Datas.Stores.Add.EfCore
{
    public abstract class AddAsyncEfCoreStore<TEntity, TKey> : AddEfCoreStore<TEntity, TKey>,
        IAddAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        public AddAsyncEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public Task AddManyAsync([Valid] IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> AddSingleAsync(TEntity entity)
        {
            if (entity == null) {
                throw new ArgumentNullException(nameof(entity));
            }

            await Set.AddAsync(entity, default);
            return true;
        }
    }
}