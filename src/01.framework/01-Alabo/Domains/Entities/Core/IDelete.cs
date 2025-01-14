﻿namespace Alabo.Domains.Entities.Core {

    /// <summary>
    ///     逻辑删除
    /// </summary>
    public interface IDelete {

        /// <summary>
        ///     是否删除
        /// </summary>
        bool IsDeleted { get; set; }
    }
}