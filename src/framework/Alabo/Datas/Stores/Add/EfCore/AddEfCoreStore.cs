using System;
using System.Collections.Generic;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Alabo.Validations.Aspects;

namespace Alabo.Datas.Stores.Add.EfCore
{
    public abstract class AddEfCoreStore<TEntity, TKey> : GetListAsyncEfCoretStore<TEntity, TKey>,
        IAddStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        public AddEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        ///     添加单个实体
        /// </summary>
        /// <param name="entity"></param>
        public bool AddSingle([Valid] TEntity entity)
        {
            if (entity == null) {
                throw new ArgumentNullException(nameof(entity));
            }

            UnitOfWork.Add(entity);
            var count = UnitOfWork.SaveChanges();
            if (count > 0) {
                return true;
            }

            return false;
        }

        /// <summary>
        ///     批量添加
        /// </summary>
        /// <param name="soucre"></param>
        public void AddMany(IEnumerable<TEntity> soucre)
        {
            if (soucre == null) {
                throw new ArgumentNullException(nameof(soucre));
            }

            UnitOfWork.AddRange(soucre);
            var count = UnitOfWork.SaveChanges();
        }
    }
}