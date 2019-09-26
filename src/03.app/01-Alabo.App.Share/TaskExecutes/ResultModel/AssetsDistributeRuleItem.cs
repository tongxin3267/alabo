﻿using System;

namespace Alabo.App.Share.TaskExecutes.ResultModel {

    public class AssetsDistributeRuleItem {

        /// <summary>
        ///     资产账户类型
        /// </summary>
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     分配比例
        /// </summary>
        public decimal Ratio { get; set; }
    }
}