using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Query.Dto;

namespace Alabo.App.Core.User.Domain.Dtos {

    /// <summary>
    /// 推广商家的统计数据
    /// </summary>
    public class UserStatisticOutput : EntityDto {

        /// <summary>
        /// 总数量
        /// </summary>
        [Display(Name = "累计商家")]
        public long Total { get; set; }

        /// <summary>
        /// 未启用
        /// </summary>
        [Display(Name = "未启用")]
        public long UnActivated { get; set; }

        /// <summary>
        /// 今日商家
        /// </summary>
        [Display(Name = "今日商家")]
        public long Today { get; set; }

        /// <summary>
        /// 本月商家
        /// </summary>
        [Display(Name = "本月商家")]
        public long ThisMonth { get; set; }
    }
}