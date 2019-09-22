using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Cms.Support.Domain.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Cms.Support.Domain.Entities {

    /// <summary>
    ///     工单系统
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Cms_WorkOrder")]
    [ClassProperty(Name = "工单系统", Icon = "fa fa-puzzle-piece", Description = "工单系统", ListApi = "Api/WorkOrder/WordOrderList",
        PageType = ViewPageType.List, PostApi = "Api/WorkOrder/WordOrderList")]
    public class WorkOrder : AggregateMongodbRoot<WorkOrder> {

        /// <summary>
        ///     标题
        /// </summary>
        [Display(Name = "标题")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ControlsType = ControlsType.TextBox, Width = "80", SortOrder = 1, ListShow = true)]
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        ///     分类ID
        /// </summary>
        public int ClassId { get; set; }

        /// <summary>
        ///     问题回复
        /// </summary>
        [Display(Name = "问题回复")]
        // [Required( ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Reply { get; set; }

        /// <summary>
        ///     <summary>
        ///         问题描述
        ///     </summary>
        [Display(Name = "问题描述")]
        [Field(ControlsType = ControlsType.TextArea, Width = "80", SortOrder = 2)]
        public string Description { get; set; }

        /// <summary>
        ///     附件
        /// </summary>
        [Display(Name = "附件")]
        public string Attachment { get; set; }

        /// <summary>
        ///     回复内容
        /// </summary>
        public string ReplyContent { get; set; }

        /// <summary>
        ///     回复内容
        ///     List<ReplyContent>的Json对象
        /// </summary>
        public string ReplyComment { get; set; }

        /// <summary>
        ///     发布用户Id
        ///     工单发起人
        /// </summary>
        [Display(Name = "工单发起人")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public long UserId { get; set; }

        /// <summary>
        ///     受理人
        /// </summary>
        public long AcceptUserId { get; set; }

        /// <summary>
        ///     是否标记为星
        /// </summary>
        [Display(Name = "是否标记为星")]
        public bool IsStar { get; set; } = false;

        /// <summary>
        ///     优先级
        /// </summary>
        [Display(Name = "优先级")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public WorkOrderPriority Priority { get; set; } = WorkOrderPriority.Hight;

        /// <summary>
        ///     回复方式
        /// </summary>
        [Display(Name = "回复方式")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public WorkOrderReplyWay ReplayWay { get; set; } = WorkOrderReplyWay.PublicWay;

        /// <summary>
        ///     工单状态
        /// </summary>
        [Display(Name = "工单状态")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public WorkOrderState State { get; set; }

        /// <summary>
        ///     工单类型
        /// </summary>
        [Display(Name = "工单类型")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public WorkOrderType Type { get; set; }

        /// <summary>
        ///     服务评分
        /// </summary>
        public long ServiceSore { get; set; } = 7;

        /// <summary>
        ///     受理时间
        /// </summary>
        [Display(Name = "受理时间")]
        public DateTime AcceptTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     解决时间
        /// </summary>
        [Display(Name = "解决时间")]
        public DateTime SolveTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     确认时间
        /// </summary>
        [Display(Name = "确认时间")]
        public DateTime ConfirmTime { get; set; } = DateTime.Now;

        /// <summary>
        ///     是否公开
        /// </summary>
        [Display(Name = "是否私有")]
        public PublishWay PublishWay { get; set; } = PublishWay.Pri;
    }
}