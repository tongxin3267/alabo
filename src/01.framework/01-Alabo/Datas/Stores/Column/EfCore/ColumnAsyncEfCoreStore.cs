using Alabo.Datas.Stores.Add.EfCore;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Entities.Core;
using Alabo.Exceptions;
using Alabo.Extensions;
using Alabo.Reflections;
using System.Threading.Tasks;

namespace Alabo.Datas.Stores.Column.EfCore
{
    public abstract class ColumnAsyncEfCoreStore<TEntity, TKey> : AddAsyncEfCoreStore<TEntity, TKey>,
        IColumnAsyncStore<TEntity, TKey>
        where TEntity : class, IKey<TKey>, IVersion, IEntity
    {
        protected ColumnAsyncEfCoreStore(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<object> GetFieldValueAsync(object id, string field)
        {
            if (id.IsNullOrEmpty() || field.IsNullOrEmpty()) {
                throw new ValidException("Id或者字段设置不能为空");
            }

            var find = await GetSingleAsync(id);
            var value = field.GetPropertyValue(find);
            return value;
        }
    }
}