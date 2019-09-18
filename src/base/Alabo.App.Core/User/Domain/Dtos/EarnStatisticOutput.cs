using System.ComponentModel.DataAnnotations;
using Alabo.Domains.Query.Dto;

namespace Alabo.App.Core.User.Domain.Dtos {

    /// <summary>
    /// 推广商家的统计数据
    /// </summary>
    public class EarnStatisticOutput : EntityDto {

        /// <summary>
        /// 总数量
        /// </summary>
        [Display(Name = "我的钱包")]
        public decimal Balance { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        [Display(Name = "累计收益")]
        public decimal Total { get; set; }

        /// <summary>
        /// 今日商家
        /// </summary>
        [Display(Name = "今日收益")]
        public decimal Today { get; set; }
    }
}