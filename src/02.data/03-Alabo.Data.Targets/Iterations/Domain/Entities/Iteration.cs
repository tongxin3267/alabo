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
        [StringLength(50, ErrorMessage = ErrorMessage.MaxStringLength)]
        [Field(ListShow = true, IsShowBaseSerach = true, SortOrder = 1, ControlsType = ControlsType.TextBox)]
        [HelpBlock("输入迭代名称,长度不超过50个字")]
        public string Name { get; set; }

        /// <summary>
        ///     用户
        /// </summary>
        [Display(Name = "负责人")]
        [Field(IsShowBaseSerach = true, IsShowAdvancedSerach = true, PlaceHolder = "请输入用户名", Link = "/Admin/User/Edit?id=[[UserId]]", ControlsType = ControlsType.TextBox,
            Width = "180", ListShow = true, SortOrder = 2)]
        [NotMapped]
        [BsonIgnore]
        public new string UserName { get; set; }

        /// <summary>
        /// 迭代阶段
        /// </summary>
        [Display(Name = "阶段")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ListShow = true, IsTabSearch = true, SortOrder = 3, ControlsType = ControlsType.RadioButton,
            DataSourceType = typeof(IterationPhase))]
        [HelpBlock("迭代阶段")]
        public IterationPhase Phase { get; set; } = IterationPhase.NotBegin;

        /// <summary>
        /// 开始时间
        /// </summary>

        [Display(Name = "开始时间")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ListShow = false, SortOrder = 5, ControlsType = ControlsType.DateTimePicker)]
        [HelpBlock("迭代预计开始时间")]
        public DateTime StartTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 开始时间
        /// </summary>

        [Display(Name = "结束时间")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ListShow = false, SortOrder = 6, ControlsType = ControlsType.DateTimePicker)]
        [HelpBlock("迭代结束时间时间")]
        public DateTime EndTime { get; set; } = DateTime.Now.AddDays(30);

        /// <summary>
        /// 迭代名称
        /// </summary>
        [Display(Name = "迭代目标")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ListShow = false, SortOrder = 10, ControlsType = ControlsType.TextArea)]
        public string Intro { get; set; }
    }
}