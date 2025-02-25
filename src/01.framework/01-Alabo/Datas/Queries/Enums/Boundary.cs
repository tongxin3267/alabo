﻿using Alabo.Web.Mvc.Attributes;

namespace Alabo.Datas.Queries.Enums {

    /// <summary>
    ///     查询边界
    /// </summary>
    [ClassProperty(Name = "查询边界")]
    public enum Boundary {

        /// <summary>
        ///     包含左边
        /// </summary>
        Left,

        /// <summary>
        ///     包含右边
        /// </summary>
        Right,

        /// <summary>
        ///     包含两边
        /// </summary>
        Both,

        /// <summary>
        ///     不包含
        /// </summary>
        Neither
    }
}