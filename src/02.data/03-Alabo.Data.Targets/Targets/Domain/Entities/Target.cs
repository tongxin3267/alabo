using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.Data.Targets.Targets.Domain.Enums;
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
    [ClassProperty(Name = "目标", Description = "极简单目标管理,小步快跑", GroupName = "基本设置,高级选项")]
    public class Target : AggregateMongodbRoot<Target>
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
        [Field(ControlsType = ControlsType.TextBox, SortOrder = 1, ListShow = true, GroupTabId = 1)]
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
        /// 安排人
        /// </summary>
        [Display(Name = "安排人")]
        public long ArrangeUserId { get; set; }

        /// <summary>
        /// 处理人
        /// </summary>
        [Display(Name = "处理人")]
        public long HandlerUserId { get; set; }

        /// <summary>
        /// 检视人
        /// </summary>
        [Display(Name = "检视人")]
        public long AuditorUserId { get; set; }

        /// <summary>
        /// 满意度
        /// </summary>
        [Display(Name = "满意度")]
        [Field(ControlsType = ControlsType.Decimal, SortOrder = 10, ListShow = true, GroupTabId = 2)]
        [HelpBlock("任务满意度")]
        public decimal Satisfaction { get; set; } = 0m;

        /// <summary>
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        [Field(ControlsType = ControlsType.RadioButton, SortOrder = 10, ListShow = true, GroupTabId = 2)]
        public TargetType Type { get; set; } = TargetType.Internal;

        /// <summary>
        /// 预估工时
        /// </summary>
        [Display(Name = "预估工时")]
        [Field(ControlsType = ControlsType.RadioButton, SortOrder = 10, ListShow = true, GroupTabId = 2)]
        [HelpBlock("该目标完成的预估工时，有任务完成人员评价")]
        public decimal EstimateWorkingHours { get; set; }

        /// <summary>
        /// 实际工时
        /// </summary>
        [Display(Name = "实际工时")]
        [Field(ControlsType = ControlsType.RadioButton, SortOrder = 10, ListShow = true, GroupTabId = 2)]
        [HelpBlock("该目标实际完成的工时，由任务检视人员填写")]
        public decimal ActualWorkingHours { get; set; }

        /// <summary>
        /// 阶段
        /// </summary>
        [Display(Name = "阶段")]
        [Field(ControlsType = ControlsType.RadioButton, SortOrder = 10, ListShow = true, GroupTabId = 2)]
        public TargetPhase Phase { get; set; } = TargetPhase.NotBegin;

        /// <summary>
        /// 难度分级
        /// </summary>
        [Display(Name = "难度分级")]
        [Field(ControlsType = ControlsType.RadioButton, SortOrder = 10, ListShow = true, GroupTabId = 2)]
        [HelpBlock("一星目标:无难度,助理或实习生可完成<br/>二星目标：会做,初级专员可完成<br/>三星目标：知道做,中级专员可完成<br/>四星目标：安排做,高级专员可完成<br/>五星目标：规划做,难度最高,总监级或架构师级")]
        public DifficultyClassification Difficulty { get; set; } = DifficultyClassification.TwoStar;
    }
}