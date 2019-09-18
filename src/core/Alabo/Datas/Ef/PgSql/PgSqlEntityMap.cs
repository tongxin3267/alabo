using Alabo.Datas.Ef.Map;

namespace Alabo.Datas.Ef.PgSql
{
    /// <summary>
    ///     实体映射配置
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class PgSqlEntityMap<TEntity> : MapBase<TEntity>, IPgSqlMap where TEntity : class
    {
    }
}