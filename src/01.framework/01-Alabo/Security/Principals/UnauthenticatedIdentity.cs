﻿using System.Security.Claims;

namespace Alabo.Security.Principals {

    /// <summary>
    ///     未认证的身份标识
    /// </summary>
    public class UnauthenticatedIdentity : ClaimsIdentity {

        /// <summary>
        ///     未认证的身份标识实例
        /// </summary>
        public static readonly UnauthenticatedIdentity Instance = new UnauthenticatedIdentity();

        /// <summary>
        ///     是否认证
        /// </summary>
        public override bool IsAuthenticated => false;
    }
}