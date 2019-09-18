using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Alabo.Datas.Ef.Map;
using Alabo.Domains.Entities.Core;

namespace Alabo.Datas.Ef.PgSql
{
    /// <summary>
    ///     聚合根映射配置
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class PgSqlAggregateRootMap<TEntity> : MapBase<TEntity>, IPgSqlMap where TEntity : class, IVersion
    {
        /// <summary>
        ///     映射乐观离线锁
        /// </summary>
        protected override void MapVersion(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(t => t.Version).IsConcurrencyToken();
        }
    }
}