using Alabo.App.Agent.Citys.Domain.CallBacks;
using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Extensions;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.App.Agent.Citys.Domain.Entities {

    [ClassProperty(Name = "城市合伙人")]
    [BsonIgnoreExtraElements]
    [AutoDelete(IsAuto = true)]
    [Table("People_City")]
    public class City : AggregateMongodbUserRoot<City> {

        /// <summary>
        ///     名称
        /// </summary>
        [Display(Name = "名称")]
        [Required(ErrorMessage = ErrorMessage.NameNotAllowEmpty)]
        [HelpBlock("请输入合伙人名称，名称不能为空并且不超过8个字符")]
        [StringLength(20, ErrorMessage = ErrorMessage.MaxStringLength)]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "180", ListShow = true, IsMain = true,
            SortOrder = 0, Link = "/Admin/Basic/Edit?Service=ICityService&View=GetView&Id=[[Id]]")]
        public string Name { get; set; }

        /// <summary>
        ///     相关的实体Id，比如如果是省代理，城市代理，区域代理，则使用Basic_Region中的Id
        ///     如果是商圈，则使用Basic_Cicle的Id
        ///     如果时候供应商，则使用Shop_Store 的Id
        ///     所属区域ID
        ///     一个区域只能有个
        ///     广东省 省代理只有一个
        ///     东莞市 市代理也只有一个
        /// </summary>
        [Display(Name = "所属城市")]
        [Required]
        [Field(ControlsType = ControlsType.CityDropList, GroupTabId = 1, Width = "120", SortOrder = 600)]
        public long RegionId { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        [Display(Name = "所属城市")]
        [Field(ControlsType = ControlsType.CityDropList, GroupTabId = 1, EditShow = false, ListShow = true, Width = "120", SortOrder = 600)]
        public string RegionName { get; set; }

        /// <summary>
        /// 代理费
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        ///     推荐人用户Id
        /// </summary>
        [Display(Name = "推荐人ID")]
        [HelpBlock("填写正确的推荐人用户名")]
        [Field(ListShow = false, EditShow = false, SortOrder = 700)]
        public long ParentUserId { get; set; }

        /// <summary>
        ///     推荐人用户Id
        /// </summary>
        [Display(Name = "推荐人")]
        [HelpBlock("填写正确的推荐人用户名")]
        [Field(ListShow = false, EditShow = true, ControlsType = ControlsType.TextBox, SortOrder = 700)]
        public string ParentUserName { get; set; }

        /// <summary>
        ///     用户等级Id
        ///     每个类型对应一个等级
        /// </summary>
        [Display(Name = "合伙人等级")]
        [HelpBlock("请设置合伙人等级")]
        [Field(ControlsType = ControlsType.DropdownList,
           DataSourceType = typeof(CityGradeConfig), GroupTabId = 1, Width = "180",
           ListShow = false, EditShow = true, SortOrder = 1)]
        public Guid GradeId { get; set; }

        /// <summary>
        ///     用户名
        /// </summary>
        [Display(Name = "介绍")]
        [Field(ControlsType = ControlsType.TextArea, GroupTabId = 2, Width = "280", ListShow = false, EditShow = true,
            SortOrder = 2500)]
        public string Intro { get; set; }

        /// <summary>
        /// 所属用户名
        /// </summary>
        [Display(Name = "所属用户名")]
        [Required]
        [Field(ControlsType = ControlsType.TextBox, ListShow = true, GroupTabId = 1, Width = "120", SortOrder = 600)]
        public string UserName { get; set; }

        /// <summary>
        ///     详细地址
        /// </summary>
        [Display(Name = "详细地址")]
        [HelpBlock("请输入详细的地址")]
        [Field(ControlsType = ControlsType.TextBox, GroupTabId = 1, Width = "280", ListShow = false, EditShow = true,
            SortOrder = 601)]
        public string Address { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        [Display(Name = "备注")]
        [Field(ControlsType = ControlsType.TextArea, GroupTabId = 2, Width = "280", ListShow = false, EditShow = true,
            SortOrder = 2501)]
        public string Remark { get; set; }

        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        [HelpBlock("只有通过审核的情况下才能进行操作")]
        [Field(ControlsType = ControlsType.RadioButton, GroupTabId = 1, IsTabSearch = true,
            IsShowAdvancedSerach = false,
            DataSourceType = typeof(UserTypeStatus),
            //DataSource = "Alabo.App.Core.UserType.Domain.Enums.UserTypeStatus",
            Width = "150",
            EditShow = true, SortOrder = 1005)]
        public UserTypeStatus Status { get; set; } = UserTypeStatus.Pending;

        /// <summary>
        ///     状态
        /// </summary>
        [Display(Name = "状态")]
        [HelpBlock("只有通过审核的情况下才能进行操作")]
        [Field(ControlsType = ControlsType.Label, GroupTabId = 1,
            IsShowAdvancedSerach = false,
            //DataSource = "Alabo.App.Core.UserType.Domain.Enums.UserTypeStatus",
            Width = "150", ListShow = true,
              SortOrder = 1005)]
        public string StatusName {
            get {
                return this.Status.GetDisplayName();
            }
            set {
                _ = value;
            }
        }
    }
}