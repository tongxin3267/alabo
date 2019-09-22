﻿using System;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Query.Dto;

namespace Alabo.App.Core.Finance.Domain.Dtos.Bill {

    /// <summary>
    ///     Api 查询参数
    /// </summary>
    public class BillApiInput : ApiInputDto {

        /// <summary>
        ///     Gets or sets Id标识
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        ///     Gets or sets the money 类型 identifier.
        /// </summary>
        public Guid? MoneyTypeId { get; set; }

        /// <summary>
        ///     Gets or sets the flow.
        /// </summary>
        public AccountFlow? Flow { get; set; }

        /// <summary>
        ///     Gets or sets the 类型.
        /// </summary>
        public BillActionType? Type { get; set; } = null;
    }
}