using System;
using Alabo.Domains.Query.Dto;

namespace Alabo.App.Core.Finance.Domain.Dtos.Account {

    public class AccountOutput : EntityDto {

        /// <summary>
        ///     货币类型
        /// </summary>
        public Guid MoneyTypeId { get; set; }

        /// <summary>
        ///     货币量(余额)
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        ///     货币类型名称
        /// </summary>
        public string MoneyTypeName { get; set; }
    }
}