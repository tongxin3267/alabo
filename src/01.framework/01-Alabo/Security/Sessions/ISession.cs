﻿using Alabo.Dependency;

namespace Alabo.Security.Sessions {

    /// <summary>
    ///     用户会话
    /// </summary>
    public interface ISession : ISingletonDependency {

        /// <summary>
        ///     是否认证
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        ///     用户标识
        /// </summary>
        string UserId { get; }

        /// <summary>
        ///     租户
        /// </summary>
        string Tenant { get; }
    }
}