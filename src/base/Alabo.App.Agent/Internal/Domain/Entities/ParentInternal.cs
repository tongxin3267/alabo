using Alabo.Core.Enums.Enum;
using Alabo.Domains.Entities;
using Alabo.Domains.Enums;
using Alabo.Web.Mvc.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alabo.App.Agent.Internal.Domain.Entities {

    [ClassProperty(Name = "内部合伙人")]
    [BsonIgnoreExtraElements]
    [Table("Agent_ParentInternal")]
    public class ParentInternal : AggregateMongodbUserRoot<ParentInternal> {

        [Display(Name = "名称")]
        [Field(ListShow = true, ControlsType = ControlsType.TextBox, SortOrder = 700)]
        public string Name { get; set; }

        public Guid Grade { get; set; }

        [Display(Name = "推荐人")]
        [Field(ListShow = true, ControlsType = ControlsType.TextBox, SortOrder = 700)]
        public string ParentUserName { get; set; }

        public long ParentUserId { get; set; }

        [Display(Name = "地址")]
        [Field(ListShow = true, ControlsType = ControlsType.TextBox, SortOrder = 700)]
        public string Address { get; set; }

        [Display(Name = "状态")]
        [Field(ListShow = true, ControlsType = ControlsType.TextBox, SortOrder = 700)]
        public UserTypeStatus Status { get; set; } = UserTypeStatus.Success;
    }
}