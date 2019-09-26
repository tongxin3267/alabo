using System.Collections.Generic;

namespace Alabo.Data.Things.Orders.ResultModel {

    public class SharedAccountParameter {

        /// <summary>
        ///     总分润金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        public List<UserParameter> UserParameters { get; set; }
    }

    public class UserParameter {

        /// <summary>
        ///     用
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     显示用
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     当前值（比如共享值）
        /// </summary>
        public decimal Amount { get; set; }
    }
}