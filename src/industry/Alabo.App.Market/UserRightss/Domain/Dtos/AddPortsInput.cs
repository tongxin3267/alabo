using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Alabo.Domains.Entities;

namespace Alabo.App.Market.UserRightss.Domain.Dtos
{
    /// <summary>
    /// 增加端口
    /// </summary>
    public class AddPortsInput
    {
        /// <summary>
        /// 登陆用户Id
        /// </summary>
        [Range(0, UInt64.MaxValue, ErrorMessage = "用户ID必须大于0")]
        public long LoginUserId { get; set; }

        /// <summary>
        ///     手机号码
        /// </summary>
        [Display(Name = "手机号码")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Mobile { get; set; }

        /// <summary>
        /// 标准商家
        /// </summary>
        [Range(0, UInt64.MaxValue, ErrorMessage = "数值范围必须大于0")]
        public long Grade1 { get; set; }

        /// <summary>
        /// 众享商家
        /// </summary>
        [Range(0, UInt64.MaxValue, ErrorMessage = "数值范围必须大于0")]
        public long Grade2 { get; set; }

        /// <summary>
        /// 至尊商家
        /// </summary>
        [Range(0, UInt64.MaxValue, ErrorMessage = "数值范围必须大于0")]
        public long Grade3 { get; set; }

        /// <summary>
        /// 准营销中心
        /// </summary>
        [Range(0, UInt64.MaxValue, ErrorMessage = "数值范围必须大于0")]
        public long Grade4 { get; set; }

        /// <summary>
        /// 营销中心
        /// </summary>
        [Range(0, UInt64.MaxValue, ErrorMessage = "数值范围必须大于0")]
        public long Grade5 { get; set; }
    }
}
