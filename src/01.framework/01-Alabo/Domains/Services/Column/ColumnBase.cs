using Alabo.Datas.Stores;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities;

namespace Alabo.Domains.Services.Column
{
    public abstract class ColumnBase<TEntity, TKey> : ColumnAsyncBase<TEntity, TKey>, IColumn<TEntity, TKey>
        where TEntity : class, IAggregateRoot<TEntity, TKey>
    {
        /// <summary>
        ///     服务构造函数
        /// </summary>
        /// <param name="unitOfWork">工作单元</param>
        /// <param name="store">仓储</param>
        protected ColumnBase(IUnitOfWork unitOfWork, IStore<TEntity, TKey> store) : base(unitOfWork, store)
        {
        }

        public object GetFieldValue(object id, string field)
        {
            return Store.GetFieldValue(id, field);
        }
    }
}