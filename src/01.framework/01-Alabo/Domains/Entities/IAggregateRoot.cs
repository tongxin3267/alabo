﻿using Alabo.Domains.Entities.Core;

namespace Alabo.Domains.Entities {

    /// <summary>
    ///     聚合根
    /// </summary>
    public interface IAggregateRoot : IEntity, IVersion {
    }

    /// <summary>
    ///     聚合根
    /// </summary>
    /// <typeparam name="TKey">标识类型</typeparam>
    public interface IAggregateRoot<TKey> : IEntity<TKey>, IAggregateRoot {
    }

    /// <summary>
    ///     聚合根
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TKey">标识类型</typeparam>
    public interface IAggregateRoot<in TEntity, TKey> : IEntity<TEntity, TKey>, IAggregateRoot<TKey>
        where TEntity : IAggregateRoot {
    }
}