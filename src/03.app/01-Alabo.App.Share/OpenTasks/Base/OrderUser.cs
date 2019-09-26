using System;

namespace Alabo.App.Share.OpenTasks.Base {

    /// <summary>
    /// 交易用户、触发用户、下单用户的用户类型
    /// </summary>
    public class OrderUser {

        /// <summary>
        /// 是否限制交易用户、触发用户、下单用户的用户类型
        /// IsLimitShareUserType
        /// </summary>
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsLimitOrderUserType { get; set; } = false;

        /// <summary>
        /// 交易用户、触发用户、下单用户的用户类型
        /// </summary>
        public Guid OrderUserTypeId { get; set; }

        /// <summary>
        /// 是否限制分润用户的用户类型等级
        /// </summary>
        public bool IsLimitOrderUserGrade { get; set; } = false;

        /// <summary>
        /// 交易用户、触发用户、下单用户类型的，等级
        /// 得到收益的用户类型 对应的等级
        /// </summary>
        public Guid OrderUserGradeId { get; set; }
    }
}