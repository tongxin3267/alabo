using Alabo.Datas.Ef.Map;

namespace Alabo.Datas.Ef.MySql {

    /// <summary>
    ///     实体映射配置
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class MySqlEntityMap<TEntity> : MapBase<TEntity>, IMySqlMap where TEntity : class {
    }
}