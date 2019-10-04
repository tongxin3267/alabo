using Alabo.Datas.Ef.Map;
using Alabo.Domains.Entities.Core;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alabo.Datas.Ef.MySql {

    /// <summary>
    ///     聚合根映射配置
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public abstract class MySqlAggregateRootMap<TEntity> : MapBase<TEntity>, IMySqlMap where TEntity : class, IVersion {

        /// <summary>
        ///     映射乐观离线锁
        /// </summary>
        protected override void MapVersion(EntityTypeBuilder<TEntity> builder) {
            //  builder.Property(t => t.Version).IsConcurrencyToken();
        }
    }
}