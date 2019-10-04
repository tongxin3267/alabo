using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Reflections;

namespace Alabo.Datas.Stores.Column.Mongo {

    public abstract class ColumnMongoStore<TEntity, TKey> : ColumnAsyncMongoStore<TEntity, TKey>,
        IColumnStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity {

        protected ColumnMongoStore(IUnitOfWork unitOfWork) : base(unitOfWork) {
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