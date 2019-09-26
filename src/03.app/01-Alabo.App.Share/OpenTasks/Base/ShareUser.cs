using System;

namespace Alabo.App.Share.OpenTasks.Base {

    /// <summary>
    /// 分润用户，获取收益的用户
    /// </summary>
    public class ShareUser {

        /// <summary>
        /// 是否限制分润用户的用户类型
        /// IsLimitShareUserType
        /// </summary>
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public bool IsLimitShareUserType { get; set; } = false;

        /// <summary>
        /// 分润用户的用户类型
        /// 得到收益的用户类型
        /// </summary>
        public Guid ShareUserTypeId { get; set; }

        /// <summary>
        /// 是否限制分润用户的用户类型等级
        /// </summary>
        public bool IsLimitShareUserGrade { get; set; } = false;

        /// <summary>
        /// 交易用户、触发用户、下单用户、类型的，等级
        /// </summary>
        public Guid ShareUserGradeId { get; set; }
    }
}