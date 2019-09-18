using System;
using System.Collections.Generic;
using Alabo.Domains.Enums;

namespace Alabo.App.Core.UserType.ViewModels {

    public class UserGradeClass {
        public Guid Id { get; set; }

        public Guid UserTypeId { get; set; }

        /// <summary>
        ///     等级名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     升级升级点
        /// </summary>
        public long Contribute { get; set; } = 0;

        /// <summary>
        ///     升级价格
        /// </summary>
        public decimal Price { get; set; } = 0;

        /// <summary>
        ///     分润基数
        /// </summary>
        public decimal Radix { get; set; } = 0;

        public string GradePrivileges { get; set; }

        /// <summary>
        ///     默认用户等级
        /// </summary>

        public bool IsDefault { get; set; } = false;

        public string Icon { get; set; }

        public string Equity { get; set; }

        public Status Status { get; set; }

        public long SortOrder { get; set; }

        public List<UserGradeClass> UserGradeClassList { get; set; }
    }
}