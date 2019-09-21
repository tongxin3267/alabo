﻿using Alabo.App.Share.Tasks.Domain.Enum;

namespace Alabo.App.Share.Tasks.Base {

    /// <summary>
    /// 封底规则
    /// </summary>
    public class LimitRule {

        /// <summary>
        /// 封顶方式
        /// </summary>
        public LimitType LimitType { get; set; } = LimitType.None;

        /// <summary>
        /// 封顶金额
        /// </summary>
        public decimal LimitValue { get; set; }
    }
}