using Alabo.Datas.Ef.Map;

namespace Alabo.Datas.Ef.SqlServer {

    /// <summary>
    ///     实体映射配置
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class MsSqlEntityMap<TEntity> : MapBase<TEntity>, IMsSqlMap where TEntity : class {
    }
}