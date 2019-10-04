using Alabo.Domains.Entities;

namespace Alabo.Domains.Services.Column {

    public interface IColumn<TEntity, in TKey> where TEntity : class, IAggregateRoot<TEntity, TKey> {

        /// <summary>
        ///     获取字段的值
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="field">字段名称，区分大小写</param>
        object GetFieldValue(object id, string field);
    }
}