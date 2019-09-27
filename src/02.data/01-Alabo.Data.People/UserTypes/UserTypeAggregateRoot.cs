using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Alabo.AutoConfigs;
using Alabo.Datas.Queries.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Validations;
using Alabo.Validations.Attributes;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;

namespace Alabo.Data.People.UserTypes
{
    /// <summary>
    /// 用户类型相关聚合根
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    ///   <typeparam name="TConfig">用户等级</typeparam>
    public abstract class UserTypeAggregateRoot<TEntity, TConfig> : AggregateMongodbUserRoot<TEntity>
        where TEntity : IAggregateRoot
    {
        /// <summary>
        ///     名称
        /// </summary>
        [Display(Name = "名称")]
        [Field(ControlsType = ControlsType.TextBox, IsMain = true, EditShow = true, Width = "150", IsShowAdvancedSerach = true, IsShowBaseSerach = true, ListShow = true, SortOrder = 1, Operator = Operator.Contains)]
        [Name60]
        public string Name { get; set; }

        /// <summary>
        /// 所属区域
        /// </summary>
        [Display(Name = "所属区域")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ListShow = false, SortOrder = 1, Width = "150", LabelColor = LabelColor.Brand, ControlsType = ControlsType.CityDropList)]
        public ObjectId RegionId { get; set; }

        /// <summary>
        /// 组件图片
        /// 多张图片
        /// </summary>
        [Display(Name = "图片列表")]
        public List<string> Images { get; set; } = new List<string>();

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [Display(Name = "最后更新时间")]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [Display(Name = "简介")]
        public string Intro { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        [Display(Name = "级别")]
        public LevelType Level { get; set; }

        /// <summary>
        /// 发布状态
        /// </summary>
        public PublishStatus Status { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// 字段
        /// </summary>
        public long SortOrder { get; set; }
    }
}