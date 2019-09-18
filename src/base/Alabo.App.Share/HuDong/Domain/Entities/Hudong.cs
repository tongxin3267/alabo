using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Alabo.App.Open.HuDong.Domain.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;

namespace Alabo.App.Open.HuDong.Domain.Entities {

    /// <summary>
    ///
    /// </summary>
    [BsonIgnoreExtraElements]
    [Table("Hudong")]
    [ClassProperty(Name = "互动", Icon = "fa fa-cog", SortOrder = 1, SideBarType = SideBarType.LogSideBar)]
    public class Hudong : AggregateMongodbUserRoot<Hudong> {

        /// <summary>
        /// 活动名称
        /// /// </summary>
        [Display(Name = "活动名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(IsShowBaseSerach = true, SortOrder = 1, ControlsType = ControlsType.TextBox, IsMain = true,
            EditShow = true, ListShow = true, Link = "/Admin/Activitys/Edit?Key=[[Key]]&Id=[[Id]]")]
        public string Name { get; set; } = "活动名称";

        /// <summary>
        /// 所属营销活动类型，如：Alabo.App.Open.HuDong.Modules.BigWheel
        /// 大转盘
        /// </summary>
        [Display(Name = "所属营销活动类型")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        public string Key { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 可抽奖次数
        /// </summary>
        public long DrawCount { get; set; }

        /// <summary>
        /// 抽奖规则文字显示
        /// </summary>
        public string DrawRule { get; set; }

        /// <summary>
        /// 奖品列表
        /// </summary>
        public List<HudongAward> Awards { get; set; } = new List<HudongAward>();

        /// <summary>
        /// 互动设置
        /// </summary>
        public HudongSetting Setting { get; set; } = new HudongSetting();
    }

    /// <summary>
    /// 互动奖品
    /// </summary>
    public class HudongAward {
        /// <summary>
        /// 互动奖品唯一标识
        /// </summary>

        public Guid AwardId { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string img { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public string Grade { get; set; }

        /// <summary>
        /// 获奖机率
        /// </summary>
        [Display(Name = "获奖机率"), Field(ControlsType = ControlsType.Label, ListShow = true, EditShow = true)]
        public decimal Rate { get; set; }

        /// <summary>
        /// 奖项数量
        /// </summary>
        [Display(Name = "奖项数量"), Field(ControlsType = ControlsType.Label, ListShow = true, EditShow = true)]
        public long Count { get; set; }

        /// <summary>
        /// 奖品类型
        /// </summary>
        [Display(Name = "奖品类型"), Field(ControlsType = ControlsType.Label, ListShow = true, EditShow = true)]
        public HudongAwardType Type { get; set; }

        /// <summary>
        /// 奖品价值
        /// </summary>
        [Display(Name = "奖品价值"), Field(ControlsType = ControlsType.Label, ListShow = true, EditShow = true)]
        public decimal worth { get; set; }

        /// <summary>
        /// 设置说明
        /// </summary>
        [Display(Name = "设置说明"), Field(ControlsType = ControlsType.Label, ListShow = true, EditShow = true)]
        public string Intro { get; set; }
    }

    /// <summary>
    /// 互动设置
    /// </summary>
    public class HudongSetting {

        /// <summary>
        /// 奖项数量设置
        /// 如果为0的时候 :用户可以在前台自定义添加奖项数量
        /// 如果大于0时：表示奖项数量固定，用户不允许自行添加数量，数量少的时候也不能保存
        /// 比如：大装盘和前台先对应，数量必须为8个。保存大转盘设置的时候，将项数量只能为8个
        /// </summary>
        public long RewardCount { get; set; } = 0;
    }
}