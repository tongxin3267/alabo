using System.Collections.Generic;
using Alabo.Data.People.Users.Dtos;
using Alabo.Domains.Query.Dto;
using Alabo.Framework.Basic.Grades.Domain.Configs;

namespace Alabo.Industry.Shop.Orders.Dtos
{
    /// <summary>
    ///     订单购买数据传输
    /// </summary>
    public class OrderBuyOutput : EntityDto
    {
        /// <summary>
        ///     订单使用人民币支付的金额
        ///     是客户实际支付的金额，微信支付宝
        /// </summary>
        public decimal PayAmount { get; set; }

        /// <summary>
        ///     支付Id
        /// </summary>
        public long PayId { get; set; }

        /// <summary>
        ///     订单Id列表
        /// </summary>
        public List<long> OrderIds { get; set; } = new List<long>();

        /// <summary>
        ///     注册的用户
        /// </summary>
        public RegInput RegUser { get; set; }

        /// <summary>
        ///     购买的等级
        /// </summary>
        public UserGradeConfig BuyGrade { get; set; }
    }
}