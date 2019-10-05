using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Data.Targets.Iterations.Domain.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Data.Targets.Iterations.Domain.Entities
{
    /// <summary>
    ///     迭代
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Target_Iteration")]
    [ClassProperty(Name = "迭代", Description = "目标迭代")]
    public class Iteration : AggregateMongodbUserRoot<Iteration>
    {
        /// <summary>
        /// 迭代名称
        /// </summary>
        [Display(Name = "迭代名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ListShow = true, SortOrder = 1, ControlsType = ControlsType.TextBox)]
        public string Name { get; set; }

        /// <summary>
        /// 迭代阶段
        /// </summary>
        [Display(Name = "阶段")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ListShow = true, SortOrder = 1, ControlsType = ControlsType.RadioButton, DataSourceType = typeof(IterationPhase))]
        public IterationPhase Phase { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>

        [Display(Name = "开始时间")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ListShow = false, SortOrder = 5, ControlsType = ControlsType.DateTimePicker)]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>

        [Display(Name = "结束时间")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ListShow = false, SortOrder = 6, ControlsType = ControlsType.DateTimePicker)]
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 迭代名称
        /// </summary>
        [Display(Name = "迭代目标")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ListShow = false, SortOrder = 10, ControlsType = ControlsType.TextBox)]
        public string Intro { get; set; }
    }
}