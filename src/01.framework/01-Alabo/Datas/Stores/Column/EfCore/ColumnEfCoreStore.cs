using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Reflections;

namespace Alabo.Datas.Stores.Column.EfCore {

    public abstract class ColumnEfCoreStore<TEntity, TKey> : ColumnAsyncEfCoreStore<TEntity, TKey>,
        IColumnStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity {

        protected ColumnEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public object GetFieldValue(object id, string field) {
            if (id.IsNullOrEmpty() || field.IsNullOrEmpty()) {
                throw new ValidException("Id或者字段设置不能为空");
            }

            var find = GetSingle(id);
            var value = field.GetPropertyValue(find);
            return value;
        }
    }
}