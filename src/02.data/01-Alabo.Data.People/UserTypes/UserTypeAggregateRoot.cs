using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Alabo.AutoConfigs;
using Alabo.Data.People.Cities.Domain.CallBacks;
using Alabo.Datas.Queries.Enums;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Framework.Core.Enums.Enum;
using Alabo.Validations;
using Alabo.Validations.Attributes;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Alabo.Data.People.UserTypes
{
    /// <summary>
    /// 用户类型相关聚合根
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class UserTypeAggregateRoot<TEntity> : AggregateMongodbUserRoot<TEntity>
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
        ///     用户
        /// </summary>
        [Display(Name = "用户名")]
        [Field(IsShowBaseSerach = true, IsShowAdvancedSerach = true, PlaceHolder = "请输入用户名", Link = "/Admin/User/Edit?id=[[UserId]]", ControlsType = ControlsType.TextBox,
            GroupTabId = 1, Width = "180", ListShow = true, EditShow = true, SortOrder = 2)]
        [NotMapped]
        [BsonIgnore]
        public new string UserName { get; set; }

        /// <summary>
        ///     用户等级Id
        ///     每个类型对应一个等级
        /// </summary>
        [Display(Name = "合伙人等级")]
        [HelpBlock("请设置合伙人等级")]
        [Field(ControlsType = ControlsType.DropdownList, GroupTabId = 1, Width = "180", ListShow = false, EditShow = true, SortOrder = 3)]
        public Guid GradeId { get; set; }

        /// <summary>
        /// 所属区域,使用区域的Id，全国区域编码
        /// 可以是省份Id，也可以是城市Id，也可以是区域Id
        /// </summary>
        [Display(Name = "所属区域")]
        [Required(AllowEmptyStrings = false, ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [Field(ListShow = false, SortOrder = 1, Width = "150", LabelColor = LabelColor.Brand, ControlsType = ControlsType.CityDropList)]
        public long RegionId { get; set; }

        /// <summary>
        /// 区域全程
        /// 如：广东省东莞市南城区
        /// </summary>
        [BsonIgnore]
        [Display(Name = "所属区域")]
        [Field(ListShow = true, EditShow = false, SortOrder = 5, Width = "150")]
        public string FullName { get; set; }

        /// <summary>
        ///     详细地址
        /// </summary>
        [Display(Name = "地址")]
        [Field(ListShow = true, EditShow = true, SortOrder = 6, Width = "150", LabelColor = LabelColor.Brand,
            ControlsType = ControlsType.CityDropList)]
        public string Address { get; set; }

        /// <summary>
        /// 发布状态
        /// </summary>
        public UserTypeStatus Status { get; set; }

        /// <summary>
        /// 推荐用户Id
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        [Display(Name = "简介")]
        public string Intro { get; set; }

        /// <summary>
        /// 价格，代理费
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// 字段
        /// </summary>
        public long SortOrder { get; set; }
    }
}