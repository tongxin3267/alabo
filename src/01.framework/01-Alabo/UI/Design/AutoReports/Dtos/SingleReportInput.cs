﻿using Alabo.Linq;
using Alabo.UI.Design.AutoReports.Enums;
using Alabo.Validations;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Alabo.UI.Design.AutoReports.Dtos {

    /// <summary>
    ///     单数据类型，输入模型
    /// </summary>
    public class SingleReportInput {

        /// <summary>
        ///     用来做缓存
        /// </summary>
        [JsonIgnore]
        [BsonIgnore]
        public string Id { get; set; }

        /// <summary>
        ///     实体类型,比如User,Order
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Display(Name = "实体类型")]
        public string EntityType { get; set; }

        /// <summary>
        ///     统计字段
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Display(Name = "统计字段")]
        public string Field { get; set; }

        /// <summary>
        ///     统计方式
        /// </summary>
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Display(Name = "统计方式")]
        public ReportStyle Style { get; set; }

        /// <summary>
        ///     字段查询提交
        /// </summary>
        public EntityQueryCondition Condition { get; set; }
    }
}