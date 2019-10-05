using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Data.Targets.Targets.Domain.Entities
{
    /// <summary>
    ///     目标事务
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Target_Target")]
    [ClassProperty(Name = "目标", GroupName = "基本设置,高级选项", Description = "目标")]
    public class Target : AggregateMongodbUserRoot<Target>
    {
        /// <summary>
        /// 迭代Id
        /// </summary>
        public ObjectId IterationId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "目标名称")]
        [Required]
        [BsonRequired]
        [Field(ControlsType = ControlsType.TextArea, SortOrder = 1, ListShow = true, GroupTabId = 1)]
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        [Field(ControlsType = ControlsType.Markdown, SortOrder = 2, GroupTabId = 1)]
        public string Content { get; set; }

        /// <summary>
        /// 奖金
        /// </summary>
        [Display(Name = "奖金")]
        [Field(ControlsType = ControlsType.Decimal, SortOrder = 10, ListShow = true, GroupTabId = 2)]
        [HelpBlock("完成该任务后,由主管部分对新任务进行满意度评价,根据满意度获取奖金。比如任务奖金为1000元,任务完成后，主管部门对任务进行评价满意度为90%，则所得到的奖金为900元")]
        public decimal Bonus { get; set; } = 0m;

        /// <summary>
        /// 目标贡献值
        /// </summary>
        [Display(Name = "目标贡献值")]
        [Field(ControlsType = ControlsType.Decimal, SortOrder = 10, ListShow = true, GroupTabId = 2)]
        public decimal Contribution { get; set; } = 0m;

        /// <summary>
        /// 满意度
        /// </summary>
        [Display(Name = "满意度")]
        [Field(ControlsType = ControlsType.Decimal, SortOrder = 10, ListShow = true, GroupTabId = 2)]
        [HelpBlock("任务满意度")]
        public decimal Satisfaction { get; set; } = 0m;
    }
}