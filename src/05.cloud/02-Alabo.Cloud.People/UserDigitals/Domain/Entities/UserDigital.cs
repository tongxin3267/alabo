using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Alabo.Domains.Entities;

namespace Alabo.Cloud.People.UserDigitals.Domain.Entities {

    /// <summary>
    /// 数字画像
    /// TODO 云应用 用户画像
    /// </summary>
    [Table("Cloud_People_UserDigital")]
    public class UserDigital : AggregateMongodbRoot<UserDigital> {

        /// <summary>
        /// 标签名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public long SortOrder { get; set; }
    }
}