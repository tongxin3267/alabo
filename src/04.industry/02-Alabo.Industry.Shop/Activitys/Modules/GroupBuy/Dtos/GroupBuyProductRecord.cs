using System.Collections.Generic;

namespace Alabo.Industry.Shop.Activitys.Modules.GroupBuy.Dtos {

    /// <summary>
    ///     Class GroupBuyProductRecord.
    ///     商品拼团记录
    /// </summary>
    public class GroupBuyProductRecord {

        /// <summary>
        ///     Gets or sets the total count.
        ///     总拼团人员
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        ///     Gets or sets the 活动 record identifier.
        ///     活动记录Id
        /// </summary>
        public long ActivityRecordId { get; set; }

        /// <summary>
        ///     Gets or sets the remain count.
        ///     剩余人数
        /// </summary>
        public long RemainCount { get; set; }

        /// <summary>
        ///     Gets or sets the remain time.
        ///     剩余时间
        /// </summary>
        public double RemainTime { get; set; }

        /// <summary>
        ///     Gets or sets the end time.
        /// </summary>
        /// <value>
        ///     The end time.
        /// </value>
        public string EndTime { get; set; }

        /// <summary>
        ///     Gets or sets the users.
        ///     拼团用户
        /// </summary>
        public IList<GroupBuyRecordUser> Users { get; set; } = new List<GroupBuyRecordUser>();
    }

    /// <summary>
    ///     Class ProductRecordUser.
    /// </summary>
    public class GroupBuyRecordUser {

        /// <summary>
        ///     Gets or sets the is father.
        ///     是否为拼主
        /// </summary>
        public bool IsFather { get; set; }

        /// <summary>
        ///     Gets or sets 会员Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        ///     Gets or sets the name of the 会员.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the avator.
        /// </summary>
        public string Avator { get; set; }

        /// <summary>
        ///     Gets or sets the activity record identifier.
        /// </summary>
        /// <value>
        ///     The activity record identifier.
        /// </value>
        public long ActivityRecordId { get; set; }

        /// <summary>
        ///     Gets or sets the end time.
        /// </summary>
        /// <value>
        ///     The end time.
        /// </value>
        public string EndTime { get; set; }

        /// <summary>
        ///     Gets or sets the remain count.
        ///     剩余人数
        /// </summary>
        public long RemainCount { get; set; }
    }
}